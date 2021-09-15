import { api } from './AxiosService'
import { logger } from '../utils/Logger'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'

class VaultsService {
  async getById(id) {
    try {
      const res = await api.get(`api/vaults/${id}`)
      console.log(res.data)
      AppState.activeProfileVaults = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async getKeepsById(id) {
    try {
      const res = await api.get(`api/vaults/${id}/keeps`)
      console.log(res.data)
      AppState.activeVaultKeeps = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async create(vault) {
    try {
      const res = await api.create('api/vaults', vault)
      // console.log(res.data)
      AppState.activeProfileVaults.push(res.data)
    } catch (error) {
      logger.log(error)
    }
  }

  async delete(id) {
    try {
      await api.delete(`api/vaults/${id}`)
      // console.log(res.data)
      AppState.vaults = AppState.vaults.filter(k => k.id !== id)
      Pop.toast('vault deleted', 'success', 'top', 1500)
    } catch (error) {
      logger.log(error)
    }
  }
}

export const vaultsService = new VaultsService()
