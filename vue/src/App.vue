<template>
  <div id="app">
    <div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'search' }">Search</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'search2electricboogaloo' }">Search2 ElectricBoogaloo</router-link>
    </div>
    <router-view />
  </div>
</template>

<style>
@import url("https://fonts.googleapis.com/css2?family=Urbanist:wght@300;400&display=swap");

#app {
  font-family: "Urbanist", sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

button {
  font-family: "Urbanist", sans-serif;
  display: inline-block;
  background: #2c3e50;
  color: #fff;
  border: none;
  padding: 10px 20px;
  border-radius: 5px;
  cursor: pointer;
  text-decoration: none;
  font-size: 15px;
}

button:hover {
  background-color: #407F7F;
}
</style>
