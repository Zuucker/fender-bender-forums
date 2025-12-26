<template>
	<v-snackbar-queue v-model="messages"></v-snackbar-queue>
</template>

<script setup lang="ts">
	import { useSnackBarStore } from '../setup/stores/SnackBarStore'

	const snackBarStore = useSnackBarStore()

	const messages = snackBarStore.getMessages
</script>
