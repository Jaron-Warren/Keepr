import { api } from './AxiosService'
import { logger } from '../utils/Logger'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'

class KeepsService {
  async getAll() {
    try {
      const res = await api.get('api/keeps')
      // console.log(res.data)
      AppState.keeps = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async getById(id) {
    try {
      const res = await api.get(`api/keeps/${id}`)
      // console.log(res.data)
      AppState.activeKeep = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async create(keep) {
    try {
      const res = await api.post('api/keeps', keep)
      // console.log(res.data)
      AppState.keeps.push(res.data)
    } catch (error) {
      logger.log(error)
    }
  }

  async delete(id) {
    try {
      await api.delete(`api/keeps/${id}`)
      // console.log(res.data)
      AppState.keeps = AppState.keeps.filter(k => k.id !== id)
      Pop.toast('Keep deleted', 'success', 'top', 1500)
    } catch (error) {
      logger.log(error)
    }
  }
}

export const keepsService = new KeepsService()
