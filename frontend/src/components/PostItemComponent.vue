<template>
	<div class="post-item d-flex col-12">
		<div v-if="small" class="thicc-border d-flex col-12">
			<div
				class="col"
				style="cursor: pointer; padding: 0.5rem"
				@click="handleClick">
				<h4 class="m-0">
					<b>{{ post?.Title }}</b>
				</h4>
			</div>
		</div>
	</div>
</template>

<script setup lang="ts">
	import { useRouter } from 'vue-router'
	import { IPost } from '../Intefaces'

	type PostItemComponentProps = {
		data: IPost
		small?: boolean
	}
	const router = useRouter()

	const { data: post, small } = defineProps<PostItemComponentProps>()

	const handleClick = () => {
		router.push(`/post/${post.Id}`)
	}
</script>
