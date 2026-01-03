<template>
	<div class="d-flex flex-column justify-content-center align-items-center">
		<div class="col-12 flex-fill overflow-auto">
			<VirtualList
				:key="posts.length"
				:dataList="posts"
				:component="PostItemComponent"
				:fetch-callback="fetchPosts"
				empty-message="No Posts found :(" />
		</div>
	</div>
</template>

<script setup lang="ts">
	import { inject, onMounted, ref, watch } from 'vue'
	import { IPost } from '../Intefaces'
	import { GetFilteredPosts } from '../setup/Endpoints'
	import { useSnackBarStore } from '../setup/stores/SnackBarStore'
	import VirtualList from '../components/VirtualList.vue'
	import { FiltersContextKey } from '../scripts/FiltersContextKey'
	import { IFilter } from '../Intefaces/IFilter'
	import PostItemComponent from '../components/PostItemComponent.vue'
	import { bool, boolean } from 'yup'

	const snackBarStore = useSnackBarStore()

	const posts = ref<IPost[]>([])
	const nextCursor = ref<string | null>(null)
	const fetchedAll = ref<boolean>(false)

	const filters = inject(FiltersContextKey)!
	const isMounted = ref(false)

	onMounted(async () => {
		isMounted.value = true
		nextCursor.value = null
		await fetchPosts(false)
	})

	watch(
		() => filters.value,
		() => {
			if (isMounted.value) {
				posts.value = []
				fetchedAll.value = false

				fetchPosts()
			}
		}
	)

	const fetchPosts = async (useCursor?: boolean) => {
		if (fetchedAll.value) return

		const newFilters = {
			...filters.value,
			SortBy: 'asc',
			OrderBy: 'CreatedAt',
		} as IFilter

		await GetFilteredPosts(newFilters, useCursor ? nextCursor.value : null)
			.then((postResponse) => {
				if (postResponse) {
					posts.value.push(...postResponse.Posts)
					nextCursor.value = postResponse.NextCursor
					if (!postResponse.NextCursor) fetchedAll.value = true
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
</script>
