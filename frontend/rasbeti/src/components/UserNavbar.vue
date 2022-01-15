<template>
  <nav
    class='navbar is-fixed-top'
    role='navigation'
    aria-label='main navigation'
  >
    <div class='navbar-brand'>
      <a class='navbar-item' href='/member'>
        <img src='@/assets/img/logo2.png' width='100' height='200' />
      </a>

      <a
        role='button'
        class='navbar-burger'
        aria-label='menu'
        aria-expanded='false'
        data-target='navbarBasicExample'
      >
        <span aria-hidden='true'></span>
        <span aria-hidden='true'></span>
        <span aria-hidden='true'></span>
      </a>
    </div>
    <div id='navbarBasicExample' class='navbar-menu'>
      <div class='navbar-end'>
        <div class='navbar-item'>
          <div class='buttons'>
            <a class='button navb' href='/user/mybets'>
              <strong>Bets</strong>
            </a>
            <a class='button navb' href='/user/mywallet'>
              <strong>Wallet</strong>
            </a>
            <a v-on:click="logout" class='button navb'>
              <strong>Sign Out</strong>
            </a>
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>
// import { mapGetters, mapState } from 'vuex'
// eslint-disable-next-line camelcase
import { url as api_url } from '@/assets/scripts/api'
import { AUTH_LOGOUT } from '@/store/actions/auth'

export default {
  name: 'UserNavbar',
  created () {
    window.addEventListener('resize', this.myEventHandler)
  },
  destroyed () {
    window.removeEventListener('resize', this.myEventHandler)
  },
  components: {
  },
  data () {
    return {
      ww: window.innerWidth,
      lr: false,
      url: api_url,
      profilePic: 'https://source.unsplash.com/random/200x200?sig=1',
      ppw: 50,
      pph: 50,
      username: ''
    }
  },
  methods: {
    logout: function () {
      this.$store.dispatch(AUTH_LOGOUT)
        .then(resp => {
          console.log('LOGOUT TEST:')
          console.log(resp)
          this.$router.push({ path: '/' })
        })
    },
    myEventHandler () {
      console.log(this.ww)
      if (window.innerWidth < 1024 && !this.lr) {
        this.ww = window.innerWidth
        this.lr = true
      } else if (window.innerWidth >= 1024 && this.lr) {
        this.ww = window.innerWidth
        this.lr = false
      }
    }
  }
}
</script>
<style scoped>
#navbarPP {
  flex-grow: 0;
  background-color: red;
}
strong {
  color: white;
}
.navb {
  border-width: 0;
  background-color: rgba(1, 1, 1, 0);
}
.navbar {
  position: fixed;
  width: 100%;
  min-height: 8vh;
  max-height: 5%;
  background-color: #a4031f;
}
</style>
