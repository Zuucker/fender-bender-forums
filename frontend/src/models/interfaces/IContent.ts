import { IOffer } from './IOffer'
import { IPost } from './IPost'

export interface IContent {
	ContentId: number
	PostId: number | undefined
	OfferId: number | undefined
	type: number
	TextContent: string
	Position: number
	GalleryPosition: number | undefined
	Path: string
	Offer: IOffer | undefined
	Post: IPost | undefined
}
