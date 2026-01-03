<template>
	<div
		:style="{
			backgroundColor: color || 'var(--tag-blue)',
			borderRadius: '10px',
			maxWidth: 'fit-content',
			height: 'fit-content',
		}"
		class="d-flex justify-content-center px-2 me-1 ms-1">
		{{ props.text }}
	</div>
</template>

<script setup lang="ts">
	import { onMounted, ref } from 'vue'
	import { Condition } from '../constants/ConditionEnum'

	const props = defineProps<{
		text: string
	}>()

	const color = ref<string>('')

	const colorMap: Record<keyof typeof Condition, string> = {
		New: 'lime',
		Used: 'lightblue',
		HeavilyUsed: 'peachpuff',
		Refurbished: 'plum',
		Damaged: 'salmon',
	}

	onMounted(() => {
		color.value = colorMap[props.text]
	})
</script>
