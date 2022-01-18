<template>
  <div>
    <UserNavbar />
    <div class="columns is-centered is-vcentered is-multiline" id="content">
      <div class="column is-three-fifths">
          <div id="result"></div>
            <div class="columns is-multiline is-centered">
              <div class="column is-one-third-desktop">
                <div v-for="h in column1" :key="h.betid" class="block">
                  <a v-on:click="betid = h.betid">
                  <div class="card">
                    <div class="card-top">
                      <div class="title is-5">{{ h.team1 }}</div>
                      <div class="title is-5">{{ h.team2 }}</div>
                    </div>
                    <div class="card-bot">
                      <div class="subtitle is-7"><b>Bet Id: {{ h.id }}</b></div>
                      <div class="subtitle is-7"><b>Register Id: {{ h.betid }}</b></div>
                      <div class="subtitle is-7"><b>Result: {{ h.pickedresult }}</b></div>
                      <div class="subtitle is-7"><b>Value: {{ h.pickedvalue }}</b></div>
                      <div class="subtitle is-7"><b>State: {{ h.state }}</b></div>
                      <div style="color:green;" class="subtitle is-7"><b>Home Odd: {{ h.home_Odd }}</b></div>
                      <div style="color:orange;" class="subtitle is-7"><b>Tie Odd: {{ h.tie_Odd }}</b></div>
                      <div style="color:red;" class="subtitle is-7"><b>Away Odd: {{ h.away_Odd }}</b></div>
                    </div>
                  </div>
                  </a>
                </div>
              </div>
              <div class="column is-one-third-desktop">
                <div v-for="h in column2" :key="h.betid" class="block">
                  <a v-on:click="betid = h.betid">
                  <div class="card">
                    <div class="card-top">
                      <div class="title is-5">{{ h.team1 }}</div>
                      <div class="title is-5">{{ h.team2 }}</div>
                    </div>
                    <div class="card-bot">
                      <div class="subtitle is-7"><b>Bet Id: {{ h.id }}</b></div>
                      <div class="subtitle is-7"><b>Register Id: {{ h.betid }}</b></div>
                      <div class="subtitle is-7"><b>Result: {{ h.pickedresult }}</b></div>
                      <div class="subtitle is-7"><b>Value: {{ h.pickedvalue }}</b></div>
                      <div class="subtitle is-7"><b>State: {{ h.state }}</b></div>
                      <div style="color:green;" class="subtitle is-7"><b>Home Odd: {{ h.home_Odd }}</b></div>
                      <div style="color:orange;" class="subtitle is-7"><b>Tie Odd: {{ h.tie_Odd }}</b></div>
                      <div style="color:red;" class="subtitle is-7"><b>Away Odd: {{ h.away_Odd }}</b></div>
                    </div>
                  </div>
                  </a>
                </div>
              </div>
              <div class="column is-one-third-desktop">
                <div v-for="h in column3" :key="h.betid" class="block">
                  <a v-on:click="betid = h.betid">
                  <div class="card">
                    <div class="card-top">
                      <div class="title is-5">{{ h.team1 }}</div>
                      <div class="title is-5">{{ h.team2 }}</div>
                    </div>
                    <div class="card-bot">
                      <div class="subtitle is-7"><b>Bet Id: {{ h.id }}</b></div>
                      <div class="subtitle is-7"><b>Register Id: {{ h.betid }}</b></div>
                      <div class="subtitle is-7"><b>Result: {{ h.pickedresult }}</b></div>
                      <div class="subtitle is-7"><b>Value: {{ h.pickedvalue }}</b></div>
                      <div class="subtitle is-7"><b>State: {{ h.state }}</b></div>
                      <div style="color:green;" class="subtitle is-7"><b>Home Odd: {{ h.home_Odd }}</b></div>
                      <div style="color:orange;" class="subtitle is-7"><b>Tie Odd: {{ h.tie_Odd }}</b></div>
                      <div style="color:red;" class="subtitle is-7"><b>Away Odd: {{ h.away_Odd }}</b></div>
                    </div>
                  </div>
                  </a>
                </div>
              </div>
            </div>
      </div>
      <div class="column is-one-quarter">
        <div class="card" id="makebetcard">
          <div class="card-top-second">
            <div class="title is-5">Place Bet</div>
          </div>
          <div class="card-bot">
            <form class="cancelbet" @submit.prevent="cancelbet">
            <div class="columns is-multiline is-centered">
              <div class="column is-four-fifths">
              <div class="field">
                <label class="label">Bet Id</label>
                <div class="control">
                  <input
                    class="input"
                    type="betid"
                    placeholder=betid
                    required
                    v-model="betid"
                  />
                </div>
              </div>
              </div>
              <div class="column">
                <div id="message" v-if="errorc != ''">
                  <p class="help is-danger">Invalid Input: Please Try Again</p>
                </div>
                <div class="has-text-centered">
                  <button class="button is-danger" type="submit">
                    <strong>Submit</strong>
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
// import DefaultNavbar from '@/components/DefaultNavbar'
import UserNavbar from '@/components/UserNavbar.vue'
import axios from 'axios'
// eslint-disable-next-line camelcase
import { url as api_url } from '@/assets/scripts/api'
import store from '@/store'

