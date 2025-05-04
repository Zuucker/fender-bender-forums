<template>
	<div class="d-flex flex-column justify-content-center align-items-center">
		<OfferItemComponent v-for="o in offers" :key="o.OfferId" :offer="o" />
	</div>
</template>

<script setup lang="ts">
	import OfferItemComponent from '../components/OfferItemComponent.vue'
	import {
		ICar,
		ICity,
		IContent,
		IOffer,
		IOfferRate,
		IUser,
	} from '../Intefaces'

	const exampleOffer = {
		Title: 'title1',
		Rating: 23,
		Ratings: [] as IOfferRate[],
		Author: {
			Id: 'id1',
			Username: 'user1',
			FirstName: 'name',
			LastName: 'nastname',
		} as IUser,
		CarId: 1,
		Car: {} as ICar,
		CityId: 1,
		City: {} as ICity,
		Color: 'red',
		Condition: 1,
		Date: new Date(),
		AuthorId: '1',
		Contents: {} as IContent,
		Fuel: 1,
		Mileage: 23,
		Price: 41,
		RatingCount: 51,
		Tags: 'sa,dsa,fa,gasa',
		OfferId: '1',
		Comments: [{}, {}],
	}

	const offers = [exampleOffer, exampleOffer, exampleOffer] as IOffer[]
</script>
