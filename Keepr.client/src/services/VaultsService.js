import { api } from './AxiosService'
import { logger } from '../utils/Logger'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { router } from '../router'

class VaultsService {
  async getById(id) {
    AppState.activeVault = {}
    try {
      const res = await api.get(`api/vaults/${id}`)
      // console.log(res.data)
      AppState.activeVault = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async getKeepsById(id) {
    try {
      const res = await api.get(`api/vaults/${id}/keeps`)
      // console.log(res.data)
      AppState.keeps = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async addKeep(keepid, vaultid) {
    const vaultkeep = {
      keepId: keepid,
      vaultId: vaultid
    }
    try {
      const res = await api.post('api/vaultkeeps', vaultkeep)
      // console.log(res.data)
      Pop.toast('Added to vault', 'success', 'top', 1500)
    } catch (error) {
      logger.log(error)
    }
  }

  async create(vault) {
    try {
      const res = await api.post('api/vaults', vault)
      // console.log(res.data)
      AppState.activeProfileVaults.push(res.data)
      AppState.userVaults.push(res.data)
      Pop.toast('vault created', 'success', 'top', 1500)
    } catch (error) {
      logger.log(error)
    }
  }

  async delete(id, accountid) {
    try {
      await api.delete(`api/vaults/${id}`)
      // console.log(res.data)
      router.push({ name: 'Profile', params: { id: accountid } })
      // AppState.activeProfileVaults.filter(v => v.id !== id)
      const res2 = await api.get(`/api/profiles/${accountid}/vaults`)
      // console.log(res2.data)
      AppState.userVaults = res2.data
      Pop.toast('vault deleted', 'success', 'top', 1500)
    } catch (error) {
      logger.log(error)
    }
  }

  async removeKeep(id) {
    try {
      await api.delete(`api/VaultKeeps/${id}`)
      // console.log(res.data)
      Pop.toast('keep removed', 'success', 'top', 1500)
    } catch (error) {
      logger.log(error)
    }
  }
}

export const vaultsService = new VaultsService()
