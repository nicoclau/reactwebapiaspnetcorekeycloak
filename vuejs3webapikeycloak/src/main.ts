import { createApp } from "vue";
import App from "./App.vue";
import KeyCloakService from "./security/KeycloakService";
import HttpService from "./services/httpservice";

const renderApp = () => {
  createApp(App).mount("#app");
};

KeyCloakService.CallLogin(renderApp);
HttpService.configureAxiosKeycloak();
