import { ITag } from '../ITag'
import { ICar } from './ICar'
import { ICity } from './ICity'
import { IComment } from './IComment'
import { IContent } from './IContent'
import { IUser } from './IUser'

export interface IOffer {
	OfferId: number
	Title: string
	Price: number
	CarId: number
	CityId: number
	AuthorId: string
	Date: Date
	Condition: string
	Fuel?: string
	Color?: string
	Mileage?: number
	Tags: ITag[]
	Car: ICar
	City: ICity
	Points: number
	VIN?: number
	PartNumber?: string
	Type: string
	Author: IUser
	Contents: IContent[]
	Comments: IComment[]
	UpVoted: boolean
	DownVoted: boolean
	NumberOfComments?: number
}
