<template>
	<div className="comment-toolbar col d-flex justify-content-between">
		<div className="d-flex w-100 py-1">
			<v-btn
				:class="`text-dark ${
					comment.UpVoted ? 'bg-secondary-subtle' : 'bg-white'
				}`"
				size="30"
				@click="handleInteraction(!comment.UpVoted, false)"
				:disabled="disableInteraction">
				<font-awesome-icon :icon="['fas', 'arrow-up']" style="font-size: 1em"
			/></v-btn>
			<div class="mx-1 my-auto">
				{{ comment.Points }}
			</div>
			<v-btn
				:class="`text-dark me-2 ${
					comment.DownVoted ? 'bg-secondary-subtle' : 'bg-white'
				}`"
				size="30"
				@click="handleInteraction(false, !comment.DownVoted)"
				:disabled="disableInteraction">
				<font-awesome-icon :icon="['fas', 'arrow-down']" style="font-size: 1em"
			/></v-btn>
			<v-text-field
				class="col me-2 text-dark"
				label="Reply"
				variant="outlined"
				v-model="newComment"
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
</template>

<script setup lang="ts">
	import { ref } from 'vue'
	import { IComment } from '../Intefaces'
	import { AddComment, InteractWithComment } from '../setup/Endpoints'
	import { useSnackBarStore } from '../setup/stores/SnackBarStore'
	import { useUserStore } from '../setup/stores/UserStore'
	import { ILike } from '../Intefaces/Models/ILike'

	const { data: comment, addCommentCallback } = defineProps<{
		data: IComment
		addCommentCallback: (comment: IComment) => void
	}>()

	const snackBarStore = useSnackBarStore()
	const userstore = useUserStore()
	const newComment = ref<string>('')
	const disableInteraction = ref<boolean>(false)

	const handleAddComment = async () => {
		if (newComment.value === '') {
			snackBarStore.addMessage({
				text: 'Cannot add empty comment!',
				color: 'warning',
				timeout: 1000,
			})
			return
		}

		const commentData = {
			Content: newComment.value,
			ParentId: comment.CommentId,
			AuthorId: userstore.getUser.value.Id,
		} as IComment

		await AddComment(commentData)
			.then((commentResponse) => {
				if (commentResponse) {
					newComment.value = ''

					snackBarStore.addMessage({
						text: 'Successfully added a new comment!',
						color: 'success',
						timeout: 1000,
					})

					addCommentCallback(commentResponse)
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

	const handleInteraction = async (up: boolean, down: boolean) => {
		disableInteraction.value = true

		const interactData = {
			CommentId: comment.CommentId,
			AuthorId: userstore.getUser.value.Id,
			Upvoted: up,
			DownVoted: down,
		} as ILike

		await InteractWithComment(interactData)
			.then(() => {
				snackBarStore.addMessage({
					text: `Successfully ${up ? 'upvoted' : 'downvoted'} post!`,
					color: 'success',
					timeout: 1000,
				})

				const pointsToAdd =
					comment.UpVoted && down
						? -2
						: comment.DownVoted && up
						? 2
						: !comment.UpVoted && up
						? 1
						: !comment.DownVoted && down
						? -1
						: comment.DownVoted && !down
						? 1
						: comment.UpVoted && !up
						? -1
						: 0

				comment.UpVoted = up
				comment.DownVoted = down

				comment.Points += pointsToAdd
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
