import { createRouter, createWebHistory } from 'vue-router'
import Offers from '../pages/Offers.vue'
import Forum from '../pages/Forum.vue'
import Profile from '../pages/Profile.vue'
import AddPost from '../pages/AddPost.vue'
import Post from '../pages/Post.vue'

const routes = [
	{ path: '/', component: Offers },
	{ path: '/forum', component: Forum },
	{ path: '/user/:id', component: Profile, meta: { layout: 'FullLayout' } },
	{ path: '/offer/add', component: Forum },
	{ path: '/post/add', component: AddPost, meta: { layout: 'FullLayout' } },
	{ path: '/post/:id', component: Post },
]

export const router = createRouter({
	history: createWebHistory(),
	routes,
})

export const goToPage = (path: string) => {
	router.push(path)
}
