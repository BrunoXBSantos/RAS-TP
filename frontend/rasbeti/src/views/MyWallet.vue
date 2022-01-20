<template>
  <div>
    <UserNavbar />
    <div class="columns is-centered is-vcentered" id="content">
      <div class="column is-four-fifths">
        <div id="result"></div>
            <div class="columns is-multiline is-centered is-vcentered">
              <div class="column is-one-quarter">
                <p class="title has-text-centered">Your Currency</p>
                <div v-for="h in column1" :key="h.coinName" class="block">
                  <a v-on:click="coinnamefrom = h.coinName">
                  <div class="card">
                    <div class="card-top-first">
                      <div class="title is-5">{{ h.coinName }}</div>
                    </div>
                    <div class="card-bot">
                      <div class="subtitle is-7"><b>Balance: {{ h.balance }}</b></div>
                    </div>
                  </div>
                  </a>
                </div>
              </div>
              <div class="column is-one-quarter">
                <p class="title has-text-centered">Available Currency</p>
                <div v-for="h in column2" :key="h.name" class="block">
                  <a v-on:click="selectedcurrencytype= h.id">
                  <div class="card">
                    <div class="card-top-second">
                      <div class="title is-5">{{ h.name }}</div>
                    </div>
                    <div class="card-bot">
                      <div class="subtitle is-7"><b>Coin Id: {{ h.id }}</b></div>
                      <div class="subtitle is-7"><b>Convertion Rate: {{ h.convertion}}</b></div>
                    </div>
                  </div>
                  </a>
                </div>
              </div>
              <div class="column is-two-fifths">
                <div class="columns is-vcentered is-multiline">
              <div class="column is-two-thirds">
                <p class="title has-text-centered">Operations</p>
                <div class="card" id="makebetcard">
                    <div class="card-top-third">
                        <div class="title is-5">Whitdraw</div>
                    </div>
                    <div class="card-bot">
                    <form class="withdrawmoney" @submit.prevent="withdrawal">
                        <div class="columns is-multiline is-centered">
                            <div class="column is-four-fifths">
                            <div class="field">
                            <label class="label">Currency Type</label>
                            <div class="control">
                            <input
                                class="input"
                                type="currencytype"
                                placeholder=selectedcurrencytype
                                required
                                v-model="selectedcurrencytype"
                            />
                            </div>
                            </div>
                        </div>
                        <div class="column is-four-fifths">
                        <div class="field">
                        <label class="label">Amount</label>
                        <div class="control">
                        <input
                            class="input"
                            type="currencytype"
                            placeholder="Amount to withdraw"
                            required
                            v-model="amount"
                        />
                        </div>
                        </div>
                        </div>
                        <div class="column">
                            <div id="message" v-if="error1 != ''">
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
              <div class="column is-two-thirds">
                <div class="card" id="makebetcard">
                    <div class="card-top-third">
                        <div class="title is-5">Load</div>
                    </div>
                    <div class="card-bot">
                    <form class="withdrawmoney" @submit.prevent="load">
                        <div class="columns is-multiline is-centered">
                            <div class="column is-four-fifths">
                            <div class="field">
                            <label class="label">Currency Type</label>
                            <div class="control">
                            <input
                                class="input"
                                type="currencytype"
                                placeholder=selectedcurrencytype
                                required
                                v-model="selectedcurrencytype"
                            />
                            </div>
                            </div>
                        </div>
                        <div class="column is-four-fifths">
                        <div class="field">
                        <label class="label">Amount</label>
                        <div class="control">
                        <input
                            class="input"
                            type="currencytype"
                            placeholder="Amount to withdraw"
                            required
                            v-model="amountl"
                        />
                        </div>
                        </div>
                        </div>
                        <div class="column">
                            <div id="message" v-if="error2 != ''">
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
              <div class="column is-two-thirds">
                <div class="card" id="makebetcard">
                    <div class="card-top-third">
                        <div class="title is-5">Exchange</div>
                    </div>
                    <div class="card-bot">
                    <form class="withdrawmoney" @submit.prevent="exchange">
                        <div class="columns is-multiline is-centered">
                            <div class="column is-four-fifths">
                            <div class="field">
                            <label class="label">Currency Type From:</label>
                            <div class="control">
                            <input
                                class="input"
                                type="currencytype"
                                placeholder=selectedcurrencytype
                                required
                                v-model="selectedcurrencytype"
                            />
                            </div>
                            </div>
                        </div>
                        <div class="column is-four-fifths">
                            <div class="field">
                            <label class="label">Currency Type To:</label>
                            <div class="control">
                            <input
                                class="input"
                                type="currencytype"
                                placeholder=selectedcurrencytypeto
                                required
                                v-model="selectedcurrencytypeto"
                            />
                            </div>
                            </div>
                        </div>
                        <div class="column is-four-fifths">
                        <div class="field">
                        <label class="label">Amount</label>
                        <div class="control">
                        <input
                            class="input"
                            type="currencytype"
                            placeholder="Amount to withdraw"
                            required
                            v-model="amountc"
                        />
                        </div>
                        </div>
                        </div>
                        <div class="column">
                            <div id="message" v-if="error3 != ''">
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
      userinfo: [{}],
      column1: [{}],
      column2: [{}],
      useridbet: store.getters.getId,
      error1: '',
      error2: '',
      error3: '',
      error4: '',
      error5: '',
      selectedcurrencytype: 0,
      selectedcurrencytypeto: 0,
      amount: 0,
      amountl: 0,
      amountc: 0
    }
  },
  methods: {
    withdrawal: function () {
      const { selectedcurrencytype, amount, useridbet } = this
      const select = parseInt(selectedcurrencytype)
      const newbal = parseFloat(amount)
      // eslint-disable-next-line camelcase
      axios({ url: api_url + '/api/Wallet/withdraw/' + useridbet, data: { balance: newbal, appUserID: useridbet, coinID: select }, method: 'PUT' })
        .then(resp => {
          location.reload()
        })
        .catch(err => {
          console.log('TEST MAKEBET-> VIEWS/LOGGEDMEMBER -> ERROR RESPONSE:')
          console.log(err)
          this.error1 = err
        })
    },
    load: function () {
      const { selectedcurrencytype, amountl, useridbet } = this
      const select = parseInt(selectedcurrencytype)
      const newbal = parseFloat(amountl)
      // eslint-disable-next-line camelcase
      axios({ url: api_url + '/api/Wallet/load/' + useridbet, data: { balance: newbal, appUserID: useridbet, coinID: select }, method: 'PUT' })
        .then(resp => {
          location.reload()
        })
        .catch(err => {
          console.log('TEST MAKEBET-> VIEWS/LOGGEDMEMBER -> ERROR RESPONSE:')
          console.log(err)
          this.error2 = err
        })
    },
    exchange: function () {
      const { selectedcurrencytype, selectedcurrencytypeto, amountc, useridbet } = this
      const selectfrom = parseInt(selectedcurrencytype)
      const selectto = parseInt(selectedcurrencytypeto)
      const newbal = parseFloat(amountc)
      console.log('App User Id:' + useridbet)
      console.log('Select from:' + selectfrom)
      console.log('Select to:' + selectto)
      console.log('Balance: ' + newbal)
      // eslint-disable-next-line camelcase
      axios({ url: api_url + '/api/Wallet/' + useridbet + '/changeCurrency', data: { appUserID: useridbet, balanceBuy: newbal, coinIDFrom: selectfrom, coinIDTo: selectto }, method: 'PUT' })
        .then(resp => {
          location.reload()
        })
        .catch(err => {
          console.log('TEST MAKEBET-> VIEWS/LOGGEDMEMBER -> ERROR RESPONSE:')
          console.log(err)
          this.error3 = err
        })
    },
    fillColumnsUser () {
      this.column1 = []
      var i
      for (i = 0; i < this.userinfo.length; i = i + 1) {
        if (i < this.userinfo.length) {
          this.column1.push(this.userinfo[i])
        }
      }
    },
    fillColumnsCoins () {
      this.column2 = []
      var i
      for (i = 0; i < this.allinfo.length; i = i + 1) {
        if (i < this.allinfo.length) {
          this.column2.push(this.allinfo[i])
        }
      }
    }
  },
  created () {
    this.userinfo = []
    this.allinfo = []
    const userid = this.useridbet
    // eslint-disable-next-line camelcase
    axios.get(api_url + '/api/Wallet/' + userid)
      .then(resp => {
        var i
        for (i = 0; i < resp.data.length; i++) {
          if (resp.data[i] != null) {
            const betobj = {
              balance: resp.data[i].balance,
              coinName: resp.data[i].coinName
            }
            this.userinfo.push(betobj)
          }
        }
        this.fillColumnsUser()
      })
      .catch(err => {
        console.log('TEST -> VIEWS/LOGGEDMEMBER.VUE -> GET BETS ERROR RESPONSE:')
        console.log(err)
        this.error4 = err
      })
    // eslint-disable-next-line camelcase
    axios.get(api_url + '/' + userid + '/allMoney/')
      .then(resp => {
        if (resp.data != null) {
          const betobj = {
            balance: resp.data.balance,
            coinName: 'Total: ' + resp.data.coinName
          }
          this.userinfo.push(betobj)
        }
        this.fillColumnsUser()
      })
      .catch(err => {
        console.log('TEST -> VIEWS/MYWALLET.VUE -> GET BETS ERROR RESPONSE:')
        console.log(err)
        this.error5 = err
      })
    // eslint-disable-next-line camelcase
    axios.get(api_url + '/api/Wallet/ValueEnxanges')
      .then(resp => {
        var i
        for (i = 0; i < resp.data.length; i++) {
          if (resp.data[i] != null) {
            const betobj = {
              id: resp.data[i].id,
              name: resp.data[i].name,
              convertion: resp.data[i].convertionToEuro
            }
            this.allinfo.push(betobj)
          }
        }
        this.fillColumnsCoins()
      })
      .catch(err => {
        console.log('TEST -> VIEWS/LOGGEDMEMBER.VUE -> GET BETS ERROR RESPONSE:')
        console.log(err)
        this.error5 = err
      })
  }
}
</script>

<style scoped>
#content {
  margin: 10% auto;
  width: 100%;
}

.card-top-first {
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
  background-color: rgb(112, 50, 50);
}

.card-top-third {
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
