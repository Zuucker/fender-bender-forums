import ILoginData from '../Intefaces/ComponentInterfaces/ILoginData'
import IRegisterData from '../Intefaces/ComponentInterfaces/IRegisterData'
import { api } from './Axios'

export const Register = async (data: IRegisterData) => {
	return await api.post('/auth/register', data)
}

export const Login = async (data: ILoginData) => {
	return await api.post('/auth/login', {
		Login: data.Email,
		Password: data.Password,
	})
}

export const GetUserData = async () => {
	return await api.get('/user/get')
}
