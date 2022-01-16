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
    } else if (store.getters.isMember) {
      next('/member')
    } else if (store.getters.isModerator) {
      next('/moderator')
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
      next('/member')
    }
  } else {
    next('/login')
  }
}

const ifAuthenticatedMember = (to, from, next) => {
  console.log('Testing if Authenticated Member')
  console.log(localStorage.getItem('user-token'))
  console.log(store.getters.isAuthenticated)
  console.log(store.getters.isMember)
  if (store.getters.isAuthenticated) {
    if (store.getters.isMember) {
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
      name: 'LoggedMember',
      path: '/member',
      component: () => import('@/views/LoggedMember'),
      beforeEnter: ifAuthenticatedMember
    },
    {
      name: 'MyWallet',
      path: '/member/mywallet',
      component: () => import('@/views/MyWallet'),
      beforeEnter: ifAuthenticatedMember
    },
    {
      name: 'MyBets',
      path: '/member/mybets',
      component: () => import('@/views/MyBets'),
      beforeEnter: ifAuthenticatedMember
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
