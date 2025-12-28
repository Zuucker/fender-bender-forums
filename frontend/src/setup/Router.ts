import { createRouter, createWebHistory } from 'vue-router'
import Offers from '../pages/Marketplace.vue'
import Forum from '../pages/Forum.vue'
import Profile from '../pages/Profile.vue'
import AddPost from '../pages/AddPost.vue'
import Post from '../pages/Post.vue'
import AddOffer from '../pages/AddOffer.vue'
import Offer from '../pages/Offer.vue'

const routes = [
	{ path: '/', component: Offers },
	{ path: '/forum', component: Forum },
	{ path: '/user/:id', component: Profile, meta: { layout: 'FullLayout' } },
	{ path: '/post/add', component: AddPost, meta: { layout: 'FullLayout' } },
	{ path: '/post/:id', component: Post },
	{ path: '/offer/add', component: AddOffer, meta: { layout: 'FullLayout' } },
]

export const router = createRouter({
	history: createWebHistory(),
	routes,
})

export const goToPage = (path: string) => {
	router.push(path)
}
