<template>
  <nav class="navbar is-fixed-top">
    <div class="navbar-menu">
      <div class="navbar-end">
        <div class="navbar-item">
          <div class="buttons">
            <a class="button navb" href="/login">
              <strong>Login</strong>
            </a>
            <a class="button navb" href="/register">
              <strong>Register</strong>
            </a>
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  name: 'DefaultNavbar'
}
</script>
<style scoped>
.navb {
  border-width: 0;
  background-color: rgba(1, 1, 1, 0);
  color: white;
}

.navbar {
  position: fixed;
  width: 100%;
  min-height: 6vh;
  max-height: 4%;
  background-color: #a4031f;
}
</style>
