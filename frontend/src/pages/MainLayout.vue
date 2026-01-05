<template>
	<div class="layout">
		<nav class="navbar d-flex pt-0 bg-blazy" style="height: 10dvh">
			<div class="logo d-flex justify-content-center col-3">
				<img
					src="../assets//logo.png"
					class="cursor-pointer"
					@click="goToPage(`/`)" />
			</div>

			<div class="sort-component col d-flex justify-content-end me-3">
				<v-menu>
					<template v-slot:activator="{ props }">
						<v-btn
							class="col-2 thicc-border py-0 px-10 m-0"
							color="white"
							style="border-radius: 0px; height: 31px"
							v-bind="props">
							Add
						</v-btn>
					</template>
					<v-list>
						<v-list-item :key="new Date()" value="post">
							<v-list-item-title
								v-if="userStore.getUser.value"
								:onclick="() => handleAdd('offer')">
								{{ 'Offer' }}</v-list-item-title
							>
							<v-list-item-title
								v-if="userStore.getUser.value"
								:onclick="() => handleAdd('post')">
								{{ 'Post' }}</v-list-item-title
							>
							<v-list-item-title
								v-if="!userStore.getUser.value"
								:onclick="() => handleAdd('login')">
								{{ 'Login first!' }}</v-list-item-title
							>
						</v-list-item>
					</v-list>
				</v-menu>
			</div>
			<div class="navbar-actions">
				<NavbarProfileComponent ref="navbarProfileComponentRef" />
			</div>
		</nav>

		<div v-if="route.meta.layout === undefined" class="mid-section d-flex">
			<div
				class="side-menu d-flex flex-column align-items-center col-3 bg-blight"
				style="height: 90dvh; overflow-y: auto; scrollbar-width: none">
				<SideMenuComponent />
			</div>
			<div
				class="content col-9 bg-blight"
				style="height: 90dvh; overflow-y: auto; scrollbar-width: none">
				<RouterView />
			</div>
		</div>

		<div v-if="route.meta.layout === 'FullLayout'" class="mid-section d-flex">
			<div class="content p-0 col-12 bg-blight" style="height: 90dvh">
				<RouterView />
			</div>
		</div>
	</div>
</template>

<script setup lang="ts">
	import { useRoute } from 'vue-router'
	import NavbarProfileComponent from '../components/NavbarProfileComponent.vue'
	import { goToPage } from '../setup/Router'
	import SideMenuComponent from '../components/SideMenuComponent.vue'
	import { provide, ref } from 'vue'
	import { IFilter } from '../Intefaces/IFilter'
	import { FiltersContextKey } from '../scripts/FiltersContextKey'
	import { ISection } from '../Intefaces'
	import { useUserStore } from '../setup/stores/UserStore'

	const route = useRoute()
	const userStore = useUserStore()

	const filters = ref<IFilter>({
		Type: 'car',
	} as IFilter)

	const isOpen = ref([])

	const sections = ref<ISection[]>(
		userStore.getUser.value
			? ([{ Name: 'Offer' }, { Name: 'Post' }] as ISection[])
			: ([{ Name: 'Login first!' }] as ISection[])
	)

	const navbarProfileComponentRef = ref<InstanceType<
		typeof NavbarProfileComponent
	> | null>(null)

	const handleAdd = (value: string) => {
		isOpen.value = []

		if (value === 'offer') goToPage('/offer/add')
		else if (value === 'post') goToPage('/post/add')
		else if (value === 'login')
			navbarProfileComponentRef.value.handleOpenLogin()
	}

	provide(FiltersContextKey, filters)
</script>

<style>
	.logo {
		height: inherit;
		padding: 0.2rem;
		padding-top: 0px;
	}
</style>

<style lang="css" scoped>
	.sort-component {
		position: relative;
		height: 30px;
		& > .v-list {
			margin-left: auto !important;
			margin-right: 1rem !important;
			z-index: 1000;
		}
	}

	.side-menu-main-item {
		background-color: var(--white);
		border: 2px solid var(--black);
		padding-inline-start: 2px;
		margin: 0px !important;
		padding: 0px !important;
	}

	.side-menu-item {
		background-color: var(--white);
		border-width: 1px 0px 0px 0px;
		border-color: var(--black);

		&:hover > span {
			opacity: 0.1;
		}
	}

	.side-menu-item.v-list-item.v-list-item--link.v-theme--light.v-list-item--density-compact.v-list-item--one-line.v-list-item--variant-text {
		padding-inline-start: 2px !important;
		padding: auto !important;
		margin: auto !important;
		text-align: center;
	}
</style>
