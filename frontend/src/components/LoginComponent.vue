<template>
	<v-card max-width="400">
		<template v-slot:actions>
			<div class="d-flex flex-column col-12 px-3 pb-3">
				<div class="d-flex justify-content-center">
					<h1>Login</h1>
				</div>
				<h4>Email</h4>
				<v-text-field
					label="Email"
					variant="outlined"
					v-model="loginData.Email"
					:error-messages="
						getErrorMessage('Email') ||
						(isEmailInvalid ? ['No user for this email found'] : [])
					"
					:error="isEmailInvalid"></v-text-field>
				<h4>Password</h4>
				<v-text-field
					type="password"
					label="Password"
					variant="outlined"
					v-model="loginData.Password"
					:error-messages="
						getErrorMessage('Password') ||
						(isPasswordInvalid ? ['Wrong password'] : [])
					"
					@keydown="handleEnterPress"></v-text-field>
				<div class="d-flex justify-content-between col-12">
					<v-btn
						text="Register"
						variant="outlined"
						@click="
							() => {
								props.showLogin = false
								props.showRegister = true
							}
						"></v-btn>
					<v-btn text="Login" variant="outlined" @click="handleLogin()"></v-btn>
				</div>
			</div>
		</template>
	</v-card>
</template>

<script setup lang="ts">
	import { ref } from 'vue'
	import ILoginDialogProps from '../Intefaces/ComponentInterfaces/ILoginComponentProps'
	import ILoginData from '../Intefaces/ComponentInterfaces/ILoginData'
	import * as yup from 'yup'
	import { Login } from '../setup/Endpoints'
	import { useSnackBarStore } from '../setup/stores/SnackBarStore'
	import { useUserStore } from '../setup/stores/UserStore'
	import { ApiErrors } from '../constants'

	const props = defineModel<ILoginDialogProps>()
	const loginData = ref<ILoginData>({} as ILoginData)
	const errors = ref<Record<string, string>>({})
	const snackBarStore = useSnackBarStore()
	const userStore = useUserStore()
	const isEmailInvalid = ref<boolean>(false)
	const isPasswordInvalid = ref<boolean>(false)

	const loginValidationSchema = yup.object({
		Email: yup
			.string()
			.email('The provided string is not a valid Email')
			.required('Email is required'),
		Password: yup.string().required('Password is required'),
	})

	const handleLogin = async () => {
		errors.value = {}
		try {
			await loginValidationSchema.validate(loginData.value, {
				abortEarly: false,
			})

			isEmailInvalid.value = false
			isPasswordInvalid.value = false

			await Login(loginData.value)
				.then((token) => {
					userStore.setToken(token)

					snackBarStore.addMessage({
						text: 'Successfully logged in!',
						color: 'success',
						timeout: 2000,
					})
					props.value.showLogin = false
				})
				.catch((error) => {
					console.log(error)

					if (!error.response || error.status === 500) {
						snackBarStore.addMessage({
							text: 'Server error! Please try again!',
							color: 'error',
							timeout: 2000,
						})
						return
					}

					if (error.response.data.Error === ApiErrors[ApiErrors.UserNotFound])
						isEmailInvalid.value = true

					if (
						error.response.data.Error ===
						ApiErrors[ApiErrors.PasswordsDontMatch]
					)
						isPasswordInvalid.value = true
				})
		} catch (err: any) {
			if (err.inner) {
				const errorMap: Record<string, string> = {}
				err.inner.forEach((e: any) => {
					errorMap[e.path] = e.message
				})
				errors.value = errorMap
			}

			snackBarStore.addMessage({
				text: 'Server error! Please try again!',
				color: 'error',
				timeout: 2000,
			})
		}
	}

	const getErrorMessage = (field: string): string | null => {
		return errors.value[field] || null
	}

	const handleEnterPress = (event: KeyboardEvent) => {
		if (event.key === 'Enter') {
			handleLogin()
		}
	}
</script>
