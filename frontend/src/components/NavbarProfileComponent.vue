<template>
	<div class="col d-flex justify-content-end align-items-center">
		<div class="profile-button me-4">
			<v-btn
				v-if="user === null"
				variant="outlined"
				density="compact"
				@click="showDialogs.showLogin = true">
				Login
			</v-btn>

			<v-menu v-if="user !== null">
				<template v-slot:activator="{ props }">
					<v-btn
						v-bind="props"
						class="px-1"
						style="min-width: 0px"
						density="compact"
						:onclick="
							() => {
								goToPage(`/user/${user.Id}`)
							}
						">
						<img :src="user.Avatar" style="max-height: 20px" />
						<span>{{ user.UserName.charAt(0) }}</span>
						<template v-slot:append>
							<v-btn
								class="px-1"
								style="border-left: 1px solid black; border-radius: 0px"
								variant="text"
								density="compact"
								@click="handleLogout()">
								Logout
							</v-btn>
						</template>
					</v-btn>
				</template>
			</v-menu>
		</div>
	</div>
	<v-dialog v-model="showDialogs.showLogin" width="auto"
		><LoginComponent class="login-modal" v-model="showDialogs"
	/></v-dialog>
	<v-dialog v-model="showDialogs.showRegister" width="auto"
		><RegisterComponent class="login-modal" v-model="showDialogs"
	/></v-dialog>
	<v-dialog v-model="showDialogs.showProfileMenu" width="auto"
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
	import { goToPage } from '../setup/Router'

	const userStore = useUserStore()
	const snackBarStore = useSnackBarStore()

	const showDialogs = ref<ILoginDialogProps>({
		showLogin: false,
		showRegister: false,
		showProfileMenu: false,
	})

	const user = userStore.getUser

	watch(userStore.getToken, async () => {
		await GetUserData()
			.then((userResponse) => {
				userStore.setUser(userResponse)
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

		snackBarStore.addMessage({
			text: 'User successfully logged out!',
			color: 'success',
			timeout: 2000,
		})

		goToPage('/')
	}

	const handleOpenLogin = () => {
		showDialogs.value.showLogin = true
		showDialogs.value.showProfileMenu = false
		showDialogs.value.showRegister = false
	}

	defineExpose({ handleOpenLogin })
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

	.v-list-item__append {
		width: 0px;
		display: none !important;
	}
	.mdi-chevron-down {
		display: none !important;
	}
</style>

<style scoped>
	.login-modal {
		border: 2px solid var(--black);
		border-radius: 0px !important;
		min-width: 40dvw;
	}

	.side-menu-item {
		background-color: var(--white);
		border-width: 1px 0px 0px 0px;
		border-color: var(--black);

		&:hover > span {
			opacity: 0.1;
		}
	}

	.side-menu-item.v-list-item.v-list-item--link.v-theme--light.v-list-item--density-compact.v-list-item--one-line.v-list-item--variant-text {
		padding-inline-start: 2px !important;
		padding: auto !important;
		margin: auto !important;
		text-align: center;
	}

	.side-menu-main-item {
		background-color: var(--white);
		border: 2px solid var(--black);
		padding-inline-start: 2px;
		margin: 0px !important;
		padding: 0px !important;
	}

	.sort-component {
		position: relative;
		height: 30px;
	}

	.v-list.v-theme--light.v-list--density-default {
		position: absolute;
		z-index: 10;
	}

	.v-list {
		overflow-y: hidden !important;
	}

	.v-list-item {
		height: 20px;
	}
</style>
