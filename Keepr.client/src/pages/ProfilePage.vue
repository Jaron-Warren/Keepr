<template>
  <div class="Profile container-fluid">
    <div class="row">
      <div v-if="!profile.id" class="loader">
      </div>
      <div class="col-12 d-flex py-4">
        <img class="rounded" :src="profile.picture" alt="profile picture">
        <div class="pl-3">
          <h2>{{ profile.name }}</h2>
          <div>Vaults:{{}}</div>
          <div>Keeps:{{}}</div>
        </div>
      </div>
      <div class="col-12 py-4">
        <h3>
          Vaults
          <span class="f-24 ml-2 pb-1 text-primary action bg-secondary rounded">&ensp;+&ensp;</span>
        </h3>
      </div>
      <div class="col-12 py-4">
        <h3>
          Keeps
          <span class="f-24 ml-2 pb-1 text-primary action bg-secondary rounded">&ensp;+&ensp;</span>
        </h3>
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
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfileService'

export default {
  name: 'Account',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      try {
        await profilesService.getById(route.params.id)
        await profilesService.getKeepsById(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.activeProfileKeeps)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
