import { IUser } from './IUser'

export interface IOfferRate {
	OfferRateId: number
	OwnerId: string
	OfferId: number
	Rating: number
	Comment: string
	CreatedAt: Date
	Owner: IUser
}
