import type { InjectionKey, Ref } from 'vue'
import { IFilter } from '../Intefaces/IFilter'

export const FiltersContextKey: InjectionKey<Ref<IFilter>> = Symbol()
