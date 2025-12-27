import { ICar } from './ICar'
import { IComment } from './IComment'
import { IContent } from './IContent'
import { ISection } from './ISection'
import { IUser } from './IUser'

export interface IPost {
	Id: number
	AuthorId: string
	SectionId: number
	Title: string
	CreationDate: Date
	Content: number
	Tags: string
	UpVoted: boolean
	DownVoted: boolean
	Points: number
	Contents: IContent[]
	Author: IUser
	Section: ISection
	CarId: number
	Car: ICar
	Comments: IComment[]
}
