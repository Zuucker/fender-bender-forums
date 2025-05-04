import { defineStore } from 'pinia'
import { IUser } from '../../Intefaces'
import { computed } from 'vue'

export const useUserStore = defineStore('user', {
	state: () => ({
		User: null as IUser | null,
		Token: null as string | null,
	}),
	actions: {
		setToken(token: string) {
			this.Token = token
		},
		setUser(user: IUser) {
			this.User = user
		},
	},
	getters: {
		getToken: (state) => computed(() => state.Token),
		getUser: (state) => computed(() => state.User),
	},
	persist: true,
})
