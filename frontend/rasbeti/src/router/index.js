import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    component: () => import('@/views/Home'),
    children: [
      {
        path: '',
        name: 'Home',
        component: () => import('@/views/Home')
      }
    ]
  },
  {
    name: 'Login',
    path: '/login',
    component: () => import('@/views/Login')
  },
  {
    name: 'Register',
    path: '/register',
    component: () => import('@/views/Register')
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
