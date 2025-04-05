<template>
	<v-text-field
		class="search-field col-10"
		variant="outlined"
		placeholder="Search..."
		density="compact"
		bg-color="white"
		rounded="lg"
		hide-details
		#append-inner>
	</v-text-field>
</template>

<script setup lang="ts">
	const searchText = ''
</script>

<style>
	.search-field {
		flex: 0 0 auto !important;

		& .v-field__input {
			padding-left: 10px !important;
		}
	}
</style>
