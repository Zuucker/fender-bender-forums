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
	Contents: IContent
	Author: IUser
	Section: ISection
}
