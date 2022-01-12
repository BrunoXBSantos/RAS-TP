import Vue from 'vue'
import Router from 'vue-router'
import store from '@/store'

const ifNotAuthenticated = (to, from, next) => {
  if (!store.getters.isAuthenticated) {
    next()
  } else {
    next('/')
  }
}

const redirectAuthenticated = (to, from, next) => {
  if (store.getters.isAuthenticated) {
    if (store.getters.isAdmin) {
      next('/admin')
    } else if (store.getters.isUser) {
      next('/user')
    }
  } else {
    next()
  }
}

const ifAuthenticatedAdmin = (to, from, next) => {
  if (store.getters.isAuthenticated) {
    if (store.getters.isAdmin) {
      next()
    } else {
      next('/user')
    }
  } else {
    next('/login')
  }
}

const ifAuthenticatedUser = (to, from, next) => {
  console.log('Testing if Authenticated User')
  console.log(localStorage.getItem('user-token'))
  console.log(store.getters.isAuthenticated)
  console.log(store.getters.isUser)
  if (store.getters.isAuthenticated) {
    if (store.getters.isUser) {
      next()
    } else {
      next('/admin')
    }
  } else {
    next('/login')
  }
}

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      component: () => import('@/views/Home'),
      children: [
        {
          path: '',
          name: 'Home',
          component: () => import('@/views/Home')
        }
      ],
      beforeEnter: redirectAuthenticated
    },
    {
      name: 'Login',
      path: '/login',
      component: () => import('@/views/Login'),
      beforeEnter: ifNotAuthenticated
    },
    {
      name: 'Register',
      path: '/register',
      component: () => import('@/views/Register')
    },
    {
      name: 'Admin',
      path: '/admin',
      component: () => import('@/views/LoggedAdmin'),
      beforeEnter: ifAuthenticatedAdmin
    },
    {
      name: 'LoggedUser',
      path: '/user',
      component: () => import('@/views/LoggedUser'),
      beforeEnter: ifAuthenticatedUser
    },
    {
      path: '/404',
      component: () => import('@/views/NotFound')
    },
    {
      path: '*',
      redirect: '/404'
    }
  ]
})
