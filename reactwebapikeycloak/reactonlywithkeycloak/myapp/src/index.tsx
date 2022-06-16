import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import KeyCloakService from "./security/KeyCloakService";
import HttpService from "./services/HttpServices";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);

const renderApp = () =>
  root.render(
    <React.StrictMode>
      <App />
    </React.StrictMode>
  );

KeyCloakService.CallLogin(renderApp);
HttpService.configure();

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
