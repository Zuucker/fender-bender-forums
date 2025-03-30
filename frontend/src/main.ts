import { createApp } from 'vue'
import App from './pages/App.vue'
import { router } from './setup/Router'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle.min.js'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import { createVuetify } from 'vuetify/lib/framework.mjs'
import { library } from '@fortawesome/fontawesome-svg-core'
import { faThumbsUp } from '@fortawesome/free-regular-svg-icons'
import { faMessage } from '@fortawesome/free-regular-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome/index'

import './styles/Main.css'
import './styles/SideMenu.css'
import './styles/Offers.css'
import './styles/Content.css'

const vuetify = createVuetify({
	components,
	directives,
})

// Add specific icons
library.add(faThumbsUp, faMessage)

const app = createApp(App)
app.use(router)
app.component('font-awesome-icon', FontAwesomeIcon)
app.use(vuetify)
app.mount('#app')
