<template>
	<div
		:style="{
			backgroundColor: props.tag.Color || 'var(--tag-blue)',
			borderRadius: '10px',
			maxWidth: 'fit-content',
			height: 'fit-content',
		}"
		class="d-flex justify-content-center px-2 me-1">
		{{ props.tag.Text }}
	</div>
</template>

<script setup lang="ts">
	import { ITag } from '../Intefaces/ITag'

	const props = defineProps<{
		tag: ITag
	}>()
</script>
