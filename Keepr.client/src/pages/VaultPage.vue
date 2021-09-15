<template>
  <div class="Vault container-fluid">
    <div class="row">
      <div v-if="!vault.name" class="loader">
      </div>
      <div class="col-12 d-flex py-4">
        <div class="pl-3">
          <h2>
            {{ vault.name }}
            <span class="border border-primary text-muted f-14" v-if="vault.isPrivate">Private</span>
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

export default {
  name: 'AboutPage',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      try {
        await vaultsService.getKeepsById(route.params.id)
        await vaultsService.getById(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.activeVaultKeeps),
      vault: computed(() => AppState.activeVault)
    }
  }
}
</script>
