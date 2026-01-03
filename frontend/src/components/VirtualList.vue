<template class="">
	<v-virtual-scroll :height="'flex-fill'" :items="props.dataList" class="p-2">
		<template v-slot:default="{ item, index }">
			<component
				:key="item.Id"
				:data="item"
				:is="props.component"
				:small="props.small" />
			<div
				v-if="index === props.dataList.length - 1"
				v-intersect="onIntersect"
				style="height: 1px"></div>
		</template>
	</v-virtual-scroll>

	<div
		v-if="!props.dataList || props.dataList.length === 0"
		class="d-flex justify-content-center">
		{{ props.emptyMessage ?? 'The list is empty' }}
	</div>
</template>

<script setup lang="ts">
	import { watch, type Component } from 'vue'

	type VirtualListProps = {
		dataList: Object[]
		component: Component
		fetchCallback: () => void
		small?: boolean
		emptyMessage?: string
	}

	const props = defineProps<VirtualListProps>()

	function onIntersect(isIntersecting: boolean) {
		if (!isIntersecting) return

		props.fetchCallback()
	}
</script>
