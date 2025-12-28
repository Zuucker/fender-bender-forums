export enum Condition {
	New = 1,
	Used = 2,
	HeavilyUsed = 3,
	Refurbished = 4,
	Damaged = 5,
	Unknown = 6,
}

export const ConditionDisplay: Record<keyof typeof Condition, string> = {
	New: 'New',
	Used: 'Used',
	HeavilyUsed: 'Heavily Used',
	Refurbished: 'Refurbished',
	Damaged: 'Damaged',
	Unknown: 'Unknown',
}
