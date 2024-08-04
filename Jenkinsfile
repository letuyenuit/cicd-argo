pipeline{
  agent any
  environment{
      netcore_image_repo = "letuyenuit212/netcore"
      dockerhub_credentials = credentials('dockerhub-letuyen')
  }
  stages{
    stage("Checkout code"){
      steps{
        git branch: 'master', url: 'https://github.com/letuyenuit212/cicd-argo.git'
      }
    }
    stage("Test dotnet version 7"){
      steps{
          sh """
              cd netcore
              docker build -t $netcore_image_repo .
              docker run --rm -v .:/app -w /app $netcore_image_repo dotnet test
          """
      }
    }
    stage("Migration database dotnet version 7"){
      steps{
          sh """
              cd netcore
              docker run --rm -v .:/app -w /app $netcore_image_repo dotnet tool install --global dotnet-ef
              docker run --rm -v .:/app -w /app $netcore_image_repo dotnet-ef database update
          """
      }
    }
  }
  post{
    always{
        echo "========always========"
    }
    success{
        echo "========pipeline executed successfully ========"
    }
    failure{
        echo "========pipeline execution failed========"
    }
  }
}