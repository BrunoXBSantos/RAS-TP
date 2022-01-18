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
import store from '@/store'

export default {
  name: 'App',
  components: {
    Footer
  },
  data: function () {
    return {
      connection: null
    }
  },
  created: function () {
    this.setConnection()
    axios.interceptors.response.use(undefined, function (err) {
      return new Promise(function (resolve, reject) {
        if (err.status === 401 && err.config && !err.config.__isRetryRequest) {
          this.$store.dispatch(AUTH_LOGOUT)
        }
        throw err
      })
    })
  },
  methods: {
    sendMessage: function (message) {
      console.log(this.connection)
      setTimeout(() => {
        this.connection.send(store.getters.getId)
      },
      4000)
    },
    setConnection: function () {
      if ((store.getters.getId !== '' && this.connection === null) || this.connection.readyState !== this.connection.OPEN) {
        console.log('Starting connection to WebSocket Server')
        this.connection = new WebSocket('ws://localhost:5000')
        this.flag = 1
        this.connection.onmessage = (event) => {
          console.log(event)
          alert(event.data)
        }
        this.connection.onopen = (event) => {
          console.log(event)
          console.log('Successfully connected to the echo websocket server...')
          this.sendMessage(store.getters.getId)
        }
        this.connection.onclose = (event) => {
          console.log('Websocket server connection closed...')
        }
      }
    }
  }
}
</script>

<style>
@import "./assets/styles/bulma.css";
</style>
