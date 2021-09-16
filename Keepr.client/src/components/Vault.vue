<template>
  <div class="Vault">
    <router-link :to="{ name: 'Vault', params: {id: vault.id} }">
      <div class="card d-flex" :title="vault.description">
        <img src="src\assets\img\product_vault_icon.svg" class="card-img vault mx-auto">
        <div class="kname text-primary bg-secondary rounded">
          <b>&ensp;{{ vault.name }}&ensp;</b>
        </div>
        <img
          :src="profile?.picture"
          alt="user photo"
          height="30"
          class="rounded kcreatorimg"
        />
        <img
          src="src\assets\img\lock_private_protect_icon.svg"
          alt="is private"
          height="30"
          class="privateimg"
          v-if="vault.isPrivate"
        />
      </div>
    </router-link>
  </div>
</template>

<script>
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'

export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {
      profile: computed(() => AppState.activeProfile)

    }
  }
}
</script>

<style lang="scss" scoped>
.card{
  position: relative !important;
  text-align: center !important;
}
.kname{
  position: absolute;
  bottom: 3px;
  left: 8px;
}
.kcreatorimg{
  position: absolute;
  top: 8px;
  right: 16px;
}
.vault{
  width: 30vw;
  height: auto;
}
.privateimg{
    position: absolute;
  top: 8px;
  right: 60px;
}
</style>
