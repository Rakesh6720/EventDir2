import axios, { AxiosHeaders, AxiosResponse } from "axios";
import { store } from "../store/configureStore";
import { User } from "./user";

axios.defaults.baseURL = "http://localhost:5089/api/";
axios.defaults.withCredentials = true;

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.request.use((config) => {
  const user: User = JSON.parse(localStorage.getItem("user")!);
  const token = user?.token;
  if (token) axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  return config;
})

const requests = {
  get: (url: string) => axios.get(url).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  delete: (url: string) => axios.delete(url).then(responseBody),
};

const Account = {
  login: (values: any) => requests.post("account/login", values),
  register: (values: any) => requests.post("account/register", values),
  currentUser: () => requests.get("account/currentUser"),
};

const agent = {
  Account,
};

export default agent;
