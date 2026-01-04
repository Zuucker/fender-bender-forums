<template>
	<div className="d-flex w-100 px-2 py-1">
		<div
			v-if="displayComment && comment"
			className="w-100 d-flex"
			style="
				border-bottom: solid 1px black;
				border-left: solid 2px black;
				border-top-left-radius: 0.2rem;
				border-bottom-left-radius: 0.2rem;
				cursor: pointer;
			"
			@click="handleToggleComment">
			<div class="col ms-2" style="cursor: auto">
				<div className="username">
					<img
						class="col-1 round-image m-1 bg-dark"
						:src="comment.Author.Avatar"
						style="height: 20px; width: 20px" />
					<router-link
						:to="`/user/${comment.Author.Id}`"
						style="
							text-decoration: none;
							color: black;
							font-weight: 600;
							font-size: smaller;
						"
						>{{ comment.Author.UserName }}</router-link
					>
				</div>
				<div className="">{{ comment.Content }}</div>
				<CommentToolbarComponent
					v-if="userstore.getUser.value"
					:data="comment"
					:addCommentCallback="addCommentCallback" />
				<div className="sub-comment" style="margin-left: '20px'">
					<CommentComponent
						v-if="comment.SubComments"
						v-for="c in comment.SubComments"
						:key="c.CommentId"
						:data="c"
						:addCommentCallback="addCommentCallback" />
				</div>
			</div>
		</div>

		<div
			v-if="!displayComment"
			class="ms-2"
			@click="toggleComment"
			style="cursor: pointer">
			Show comment
		</div>
	</div>
</template>

<script setup lang="ts">
	import { ref } from 'vue'
	import { IComment } from '../Intefaces'
	import CommentToolbarComponent from './CommentToolbarComponent.vue'
	import { useUserStore } from '../setup/stores/UserStore'

	const { data: comment, addCommentCallback } = defineProps<{
		data: IComment
		addCommentCallback: (comment: IComment) => void
	}>()

	const userstore = useUserStore()

	const displayComment = ref<boolean>(true)

	const handleToggleComment = (e: MouseEvent) => {
		const rect = (e.currentTarget as HTMLElement).getBoundingClientRect()

		const x = e.clientX - rect.left

		if (x < 10) {
			displayComment.value = !displayComment.value
		}
		return
	}

	const toggleComment = () => {
		displayComment.value = !displayComment.value
	}
</script>
