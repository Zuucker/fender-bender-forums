<template>
	<div ref="sideMenuRef" class="col-10">
		<div class="col-12 mt-4 mb-5">
			<v-btn
				class="col-12 thicc-border py-0 px-10 m-0"
				color="white"
				style="border-radius: 0px"
				:onclick="
					() =>
						route.fullPath === '/forum' ? goToPage('/') : goToPage('/forum')
				">
				{{ route.fullPath === '/forum' ? 'Marketplace' : 'Forum' }}
			</v-btn>
		</div>
		<div class="col-12">
			<v-text-field
				label="Title"
				variant="outlined"
				v-model="filters.Title"
				:flat="true" />
		</div>
		<div v-if="route.fullPath === '/'" class="col-12">
			<v-switch
				:model-value="filters.Type === 'car'"
				:label="`${
					filters.Type[0].toUpperCase() + filters.Type.slice(1)
				} offer`"
				color="blazy"
				:onchange="
					(e) => {
						filters.Type = e.target.checked ? 'car' : 'part'

						if (e.target.checked) {
							filters.PartNumber = null
							filters.Fuel = FuelType[FuelType.Gasoline]
							selectedFuel = FuelType.Gasoline
						} else {
							filters.Fuel = null
							filters.Mileage = null
							filters.Color = null
							mileageString = ''
						}
					}
				"
				density="compact"
				min-heigh="20px"
				inset></v-switch>
		</div>

		<div
			v-if="route.fullPath === '/forum' || route.fullPath.includes('post')"
			class="col-12">
			<v-autocomplete
				class="col-12"
				label="Select Section"
				:items="sections"
				item-title="Name"
				item-value="SectionId"
				return-object
				density="compact"
				variant="outlined"
				v-model:opened="isSectionSelectOpen"
				v-model="selectedSection"
				@update:model-value="handleChangeSection"
				:menu-props="{ maxHeight: 200 }">
				<template #item="{ item, props }">
					<div v-bind="props" class="select-item px-4">
						{{ item.title }}
					</div>
				</template>
			</v-autocomplete>
		</div>

		<div class="col-12">
			<v-autocomplete
				class="col-12"
				:key="sideMenuRef?.getBoundingClientRect().width"
				label="Select Car"
				:items="cars"
				:item-title="getCarName"
				return-object
				density="compact"
				variant="outlined"
				v-model:opened="isCarSelectOpen"
				v-model="selectedCar"
				@update:model-value="handleChangeCar"
				:menu-props="{
					maxHeight: 200,
					maxWidth: sideMenuRef?.getBoundingClientRect().width,
				}">
				<template #item="{ item, props }">
					<div
						v-bind="props"
						class="select-item px-4"
						style="white-space: normal; word-wrap: break-word">
						{{ item.title }}
					</div>
				</template>
			</v-autocomplete>
		</div>

		<div v-if="route.fullPath === '/'" class="col-12">
			<v-autocomplete
				class="col-12"
				:key="sideMenuRef?.getBoundingClientRect().width"
				label="Select City"
				:items="cities"
				item-title="Name"
				item-value="Id"
				return-object
				density="compact"
				variant="outlined"
				v-model:opened="isCitySelectOpen"
				v-model="selectedCity"
				@update:model-value="handleChangeCity"
				:menu-props="{
					maxHeight: 200,
					maxWidth: sideMenuRef?.getBoundingClientRect().width,
				}">
				<template #item="{ item, props }">
					<div v-bind="props" class="select-item px-4">
						{{ item.title }}
					</div>
				</template>
			</v-autocomplete>
		</div>

		<div v-if="filters.Type === 'car' && route.fullPath === '/'" class="col-12">
			<v-select
				class="col-12"
				label="Select Fuel Type"
				:items="Object.keys(FuelType).filter((c) => isNaN(Number(c)))"
				:item-value="c => FuelType[c as keyof typeof FuelType]"
				:item-title="(c) => c"
				density="compact"
				variant="outlined"
				v-model:opened="isFuelSelectOpen"
				v-model="selectedFuel"
				@update:model-value="handleChangeFuel">
				<template #item="{ item, props }">
					<div v-bind="props" class="select-item px-4">
						{{ item.title }}
					</div>
				</template>
			</v-select>
		</div>

		<div v-if="route.fullPath === '/'" class="col-12">
			<v-select
				class="col-12"
				label="Select Condition"
				:items="Object.keys(Condition).filter((c) => isNaN(Number(c)))"
				:item-value="c => Condition[c as keyof typeof Condition]"
				:item-title="c => ConditionDisplay[c as keyof typeof Condition]"
				density="compact"
				variant="outlined"
				v-model:opened="isConditionSelectOpen"
				v-model="selectedCondition"
				@update:model-value="handleChangeCondition">
				<template #item="{ item, props }">
					<div v-bind="props" class="select-item px-4" style="width: inherit">
						{{ item.title }}
					</div>
				</template>
			</v-select>
		</div>

		<div v-if="filters.Type === 'car' && route.fullPath === '/'" class="col-12">
			<v-text-field
				label="Color"
				variant="outlined"
				v-model="filters.Color"
				:flat="true" />
		</div>

		<div v-if="filters.Type === 'car' && route.fullPath === '/'" class="col-12">
			<v-text-field
				label="Max mileage"
				variant="outlined"
				v-model="mileageString"
				:key="mileageFieldKey" />
		</div>

		<div v-if="route.fullPath === '/'" class="col-12">
			<v-text-field
				label="Min price"
				variant="outlined"
				v-model="minPriceString"
				:key="minPriceFieldKey" />
		</div>

		<div v-if="route.fullPath === '/'" class="col-12">
			<v-text-field
				label="Max price"
				variant="outlined"
				v-model="maxPriceString"
				:key="maxPriceFieldKey" />
		</div>

		<div
			v-if="filters.Type === 'part' && route.fullPath === '/'"
			class="col-12">
			<v-text-field
				label="PartNumber"
				variant="outlined"
				v-model="filters.PartNumber" />
		</div>

		<div class="col-12">
			<v-text-field label="Tags" variant="outlined" v-model="filters.Tags" />
		</div>

		<div
			v-if="
				!(route.fullPath.includes('offer') || route.fullPath.includes('post'))
			"
			class="col-12 mb-3">
			<v-btn
				class="col-12 thicc-border py-0 px-10 m-0"
				color="white"
				style="border-radius: 0px"
				:onclick="clearFilters">
				Clear
			</v-btn>
		</div>

		<div
			v-if="
				!(route.fullPath.includes('offer') || route.fullPath.includes('post'))
			"
			class="col-12 mb-4">
			<v-btn
				class="col-12 thicc-border py-0 px-10 m-0"
				color="white"
				style="border-radius: 0px"
				:onclick="applyFilter">
				Apply
			</v-btn>
		</div>

		<div
			v-if="route.fullPath.includes('offer') || route.fullPath.includes('post')"
			class="col-12 mb-4">
			<v-btn
				class="col-12 thicc-border py-0 px-10 m-0"
				color="white"
				style="border-radius: 0px"
				:onclick="
					() =>
						route.fullPath.includes('post') ? goToPage('/forum') : goToPage('/')
				">
				Back
			</v-btn>
		</div>
	</div>
</template>

<script setup lang="ts">
	import { inject, onMounted, ref, watch } from 'vue'
	import { ICar, ICity, ISection } from '../Intefaces'
	import { GetCars, GetCities, GetMenuSections } from '../setup/Endpoints'
	import { useRoute } from 'vue-router'
	import { goToPage } from '../setup/Router'
	import { FuelType } from '../constants/FuelEnum'
	import { Condition, ConditionDisplay } from '../constants/ConditionEnum'
	import { IFilter } from '../Intefaces/IFilter'
	import { FiltersContextKey } from '../scripts/FiltersContextKey'

	const isOpen = ref([])
	const isLoading = ref(true)

	const route = useRoute()

	const filters = ref<IFilter>({ Type: 'car' } as IFilter)

	const cars = ref<ICar[]>([])
	const cities = ref<ICity[]>([])
	const sections = ref<ISection[]>([])
	const isCarSelectOpen = ref([])
	const isCitySelectOpen = ref([])
	const isConditionSelectOpen = ref([])
	const isFuelSelectOpen = ref([])
	const isSectionSelectOpen = ref([])
	const selectedCar = ref<ICar | null>(null)
	const selectedCity = ref<ICity | null>(null)
	const selectedCondition = ref<Condition | null>(null)
	const selectedFuel = ref<FuelType | null>(null)
	const selectedSection = ref<ISection | null>()
	const minPriceString = ref<string>('')
	const minPriceFieldKey = ref<number>(0)
	const maxPriceString = ref<string>('')
	const maxPriceFieldKey = ref<number>(0)
	const mileageString = ref<string>('')
	const mileageFieldKey = ref<number>(0)

	const sideMenuRef = ref<HTMLDivElement | null>(null)

	const filtersCtx = inject(FiltersContextKey)!

	watch(minPriceString, () => {
		const sanitized = minPriceString.value.replace(/[^0-9.]/g, '')

		if (sanitized) {
			filters.value.MinPrice = Number(sanitized)
			minPriceString.value = sanitized
		} else {
			minPriceString.value = ''
			filters.value.MinPrice = null
			minPriceFieldKey.value++
		}
	})

	watch(maxPriceString, () => {
		const sanitized = maxPriceString.value.replace(/[^0-9.]/g, '')

		if (sanitized) {
			filters.value.MaxPrice = Number(sanitized)
			maxPriceString.value = sanitized
		} else {
			maxPriceString.value = ''
			filters.value.MaxPrice = null
			maxPriceFieldKey.value++
		}
	})

	watch(mileageString, () => {
		const sanitized = mileageString.value.replace(/[^0-9.]/g, '')

		if (sanitized) {
			filters.value.Mileage = Number(sanitized)
			mileageString.value = sanitized
		} else {
			mileageString.value = ''
			filters.value.Mileage = null
			mileageFieldKey.value++
		}
	})

	watch(
		() => route.fullPath,
		(newPath, oldPath) => {
			if (
				(newPath === '/forum' && oldPath === '/') ||
				(newPath === '/' && oldPath === '/forum')
			)
				clearFilters()
		}
	)
	onMounted(async () => {
		await GetMenuSections()
			.then((response) => {
				sections.value = response
				isLoading.value = false
			})
			.catch((error) => {
				console.log(error)
				isLoading.value = false
			})

		await GetCars('', 10)
			.then((response) => {
				cars.value = response
			})
			.catch((error) => {
				console.log(error)
			})

		await GetCities()
			.then((response) => {
				cities.value = response
			})
			.catch((error) => {
				console.log(error)
			})
	})

	const handleMenuSectionClick = (section?: ISection) => {
		if (section.Url) {
			goToPage(section.Url)
		}

		if (section?.SectionId) {
			isOpen.value = []
			filters.value.SectionId = section.SectionId
		}
	}

	const getCarName = (car: ICar) => {
		return `${car.Manufacturer} ${car.Model} ${car.Type} ${new Date(
			car.Year
		).getUTCFullYear()}`
	}

	const handleChangeCar = (car: ICar) => {
		selectedCar.value = car

		filters.value.CarId = car.CarId

		isCarSelectOpen.value = []
	}

	const handleChangeCity = (city: ICity) => {
		selectedCity.value = city

		filters.value.CityId = city.Id

		isCitySelectOpen.value = []
	}

	const handleChangeCondition = (condition: Condition) => {
		selectedCondition.value = condition

		filters.value.Condition = Condition[condition]

		isConditionSelectOpen.value = []
	}

	const handleChangeFuel = (fuel: FuelType) => {
		selectedFuel.value = fuel

		filters.value.Fuel = FuelType[fuel]

		isFuelSelectOpen.value = []
	}

	const handleChangeSection = (section: ISection) => {
		selectedSection.value = section

		filters.value.SectionId = section.SectionId

		isSectionSelectOpen.value = []
	}

	const clearFilters = () => {
		filters.value = {
			Type: 'car',
		} as IFilter

		selectedCar.value = null
		selectedCity.value = null
		selectedCondition.value = null
		selectedFuel.value = null
		selectedSection.value = null

		minPriceString.value = ''
		maxPriceString.value = ''
		mileageString.value = ''

		applyFilter()
	}

	const applyFilter = () => {
		filtersCtx.value = { ...filters.value }
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
