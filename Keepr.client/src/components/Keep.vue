<template>
  <KeepModal :keep="keep" />
  <div class="Keep">
    <div class="card action" data-toggle="modal" :data-target="'#keepModal' + keep.id" @click="getKeep()">
      <img :src="keep.img" class="card-img" :alt="keep.name">
      <div class="kname font-weight-bold text-light imgtxt">
        {{ keep.name }}
      </div>
      <img
        :src="keep.creator?.picture"
        alt="user photo"
        height="40"
        class="rounded kcreatorimg"
      />
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
      getKeep() {
        keepsService.getById(props.keep.id)
      }
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
  bottom: 8px;
  left: 16px;
}
.kcreatorimg{
  position: absolute;
  bottom: 8px;
  right: 16px;
}
</style>
