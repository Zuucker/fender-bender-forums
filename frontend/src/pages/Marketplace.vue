<template>
	<div class="d-flex flex-column justify-content-center align-items-center">
		<div class="col-12 flex-fill overflow-auto">
			<VirtualList
				:key="offers.length"
				:dataList="offers"
				:component="OfferItemComponent"
				:fetch-callback="fetchOffers"
				empty-message="No Posts found :(" />
		</div>
	</div>
</template>

<script setup lang="ts">
	import { inject, onMounted, ref, watch } from 'vue'
	import OfferItemComponent from '../components/OfferItemComponent.vue'
	import { IOffer } from '../Intefaces'
	import { GetFilteredOffers } from '../setup/Endpoints'
	import { useSnackBarStore } from '../setup/stores/SnackBarStore'
	import VirtualList from '../components/VirtualList.vue'
	import { FiltersContextKey } from '../scripts/FiltersContextKey'
	import { IFilter } from '../Intefaces/IFilter'

	const snackBarStore = useSnackBarStore()

	const offers = ref<IOffer[]>([])
	const nextCursor = ref<string | null>(null)
	const fetchedAll = ref<boolean>(false)

	const filters = inject(FiltersContextKey)!
	const isMounted = ref(false)

	onMounted(async () => {
		isMounted.value = true
		nextCursor.value = null
		await fetchOffers(false)
	})

	watch(
		() => filters.value,
		() => {
			if (isMounted.value) {
				offers.value = []
				fetchedAll.value = false

				fetchOffers()
			}
		}
	)

	const fetchOffers = async (useCursor?: boolean) => {
		if (fetchedAll.value) return

		const newFilters = {
			...filters.value,
			SortBy: 'asc',
			OrderBy: 'CreatedAt',
		} as IFilter

		await GetFilteredOffers(newFilters, useCursor ? nextCursor.value : null)
			.then((offerResponse) => {
				if (offerResponse) {
					offers.value.push(...offerResponse.Offers)
					nextCursor.value = offerResponse.NextCursor
					if (!offerResponse.NextCursor) fetchedAll.value = true
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
