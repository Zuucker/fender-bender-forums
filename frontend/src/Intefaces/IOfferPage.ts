import { IOffer } from './Models/IOffer'

export interface IOfferPage {
	Offers: IOffer[]
	NextCursor?: string
}
