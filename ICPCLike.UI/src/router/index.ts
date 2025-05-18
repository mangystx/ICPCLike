import { createRouter, createWebHistory } from 'vue-router'
import TableView from '@/views/TableView.vue'

const routes = [
  {
    path: '/',
    name: 'table',
    component: TableView,
  }
]

export default createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})
