import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import axios from 'axios'

Vue.config.productionTip = false

axios.defaults.baseURL='https://localhost:7074/';

//axios.defaults.baseURL='https://localhost:44324/'

// Si necesitas enviar cookies o credenciales junto con la solicitud:
axios.defaults.withCredentials = true;

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
