<script setup lang="ts">
	import NavbarProfileComponent from '../components/NavbarProfileComponent.vue'
	import SearchComponent from '../components/SearchComponent.vue'
</script>

<template>
	<div class="layout">
		<nav class="navbar d-flex pt-0 bg-blazy" style="height: 10dvh">
			<div class="logo d-flex justify-content-center col-3">
				<img src="../assets//logo.png" />
			</div>
			<div class="search-bar d-flex justify-content-center col-6">
				<SearchComponent />
			</div>
			<div class="navbar-actions col-3">
				<NavbarProfileComponent />
			</div>
		</nav>
		<div class="mid-section d-flex">
			<div
				class="side-menu d-flex flex-column align-items-center col-3 bg-blight"
				style="height: 90dvh">
				<div class="side-menu-select col-10">car type</div>
				<div class="side-menu-select col-10">history</div>
				<div class="side-menu-select col-10">DIY</div>
				<div class="side-menu-select col-10">MarketPlace</div>
				<div class="side-menu-select col-10">Race results</div>
			</div>
			<div class="content col-9 bg-blight" style="height: 90dvh">
				<RouterView />
			</div>
		</div>
	</div>
</template>

<style>
	.logo {
		height: inherit;
		padding: 0.2rem;
		padding-top: 0px;
	}
</style>
