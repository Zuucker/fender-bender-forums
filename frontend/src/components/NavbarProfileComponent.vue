<template>
	<div class="col d-flex justify-content-between align-items-center">
		<div class="sort-component col-6">
			<v-select
				label=""
				:items="sortOptions"
				variant="outlined"
				density="compact"
				bg-color="white"
				hide-details="auto"
				v-model="selectedSort"
				class="small-select"></v-select>
		</div>
		<div class="profile-button me-4">
			<v-btn variant="outlined" density="compact"> Login </v-btn>
		</div>
	</div>
</template>

<script setup lang="ts">
	const sortOptions = ['Sort by new', 'Sort by price']
	const selectedSort = sortOptions[0]
</script>

<style>
	.v-field__input,
	.v-select__selection {
		height: var(--nav-act-height) !important;
		padding: 0 !important; /* here this*/
	}

	.v-field__append-inner {
		height: var(--nav-act-height) !important;
		display: flex;
		align-items: center;
	}

	.v-select__selection,
	.v-field__input {
		min-height: var(--nav-act-height) !important;
		font-size: 20px !important;
	}

	.v-select__selection-text {
		position: relative;
		left: 20%;
	}

	.profile-button {
		& > button {
			min-height: var(--nav-act-height);
			background-color: var(--white);
		}
	}

	.v-btn--variant-outlined:active {
		border: 2px solid black;
		border-radius: 4px;
	}
</style>
