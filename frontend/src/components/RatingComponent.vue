<template>
	<div
		:style="{
			backgroundColor: color || 'var(--tag-blue)',
			borderRadius: '10px',
			maxWidth: 'fit-content',
		}"
		class="d-flex justify-content-center px-2 py-1">
		<font-awesome-icon
			v-for="(_, index) in 5"
			:key="index"
			:icon="getStarIcon(index)"
			style="font-size: 1em" />
	</div>
</template>

<script setup lang="ts">
	const props = defineProps<{
		rating: number
		color?: string
	}>()

	const getStarIcon = (index: number): [string, string] => {
		const fullStars = Math.floor(props.rating)
		const hasHalfStar = props.rating - fullStars >= 0.5

		if (index < fullStars) {
			return ['fas', 'star']
		} else if (index === fullStars && hasHalfStar) {
			return ['fas', 'star-half-stroke']
		} else {
			return ['far', 'star']
		}
	}
</script>
