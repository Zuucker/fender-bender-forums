import { IUser } from './IUser'

export interface IComment {
	Id: string
	Text: string
	Author: IUser
}
