<template>
  <div class="Vault container-fluid">
    <div class="row">
      <div v-if="!vault.name" class="loader">
      </div>
      <div class="col-12 d-flex py-4">
        <div class="pl-3">
          <h2>
            {{ vault.name }}
            <span class="bg-light border border-primary text-muted f-14" v-if="vault.isPrivate">Private</span>
            <button type="button"
                    class="btn"
                    title="Delete Keep"
                    data-dismiss="modal"
                    v-if="account?.id == vault.creatorId"
                    @click="deleteVault(vault)"
            >
              <img src="src\assets\img\delete-outline.png" alt="delete" height="32" width="32">
            </button>
          </h2>
          <h5>Keeps: {{ keeps.length }}</h5>
        </div>
      </div>
      <div class="col-12 text-center" v-if="!keeps.length">
        <h5>Empty Vault :(</h5>
      </div>
      <div class="col-12">
        <div class="card-columns">
          <div v-for="k in keeps" :key="k.id">
            <Keep :keep="k" />
            <button type="button"
                    class="btn"
                    title="Delete Keep"
                    data-dismiss="modal"
                    v-if="account?.id == vault.creatorId"
                    @click="removeKeep(k.vaultKeepId)"
            >
              <img src="src\assets\img\delete-outline.png" alt="delete" height="32" width="32">
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import Pop from '../utils/Notifier'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import { router } from '../router'
import { keepsService } from '../services/KeepsService'

export default {
  name: 'VaultPage',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      AppState.keeps = {}
      try {
        await vaultsService.getById(route.params.id)
        if (!AppState.activeVault.name) {
          router.push({ name: 'Home' })
        }
        await vaultsService.getKeepsById(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps),
      vault: computed(() => AppState.activeVault),
      account: computed(() => AppState.account),
      async removeKeep(id) {
        try {
          if (await Pop.confirm('Confirm Delete', 'this can\'t be undone!', 'warning')) {
            await keepsService.delete(id)
          }
        } catch (error) {
          Pop.toast(error)
        }
      },
      async deleteVault(vault) {
        try {
          if (await Pop.confirm('Confirm Delete', 'this can\'t be undone!', 'warning')) {
            await vaultsService.delete(vault.id, this.account.id)
          }
        } catch (error) {
          Pop.toast(error)
        }
      }
    }
  }
}
</script>
