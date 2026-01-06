<template>
	<v-card
		class="col-12 d-flex bg-blight mb-3"
		style="border: 2px solid var(--black); border-radius: 0px">
		<div class="col px-3 mb-3" ref="contentBoxRef">
			<div class="d-flex col justify-content-between align-items-top">
				<div
					class="me-2 d-flex justify-content-center align-items-center drag-handle"
					:style="{
						paddingBottom: getErrorMessage('SubTitle') ? '23px' : '0px',
					}">
					<div class="bg-white px-2 pb-1 cursor-pointer">⋮⋮</div>
				</div>
				<v-text-field
					label="SubTitle"
					variant="outlined"
					v-model="content.SubTitle"
					hide-details="auto"
					class="py-2"
					:error-messages="getErrorMessage(`SubTitle`)" />
				<div
					class="ms-2 d-flex justify-content-center align-items-center"
					:style="{
						paddingBottom: getErrorMessage('SubTitle') ? '23px' : '0px',
					}">
					<div
						class="bg-white px-2 pb-1 cursor-pointer"
						:onclick="() => props.removeContent(content.Position)">
						x
					</div>
				</div>
			</div>
			<v-textarea
				v-if="content.Type === ContentTypeEnum.Text"
				variant="outlined"
				label="Text"
				v-model="content.TextContent"
				rows="5"
				hide-details="auto"
				style="mask-image: none"
				:error-messages="getErrorMessage(`TextContent`)" />
			<div
				class="col d-flex"
				v-if="content.Type === ContentTypeEnum.Gallery"
				id="app">
				<DropGallery
					v-if="contentBoxRef"
					:galleryElements="props.content.GalleryElements"
					:addElement="handleAddImages"
					:removeElement="handleRemoveImage"
					:width="contenBoxWidth" />
			</div>
			<div
				v-if="getErrorMessage('GalleryElements')"
				class="col-12 d-flex justify-content-center text-danger mt-1">
				<span>{{ getErrorMessage('GalleryElements') }}</span>
			</div>
		</div>
	</v-card>
</template>

<script setup lang="ts">
	import { ContentTypeEnum } from '../constants/ContentTypeEnum'
	import { IContent } from '../Intefaces'
	import { onMounted, onUnmounted, ref } from 'vue'
	import { IGalleryElement } from '../Intefaces/Models/GalleryElement'

	import DropGallery from './DropGallery.vue'

	const contentBoxRef = ref<HTMLElement>()
	const contenBoxWidth = ref<number>(0)

	let resizeObserver: ResizeObserver

	type ContentProps = {
		content: IContent
		editContent: (data: IContent) => void
		removeContent: (position: number) => void
		errors?: Record<string, string>
	}

	const props = defineProps<ContentProps>()

	const handleAddImages = async (images: File[]) => {
		const base64Files = await Promise.all(
			images.map((file) => fileToBase64(file))
		)

		base64Files.forEach((i) =>
			props.content.GalleryElements.push({
				Base64Data: i,
				GalleryPosition: props.content.GalleryElements.length,
			} as IGalleryElement)
		)
	}

	const fileToBase64 = (file: File): Promise<string> => {
		return new Promise((resolve, reject) => {
			const reader = new FileReader()
			reader.onload = () => resolve(reader.result as string)
			reader.onerror = reject
			reader.readAsDataURL(file)
		})
	}

	const handleRemoveImage = (element: IGalleryElement) => {
		const filtered = props.content.GalleryElements.filter(
			(ge) => ge.GalleryPosition !== element.GalleryPosition
		)

		const newGallery = filtered.map((ge, idx) => {
			return { ...ge, GalleryPosition: idx } as IGalleryElement
		})

		props.content.GalleryElements = newGallery
	}

	onMounted(() => {
		if (contentBoxRef.value) {
			resizeObserver = new ResizeObserver((entries) => {
				for (const entry of entries) {
					contenBoxWidth.value = entry.contentRect.width
				}
			})
			resizeObserver.observe(contentBoxRef.value)
		}
	})

	const getErrorMessage = (field: string): string | null => {
		return props.errors?.[field] ?? null
	}

	onUnmounted(() => {
		if (resizeObserver) {
			resizeObserver.disconnect()
		}
	})
</script>

<style lang="css">
	textarea.v-field__input {
		mask-image: none !important;
		height: 150px !important;
	}
</style>
