<template>
	<div class="d-flex" v-if="userData.UserName" style="max-height: 40dvh">
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
			<v-btn class="col-12 mb-4 text-dark">Add Offer</v-btn>
			<v-btn class="col-12 text-dark">Add Post</v-btn>
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
		style="max-height: 60dvh">
		<div class="d-flex flex-grow-1 overflow-hidden">
			<div class="col-8 d-flex flex-column hide-scrollbar">
				<div class="flex-fill overflow-auto">
					<VirtualList :dataList="mockOffers" :component="OfferItemComponent" />
				</div>
			</div>
			<div class="col-4 d-flex flex-column hide-scrollbar">
				<div class="flex-fill overflow-auto">
					<VirtualList
						:dataList="mockPosts"
						:component="PostItemComponent"
						:small="true" />
				</div>
			</div>
		</div>
	</div>
</template>

<script setup lang="ts">
	import { onMounted, reactive, ref, watch } from 'vue'
	import RatingComponent from '../components/RatingComponent.vue'
	import VirtualList from '../components/VirtualList.vue'
	import {
		ICar,
		ICity,
		IComment,
		IContent,
		IOffer,
		IOfferRate,
		IPost,
		ISection,
		IUser,
	} from '../Intefaces'
	import { useUserStore } from '../setup/stores/UserStore'
	import { EditUser, GetUserData } from '../setup/Endpoints'
	import { useRoute } from 'vue-router'
	import { useSnackBarStore } from '../setup/stores/SnackBarStore'
	import { PulseLoader } from 'vue-spinner/dist/vue-spinner.min.js'
	import OfferItemComponent from '../components/OfferItemComponent.vue'
	import PostItemComponent from '../components/PostItemComponent.vue'

	const userstore = useUserStore()
	const snackBarStore = useSnackBarStore()

	const route = useRoute()
	const userId = Array.isArray(route.params.id)
		? route.params.id[0]
		: route.params.id

	const userData = reactive<IUser>({} as IUser)
	const newUserData = reactive<IUser>({} as IUser)
	const userNotFound = ref(false)

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
					text: 'Server error! Please try again!',
					color: 'error',
					timeout: 2000,
				})
			})
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

	const mockPosts: IPost[] = Array.from({ length: 10 }, (_, i) => ({
		Id: i + 1,
		AuthorId: `author${i + 1}`,
		SectionId: 200 + i,
		Title: `Post Title ${i + 1}`,
		CreationDate: new Date(`2023-06-${(i + 1).toString()}`),
		Content: i * 10,
		Tags: `tag${i + 1},tag${i + 2}`,
		Contents: {} as IContent,
		Author: { UserName: `User ${i + 1}` } as IUser,
		Section: {} as ISection,
	}))

	const mockOffers: IOffer[] = Array.from({ length: 10 }, (_, i) => ({
		OfferId: `offer${i + 1}`,
		Title: `Offer Title ${i + 1}`,
		Price: 10000 + i * 500,
		CarId: i + 1,
		CityId: 100 + i,
		AuthorId: `user${i + 1}`,
		Date: new Date(`2023-05-${(i + 1).toString()}`),
		Condition: (i % 5) + 1,
		Fuel: i % 3,
		Color: ['Red', 'Blue', 'Green', 'Black', 'White'][i % 5],
		Mileage: 10000 + i * 1000,
		Tags: `tag${i + 1},tag${i + 2}`,
		Rating: 3.5 + (i % 2),
		RatingCount: 5 + i,
		Car: {} as ICar,
		City: {} as ICity,
		Author: { UserName: `User ${i + 1}` } as IUser,
		Ratings: [] as IOfferRate[],
		Contents: {} as IContent,
		Comments: [] as IComment[],
	}))
</script>
