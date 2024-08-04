import axios from "axios";
const createAxiosInstance = () => {
  const token = localStorage.getItem("token")
    ? JSON.parse(localStorage.getItem("token") || "")
    : "";
  const headers = {
    Authorization: `Bearer ${token}`,
  };

  return axios.create({
    baseURL: "/api",
    // baseURL: `http://localhost:5273`,
    headers,
  });
};

const axiosInstance = createAxiosInstance();
export default axiosInstance;
