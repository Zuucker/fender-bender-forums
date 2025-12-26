import { IGalleryElement } from './GalleryElement'
import { IOffer } from './IOffer'
import { IPost } from './IPost'

export interface IContent {
	ContentId: number
	Type: number
	TextContent: string
	SubTitle: string
	Position: number
	GalleryPosition: number
	Path: string
	Offer: IOffer
	Post: IPost
	GalleryElements: IGalleryElement[]
}
