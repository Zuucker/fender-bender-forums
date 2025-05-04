<template>
	<div class="col d-flex justify-content-between align-items-center">
		<div class="sort-component col-6">
			<v-select
				label=""
				:items="sortOptions"
				variant="outlined"
				density="compact"
				bg-color="white"
				hide-details="auto"
				v-model="selectedSort"
				class="small-select"></v-select>
		</div>
		<div class="profile-button me-4">
			<v-btn
				v-if="user === null"
				variant="outlined"
				density="compact"
				@click="showDialogs.showLogin = true">
				Login
			</v-btn>
			<v-btn
				class="text-dark h-100 px-1"
				v-if="user !== null"
				variant="outlined"
				density="compact">
				<v-btn
					class="px-1"
					style="min-width: 0px"
					variant=""
					density="compact"
					@click="console.log('show profile menu')">
					<img src="../assets/vue.svg" style="max-height: 20px" />
					<span>{{ user.UserName.charAt(0) }}</span>
				</v-btn>
				<v-btn
					class="px-1"
					style="border-left: 1px solid black; border-radius: 0px"
					variant=""
					density="compact"
					@click="handleLogout()">
					Logout
				</v-btn>
			</v-btn>
		</div>
	</div>
	<v-dialog v-model="showDialogs.showLogin" width="auto"
		><LoginComponent class="login-modal" v-model="showDialogs"
	/></v-dialog>
	<v-dialog v-model="showDialogs.showRegister" width="auto"
		><RegisterComponent class="login-modal" v-model="showDialogs"
	/></v-dialog>
	<SnackBarComponent />
</template>

<script setup lang="ts">
	import { ref, watch } from 'vue'
	import ILoginDialogProps from '../Intefaces/ComponentInterfaces/ILoginComponentProps'
	import LoginComponent from './LoginComponent.vue'
	import RegisterComponent from './RegisterComponent.vue'
	import SnackBarComponent from './SnackBarComponent.vue'
	import { useUserStore } from '../setup/stores/UserStore'
	import { GetUserData } from '../setup/Endpoints'
	import { useSnackBarStore } from '../setup/stores/SnackBarStore'
	import { IUser } from '../Intefaces'

	const sortOptions = ['Sort by new', 'Sort by price']
	const selectedSort = sortOptions[0]
	const userStore = useUserStore()
	const snackBarStore = useSnackBarStore()

	const showDialogs = ref<ILoginDialogProps>({
		showLogin: false,
		showRegister: false,
	})

	const user = userStore.getUser

	watch(userStore.getToken, async () => {
		console.log('now fetch data with', userStore.getUser)
		await GetUserData()
			.then((response) => {
				userStore.setUser(response.data as IUser)
			})
			.catch((error) => {
				console.log(error)
				setTimeout(() => {
					userStore.setToken(null)
				}, 250)

				snackBarStore.addMessage({
					text: 'Server error! Please try again!',
					color: 'error',
					timeout: 2000,
				})
			})
	})

	const handleLogout = () => {
		userStore.setUser(null)
	}
</script>

<style>
	.v-field__input,
	.v-select__selection {
		height: var(--nav-act-height) !important;
		padding: 0 !important;
	}

	.v-field__append-inner {
		height: var(--nav-act-height) !important;
		display: flex;
		align-items: center;
	}

	.v-select__selection,
	.v-field__input {
		min-height: var(--nav-act-height) !important;
		font-size: 20px !important;
	}

	.v-select__selection-text {
		position: relative;
		left: 20%;
	}

	.profile-button {
		& > button {
			min-height: var(--nav-act-height);
			background-color: var(--white);
		}
	}

	.v-btn--variant-outlined:active {
		border: 2px solid var(--black);
		border-radius: 4px;
	}
</style>

<style scoped>
	.login-modal {
		border: 2px solid var(--black);
		border-radius: 0px !important;
		min-width: 40dvw;
	}
</style>
