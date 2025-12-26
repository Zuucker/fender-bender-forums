export interface IUser {
	Id: string
	UserName: string
	Email: string
	Avatar: string
	Rating: number
	NewPassword: string | null
	Password: string | null
	ConfirmPassword: string | null
}
