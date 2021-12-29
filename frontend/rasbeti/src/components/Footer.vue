<template>
  <footer class="footer">
    <div class="content has-text-centered">
      <p><strong>RASBet</strong> @ University of Minho</p>
    </div>
  </footer>
</template>

<script>
export default {
  name: 'Footer'
}
</script>

<style scoped>

strong {
  color: white;
}

.footer {
  position: fixed;
  bottom: 0;
  width: 100%;
  min-height: 6vh;
  max-height: 4%;
  padding: 1% 0;
  background-color: #a4031f;
  color: white;
}
</style>
