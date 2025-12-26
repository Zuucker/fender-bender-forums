<template>
	<v-list v-model:opened="isOpen" class="col-10 py-4 bg-blight">
		<template v-for="section in sections" :key="section.SectionId">
			<v-list-group :value="section.Name" class="side-menu-main-item mb-3">
				<template v-slot:activator="{ props }">
					<v-list-item
						v-bind="props"
						:title="section.Name"
						density="compact"
						:onclick="() => handleMenuSectionClick(section.Url)"></v-list-item>
				</template>

				<v-list-item
					v-for="sub in section.SubSections"
					:key="sub.SectionId"
					:title="sub.Name"
					:value="sub.Name"
					class="side-menu-item"
					density="compact"></v-list-item>
			</v-list-group>
		</template>
	</v-list>
	<LoaderComponent
		:data="sections"
		:loading="isLoading"
		notLoadedText="Sections not found :(" />
</template>

<script setup lang="ts">
	import { onMounted, ref, watch } from 'vue'
	import { ISection } from '../Intefaces'
	import { GetMenuSections } from '../setup/Endpoints'
	import LoaderComponent from './LoaderComponent.vue'
	import { useRoute } from 'vue-router'
	import { goToPage } from '../setup/Router'

	const isOpen = ref([])
	const sections = ref<ISection[]>([])
	const isLoading = ref(true)

	const retrivedSections = ref<ISection[]>([])
	const route = useRoute()

	watch([() => route.fullPath, retrivedSections], ([newPath, newSort]) => {
		var allSections = []
		if (newPath === '/forum') {
			allSections = [
				...retrivedSections.value,
				{
					Name: 'Marketplace',
					Description: 'Find your dream car!',
					Url: '/',
				} as ISection,
			]
		} else {
			allSections = [
				...retrivedSections.value,
				{
					Name: 'Forums',
					Description: 'Share your car with others!',
					Url: '/forum',
				} as ISection,
			]
		}

		sections.value = allSections
	})

	onMounted(async () => {
		await GetMenuSections()
			.then((response) => {
				retrivedSections.value = response
				isLoading.value = false
			})
			.catch((error) => {
				console.log(error)
				isLoading.value = false
			})
	})

	const handleMenuSectionClick = (url?: string) => {
		if (url) {
			goToPage(url)
		}
	}
</script>

<style scoped>
	.side-menu-item {
		background-color: var(--white);
		border-width: 1px 0px 0px 0px;
		border-color: var(--black);
		&:hover > span {
			opacity: 0.1;
		}
	}
	.side-menu-main-item {
		background-color: var(--white);
		border: 2px solid var(--black);
	}
</style>