export default {
  name: 'MyBets',
  components: {
    UserNavbar
  },
  data () {
    return {
      allinfo: [{}],
      queryParam: '',
      column1: [{}],
      column2: [{}],
      column3: [{}],
      indiceatual: 0,
      errorc: '',
      pages: 1,
      total: 0,
      useridbet: store.getters.getId,
      betid: 0
    }
  },
  methods: {
    cancelbet: function () {
      const { useridbet, betid } = this
      // eslint-disable-next-line camelcase
      axios({ url: api_url + '/' + useridbet + '/' + betid, method: 'DELETE' })
        .then(resp => {
          location.reload()
        })
        .catch(err => {
          console.log('TEST MAKEBET-> VIEWS/LOGGEDMEMBER -> ERROR RESPONSE:')
          console.log(err)
          this.errorc = err
        })
    },
    fillColumns () {
      this.column1 = []
      this.column2 = []
      this.column3 = []
      var i
      console.log('length: ' + this.allinfo.length)
      for (i = 0; i < this.allinfo.length; i = i + 3) {
        if (i < this.allinfo.length) {
          this.column1.push(this.allinfo[i])
        }
        if (i + 1 < this.allinfo.length) {
          this.column2.push(this.allinfo[i + 1])
        }
        if (i + 2 < this.allinfo.length) {
          this.column3.push(this.allinfo[i + 2])
        }
      }
    }
  },
  created () {
    const userid = this.useridbet
    this.allinfo = []
    // eslint-disable-next-line camelcase
    axios.get(api_url + '/api/Bet/user/idOrUsername?PageSize=1000&idOrUsername=' + userid)
      .then(res => {
        this.pages = Math.ceil(res.data.length / 6)
        var i
        for (i = 0; i < res.data.length; i++) {
          if (res.data[i] != null) {
            const result = res.data[i].result
            const value = res.data[i].value
            const eventid = res.data[i]._eventId
            const betid = res.data[i].id
            // eslint-disable-next-line camelcase
            // eslint-disable-next-line camelcase
            axios.get(api_url + '/api/Event/' + eventid)
              .then(resp => {
                if (resp.data != null) {
                  const betobj = {
                    id: resp.data.id,
                    away_Odd: resp.data.away_Odd,
                    home_Odd: resp.data.home_Odd,
                    tie_Odd: resp.data.tie_Odd,
                    sport: resp.data.sport,
                    state: resp.data.state,
                    team1: resp.data.team1.substring(0, 20),
                    team2: resp.data.team2.substring(0, 20),
                    type: resp.data.type,
                    pickedresult: result,
                    pickedvalue: value,
                    betid: betid
                  }
                  this.allinfo.push(betobj)
                }
                this.fillColumns()
              })
              .catch(err => {
                console.log('TEST -> VIEWS/MYBETS.VUE -> GET BETS ERROR RESPONSE:')
                console.log(err)
                this.error = err
              })
          }
        }
        if (this.column1.length === 0) {
          document.getElementById('result').innerHTML = '<h1 class="title">Place your bet first!</h1>'
        }
      })
      .catch(err => {
        console.log('TEST -> VIEWS/MYBETS.VUE -> GET BETS ERROR RESPONSE:')
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

.card-top {
  pointer-events: none;
  width: 100%;
  height: 20%;
  padding: 6% 6% 6% 6%;
  text-transform: capitalize;
  background-color: brown;
}

.title {
  color:white;
}

.card-bot {
  width: 100%;
  height: 80%;
  padding: 6% 6% 6% 6%;
  }

</style>
