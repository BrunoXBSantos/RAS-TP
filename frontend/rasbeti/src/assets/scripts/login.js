import axios from 'axios'
import { url } from './api'

export const myLoginRoutine = user => new Promise((resolve, reject) => {
  axios({ url: url + '/api/Account/login', data: user, method: 'POST' })
    .then(resp => {
      const token = resp.data.id
      localStorage.setItem('user-token', token) // store the token in localstorage
      resolve(resp)
    })
    .catch(err => {
      localStorage.removeItem('user-token') // if the request fails, remove any possible user token if possible
      reject(err)
    })
})
