<template>
	<v-card max-width="400">
		<template v-slot:actions>
			<div class="d-flex flex-column col-12 px-3 pb-3">
				<div class="d-flex justify-content-center">
					<h1>Register</h1>
				</div>

				<h4>Email</h4>
				<v-text-field
					v-model="registerData.Email"
					label="Email"
					variant="outlined"
					:error-messages="getErrorMessage('Email')"></v-text-field>

				<h4>Username</h4>
				<v-text-field
					v-model="registerData.Username"
					label="Username"
					variant="outlined"
					:error-messages="getErrorMessage('Username')"></v-text-field>

				<h4>Password</h4>
				<v-text-field
					v-model="registerData.Password"
					label="Password"
					variant="outlined"
					type="password"
					:error-messages="getErrorMessage('Password')"></v-text-field>

				<h4>Confirm Password</h4>
				<v-text-field
					v-model="registerData.ConfirmPassword"
					label="ConfirmPassword"
					variant="outlined"
					type="password"
					:error-messages="getErrorMessage('ConfirmPassword')"
					@keydown="handleEnterPress"></v-text-field>

				<div class="d-flex justify-content-between col-12">
					<v-btn
						text="Login"
						variant="outlined"
						@click="
							() => {
								props.showRegister = false
								props.showLogin = true
							}
						"></v-btn>
					<v-btn
						text="Register"
						variant="outlined"
						@click="handleRegistration()"></v-btn>
				</div>
			</div>
		</template>
	</v-card>
</template>

<script setup lang="ts">
	import { ref } from 'vue'
	import ILoginDialogProps from '../Intefaces/ComponentInterfaces/ILoginComponentProps'
	import IRegisterData from '../Intefaces/ComponentInterfaces/IRegisterData'
	import * as yup from 'yup'
	import { Register } from '../setup/Endpoints'
	import { useSnackBarStore } from '../setup/stores/SnackBarStore'

	const props = defineModel<ILoginDialogProps>()
	const registerData = ref<IRegisterData>({} as IRegisterData)
	const errors = ref<Record<string, string>>({})
	const snackBarStore = useSnackBarStore()

	const registerValidationSchema = yup.object({
		Email: yup
			.string()
			.email('The provided string is not a valid Email')
			.required('Email is required'),
		Username: yup
			.string()
			.min(3, 'Username needs at least 3 characers')
			.required('Username is required'),
		Password: yup
			.string()
			.min(6, 'Password needs at least 6 characers')
			.required('Password is required'),
		ConfirmPassword: yup
			.string()
			.oneOf([yup.ref('Password')], 'Passwords must match')
			.required(),
	})

	const handleRegistration = async () => {
		errors.value = {}
		try {
			await registerValidationSchema.validate(registerData.value, {
				abortEarly: false,
			})

			await Register(registerData.value)
				.then(() => {
					snackBarStore.addMessage({
						text: 'Successfully registered!',
						color: 'success',
						timeout: 2000,
					})

					props.value.showRegister = false
					props.value.showLogin = true
				})
				.catch(() => {
					snackBarStore.addMessage({
						text: 'Server error! Please try again!',
						color: 'error',
						timeout: 2000,
					})
				})
		} catch (err: any) {
			if (err.inner) {
				const errorMap: Record<string, string> = {}
				err.inner.forEach((e: any) => {
					errorMap[e.path] = e.message
				})
				errors.value = errorMap
			}
		}
	}

	const getErrorMessage = (field: string): string | null => {
		return errors.value[field] || null
	}

	const handleEnterPress = (event: KeyboardEvent) => {
		if (event.key === 'Enter') {
			handleRegistration()
		}
	}
</script>

<style scoped>
	.v-input__control {
		padding-left: 10px !important;
	}
</style>
