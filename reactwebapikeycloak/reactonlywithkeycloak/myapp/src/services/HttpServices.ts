import axios from "axios";
import KeyCloakService from "../security/KeyCloakService";

const HttpMethods = {
  GET: "GET",
  POST: "POST",
  DELETE: "DELETE",
};

const _axios = axios.create();

const configure = () => {
  _axios.interceptors.request.use((config: any) => {
    if (KeyCloakService.IsLoggedIn()) {
      const cb = () => {
        config.headers.Authorization = `Bearer ${KeyCloakService.GetToken()}`;
        return Promise.resolve(config);
      };
      return KeyCloakService.UpdateToken(cb);
    }
  });
};

const getAxiosClient = () => _axios;

const HttpService = {
  HttpMethods,
  configure,
  getAxiosClient,
};

export default HttpService;
