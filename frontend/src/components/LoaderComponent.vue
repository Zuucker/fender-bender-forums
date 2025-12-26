<template>
	<div
		v-if="isEmpty && loading"
		class="d-flex h-75 flex-collumn align-items-center justify-content-center">
		<PulseLoader :loading="true" :color="'var(--blazy)'" :size="'30px'" />
	</div>

	<div
		v-if="isEmpty && !loading"
		class="d-flex h-75 flex-collumn align-items-center justify-content-center">
		{{ notLoadedText }}
	</div>
</template>

<script setup lang="ts">
	import { computed } from 'vue'
	import { PulseLoader } from 'vue-spinner/dist/vue-spinner.min.js'

	type LoaderProps = {
		data: Object
		loading: boolean
		notLoadedText: string
	}

	const { data, loading, notLoadedText } = defineProps<LoaderProps>()

	const isEmpty = computed(() => {
		return Array.isArray(data) && data.length === 0
	})
</script>
