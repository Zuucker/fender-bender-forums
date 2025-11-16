import { IApiResponse, IPost, ISection, IUser } from '../Intefaces'
import ILoginData from '../Intefaces/ComponentInterfaces/ILoginData'
import IRegisterData from '../Intefaces/ComponentInterfaces/IRegisterData'
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
		`/posts/get/${userId ?? ''}`
	)

	return response.data.Data
}
