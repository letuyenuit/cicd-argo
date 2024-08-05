pipeline{
  agent any
  environment{
      netcore_image = "letuyenuit212/netcore"
      reactjs_image = "letuyenuit212/reactjs"
      nodejs_image = "letuyenuit212/nodejs"
      DOCKERHUB_CREDENTIALS_PSW = credentials('dockerhub-letuyen')
  }
  stages{
    stage("Checkout code"){
      steps{
        git branch: 'master', url: 'https://github.com/letuyenuit/cicd-argo.git'
      }
    }
    
    stage("Test"){
      steps{
          sh """
              cd netcore
              dotnet test
          """
      }
    }
    stage("Start sqlserver"){
      steps{
        sh '''

          if docker inspect sql_server_container > /dev/null 2>&1; then
              if $(docker inspect -f '{{.State.Status}}' sql_server_container | grep -q "up"); then
                  echo "The container $container_name is running."
              else
                  echo "The container $container_name is not running."
                  docker start sql_server_container
              fi
          else
              echo "The container $container_name does not exist."
              docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password#1234" -p 1433:1433 --name sql_server_container -d mcr.microsoft.com/mssql/server
          fi
        '''
      }
    }
    stage("Migration database"){
      steps{
          sh """
              cd netcore
              sudo ~/.dotnet/tools/dotnet-ef database update
          """
      }
    }

    stage("Build docker image"){
      parallel{
        stage("Build reactjs image"){
          steps{
            sh """
              cd reactjs
              docker build -t ${reactjs_image} .
            """
          }
        }
        stage("Build nodejs image"){
          steps{
            sh """
              cd nodejs
              docker build -t ${nodejs_image} .
            """
          }
        }
        stage("Build netcore image"){
          steps{
            sh """
              cd netcore
              docker build -t ${netcore_image} .
            """
          }
        }
      }
    }

    stage("Push docker image"){
      steps{
        sh """
          echo $DOCKERHUB_CREDENTIALS_PSW | docker login -u letuyenuit212 --password-stdin
          docker push ${netcore_image}
          docker push ${reactjs_image}
          docker push ${nodejs_image}
        """
      }
    }

    stage('Install argoCD') {
      steps {
          sh "ssh vagrant@192.168.56.70 'kubectl create namespace argocd'"
          sh "ssh vagrant@192.168.56.70 'kubectl apply -n argocd -f https://raw.githubusercontent.com/argoproj/argo-cd/stable/manifests/install.yaml'"
      }
    }

    stage("Deploy"){
      steps{
        withKubeCredentials(kubectlCredentials: [[caCertificate: '', clusterName: '', contextName: '', credentialsId: 'k8s', namespace: '', serverUrl: '']]) {
            sh "helm upgrade --install --force messchart messchart --set netcore_image=${netcore_image} --set reactjs_image=${reactjs_image} --set nodejs_image=${nodejs_image}"
        }
      }
    }
  }
}