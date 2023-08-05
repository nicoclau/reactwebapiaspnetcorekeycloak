import { AxiosInstance, InternalAxiosRequestConfig } from "axios";
import axios from "axios";
import KeyCloakService from "../security/KeycloakService";

const HttpMethods = {
  GET: "GET",
  POST: "POST",
  DELETE: "DELETE",
};

const _axios = axios.create();

const configureAxiosKeycloak = (): void => {
  _axios.interceptors.request.use(
    (config: InternalAxiosRequestConfig): InternalAxiosRequestConfig => {
      const { method, url } = config;

      console.log(method);
      console.log(url);

      console.log(KeyCloakService.IsLoggedIn());

      if (KeyCloakService.IsLoggedIn()) {
        const cb = (config: InternalAxiosRequestConfig) => {
          config.headers.Authorization = `Bearer ${KeyCloakService.GetAccesToken()}`;
          return config;
        };

        KeyCloakService.UpdateToken(cb(config));
      }

      return config;
    }
  );
};

const getAxiosClient = (): AxiosInstance => _axios;

const HttpService = {
  HttpMethods,
  configureAxiosKeycloak,
  getAxiosClient,
};

export default HttpService;
