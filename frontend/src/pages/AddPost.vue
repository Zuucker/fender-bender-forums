<template>
	<div class="d-flex justify-content-center h-100 w-100 px-4 py-2">
		<div class="col-5 pe-4" style="overflow-y: auto">
			<h2>Add a Post!</h2>
			<div class="col-12">
				<v-text-field
					label="Title"
					variant="outlined"
					v-model="postData.Title"
					:flat="true"
					:error-messages="getErrorMessage('Title')" />
			</div>
			<div class="col-12">
				<v-autocomplete
					class="col-12"
					label="Select Car"
					:items="cars.map((s) => getCarName(s))"
					density="compact"
					variant="outlined"
					v-model:opened="isCarSelectOpen"
					v-model="selectedCar"
					@update:model-value="handleChangeCar"
					:error-messages="getErrorMessage('CarId')">
					<template #item="{ item, props }">
						<div v-bind="props" class="select-item px-4">
							{{ item.title }}
						</div>
					</template>
				</v-autocomplete>
			</div>
			<div class="col-12">
				<v-text-field
					label="Tags"
					variant="outlined"
					v-model="postData.Tags"
					:error-messages="getErrorMessage('Tags')" />
			</div>
			<div class="col-12">
				<v-select
					class="col-12"
					label="Select Section"
					:items="sections.map((s) => s.Name)"
					density="compact"
					variant="outlined"
					v-model:opened="isSectionSelectOpen"
					v-model="selectedSection"
					@update:model-value="handleChangeSection"
					:error-messages="getErrorMessage('SectionId')">
					<template #item="{ item, props }">
						<div v-bind="props" class="select-item px-4">
							{{ item.title }}
						</div>
					</template>
				</v-select>
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
				<span>{{ `${postData.Contents.length} blocks` }}</span>
				<span>{{
					`${
						postData.Contents.filter((c) => c.Type === ContentTypeEnum.Text)
							.length
					} Text`
				}}</span>
				<span>{{
					`${
						postData.Contents.filter((c) => c.Type === ContentTypeEnum.Gallery)
							.length
					} Gallery`
				}}</span>
			</div>
			<div class="col-12 d-flex justify-content-center gap-5 mt-4">
				<v-btn
					class="col-5 thicc-border py-0 px-10 m-0"
					color="white"
					style="border-radius: 0px"
					:onclick="handleAddPost">
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
		<div class="col-7 d-flex flex-column">
			<h2>Contents</h2>
			<v-card class="p-3 hide-scrollbar">
				<div class="h-100" style="overflow-y: auto">
					<Draggable
						v-model="postData.Contents"
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
						v-if="postData.Contents.length === 0"
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
	import { onMounted, ref } from 'vue'
	import { ICar, IContent, IPost, ISection } from '../Intefaces'
	import Draggable from 'vuedraggable'
	import Content from '../components/Content.vue'
	import { ContentTypeEnum } from '../constants/ContentTypeEnum'
	import { AddPost, GetCars, GetMenuSections } from '../setup/Endpoints'
	import { useSnackBarStore } from '../setup/stores/SnackBarStore'
	import { goToPage } from '../setup/Router'
	import { useUserStore } from '../setup/stores/UserStore'
	import * as yup from 'yup'

	const snackBarStore = useSnackBarStore()
	const userStore = useUserStore()

	const sections = ref<ISection[]>([])
	const cars = ref<ICar[]>([])
	const isSectionSelectOpen = ref([])
	const isCarSelectOpen = ref([])
	const errors = ref<Record<string, string>>({})
	const selectedSection = ref<string | null>(null)
	const selectedCar = ref<string | null>(null)
	const carSearchString = ref<string>('')

	const postData = ref<IPost>({
		Contents: [
			{
				Type: 2,
				Position: 0,
				GalleryElements: [],
			},
		],
	} as IPost)

	const handleRemoveContent = (position: number) => {
		const newContents = postData.value.Contents.filter(
			(c) => c.Position !== position
		).map((item, index) => ({
			...item,
			Position: index,
		}))

		postData.value.Contents = newContents
	}

	const handleEditContent = (data: IContent) => {
		postData.value.Contents.map((c) => {
			if (c.Position === data.Position) return data
			else return c
		})
	}

	const handleAddContent = (type: string) => {
		const nextPosition = postData.value.Contents.length

		if (type === ContentTypeEnum[ContentTypeEnum.Text]) {
			postData.value.Contents.push({
				Type: ContentTypeEnum.Text,
				Position: nextPosition,
			} as IContent)
		} else if (type === ContentTypeEnum[ContentTypeEnum.Gallery]) {
			postData.value.Contents.push({
				Type: ContentTypeEnum.Gallery,
				Position: nextPosition,
				GalleryElements: [],
			} as IContent)
		}
	}

	const handleAddPost = async () => {
		errors.value = {}
		try {
			const newPostData = {
				...postData.value,
				AuthorId: userStore.getUser.value.Id,
			} as IPost

			await addPostValidationSchema.validate(newPostData, {
				abortEarly: false,
			})

			await AddPost(newPostData)
				.then(() => {
					snackBarStore.addMessage({
						text: 'Successfully added a new Post!',
						color: 'success',
						timeout: 2000,
					})

					goToPage('/forum')
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

	const handleChangeSection = (sectionName: string) => {
		const section = sections.value.find((s) => s.Name === sectionName)

		selectedSection.value = section.Name
		postData.value.SectionId = section.SectionId

		isSectionSelectOpen.value = []
	}

	const handleChangeCar = (carName: string) => {
		const car = cars.value.find((s) => getCarName(s) === carName)

		selectedCar.value = getCarName(car)

		postData.value.CarId = car.CarId

		isCarSelectOpen.value = []
	}

	const getCarName = (car: ICar) => {
		return `${car.Manufacturer} ${car.Model} ${car.Type} ${new Date(
			car.Year
		).getUTCFullYear()}`
	}

	const addPostValidationSchema = yup.object({
		Title: yup.string().required('Title is required'),
		SectionId: yup.number().required('Section is required'),
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

	const flattenSections = (sections: ISection[]): ISection[] => {
		const result: ISection[] = []

		for (const section of sections) {
			result.push(section)

			if (section.SubSections) {
				result.push(...flattenSections(section.SubSections))
			}
		}

		return result
	}

	onMounted(async () => {
		await GetMenuSections()
			.then((response) => {
				sections.value = response
			})
			.catch((error) => {
				console.log(error)
			})

		await GetCars(carSearchString.value, 10)
			.then((response) => {
				cars.value = response
			})
			.catch((error) => {
				console.log(error)
			})
	})
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
