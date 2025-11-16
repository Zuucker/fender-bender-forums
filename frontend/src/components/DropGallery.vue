<template>
	<div
		:class="['drop-gallery d-flex gap-2', { 'pb-3': needsPadding }]"
		:style="{ width: props.width + 'px' }">
		<div
			v-for="image in props.galleryElements"
			class="preview d-flex justify-content-center">
			<button
				class="remove-btn d-flex justify-content-center align-items-center text-dark px-1"
				@click="props.removeElement({ ...image })">
				X
			</button>
			<img class="img-preview" :src="image.Base64Data" />
		</div>
		<div ref="dropzoneElement" class="dropzone"></div>
	</div>
</template>

<script setup lang="ts">
	import { ref, onMounted, onUnmounted, computed } from 'vue'
	import { IGalleryElement } from '../Intefaces'
	import Dropzone from 'dropzone'
	import type { DropzoneOptions } from 'dropzone'

	Dropzone.autoDiscover = false

	type GalleryProps = {
		galleryElements: IGalleryElement[]
		removeElement: (elment: IGalleryElement) => void
		addElement: (files: File[]) => void
		width: number
	}

	const props = defineProps<GalleryProps>()

	const dropzoneElement = ref<HTMLElement>()
	let dropzone: Dropzone | null = null
	let pendingFiles: File[] = []
	let timeoutId: number | null = null

	const needsPadding = computed(() => {
		const itemWidth = window.innerWidth / 10 // 10vw in px
		const totalWidth = (props.galleryElements.length + 2) * itemWidth

		return totalWidth > props.width - 24
	})

	const updateScrollbarPadding = () => {
		const el = document.querySelector('.drop-gallery')

		if (el.scrollWidth > el.clientWidth) {
			el.classList.add('pb-3')
		} else {
			el.classList.remove('pb-3')
		}
	}

	onMounted(() => {
		if (dropzoneElement.value) {
			const options: DropzoneOptions = {
				url: '#',
				autoProcessQueue: false,
				acceptedFiles: 'image/*',
				maxFiles: 10,
				maxFilesize: 5,
				addRemoveLinks: false,
				createImageThumbnails: true,
				previewTemplate: '<div style="display:none;"></div>',

				addedfile: (file) => {
					pendingFiles.push(file as File)
					dropzone?.removeFile(file)

					if (timeoutId) clearTimeout(timeoutId)

					timeoutId = setTimeout(() => {
						if (pendingFiles.length > 0) {
							props.addElement([...pendingFiles])
							pendingFiles = []
						}
					}, 100)
				},
			}

			dropzone = new Dropzone(dropzoneElement.value, options)
		}

		updateScrollbarPadding()

		const container = document.querySelector('.drop-gallery')
		container.addEventListener('resize', () => updateScrollbarPadding())
	})

	onUnmounted(() => {
		dropzone?.destroy()
	})
</script>

<style scoped>
	.drop-gallery {
		overflow-x: auto;
	}
	.dropzone {
		min-width: 10vw;
		min-height: 10vw;
		border: 2px dashed #7f8084 !important;
		border-radius: 4px !important;
		background: #fafafa !important;
		display: flex;
		align-items: center;
		justify-content: center;
		cursor: pointer;
	}

	.dropzone::before {
		content: 'Drop here';
		font-size: 1rem;
		color: #7f8084;
	}

	.preview {
		min-width: 10vw;
		min-height: 10vw;
		background-color: rgba(0, 0, 0, 0.8);
		position: relative;
	}
	.img-preview {
		max-width: 10vw;
		max-height: 10vw;
	}

	:deep(.dz-message) {
		display: none;
	}

	.remove-btn {
		position: absolute;
		top: 4px;
		right: 4px;
		line-height: 1;
		border: 1px solid black;
		border-radius: 0px;
		background-color: white;
	}

	.remove-btn:hover {
		background-color: lightgray;
	}
</style>
