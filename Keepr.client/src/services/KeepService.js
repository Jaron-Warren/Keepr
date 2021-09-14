import { api } from './AxiosService'
import { logger } from '../utils/Logger'
import { AppState } from '../AppState'

class KeepService {
  async getAll() {
    try {
      const res = await api.get('api/keeps')
      // console.log(res.data)
      AppState.keeps = res.data
    } catch (error) {
      logger.log("Can't retrieve keeps")
    }
  }
}

export const keepService = new KeepService()
