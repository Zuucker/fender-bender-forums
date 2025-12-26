<template>
	<div
		:style="{
			backgroundColor: color || 'var(--tag-blue)',
			borderRadius: '10px',
			maxWidth: 'fit-content',
		}"
		class="d-flex justify-content-center px-2 me-1">
		{{ props.text }}
	</div>
</template>

<script setup lang="ts">
	import { onMounted, ref } from 'vue'

	const props = defineProps<{
		text: string
	}>()

	const color = ref<string>('')

	onMounted(() => {
		const hue = Math.floor(Math.random() * 360)
		const saturation = 85 + Math.random() * 15
		const lightness = 50 + Math.random() * 10

		color.value = `hsl(${hue}, ${saturation}%, ${lightness}%)`
	})
</script>
