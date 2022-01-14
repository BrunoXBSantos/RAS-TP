<template>
  <div>
    <UserNavbar />
    <div class="columns is-centered is-vcentered" id="content">
      <div class="column is-two-thirds">
        <div class="card" id="registercard">
          <div class="card-content">
            <p class="title has-text-centered">Bet List</p>
            <div class="block" id="pagination">
              <pagination
                :selectedPage="1"
                :numberOfPages="pages"
              >
              </pagination>
            </div>
          </div>
        </div>
      </div>
      <div class="column is-one-quarter">
        <div class="card" id="registercard">
          <div class="card-content">
            <p class="title has-text-centered">Make Bet</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
// import DefaultNavbar from '@/components/DefaultNavbar'
import UserNavbar from '@/components/UserNavbar.vue'
import axios from 'axios'
// eslint-disable-next-line camelcase
import { url as api_url } from '@/assets/scripts/api'
import Pagination from '@/components/Pagination.vue'

export default {
  name: 'LoggedMember',
  components: {
    UserNavbar,
    Pagination
  },
  data () {
    return {
      allinfo: [],
      queryParam: '',
      column1: [],
      column2: [],
      column3: [],
      indiceatual: 0,
      pages: 1,
      total: 0
    }
  },
  methods: {
    fillColumns () {
      this.column1 = []
      this.column2 = []
      this.column3 = []
      var i
      console.log('length: ' + this.allinfo.length)
      for (i = 0; i < 6; i = i + 3) {
        if (i < this.allinfo.length) {
          this.column1.push(this.allinfo[i])
          // console.log(this.column1)
        }
        if (i + 1 < this.allinfo.length) {
          this.column2.push(this.allinfo[i + 1])
          // console.log(this.column2)
        }
        if (i + 2 < this.allinfo.length) {
          this.column3.push(this.allinfo[i + 2])
          // console.log(this.column3)
        }
      }
    },
    onChange (page) {
      console.log(`Getting page ${page}`)
      console.log(page)
    }
  },
  created () {
    // eslint-disable-next-line camelcase
    axios.get(api_url + '/api/Event')
      .then(resp => {
        this.pages = Math.ceil(resp.data.length / 6)
        console.log(resp.data.length)
        console.log(this.pages)
        console.log(resp.data)
        var i
        for (i = 0; i < resp.data.length; i + 1) {
          console.log(resp.data[i].id)
          console.log(resp.data[i].away_Odd)
          console.log(resp.data[i].home_Odd)
          console.log(resp.data[i].tie_Odd)
          console.log(resp.data[i].sport)
          console.log(resp.data[i].state)
          console.log(resp.data[i].team1)
          console.log(resp.data[i].team2)
          console.log(resp.data[i].type)
          if (resp.data[i] != null) {
            var betobject = {
              id: resp.data[i].id,
              away_Odd: resp.data[i].away_Odd,
              home_Odd: resp.data[i].home_Odd,
              tie_Odd: resp.data[i].tie_Odd,
              sport: resp.data[i].sport,
              state: resp.data[i].state,
              team1: resp.data[i].team1,
              team2: resp.data[i].team2,
              type: resp.data[i].type
            }
            // console.log(betobject)
            this.allinfo.push(betobject)
          }
        }

        this.fillColumns()

        // console.log(this.allinfo)

        if (this.allinfo.length === 0) {
          document.getElementById('pagination').innerHTML = ''
          document.getElementById('result').innerHTML = '<h1 class="title">No results found!</h1>'
        }
        // console.log('TEST -> VIEWS/LOGGEDMEMBER.VUE -> GET BETS SUCESS RESPONSE:')
        // console.log(resp)
      })
      .catch(err => {
        console.log('TEST -> VIEWS/LOGGEDMEMBER.VUE -> GET BETS ERROR RESPONSE:')
        console.log(err)
        this.error = err
      })
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
