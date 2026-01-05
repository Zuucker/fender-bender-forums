<template>
	<div v-if="postData">
		<div class="d-flex flex-column thicc-border bg-white w-100 p-2">
			<div class="d-flex col-12 justify-content-between">
				<h2 class="d-flex align-items-end mb-0">{{ postData.Title }}</h2>
				<div class="fs-6 d-flex align-items-center">
					{{ postData.Author.UserName }}
				</div>
			</div>
			<div class="d-flex col-12">
				<TagChip
					v-for="tag in [
						{Text:postData.Car?.Manufacturer, Color:'#007BFF'} as ITag,
						{Text:postData.Car?.Model, Color:'#6420FF'} as ITag,
						{Text:postData.Car?.Type, Color:'#9B30FF'} as ITag,
						...postData.Tags,
					].filter((f) => f)"
					:tag="tag" />
			</div>
			<div
				class="d-flex flex-column h-100 col-12 justify-content-between"
				style="overflow-y: auto">
				<div v-for="content in postData.Contents" class="mb-2">
					<div class="col d-flex justify-content-center">
						<span>{{ content.SubTitle }}</span>
					</div>

					<v-card
						v-if="content.Type === ContentTypeEnum.Text"
						class="p-3"
						variant="outlined"
						>{{ content.TextContent }}</v-card
					>
					<v-card
						v-if="content.Type === ContentTypeEnum.Gallery"
						variant="tonal">
						<v-carousel show-arrows="hover" hide-delimiters height="300px">
							<v-carousel-item
								v-for="galleryElement in content.GalleryElements"
								:src="galleryElement.Path"></v-carousel-item>
						</v-carousel>
					</v-card>
				</div>
			</div>
		</div>

		<div
			v-if="userstore.getUser.value"
			class="d-flex flex-column thicc-border bg-white w-100 py-1">
			<div class="d-flex justify-content-between align-items-center">
				<img
					class="col-1 round-image m-1 me-2 bg-dark"
					:src="
						userstore.getUser.value.Avatar ?? 'https://placehold.co/50x50.png'
					"
					style="height: 30px; width: 30px" />

				<v-btn
					:class="`text-dark ${
						postData.UpVoted ? 'bg-secondary-subtle' : 'bg-white'
					}`"
					size="30"
					@click="handleInteraction(!postData.UpVoted, false)"
					:disabled="disableInteraction">
					<font-awesome-icon :icon="['fas', 'arrow-up']" style="font-size: 1em"
				/></v-btn>
				<div class="mx-1">
					{{ postData.Points }}
				</div>
				<v-btn
					:class="`text-dark me-2 ${
						postData.DownVoted ? 'bg-secondary-subtle' : 'bg-white'
					}`"
					size="30"
					@click="handleInteraction(false, !postData.DownVoted)"
					:disabled="disableInteraction">
					<font-awesome-icon
						:icon="['fas', 'arrow-down']"
						style="font-size: 1em"
				/></v-btn>

				<v-text-field
					class="col me-2 text-dark"
					label="New comment"
					variant="outlined"
					v-model="comment"
					hide-details
					@keyup.enter="handleAddComment" />
				<v-btn
					class="text-dark me-2"
					maxHeight="30"
					maxWidth="50"
					@click="handleAddComment"
					>Enter</v-btn
				>
			</div>
		</div>
		<div class="d-flex flex-column thicc-border bg-white w-100">
			<div class="d-flex col-12 justify-content-between">
				<div
					className="col d-collumn justify-content-center align-items-center">
					<CommentComponent
						v-if="postData.Comments"
						v-for="comment in postData.Comments"
						:key="comment.CommentId"
						:data="comment"
						:addCommentCallback="handleAddSubComment" />

					<div
						v-if="!postData.Comments || postData.Comments.length === 0"
						class="ps-2">
						There are no comments yet
					</div>
				</div>
			</div>
		</div>
	</div>
	<div
		v-if="!postData"
		class="d-flex h-75 flex-collumn align-items-center justify-content-center">
		<PulseLoader :loading="true" :color="'var(--blazy)'" :size="'30px'" />
	</div>
</template>

<script setup lang="ts">
	import { useRoute } from 'vue-router'
	import { ContentTypeEnum } from '../constants/ContentTypeEnum'
	import { IComment, IPost } from '../Intefaces'
	import { onMounted, ref } from 'vue'
	import { AddComment, GetPost, InteractWithPost } from '../setup/Endpoints'
	import { useSnackBarStore } from '../setup/stores/SnackBarStore'
	import { PulseLoader } from 'vue-spinner/dist/vue-spinner.min.js'
	import TagChip from '../components/TagChip.vue'
	import { useUserStore } from '../setup/stores/UserStore'
	import CommentComponent from '../components/CommentComponent.vue'
	import { ILike } from '../Intefaces/Models/ILike'
	import { ITag } from '../Intefaces/ITag'

	const route = useRoute()
	const snackBarStore = useSnackBarStore()
	const userstore = useUserStore()

	const postId = Array.isArray(route.params.id)
		? route.params.id[0]
		: route.params.id

	const postData = ref<IPost | null>(null)
	const postNotFound = ref<boolean>(false)
	const comment = ref<string>('')
	const disableInteraction = ref<boolean>(false)

	onMounted(async () => {
		await GetPost(postId)
			.then((postResponse) => {
				if (postResponse) {
					postData.value = postResponse
				}
			})
			.catch((error) => {
				console.log(error)

				if (error.response?.status === 404) {
					postNotFound.value = true
					return
				}

				snackBarStore.addMessage({
					text: 'Server error! Please try again later!',
					color: 'error',
					timeout: 2000,
				})
			})
	})

	const handleAddComment = async () => {
		if (comment.value === '') {
			snackBarStore.addMessage({
				text: 'Cannot add empty comment!',
				color: 'warning',
				timeout: 1000,
			})
			return
		}

		const commentData = {
			Content: comment.value,
			PostId: postData.value.Id,
			AuthorId: userstore.getUser.value.Id,
		} as IComment

		await AddComment(commentData)
			.then((commentResponse) => {
				if (commentResponse) {
					comment.value = ''

					snackBarStore.addMessage({
						text: 'Successfully added a new comment!',
						color: 'success',
						timeout: 1000,
					})

					const newComments = [commentResponse, ...postData.value.Comments]

					postData.value = {
						...postData.value,
						Comments: newComments,
					}
				}
			})
			.catch((error) => {
				console.log(error)

				snackBarStore.addMessage({
					text: 'Server error! Please try again later!',
					color: 'error',
					timeout: 2000,
				})
			})
	}

	const addSubCommentRecursive = (
		comments: IComment[],
		newComment: IComment
	): IComment[] => {
		return comments.map((c) => {
			if (c.CommentId === newComment.ParentId) {
				return {
					...c,
					SubComments: [...c.SubComments, newComment],
				}
			}

			if (c.SubComments && c.SubComments.length > 0) {
				return {
					...c,
					SubComments: addSubCommentRecursive(c.SubComments, newComment),
				}
			}

			return c
		})
	}

	const handleAddSubComment = (comment: IComment) => {
		postData.value = {
			...postData.value,
			Comments: addSubCommentRecursive(postData.value.Comments, comment),
		}
	}

	const handleInteraction = async (up: boolean, down: boolean) => {
		disableInteraction.value = true

		const interactData = {
			PostId: postData.value.Id,
			AuthorId: userstore.getUser.value.Id,
			Upvoted: up,
			DownVoted: down,
		} as ILike

		await InteractWithPost(interactData)
			.then(() => {
				snackBarStore.addMessage({
					text: `Successfully ${up ? 'upvoted' : 'downvoted'} post!`,
					color: 'success',
					timeout: 1000,
				})

				const pointsToAdd =
					postData.value.UpVoted && down
						? -2
						: postData.value.DownVoted && up
						? 2
						: !postData.value.UpVoted && up
						? 1
						: !postData.value.DownVoted && down
						? -1
						: postData.value.DownVoted && !down
						? 1
						: postData.value.UpVoted && !up
						? -1
						: 0

				postData.value = {
					...postData.value,
					UpVoted: up,
					DownVoted: down,
					Points: postData.value.Points + pointsToAdd,
				}
			})
			.catch((error) => {
				console.log(error)

				snackBarStore.addMessage({
					text: 'Server error! Please try again later!',
					color: 'error',
					timeout: 2000,
				})
			})

		setTimeout(async () => {
			disableInteraction.value = false
		}, 2000)
	}
</script>
