<script setup lang="ts">
	import axios from 'axios'
	import { onMounted, ref } from 'vue'
	import { ICar, ICity } from '../Intefaces'

	const cars = ref(<ICar[]>[])
	const cities = ref(<ICity[]>[])

	const fetchCars = async () => {
		try {
			const response = await axios.get('https://localhost:7151/api/car/get')
			cars.value = response.data
			console.log(response.data)
		} catch (error) {
			console.error('Error fetching data:', error)
		}
	}

	const fetchCities = async () => {
		try {
			const response = await axios.get('https://localhost:7151/api/city/get')
			cities.value = response.data
			console.log(response.data)
		} catch (error) {
			console.error('Error fetching data:', error)
		}
	}

	onMounted(() => {
		fetchCars()
		fetchCities()
	})
</script>

<template>
	<div>
		<h1>CARS</h1>
		<ul>
			<li v-for="car in cars.slice(0, 20)" :key="car.Id">
				<strong>{{ car.Manufacturer }}</strong> - {{ car.Model }}
			</li>
		</ul>

		<h1>CITIES</h1>
		<ul>
			<li v-for="city in cities.slice(0, 20)" :key="city.Id">
				<strong>{{ city.Name }}</strong> - {{ city.Altitude }} ,
				{{ city.Latitude }}
			</li>
		</ul>
	</div>
</template>

<style scoped></style>
