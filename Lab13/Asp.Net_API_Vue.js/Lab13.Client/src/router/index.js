import { createRouter, createWebHistory } from 'vue-router'
import ProfilePage from '@/views/ProfilePage.vue'
import LoginPage from '@/views/LoginPage.vue'
import SignupPage from '@/views/SignupPage.vue'
import Lab1Page from '@/views/Lab1Page.vue'
import Lab2Page from '@/views/Lab2Page.vue'
import Lab3Page from '@/views/Lab3Page.vue'
import HomePage from '@/views/HomePage.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomePage,
    },
    {
      path: '/profile',
      name: 'profile',
      component: ProfilePage,
    },
    {
      path: '/login',
      name: 'login',
      component: LoginPage,
    },
    {
      path: '/signup',
      name: 'signup',
      component: SignupPage,
    },
    {
      path: '/lab1',
      name: 'lab1',
      component: Lab1Page,
    },
    {
      path: '/lab2',
      name: 'lab2',
      component: Lab2Page,
    },
    {
      path: '/lab3',
      name: 'lab3',
      component: Lab3Page,
    },
  ],
})

export default router
