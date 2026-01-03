import { IPost } from './Models/IPost'

export interface IPostPage {
	Posts: IPost[]
	NextCursor?: string
}
