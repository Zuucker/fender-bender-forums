import { defineStore } from 'pinia'
import { ISnackBarMessage } from '../../Intefaces'

export const useSnackBarStore = defineStore('snackbar', {
	state: () => ({
		messages: [] as ISnackBarMessage[],
	}),
	actions: {
		addMessage(message: ISnackBarMessage) {
			this.messages.push(message)

			setTimeout(() => {
				this.messages.shift()
			}, message.timeout ?? 3000)
		},
	},
	getters: {
		getMessages: (state) => state.messages,
	},
})
