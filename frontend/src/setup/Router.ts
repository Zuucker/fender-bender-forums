import { createRouter, createWebHistory } from 'vue-router'
import Offers from '../pages/Offers.vue'
import Forum from '../pages/Forum.vue'

const routes = [
	{ path: '/', component: Offers },
	{ path: '/forum', component: Forum },
]

export const router = createRouter({
	history: createWebHistory(),
	routes,
})
