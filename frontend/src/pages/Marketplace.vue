<template>
	<div class="d-flex flex-column justify-content-center align-items-center">
		<OfferItemComponent v-for="o in offers" :key="o.OfferId" :data="o" />
	</div>
</template>

<script setup lang="ts">
	import OfferItemComponent from '../components/OfferItemComponent.vue'
	import {
		ICar,
		ICity,
		IComment,
		IOffer,
		IOfferRate,
		IUser,
	} from '../Intefaces'

	const exampleOffer = {
		Title: 'title1',
		Points: 23,
		Ratings: [] as IOfferRate[],
		Author: {
			Id: 'id1',
			UserName: 'user1',
			Email: 'user1@gmail.com',
		} as IUser,
		CarId: 1,
		Car: {} as ICar,
		CityId: 1,
		City: {} as ICity,
		Color: 'red',
		Condition: 'ok',
		Date: new Date(),
		AuthorId: '1',
		Fuel: 'Diesel',
		Mileage: 23,
		Price: 41,
		Tags: 'sa,dsa,fa,gasa',
		OfferId: '1',
		Comments: [{} as IComment, {} as IComment],
		Contents: [],
		UpVoted: false,
		DownVoted: true,
		Type: 'car',
	} as IOffer

	const offers = [exampleOffer, exampleOffer, exampleOffer] as IOffer[]
</script>
