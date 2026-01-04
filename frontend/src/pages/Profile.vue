<template>
	<div
		ref="profileInfoRef"
		class="d-flex"
		v-if="userData.UserName"
		style="max-height: 40dvh">
		<div class="col-2 d-flex justify-content-center align-items-center">
			<img :src="userData.Avatar" style="height: 80px" />
		</div>
		<div
			class="col-2 d-flex flex-column justify-content-center align-items-center py-3">
			<div class="mb-4">
				<h2>{{ userData.UserName }}</h2>
			</div>
			<RatingComponent :rating="userData.Rating" />
		</div>
		<div class="d-flex justify-content-between col-5">
			<div
				v-if="userData.Id === userstore?.getUser?.value?.Id"
				class="d-flex col-7 flex-column align-items-center">
				<v-text-field
					class="col-12 mt-2 text-dark"
					label="Username"
					variant="outlined"
					density="compact"
					hide-details
					single-line
					v-model="newUserData.UserName" />
				<v-text-field
					class="col-12 mt-2 text-dark"
					label="Email"
					variant="outlined"
					density="compact"
					hide-details
					single-line
					v-model="newUserData.Email" />
				<v-text-field
					class="col-12 mt-2 text-dark"
					label="NewPassword"
					variant="outlined"
					density="compact"
					hide-details
					single-line
					v-model="newUserData.NewPassword"
					type="password" />
				<v-text-field
					class="col-12 mt-2 text-dark"
					label="ConfirmPassword"
					variant="outlined"
					density="compact"
					hide-details
					single-line
					v-model="newUserData.ConfirmPassword"
					type="password" />
				<v-text-field
					class="col-12 mt-2 text-dark"
					label="Password"
					variant="outlined"
					density="compact"
					hide-details
					single-line
					v-model="newUserData.Password"
					type="password" />
			</div>
			<div
				v-if="userData.Id === userstore?.getUser?.value?.Id"
				class="d-flex col-4 flex-column align-items-center justify-content-center">
				<v-btn class="col-12 mb-4 text-dark" @click="saveUserChanges()"
					>Confirm</v-btn
				>
				<v-btn class="col-12 text-dark" @click="cancelUserChanges()"
					>Cancel</v-btn
				>
			</div>
		</div>
		<div
			v-if="userData.Id === userstore?.getUser?.value?.Id"
			class="col-2 ms-auto me-4 d-flex flex-column justify-content-center align-items-center py-3">
			<v-btn
				class="col-12 mb-4 text-dark"
				:onclick="
					() => {
						goToPage('/offer/add')
					}
				"
				>Add Offer</v-btn
			>
			<v-btn
				class="col-12 text-dark"
				:onclick="
					() => {
						goToPage('/post/add')
					}
				"
				>Add Post</v-btn
			>
		</div>
	</div>
	<div
		v-if="!userData.UserName && !userNotFound"
		class="d-flex h-75 flex-collumn align-items-center justify-content-center">
		<PulseLoader :loading="true" :color="'var(--blazy)'" :size="'30px'" />
	</div>

	<div
		v-if="!userData.UserName && userNotFound"
		class="d-flex h-75 flex-collumn align-items-center justify-content-center">
		No such user found
	</div>

	<div
		v-if="userData.UserName"
		class="d-flex flex-column py-3"
		:style="{ maxHeight: `${maxHeight}px` }">
		<div class="d-flex flex-grow-1 overflow-hidden">
			<div class="col-8 d-flex flex-column hide-scrollbar">
				<div class="flex-fill overflow-auto">
					<VirtualList
						:dataList="userOffers"
						:component="OfferItemComponent"
						:fetch-callback="() => {}"
						empty-message="This user has no offers yet!" />
				</div>
			</div>
			<div class="col-4 d-flex flex-column hide-scrollbar">
				<div class="flex-fill overflow-auto">
					<VirtualList
						:dataList="userPosts"
						:component="PostItemComponent"
						:small="true"
						:fetch-callback="() => {}"
						empty-message="This user has no posts yet!" />
				</div>
			</div>
		</div>
	</div>
