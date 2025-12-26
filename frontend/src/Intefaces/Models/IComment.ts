import { IUser } from './IUser'

export interface IComment {
	CommentId: number
	Content: string
	Points: number
	ParentId?: number
	PostId?: number
	OfferId?: number
	AuthorId: string
	CreatedAt: Date
	UpVoted: boolean
	DownVoted: boolean
	Author: IUser
	SubComments: IComment[]
}
