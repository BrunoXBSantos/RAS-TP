<template>
  <div>
    <DefaultNavbar />

    <div class="columns is-centered is-vcentered" id="content">
      <div class="column is-half">
        <div class="card" id="logincard">
          <div class="card-content">
            <p class="title has-text-centered">Welcome</p>
            <form class="login" @submit.prevent="login">
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

              <div class="field">
                <label class="label">Password</label>
                <div class="control">
                  <input
                    class="input"
                    type="password"
                    placeholder="********"
                    required
                    v-model="password"
                  />
                </div>
              </div>

              <div id="message" v-if="error == 'error' && loaded == 1">
                <p class="help is-danger">Invalid Credentials!</p>
              </div>

              <div class="has-text-centered">
                <button class="button is-danger" type="submit">
                  <strong>Sign in</strong>
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { AUTH_REQUEST } from '@/store/actions/auth'
import DefaultNavbar from '@/components/DefaultNavbar'
import { mapGetters, mapState } from 'vuex'

export default {
  name: 'Login',
  components: {
    DefaultNavbar
  },
  data () {
    return {
      username: '',
      password: '',
      loaded: 0
    }
  },
  computed: {
    ...mapGetters(['authStatus']),
    ...mapState({
      error: (state) => `${state.auth.status}`
    })
  },
  methods: {
    login: function () {
      const { username, password } = this
      this.loaded++
      this.$store.dispatch(AUTH_REQUEST, { username, password }).then(resp => {
        if (resp === 'Admin') {
          this.$router.push({ path: '/admin' })
        } else if (resp === 'Moderator') {
          this.$router.push({ path: '/moderator' })
        } else if (resp === 'Member') {
          this.$router.push({ path: '/member' })
        } else {
          this.$router.push({ path: '/404' })
        }
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

.login {
  margin: 0px 50px;
}
</style>
