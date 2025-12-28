import {
	IApiResponse,
	ICar,
	ICity,
	IComment,
	IOffer,
	IPost,
	ISection,
	IUser,
} from '../Intefaces'
import ILoginData from '../Intefaces/ComponentInterfaces/ILoginData'
import IRegisterData from '../Intefaces/ComponentInterfaces/IRegisterData'
import { ILike } from '../Intefaces/Models/ILike'
import { api } from './Axios'

export const Register = async (data: IRegisterData): Promise<null> => {
	const response = await api.post<IApiResponse<string>>('/auth/register', data)

	return null // There is no response
}

export const Login = async (data: ILoginData): Promise<string> => {
	const response = await api.post<IApiResponse<string>>('/auth/login', {
		Login: data.Email,
		Password: data.Password,
	})

	return response.data.Data
}

export const GetUserData = async (id?: string): Promise<IUser> => {
	const response = await api.get<IApiResponse<IUser>>(`/user/get/${id ?? ''}`)

	return response.data.Data
}

export const EditUser = async (data: IUser): Promise<IUser> => {
	const response = await api.post<IApiResponse<IUser>>('/user/edit', data)

	return response.data.Data
}

export const GetMenuSections = async (): Promise<ISection[]> => {
	const response = await api.get<IApiResponse<ISection[]>>('/section/get-list')

	return response.data.Data
}

export const AddPost = async (data: IPost): Promise<IPost[]> => {
	const response = await api.post<IApiResponse<IPost[]>>('/posts/add', data)

	return response.data.Data
}

export const GetUserTopPosts = async (userId: string): Promise<IPost[]> => {
	const response = await api.get<IApiResponse<IPost[]>>(
		`/user/${userId ?? ''}/posts`
	)

	return response.data.Data
}

export const GetPost = async (postId: string): Promise<IPost> => {
	const response = await api.get<IApiResponse<IPost>>(`/posts/get/${postId}`)

	return response.data.Data
}

export const AddComment = async (comment: IComment): Promise<IComment> => {
	const response = await api.post<IApiResponse<IComment>>(
		'/posts/comment/add',
		comment
	)

	return response.data.Data
}

export const InteractWithPost = async (interaction: ILike): Promise<ILike> => {
	const response = await api.post<IApiResponse<ILike>>(
		'/posts/interact',
		interaction
	)

	return response.data.Data
}

export const InteractWithComment = async (
	interaction: ILike
): Promise<ILike> => {
	const response = await api.post<IApiResponse<ILike>>(
		'/posts/comment/interact',
		interaction
	)

	return response.data.Data
}

export const GetCars = async (
	searchString: string,
	pageSize: number
): Promise<ICar[]> => {
	const response = await api.get<IApiResponse<ICar[]>>('/car/get', {
		params: {
			searchString,
			pageSize,
		},
	})

	return response.data.Data
}

export const AddOffer = async (data: IOffer): Promise<IOffer[]> => {
	const response = await api.post<IApiResponse<IOffer[]>>('/offers/add', data)

	return response.data.Data
}

export const GetCities = async (): Promise<ICity[]> => {
	const response = await api.get<IApiResponse<ICity[]>>('/city/get')

	return response.data.Data
}
