<template>
	<div class="d-flex justify-content-center h-100 w-100 px-4 py-2">
		<div class="col-5 hide-scrollbar pe-4" style="overflow-y: auto">
			<div class="h-100" style="overflow-y: auto">
				<h2>Add an Offer!</h2>
				<div class="col-12">
					<v-text-field
						label="Title"
						variant="outlined"
						v-model="offerData.Title"
						:flat="true"
						:error-messages="getErrorMessage('Title')" />
				</div>
				<div class="col-12">
					<v-switch
						:model-value="offerData.Type === 'car'"
						:label="`${
							offerData.Type[0].toUpperCase() + offerData.Type.slice(1)
						} offer`"
						color="blazy"
						:onchange="
							(e) => {
								offerData.Type = e.target.checked ? 'car' : 'part'

								if (e.target.checked) {
									offerData.PartNumber = null
									offerData.Fuel = FuelType[FuelType.Gasoline]
									selectedFuel = FuelType.Gasoline
								} else {
									offerData.VIN = null
									offerData.Fuel = null
									offerData.Mileage = null
									offerData.Color = null
									mileageString = ''
								}
							}
						"
						density="compact"
						min-heigh="20px"
						inset></v-switch>
				</div>
				<div class="col-12">
					<v-autocomplete
						class="col-12"
						label="Select Car"
						:items="cars"
						:item-title="getCarName"
						return-object
						density="compact"
						variant="outlined"
						v-model:opened="isCarSelectOpen"
						v-model="selectedCar"
						@update:model-value="handleChangeCar"
						:error-messages="getErrorMessage('CarId')"
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
						:error-messages="getErrorMessage('CityId')"
						:menu-props="{ maxHeight: 200 }">
						<template #item="{ item, props }">
							<div v-bind="props" class="select-item px-4">
								{{ item.title }}
							</div>
						</template>
					</v-autocomplete>
				</div>
				<div v-if="offerData.Type === 'car'" class="col-12">
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
						@update:model-value="handleChangeFuel"
						:error-messages="getErrorMessage('Fuel')">
						<template #item="{ item, props }">
							<div v-bind="props" class="select-item px-4">
								{{ item.title }}
							</div>
						</template>
					</v-select>
				</div>
				<div class="col-12">
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
						@update:model-value="handleChangeCondition"
						:error-messages="getErrorMessage('Condition')">
						<template #item="{ item, props }">
							<div v-bind="props" class="select-item px-4">
								{{ item.title }}
							</div>
						</template>
					</v-select>
				</div>
				<div v-if="offerData.Type === 'car'" class="col-12">
					<v-text-field
						label="Color"
						variant="outlined"
						v-model="offerData.Color"
						:flat="true"
						:error-messages="getErrorMessage('Color')" />
				</div>
				<div v-if="offerData.Type === 'car'" class="col-12">
					<v-text-field
						label="Mileage"
						variant="outlined"
						v-model="mileageString"
						:key="mileageFieldKey"
						:error-messages="getErrorMessage('Mileage')" />
				</div>
				<div class="col-12">
					<v-text-field
						label="Price"
						variant="outlined"
						v-model="priceString"
						:key="priceFieldKey"
						:error-messages="getErrorMessage('Price')" />
				</div>
				<div v-if="offerData.Type === 'car'" class="col-12">
					<v-text-field
						label="VIN"
						variant="outlined"
						v-model="offerData.VIN"
						:error-messages="getErrorMessage('VIN')" />
				</div>
				<div v-if="offerData.Type === 'part'" class="col-12">
					<v-text-field
						label="PartNumber"
						variant="outlined"
						v-model="offerData.PartNumber"
						:error-messages="getErrorMessage('PartNumber')" />
				</div>
				<div class="col-12">
					<v-text-field
						label="Tags"
						variant="outlined"
						v-model="tagsString"
						:error-messages="getErrorMessage('Tags')" />
				</div>
				<div class="col-12 d-flex justify-content-center gap-5">
					<v-btn
						class="col-4 text-dark thicc-border py-0 px-10 m-0"
						density="compact"
						style="min-height: 28px; border-radius: 0px"
						@click="handleAddContent(ContentTypeEnum[ContentTypeEnum.Text])"
						>Add Text</v-btn
					><v-btn
						class="col-4 text-dark thicc-border py-0 px-10 m-0"
						density="compact"
						style="min-height: 28px; border-radius: 0px"
						@click="handleAddContent(ContentTypeEnum[ContentTypeEnum.Gallery])"
						>Add Gallery</v-btn
					>
				</div>
				<div class="col-12 d-flex justify-content-center gap-3 mt-2">
					<span>{{ `${offerData.Contents.length} blocks` }}</span>
					<span>{{
						`${
							offerData.Contents.filter((c) => c.Type === ContentTypeEnum.Text)
								.length
						} Text`
					}}</span>
					<span>{{
						`${
							offerData.Contents.filter(
								(c) => c.Type === ContentTypeEnum.Gallery
							).length
						} Gallery`
					}}</span>
				</div>
				<div class="col-12 d-flex justify-content-center gap-5 mt-4">
					<v-btn
						class="col-5 thicc-border py-0 px-10 m-0"
						color="white"
						style="border-radius: 0px"
						:onclick="handleAddOffer">
						Save
					</v-btn>
					<v-btn
						class="col-5 thicc-border py-0 px-10 m-0"
						color="white"
						style="border-radius: 0px"
						:onclick="handleCancel">
						Cancel
					</v-btn>
				</div>
			</div>
		</div>
		<div class="col-7 d-flex flex-column">
			<h2>Contents</h2>
			<v-card class="p-3 hide-scrollbar">
				<div class="h-100">
					<Draggable
						v-model="offerData.Contents"
						tag="div"
						item-key="ContentId"
						handle=".drag-handle">
						<template #item="{ element, index }">
							<Content
								:content="element"
								:remove-content="handleRemoveContent"
								:edit-content="handleEditContent"
								:errors="getErrorMessagesForContent(index)" />
						</template>
					</Draggable>
					<div
						v-if="offerData.Contents.length === 0"
						class="d-flex justify-content-center align-items-center"
						style="height: 100px">
						<span>Add some contents first!</span><br />
					</div>
				</div>
			</v-card>
			<div
				v-if="getErrorMessage('Contents')"
				class="col-12 d-flex justify-content-center mt-2">
				<span class="text-danger">{{ getErrorMessage('Contents') }}</span>
			</div>
		</div>
	</div>
</template>

<script setup lang="ts">
	import { onMounted, ref, watch } from 'vue'
	import { ICar, ICity, IContent, IOffer } from '../Intefaces'
	import Draggable from 'vuedraggable'
	import Content from '../components/Content.vue'
	import { ContentTypeEnum } from '../constants/ContentTypeEnum'
	import { AddOffer, GetCars, GetCities } from '../setup/Endpoints'
	import { useSnackBarStore } from '../setup/stores/SnackBarStore'
	import { goToPage } from '../setup/Router'
	import { useUserStore } from '../setup/stores/UserStore'
	import * as yup from 'yup'
	import { Condition, ConditionDisplay } from '../constants/ConditionEnum'
	import { FuelType } from '../constants/FuelEnum'
	import { ITag } from '../Intefaces/ITag'

	const snackBarStore = useSnackBarStore()
	const userStore = useUserStore()

	const cars = ref<ICar[]>([])
	const cities = ref<ICity[]>([])
	const isCarSelectOpen = ref([])
	const isCitySelectOpen = ref([])
	const isConditionSelectOpen = ref([])
	const isFuelSelectOpen = ref([])
	const errors = ref<Record<string, string>>({})
	const selectedCar = ref<ICar | null>(null)
	const selectedCity = ref<ICity | null>(null)
	const selectedCondition = ref<Condition>(Condition.New)
	const selectedFuel = ref<FuelType>(FuelType.Gasoline)
	const priceString = ref<string>('')
	const priceFieldKey = ref<number>(0)
	const mileageString = ref<string>('')
	const mileageFieldKey = ref<number>(0)
	const tagsString = ref<string>('')

	const offerData = ref<IOffer>({
		Contents: [
			{
				Type: 2,
				Position: 0,
				GalleryElements: [],
			},
		],
		Type: 'car',
		Condition: Condition[Condition.New],
		Fuel: FuelType[FuelType.Gasoline],
	} as IOffer)

	onMounted(async () => {
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

	watch(priceString, () => {
		const sanitized = priceString.value.replace(/[^0-9.]/g, '')

		if (sanitized) {
			offerData.value.Price = Number(sanitized)
			priceString.value = sanitized
		} else {
			priceString.value = ''
			priceFieldKey.value++
		}
	})

	watch(mileageString, () => {
		const sanitized = mileageString.value.replace(/[^0-9.]/g, '')

		if (sanitized) {
			offerData.value.Mileage = Number(sanitized)
			mileageString.value = sanitized
		} else {
			mileageString.value = ''
			mileageFieldKey.value++
		}
	})

	watch(tagsString, () => {
		offerData.value.Tags = tagsString.value
			.split(' ')
			.filter((t) => t)
			.map((t) => {
				return { Text: t, Color: randomColor() } as ITag
			})
	})

	const handleRemoveContent = (position: number) => {
		const newContents = offerData.value.Contents.filter(
			(c) => c.Position !== position
		).map((item, index) => ({
			...item,
			Position: index,
		}))

		offerData.value.Contents = newContents
	}

	const handleEditContent = (data: IContent) => {
		offerData.value.Contents.map((c) => {
			if (c.Position === data.Position) return data
			else return c
		})
	}

	const handleAddContent = (type: string) => {
		const nextPosition = offerData.value.Contents.length

		if (type === ContentTypeEnum[ContentTypeEnum.Text]) {
			offerData.value.Contents.push({
				Type: ContentTypeEnum.Text,
				Position: nextPosition,
			} as IContent)
		} else if (type === ContentTypeEnum[ContentTypeEnum.Gallery]) {
			offerData.value.Contents.push({
				Type: ContentTypeEnum.Gallery,
				Position: nextPosition,
				GalleryElements: [],
			} as IContent)
		}
	}

	const handleAddOffer = async () => {
		errors.value = {}
		try {
			const newOfferData = {
				...offerData.value,
				AuthorId: userStore.getUser.value.Id,
			} as IOffer

			await addOfferValidationSchema.validate(newOfferData, {
				abortEarly: false,
			})

			await AddOffer(newOfferData)
				.then(() => {
					snackBarStore.addMessage({
						text: 'Successfully added a new Offer!',
						color: 'success',
						timeout: 2000,
					})

					goToPage('/')
				})
				.catch(() => {
					snackBarStore.addMessage({
						text: 'Server error! Please try again!',
						color: 'error',
						timeout: 2000,
					})
				})
		} catch (err: any) {
			if (err.inner) {
				const errorMap: Record<string, string> = {}
				err.inner.forEach((e: any) => {
					errorMap[e.path] = e.message
				})
				errors.value = errorMap
			}

			if (!(err instanceof yup.ValidationError)) {
				snackBarStore.addMessage({
					text: 'Server error! Please try again!',
					color: 'error',
					timeout: 2000,
				})
			}
		}
	}

	const handleCancel = async () => {
		goToPage('/')
	}

	const handleChangeCar = (car: ICar) => {
		selectedCar.value = car

		offerData.value.CarId = car.CarId

		isCarSelectOpen.value = []
	}

	const handleChangeCity = (city: ICity) => {
		selectedCity.value = city

		offerData.value.CityId = city.Id

		isCitySelectOpen.value = []
	}

	const handleChangeCondition = (condition: Condition) => {
		selectedCondition.value = condition

		offerData.value.Condition = Condition[condition]

		isConditionSelectOpen.value = []
	}

	const handleChangeFuel = (fuel: FuelType) => {
		selectedFuel.value = fuel

		offerData.value.Fuel = FuelType[fuel]

		isFuelSelectOpen.value = []
	}

	const getCarName = (car: ICar) => {
		return `${car.Manufacturer} ${car.Model} ${car.Type} ${new Date(
			car.Year
		).getUTCFullYear()}`
	}

	const addOfferValidationSchema = yup.object({
		Title: yup.string().required('Title is required'),
		Contents: yup
			.array()
			.of(
				yup.object({
					Type: yup.number().required('The type is required!'),
					TextContent: yup.string().when('Type', {
						is: ContentTypeEnum.Text,
						then: (schema) => schema.required('Text content is required'),
						otherwise: (schema) => schema.notRequired(),
					}),
					SubTitle: yup.string().required('Subtitle is required'),
					Position: yup.number().required('The position is required!'),
					GalleryElements: yup.array().when('Type', {
						is: ContentTypeEnum.Gallery,
						then: (schema) =>
							schema
								.required('Gallery elements are required')
								.min(1, 'At least one Image is required in a Gallery'),
						otherwise: (schema) => schema.notRequired(),
					}),
				})
			)
			.min(1, 'At least one Content is required!')
			.required('Contents are required!'),
		CarId: yup.string().required('Car is required'),
		CityId: yup.string().required('City is required'),
		Fuel: yup.string().when('Type', {
			is: 'car',
			then: (schema) => schema.required('Fuel is required'),
			otherwise: (schema) => schema.notRequired(),
		}),
		Condition: yup.string().required('Condition is required'),
		Color: yup.string().when('Type', {
			is: 'car',
			then: (schema) => schema.required('Color is required'),
			otherwise: (schema) => schema.notRequired(),
		}),
		Price: yup.number().moreThan(0).required('City is required'),
		Mileage: yup.number().when('Type', {
			is: 'car',
			then: (schema) => schema.moreThan(0).required('Mileage is required'),
			otherwise: (schema) => schema.notRequired(),
		}),
		VIN: yup.string().when('Type', {
			is: 'car',
			then: (schema) => schema.required('VIN is required'),
			otherwise: (schema) => schema.notRequired(),
		}),
		PartNumber: yup.string().when('Type', {
			is: 'part',
			then: (schema) => schema.required('Part number is required'),
			otherwise: (schema) => schema.notRequired(),
		}),
	})

	const getErrorMessage = (field: string): string | null => {
		return errors.value[field] || null
	}

	const getErrorMessagesForContent = (
		contentIndex: number
	): Record<string, string> | null => {
		const keys = ['TextContent', 'SubTitle', 'Position', 'GalleryElements']

		const result: Record<string, string> = {}

		keys.forEach((key) => {
			const error = errors.value?.[`Contents[${contentIndex}].${key}`]
			if (error) {
				result[key] = error
			}
		})

		return Object.keys(result).length > 0 ? result : null
	}

	const randomColor = () => {
		const hue = Math.floor(Math.random() * 360)
		const saturation = 85 + Math.random() * 15
		const lightness = 50 + Math.random() * 10

		return `hsl(${hue}, ${saturation}%, ${lightness}%)`
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

	.side-menu-item.v-list-item.v-list-item--link.v-theme--light.v-list-item--density-compact.v-list-item--one-line.v-list-item--variant-text {
		padding-inline-start: 2px !important;
		padding: auto !important;
		margin: auto !important;
		text-align: center;
	}
	.side-menu-main-item {
		background-color: var(--white);
		border: 2px solid var(--black);
		padding-inline-start: 2px;
		margin: 0px !important;
		padding: 0px !important;
	}

	.sort-component {
		position: relative;
		height: 30px;
	}

	.v-list.v-theme--light.v-list--density-default {
		position: absolute;
		z-index: 10;
	}

	.v-list {
		overflow-y: hidden !important;
	}

	.v-list-item {
		height: 20px;
	}

	.custom-select {
		border: 2px solid black;
	}

	.select-item {
		cursor: pointer;
	}

	.select-item:hover {
		background-color: var(--blight);
	}
</style>

<style>
	span.v-select__selection-text {
		left: 0px !important;
	}
</style>
