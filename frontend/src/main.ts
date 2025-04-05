import { createApp } from 'vue'
import App from './pages/App.vue'
import { router } from './setup/Router'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle.min.js'
import { library } from '@fortawesome/fontawesome-svg-core'
import { faThumbsUp } from '@fortawesome/free-regular-svg-icons'
import { faMessage } from '@fortawesome/free-regular-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome/index'
import './styles/Main.css'
import './styles/SideMenu.css'
import './styles/Offers.css'
import './styles/Content.css'
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons'
import 'vuetify/styles'
import 'vuetify/styles'
import { createVuetify } from 'vuetify/dist/vuetify'
import * as components from 'vuetify/lib/components/index'
import * as directives from 'vuetify/lib/directives/index'

const vuetify = createVuetify({
	components,
	directives,
})

// Add specific icons
library.add(faThumbsUp, faMessage, faMagnifyingGlass)

const app = createApp(App)
app.use(router)
app.component('font-awesome-icon', FontAwesomeIcon)
app.use(vuetify)
app.mount('#app')