</template>

<script setup lang="ts">
	import { nextTick, onMounted, reactive, ref } from 'vue'
	import RatingComponent from '../components/RatingComponent.vue'
	import VirtualList from '../components/VirtualList.vue'
	import { IOffer, IPost, IUser } from '../Intefaces'
	import { useUserStore } from '../setup/stores/UserStore'
	import {
		EditUser,
		GetUserData,
		GetUserTopOffers,
		GetUserTopPosts,
	} from '../setup/Endpoints'
	import { useRoute } from 'vue-router'
	import { useSnackBarStore } from '../setup/stores/SnackBarStore'
	import { PulseLoader } from 'vue-spinner/dist/vue-spinner.min.js'
	import OfferItemComponent from '../components/OfferItemComponent.vue'
	import PostItemComponent from '../components/PostItemComponent.vue'
	import { goToPage } from '../setup/Router'

	const userstore = useUserStore()
	const snackBarStore = useSnackBarStore()

	const route = useRoute()
	const userId = Array.isArray(route.params.id)
		? route.params.id[0]
		: route.params.id

	const userData = reactive<IUser>({} as IUser)
	const newUserData = reactive<IUser>({} as IUser)
	const userNotFound = ref(false)
	const userPosts = ref<IPost[]>([])
	const userOffers = ref<IOffer[]>([])

	const profileInfoRef = ref<HTMLDivElement | null>(null)
	const maxHeight = ref<number>(0)

	onMounted(async () => {
		await GetUserData(userId)
			.then((userResponse) => {
				if (userResponse) {
					Object.assign(userData, userResponse)
					Object.assign(newUserData, userResponse)
				}
			})
			.catch((error) => {
				console.log(error)
				if (error.response?.status === 404) {
					userNotFound.value = true
					return
				}

				snackBarStore.addMessage({
					text: 'Server error! Please try again later!',
					color: 'error',
					timeout: 2000,
				})
			})

		await GetUserTopPosts(userId)
			.then((postResponse) => {
				if (postResponse) {
					userPosts.value = postResponse
				}
			})
			.catch((error) => {
				console.log(error)
				snackBarStore.addMessage({
					text: 'Server error! Please try again later!',
					color: 'error',
					timeout: 2000,
				})
			})

		await GetUserTopOffers(userId)
			.then((offerResponse) => {
				if (offerResponse) {
					userOffers.value = offerResponse
				}
			})
			.catch((error) => {
				console.log(error)
				snackBarStore.addMessage({
					text: 'Server error! Please try again later!',
					color: 'error',
					timeout: 2000,
				})
			})

		await nextTick()
		maxHeight.value =
			window.innerHeight * 0.9 -
			(profileInfoRef.value?.getBoundingClientRect().height ?? 0) +
			16
	})

	const saveUserChanges = async () => {
		if (newUserData.NewPassword !== newUserData.ConfirmPassword) {
			snackBarStore.addMessage({
				text: 'New password and Confirm password have to match!',
				color: 'error',
				timeout: 2000,
			})
			return
		}

		await EditUser(newUserData)
			.then((editedUser) => {
				snackBarStore.addMessage({
					text: 'User changes successfully saved!',
					color: 'success',
					timeout: 2000,
				})

				if (editedUser) {
					Object.assign(newUserData, editedUser)
					Object.assign(userData, editedUser)
				}
			})
			.catch((error) => {
				console.log(error)

				cancelUserChanges()

				snackBarStore.addMessage({
					text: 'Server error! Please try again!',
					color: 'error',
					timeout: 2000,
				})
			})
	}

	const cancelUserChanges = () => {
		Object.assign(newUserData, userData)
	}
</script>
