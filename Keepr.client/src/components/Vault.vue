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
          height="40"
          class="rounded kcreatorimg"
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
  setup(props) {
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
  width: 10vw;
  height: auto;
}
</style>
