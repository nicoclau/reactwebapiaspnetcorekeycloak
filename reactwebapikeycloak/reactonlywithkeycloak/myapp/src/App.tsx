import React from "react";
import logo from "./logo.svg";
import "./App.css";
import KeyCloakService from "./security/KeyCloakService";
import HttpService from "./services/HttpServices";

function logout() {
  KeyCloakService.CallLogout();
}

function weather() {
  HttpService.getAxiosClient()
    .get("https://10.7.7.11:5001/WeatherForecast")
    .then(
      (p) => alert(JSON.stringify(p.data)),
      (e) => alert(e.message)
    );
}

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <p>Welcome {KeyCloakService.GetUserName()}</p>
        <p>Roles: {KeyCloakService.GetUserRoles()?.join(" ")}</p>
        <button onClick={logout}>Log Out</button>
        <button onClick={weather}>WeatherCast</button>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
