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
		<div
			v-if="!small"
			class="thicc-border d-flex col-12"
			style="cursor: pointer; padding: 0.5rem">
			<div
				class="offer-left-part d-flex flex-column justify-content-between col-10"
				style="cursor: pointer"
				@click="handleClick">
				<h4>
					<b>{{ post?.Title }}</b>
				</h4>
				<div class="d-flex">
					<TagChip
						v-for="tag in [
 						{Text:post.Car?.Model, Color:'#6420FF'} as ITag,
						{Text:post.Car?.Type, Color:'#9B30FF'} as ITag,
						...post.Tags,
					].filter((f) => f)"
						:tag="tag" />
				</div>
			</div>
			<div class="offer-right-part d-flex flex-column col-1">
				<div
					class="d-flex flex-column justify-content-between align-items-between h-100 col-6">
					<div class="d-flex">
						<div class="d-flex align-items-center">
							<font-awesome-icon
								:icon="['far', 'thumbs-up']"
								class="me-2"
								style="font-size: 1.5em" />
							<span class="fs-3" style="height: fit-content">
								{{ post?.Points }}
							</span>
						</div>
					</div>
					<div class="d-flex">
						<div class="d-flex align-items-center">
							<font-awesome-icon
								:icon="['far', 'message']"
								class="me-2"
								style="font-size: 1.5em" />
						</div>
						<span class="fs-3" style="height: fit-content">{{
							post?.Comments.length ?? 0
						}}</span>
					</div>
				</div>
			</div>
			<div
				class="offer-right-part d-flex flex-column justify-content-between col-1">
				<div
					class="d-flex flex-column justify-content-between align-items-between h-100">
					<div class="d-flex justify-content-center">
						<img
							class="round-image"
							:src="post?.Author?.Avatar ?? 'https://placehold.co/50x50.png'"
							style="height: inherit" />
					</div>

					<div class="d-flex justify-content-center">
						{{ post?.Author?.UserName }}
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script setup lang="ts">
	import { useRouter } from 'vue-router'
	import { IPost } from '../Intefaces'
	import TagChip from './TagChip.vue'
	import { ITag } from '../Intefaces/ITag'

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
