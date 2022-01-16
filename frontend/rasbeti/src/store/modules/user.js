import { USER_REQUEST, USER_ERROR, USER_SUCCESS } from '../actions/user'
import { AUTH_LOGOUT } from '../actions/auth'

const state = {
  status: '',
  role: '',
  name: '',
  id: '',
  email: ''
}

const getters = {
  getType: state => state.type,
  getName: state => state.name,
  getId: state => state.id,
  getEmail: state => state.email,
  isAdmin: state => state.role === 'Admin',
  isMember: state => state.role === 'Member',
  isModerator: state => state.role === 'Moderator',
  isProfileLoaded: state => !!state.profile.name
}

const actions = {
  [USER_REQUEST]: ({ commit, dispatch }, token) => {
    commit(USER_REQUEST)
    var resp = {}
    resp.role = token.role
    resp.name = token.name
    resp.email = token.email
    resp.id = token.id
    commit(USER_SUCCESS, resp)
    // console.log('after user sucess ' + JSON.stringify(state))
    if (resp.role !== 'Member' && resp.role !== 'Admin' && resp.role !== 'Moderator') {
      dispatch(AUTH_LOGOUT)
    }
    // console.log('after logout ' + JSON.stringify(state))
  }
}

const mutations = {
  [USER_REQUEST]: state => {
    state.status = 'loading'
  },
  [USER_SUCCESS]: (state, resp) => {
    state.status = 'success'
    state.role = resp.role
    state.name = resp.name
    state.id = resp.id
    state.email = resp.email
  },
  [USER_ERROR]: state => {
    state.status = 'error'
  },
  [AUTH_LOGOUT]: state => {
    state.profile = {}
  }
}

export default {
  state,
  getters,
  actions,
  mutations
}
