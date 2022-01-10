<template>
  <div>
    <DefaultNavbar />

    <div class="columns is-centered is-vcentered" id="content">
      <div class="column is-three-quarters">
        <div class="card" id="registercard">
          <div class="card-content">
            <p class="title has-text-centered">Register</p>
            <form class="register" @submit.prevent="register">
            <div class="columns is-multiline">
              <div class="column is-half">
              <div class="field">
                <label class="label">Name</label>
                <div class="control">
                  <input
                    class="input"
                    type="name"
                    placeholder="Name"
                    required
                    v-model="name"
                  />
                </div>
              </div>
              </div>

              <div class="column is-half">
              <div class="field">
                <label class="label">Contact</label>
                <div class="control">
                  <input
                    class="input"
                    type="contact"
                    placeholder="Contact"
                    required
                    v-model="contact"
                  />
                </div>
              </div>
              </div>

              <div class="column is-half">
              <div class="field">
                <label class="label">Username</label>
                <div class="control">
                  <input
                    class="input"
                    type="username"
                    placeholder="username"
                    required
                    v-model="username"
                  />
                </div>
              </div>
              </div>

              <div class="column is-half">
              <div class="field">
                <label class="label">Phone Number</label>
                <div class="control">
                  <input
                    class="input"
                    type="phone"
                    placeholder="Phone Number"
                    required
                    v-model="phone"
                  />
                </div>
              </div>
              </div>

              <div class="column is-half">
              <div class="field">
                <label class="label">Email</label>
                <div class="control">
                  <input
                    class="input"
                    type="email"
                    placeholder="Email"
                    required
                    v-model="email"
                  />
                </div>
              </div>
              </div>

              <div class="column is-half">
              <div class="field">
                <label class="label">Password</label>
                <div class="control">
                  <input
                    class="input"
                    type="password"
                    placeholder="Password"
                    required
                    v-model="password"
                  />
                </div>
              </div>
              </div>
              <div class="column">

              <div id="message" v-if="error != ''">
                <p class="help is-danger">Invalid Credentials!</p>
              </div>

              <div class="has-text-centered">
                    <button class="button is-danger" type="submit">
                <strong>Register</strong>
                </button>
              </div>
              </div>
            </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import DefaultNavbar from '@/components/DefaultNavbar'
import axios from 'axios'
// eslint-disable-next-line camelcase
import { url as api_url } from '@/assets/scripts/api'

export default {
  name: 'Register',
  components: {
    DefaultNavbar
  },
  data () {
    return {
      username: '',
      password: '',
      contact: '',
      phone: '',
      name: '',
      email: '',
      error: ''
    }
  },
  methods: {
    register: function () {
      const { username, password, contact, phone, name, email } = this
      // eslint-disable-next-line camelcase
      axios({ url: api_url + '/api/Account/register', data: { name, contact, phone, username, email, password }, method: 'POST' })
        .then(resp => {
          console.log('Teste Register Sucess:')
          console.log(resp)
          this.$router.push({ path: '/' })
        })
        .catch(err => {
          console.log('Teste Register Error:')
          console.log(err)
          this.error = err
        })
    }
  }
}
</script>

<style scoped>
#content {
  margin: 10% auto;
  width: 100%;
}

.register {
  margin: 0px 50px;
}
</style>
