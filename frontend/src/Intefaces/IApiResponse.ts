import { ApiError } from '../constants'

export interface IApiResponse<T = null> {
	Data: T | null
	Error: ApiError
}
