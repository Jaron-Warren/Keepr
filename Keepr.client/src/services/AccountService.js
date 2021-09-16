import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
      const userId = AppState.account.id
      const res2 = await api.get(`/api/profiles/${userId}/vaults`)
      // console.log(res2.data)
      AppState.userVaults = res2.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
}

export const accountService = new AccountService()
