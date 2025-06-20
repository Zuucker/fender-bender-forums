export interface ISection {
	SectionId: number
	Name: string
	Description: string
	SubSections?: ISection[]
	Url?: string
}
