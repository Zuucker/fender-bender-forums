export interface IFilter {
	Title?: string
	MinPrice?: number
	MaxPrice?: number
	CarId?: number
	CityId?: number
	AuthorId?: string
	Condition?: string
	Fuel?: string
	Color?: string
	Mileage?: number
	Tags?: string
	PartNumber?: string
	Type?: string
	SectionId?: number
	CreationDate?: Date
	SortBy: string
	OrderBy: string
}
