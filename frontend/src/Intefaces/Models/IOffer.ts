import { ICar } from './ICar'
import { ICity } from './ICity'
import { IComment } from './IComment'
import { IContent } from './IContent'
import { IOfferRate } from './IRating'
import { IUser } from './IUser'

export interface IOffer {
	OfferId: string
	Title: string
	Price: number
	CarId: number
	CityId: number
	AuthorId: string
	Date: Date
	Condition: number
	Fuel: number
	Color: string
	Mileage: number
	Tags: string
	Rating: number
	RatingCount: number
	Car: ICar
	City: ICity
	Author: IUser
	Ratings: IOfferRate[]
	Contents: IContent
	Comments: IComment[]
}
