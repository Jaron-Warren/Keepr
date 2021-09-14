<template>
  <div class="home flex-grow-1 d-flex flex-column align-items-center justify-content-center mt-4 container">
    <div v-if="!keeps.length" class="loader">
    </div>
    <div class="card-columns">
      <div v-for="k in keeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import Pop from '../utils/Notifier'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
export default {
  name: 'Home',
  setup() {
    onMounted(async() => {
      try {
        keepsService.getAll()
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })

    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
// .masonry-with-columns {
//   columns: 6 200px;
//   column-gap: 1rem;
//   div {
//     width: 150px;
//     background: #EC985A;
//     color: white;
//     margin: 0 1rem 1rem 0;
//     display: inline-block;
//     width: 100%;
//     text-align: center;
//     font-family: system-ui;
//     font-weight: 900;
//     font-size: 2rem;
//   }
//   @for $i from 1 through 36 {
//     div:nth-child(#{$i}) {
//       $h: (random(400) + 100) + px;
//       height: $h;
//       line-height: $h;
//     }
//   }
// }
</style>
