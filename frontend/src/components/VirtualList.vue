<template class="">
	<v-virtual-scroll :height="'flex-fill'" :items="props.dataList" class="p-2">
		<template v-slot:default="{ item }">
			<component
				:itemKey="item"
				:data="item"
				:is="props.component"
				:small="props.small" />
		</template>
	</v-virtual-scroll>
</template>

<script setup lang="ts">
	import { type Component } from 'vue'

	type VirtualListProps = {
		dataList: Object[]
		component: Component
		small?: boolean
	}

	const props = defineProps<VirtualListProps>()
</script>
