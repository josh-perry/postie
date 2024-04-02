import axios from 'axios'
import store from './store'

export default function setupAxios() {
    axios.interceptors.request.use(config => {
        const token = store.getters["user/bearerToken"]

        if (token) {
            config.headers["Authorization"] = `Bearer ${token}`
        }

        return config
    }, error => {
        return Promise.reject(error)
    })
}