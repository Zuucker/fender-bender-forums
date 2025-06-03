import { createRouter, createWebHistory, useRoute } from 'vue-router'
import Offers from '../pages/Offers.vue'
import Forum from '../pages/Forum.vue'
import { watch } from 'vue'
import Profile from '../pages/Profile.vue'

const route = useRoute()

const fullSizePages = ['/user/:id']

const routes = [
	{ path: '/', component: Offers },
	{ path: '/forum', component: Forum },
	{ path: '/user/:id', component: Profile, meta: { layout: 'FullLayout' } },
	{ path: '/offer/add', component: Forum },
	{ path: '/post/add', component: Forum },
]

export const router = createRouter({
	history: createWebHistory(),
	routes,
})

export const goToPage = (path: string) => {
	router.push(path)
}
