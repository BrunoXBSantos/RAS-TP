<template>
  <div>
    <UserNavbar />
    <div class="columns is-centered is-vcentered" id="content">
      <div class="column is-three-fifths">
        <div id="result"></div>
            <div class="columns is-multiline is-centered">
              <div class="column is-one-third-desktop">
                <div v-for="h in column1" :key="h.id" class="block">
                  <a v-on:click="betidbet = h.id">
                  <div class="card">
                    <div class="card-top">
                      <div class="title is-5">{{ h.team1 }}</div>
                      <div class="title is-5">{{ h.team2 }}</div>
                    </div>
                    <div class="card-bot">
                      <div class="subtitle is-7"><b>Bet Id: {{ h.id }}</b></div>
                      <div class="subtitle is-7"><b>Event State: {{ h.state }}</b></div>
                      <div style="color:green;" class="subtitle is-7"><b>Home Odd: {{ h.home_Odd }}</b></div>
                      <div style="color:orange;" class="subtitle is-7"><b>Tie Odd: {{ h.tie_Odd }}</b></div>
                      <div style="color:red;" class="subtitle is-7"><b>Away Odd: {{ h.away_Odd }}</b></div>
                    </div>
                  </div>
                  </a>
                </div>
              </div>
              <div class="column is-one-third-desktop">
                <div v-for="h in column2" :key="h.id" class="block">
                  <a v-on:click="betidbet = h.id">
                  <div class="card">
                    <div class="card-top">
                      <div class="title is-5">{{ h.team1 }}</div>
                      <div class="title is-5">{{ h.team2 }}</div>
                    </div>
                    <div class="card-bot">
                      <div class="subtitle is-7"><b>Bet Id: {{ h.id }}</b></div>
                      <div class="subtitle is-7"><b>Event State: {{ h.state }}</b></div>
                      <div style="color:green;" class="subtitle is-7"><b>Home Odd: {{ h.home_Odd }}</b></div>
                      <div style="color:orange;" class="subtitle is-7"><b>Tie Odd: {{ h.tie_Odd }}</b></div>
                      <div style="color:red;" class="subtitle is-7"><b>Away Odd: {{ h.away_Odd }}</b></div>
                    </div>
                  </div>
                  </a>
                </div>
              </div>
              <div class="column is-one-third-desktop">
                <div v-for="h in column3" :key="h.id" class="block">
                  <a v-on:click="betidbet = h.id">
                  <div class="card">
                    <div class="card-top">
                      <div class="title is-5">{{ h.team1 }}</div>
                      <div class="title is-5">{{ h.team2 }}</div>
                    </div>
                    <div class="card-bot">
                      <div class="subtitle is-7"><b>Bet Id: {{ h.id }}</b></div>
                      <div class="subtitle is-7"><b>Event State: {{ h.state }}</b></div>
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
            <form class="placebet" @submit.prevent="placebet">
            <div class="columns is-multiline is-centered">
              <div class="column is-four-fifths">
              <div class="field">
                <label class="label">Bet Id</label>
                <div class="control">
                  <input
                    class="input"
                    type="betid"
                    placeholder=betidbet
                    required
                    v-model="betidbet"
                  />
                </div>
              </div>
              </div>
              <div class="column is-four-fifths">
              <div class="field">
                <label class="label">Result</label>
                <div class="control">
                  <input
                    class="input"
                    type="name"
                    placeholder="1(home)/X(tie)/2(away)"
                    required
                    v-model="resultbet"
                  />
                </div>
              </div>
              </div>
              <div class="column is-four-fifths">
              <div class="field">
                <label class="label">Currency Type</label>
                <div class="control">
                  <input
                    class="input"
                    type="currencytipe"
                    placeholder="Currency Type ID"
                    required
                    v-model="coinidbet"
                  />
                </div>
              </div>
              </div>
              <div class="column is-four-fifths">
              <div class="field">
                <label class="label">Value</label>
                <div class="control">
                  <input
                    class="input"
                    type="betvalue"
                    placeholder=valuebet
                    required
                    v-model="valuebet"
                  />
                </div>
              </div>
              </div>
              <div class="column">
                <div id="message" v-if="error != ''">
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
  name: 'LoggedMember',
  components: {
    UserNavbar
  },
  data () {
    return {
      allinfo: [{}],
      column1: [{}],
      column2: [{}],
      column3: [{}],
      valuebet: 0,
      coinidbet: 0,
      resultbet: '',
      useridbet: store.getters.getId,
      betidbet: 0,
      error: ''
    }
  },
  methods: {
    placebet: function () {
      const { valuebet, coinidbet, resultbet, useridbet, betidbet } = this
      // eslint-disable-next-line camelcase
      axios({ url: api_url + '/api/Bet', data: { value: valuebet, coinID: coinidbet, result: resultbet, appUserId: useridbet, _eventId: betidbet }, method: 'POST' })
        .then(resp => {
          location.reload()
        })
        .catch(err => {
          console.log('TEST MAKEBET-> VIEWS/LOGGEDMEMBER -> ERROR RESPONSE:')
          console.log(err)
          this.error = err
        })
    },
    fillColumns () {
      this.column1 = []
      this.column2 = []
      this.column3 = []
      var i
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
    this.allinfo = []
    // eslint-disable-next-line camelcase
    axios.get(api_url + '/api/Event/NotFinished?PageSize=1000')
      .then(resp => {
        var i
        for (i = 0; i < resp.data.length; i++) {
          if (resp.data[i] != null) {
            const betobj = {
              id: resp.data[i].id,
              away_Odd: resp.data[i].away_Odd,
              home_Odd: resp.data[i].home_Odd,
              tie_Odd: resp.data[i].tie_Odd,
              sport: resp.data[i].sport,
              state: resp.data[i].state,
              team1: resp.data[i].team1.substring(0, 20),
              team2: resp.data[i].team2.substring(0, 20),
              type: resp.data[i].type
            }
            this.allinfo.push(betobj)
          }
        }
        this.fillColumns()
        if (this.allinfo.length === 0) {
          document.getElementById('result').innerHTML = '<h1 class="title">Sorry, there are no open bets!</h1>'
        }
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

.card-top {
  pointer-events: none;
  width: 100%;
  height: 20%;
  padding: 6% 6% 6% 6%;
  text-transform: capitalize;
  background-color: brown;
}

.card-top-second {
  pointer-events: none;
  width: 100%;
  height: 20%;
  padding: 6% 6% 6% 6%;
  text-transform: capitalize;
  background-color: black;
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
