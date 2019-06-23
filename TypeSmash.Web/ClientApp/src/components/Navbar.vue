<template>
  <nav class="navbar navbar-expand-lg fixed-top navbar-dark bg-dark shadow-lg">
  <a class="navbar-brand" href="#">TypeSmash</a>
  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
  </button>

  <div class="collapse navbar-collapse" id="navbarSupportedContent">
    <ul class="navbar-nav ml-auto">
      <li class="nav-item">
        <router-link class="nav-link" to="/">Home</router-link>
      </li>
      <li class="nav-item">
        <router-link class="nav-link" to="/login">Login</router-link>
      </li>
      <li class="nav-item">
        <router-link class="nav-link" to="/signup">Signup</router-link>
      </li>
    </ul>
  </div>
</nav>
</template>

<script>
export default {
  name: 'Navbar',
}
</script>

<style scoped>

</style>
