<template>
  <div class="login">
    <h2>Login {{ Login() }}</h2>
    <h2>Roles {{ UserRoles()?.join(" ") }}</h2>
    <h2>Access Token {{ AccessToken() }}</h2>
    <button @click="LogOut">Log Out</button>
    <button @click="Weather">Weather</button>
  </div>
</template>

<script lang="ts">
import HttpService from "@/services/httpservice";
import { defineComponent } from "vue";
import KeycloakService from "../security/KeycloakService";

export default defineComponent({
  name: "Login",
  methods: {
    Login() {
      return KeycloakService.GetUserName();
    },
    AccessToken() {
      return KeycloakService.GetAccesToken();
    },
    LogOut() {
      return KeycloakService.CallLogOut();
    },
    UserRoles() {
      return KeycloakService.GetUserRoles();
    },
    Weather() {
      HttpService.getAxiosClient()
        .get("https://10.7.7.11:5001/WeatherForecast")
        .then(
          (p) => alert(JSON.stringify(p.data)),
          (e) => alert(e.message)
        );
    },
  },
});
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
