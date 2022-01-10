<template>
  <div>
    <UserNavbar />
    <div class="columns is-centered is-vcentered" id="content">
      <div class="column is-three-quarters">
        <div class="card" id="registercard">
          <div class="card-content">
            <p class="title has-text-centered">Logged User</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
// import DefaultNavbar from '@/components/DefaultNavbar'
import UserNavbar from '@/components/UserNavbar.vue'
// import axios from 'axios'
// eslint-disable-next-line camelcase
// import { url as api_url } from '@/assets/scripts/api'

export default {
  name: 'LoggedUser',
  components: {
    UserNavbar
  },
  data () {
    return {}
  },
  methods: {}
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
