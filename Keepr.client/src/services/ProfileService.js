import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfilesService {
  async getById(id) {
    try {
      const res = await api.get(`api/profiles/${id}`)
      // console.log(res.data)
      AppState.activeProfile = res.data
    } catch (error) {
      logger.log('Unable to retrieve profile')
    }
  }

  async getKeepsById(id) {
    try {
      const res = await api.get(`api/profiles/${id}/keeps`)
      // console.log(res.data)
      AppState.activeProfileKeeps = res.data
    } catch (error) {
      logger.log('Unable to retrieve profile')
    }
  }

  async getVaultsById(id) {
    try {
      const res = await api.get(`api/profiles/${id}/vaults`)
      // console.log(res.data)
      AppState.activeProfileVaults = res.data
    } catch (error) {
      logger.log('Unable to retrieve profile')
    }
  }
}

export const profilesService = new ProfilesService()
