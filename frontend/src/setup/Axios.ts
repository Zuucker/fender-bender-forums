import axios from 'axios'
import { useUserStore } from './stores/UserStore'

export const api = axios.create({
	baseURL: 'https://localhost:7028/api',
	headers: {
		'Content-Type': 'application/json',
	},
})

api.interceptors.request.use(
	(config) => {
		const userStore = useUserStore()

		const token = userStore.getToken.value

		if (token) {
			config.headers['Authorization'] = `Bearer ${token}`
		}

		return config
	},
	(error) => {
		return Promise.reject(error)
	}
)
