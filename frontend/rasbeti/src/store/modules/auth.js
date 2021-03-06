import {
  AUTH_REQUEST,
  AUTH_ERROR,
  AUTH_SUCCESS,
  AUTH_LOGOUT
} from '@/store/actions/auth'
import { USER_REQUEST } from '@/store/actions/user'
import axios from 'axios'
// eslint-disable-next-line camelcase
import jwt_decode from 'jwt-decode'
// eslint-disable-next-line camelcase
import { url as api_url } from '@/assets/scripts/api'

const state = {
  token: localStorage.getItem('user-token') || '',
  status: '',
  hasLoadedOnce: false
}

const getters = {
  getToken: state => state.token,
  isAuthenticated: state => !!state.token,
  authStatus: state => state.status
}

const actions = {
  [AUTH_REQUEST]: ({ commit, dispatch }, user) => {
    return new Promise((resolve, reject) => {
      commit(AUTH_REQUEST)
      // eslint-disable-next-line camelcase
      axios({ url: api_url + '/api/Account/login', data: user, method: 'POST' })
        .then(resp => {
          var token = resp.data.token
          localStorage.setItem('user-token', token)
          axios.defaults.headers.common.Authorization = `Bearer ${token}`
          commit(AUTH_SUCCESS, resp)
          var decoded = jwt_decode(token)
          dispatch(USER_REQUEST, { role: decoded.role, name: decoded.nameid, email: resp.data.email, id: resp.data.id })
          resolve(decoded.role)
        })
        .catch(err => {
          commit(AUTH_ERROR, err)
          localStorage.removeItem('user-token')
          reject(err)
        })
    })
  },
  [AUTH_LOGOUT]: ({ commit }) => {
    return new Promise(resolve => {
      commit(AUTH_LOGOUT)
      delete axios.defaults.headers.common.Authorization
      localStorage.removeItem('user-token')
      resolve()
    })
  }
}

const mutations = {
  [AUTH_REQUEST]: state => {
    state.status = 'loading'
  },
  [AUTH_SUCCESS]: (state, resp) => {
    state.status = 'success'
    state.token = resp.data.token
    state.hasLoadedOnce = true
  },
  [AUTH_ERROR]: state => {
    state.status = 'error'
    state.hasLoadedOnce = true
  },
  [AUTH_LOGOUT]: state => {
    state.token = ''
  }
}

export default {
  state,
  getters,
  actions,
  mutations
}
