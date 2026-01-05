<template>
	<div class="offer-item d-flex col-12">
		<div
			class="offer-left-part d-flex flex-column justify-content-between col-10"
			style="cursor: pointer"
			@click="handleClick">
			<h4>
				<b>{{ offer?.Title }}</b>
			</h4>
			<div class="d-flex">
				<TagChip
					v-for="tag in [
 						{Text:offer.Car?.Model, Color:'#6420FF'} as ITag,
						{Text:offer.Car?.Type, Color:'#9B30FF'} as ITag,
						...offer.Tags,
					].filter((f) => f)"
					:tag="tag" />
				<h4 class="ms-auto me-4">{{ offer.Price }} $</h4>
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
							{{ offer?.Points }}
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
						offer?.NumberOfComments ?? 0
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
						:src="offer?.Author?.Avatar ?? 'https://placehold.co/50x50.png'"
						style="height: inherit" />
				</div>

				<div class="d-flex justify-content-center">
					{{ offer?.Author?.UserName }}
				</div>
			</div>
		</div>
	</div>
</template>

<script setup lang="ts">
	import { useRouter } from 'vue-router'
	import { IOffer } from '../Intefaces'
	import TagChip from './TagChip.vue'
	import { ITag } from '../Intefaces/ITag'

	const { data: offer } = defineProps<{
		data: IOffer
	}>()

	const router = useRouter()

	const handleClick = () => {
		router.push(`/offer/${offer.OfferId}`)
	}
</script>
