<script setup lang="ts">
import axios from 'axios';
import { onMounted, ref } from 'vue';

interface ICar {
	id: number;
	model: string;
	manufacturer: string;
}

interface ICity {
	id: number;
	name: string;
	altitude: string;
	latitude: string;
}

const cars = ref(<ICar[]>[]);
const cities = ref(<ICity[]>[]);

const fetchCars = async () => {
	try {
		const response = await axios.get('https://localhost:7151/api/car/get');
		cars.value = response.data;
		console.log(response.data);
	} catch (error) {
		console.error('Error fetching data:', error);
	}
};

const fetchCities = async () => {
	try {
		const response = await axios.get('https://localhost:7151/api/city/get');
		cities.value = response.data;
		console.log(response.data);
	} catch (error) {
		console.error('Error fetching data:', error);
	}
};

onMounted(() => {
	fetchCars();
	fetchCities();
});
</script>

<template>
	<div>
		<h1>CARS</h1>
		<ul>
			<li v-for="car in cars.slice(0, 20)" :key="car.id">
				<strong>{{ car.manufacturer }}</strong> - {{ car.model }}
			</li>
		</ul>

		<h1>CITIES</h1>
		<ul>
			<li v-for="city in cities.slice(0, 20)" :key="city.id">
				<strong>{{ city.name }}</strong> - {{ city.altitude }} ,
				{{ city.latitude }}
			</li>
		</ul>
	</div>
</template>

<style scoped></style>
