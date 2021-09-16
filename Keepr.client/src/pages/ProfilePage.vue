<template>
  <CreateVaultModal />
  <CreateKeepModal />
  <div class="Profile container-fluid">
    <div class="row">
      <div v-if="!profile.id" class="loader">
      </div>
      <div class="col-12 d-flex py-4">
        <img :src="profile.picture" alt="profile picture">
        <div class="pl-3">
          <h2>{{ profile.name }}</h2>
          <h5>Vaults: {{ vaults.length }}</h5>
          <h5>Keeps: {{ keeps.length }}</h5>
        </div>
      </div>
      <div class="col-12 py-4">
        <h3>
          Vaults
          <span class="f-24 ml-2 pb-1 text-primary action bg-secondary rounded" title="New Vault" data-toggle="modal" data-target="#createNewVault" v-if="profile?.id == account.id">&ensp;+&ensp;</span>
        </h3>
        <div class="pt-2 row">
          <div class="p-2 col-md-4 col-sm-6" v-for="v in vaults" :key="v.id">
            <Vault :vault="v" />
          </div>
        </div>
      </div>
      <div class="col-12 py-4">
        <h3>
          Keeps
          <span class="f-24 ml-2 pb-1 text-primary action bg-secondary rounded" title="New keep" data-toggle="modal" data-target="#createNewkeep" v-if="profile?.id == account.id">&ensp;+&ensp;</span>
        </h3>
        <div class="card-columns pt-3">
          <div v-for="k in keeps" :key="k.id">
            <Keep :keep="k" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, watch } from 'vue'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfileService'

export default {
  name: 'ProfilePage',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      try {
        await profilesService.getById(route.params.id)
        await profilesService.getVaultsById(route.params.id)
        await profilesService.getKeepsById(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    watch(() => route.params.id,
      async() => {
        if (route.name === 'Profile') {
          try {
            await profilesService.getById(route.params.id)
            await profilesService.getVaultsById(route.params.id)
            await profilesService.getKeepsById(route.params.id)
          } catch (error) {
            Pop.toast(error, 'error')
          }
        }
      }
    )
    return {
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.activeProfileVaults)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
