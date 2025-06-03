<template>
	<div class="post-item d-flex col-12">
		<div v-if="small" class="thicc-border d-flex align-items-content col-12">
			<h4 class="m-0">
				<b>{{ post?.Title }}</b>
			</h4>
		</div>
	</div>
</template>

<script setup lang="ts">
	import { IPost } from '../Intefaces'

	type PostItemComponentProps = {
		data: IPost
		small?: boolean
	}

	const { data: post, small } = defineProps<PostItemComponentProps>()
</script>
