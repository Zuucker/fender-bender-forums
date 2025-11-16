import { createApp } from 'vue'
import App from './pages/App.vue'
import { router } from './setup/Router'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle.min.js'
import { library } from '@fortawesome/fontawesome-svg-core'
import {
	faThumbsUp,
	faMessage,
	faStar as faStarR,
	faTrashCan,
} from '@fortawesome/free-regular-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome/index'
import './styles/Main.css'
import './styles/SideMenu.css'
import './styles/Offers.css'
import './styles/Content.css'
import {
	faArrowLeft,
	faArrowRight,
	faMagnifyingGlass,
	faStar,
	faStarHalfStroke,
} from '@fortawesome/free-solid-svg-icons'
import 'vuetify/styles'
import 'vuetify/styles'
import { createVuetify } from 'vuetify/dist/vuetify'
import * as components from 'vuetify/lib/components/index'
import * as directives from 'vuetify/lib/directives/index'
import { createPinia } from 'pinia'
import piniaPersist from 'pinia-plugin-persistedstate'
import { aliases, mdi } from 'vuetify/iconsets/mdi'

import VueFileAgentNext from '@boindil/vue-file-agent-next'
import '@boindil/vue-file-agent-next/dist/vue-file-agent-next.css'

const vuetify = createVuetify({
	components,
	directives,
	icons: {
		defaultSet: 'mdi',
		aliases,
		sets: {
			mdi,
		},
	},
})

// Add specific icons
library.add(
	faThumbsUp,
	faMessage,
	faMagnifyingGlass,
	faStar,
	faStarR,
	faStarHalfStroke,
	faTrashCan,
	faArrowLeft,
	faArrowRight
)

const app = createApp(App)
app.use(createPinia().use(piniaPersist))
app.component('font-awesome-icon', FontAwesomeIcon)
app.use(vuetify)
app.use(router)
app.use(VueFileAgentNext)
app.mount('#app')
