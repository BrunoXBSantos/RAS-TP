<template>
  <div>
    <router-view />
    <Footer />
  </div>
</template>

<script>
import axios from 'axios'
import { AUTH_LOGOUT } from '@/store/actions/auth'
import Footer from '@/components/Footer'

export default {
  name: 'App',
  components: {
    Footer
  },
  created: function () {
    axios.interceptors.response.use(undefined, function (err) {
      return new Promise(function (resolve, reject) {
        if (err.status === 401 && err.config && !err.config.__isRetryRequest) {
          this.$store.dispatch(AUTH_LOGOUT)
        }
        throw err
      })
    })
  }
}
</script>

<style>
@import "./assets/styles/bulma.css";
</style>
