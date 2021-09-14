<template>
  <!-- Modal -->
  <div class="modal"
       :id="'keepModal' + keep.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="modelTitleId"
       aria-hidden="true"
  >
    <form class="modal-dialog modal-dialog-centered modal-xl" role="document" @submit.prevent="deleteKeep">
      <div class="modal-content bg-light">
        <div class="modal-body">
          <div class="conatiner-fluid">
            <div class="row">
              <div class="col-lg-6 pr-0">
                <img class="kimg" :src="keep.img" :alt="keep.name">
              </div>
              <div class="col-lg-6 d-flex flex-column">
                <div>
                  <button type="button" class="close ml-auto" data-dismiss="modal" aria-label="Close">
                    <span class="f-24" aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="mt-2 mb-4">
                  <img src="src\assets\img\eye-plus.png" alt="views">
                  <span>&ensp;{{ keep.views }} &ensp;</span>
                  <img src="src\assets\img\k_title.png" alt="keeps" height="24" width="24">
                  <span>&ensp;{{ keep.keeps }} &ensp;</span>
                  <img src="src\assets\img\share-variant.png" alt="share">
                  <span>&ensp;{{ keep.shares }} &ensp;</span>
                </div>
                <h2 class="modal-title border-bottom">
                  {{ keep.name }}
                </h2>
                <div class="d-flex flex-grow-1 flex-column">
                  <div class="my-auto">
                    {{ keep.description }}
                  </div>
                  <div class="my-auto border-top border-bottom">
                    tags
                  </div>
                  <div class="my-auto">
                    creator
                  </div>
                </div>
                <div class="mt-auto justify-content-around d-flex">
                  <button type="button" class="btn btn-primary" data-toggle data-target>
                    Add to Vault
                  </button>
                  <button type="submit" class="btn" data-dismiss="modal" v-if="account?.id == keep.creatorId">
                    <img src="src\assets\img\delete-outline.png" alt="delete" height="32" width="32">
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepService } from '../services/KeepService'
import Pop from '../utils/Notifier'
import $ from 'jquery'

export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
      account: computed(() => AppState.account),
      async deleteKeep() {
        try {
          keepService.delete()
          // modal close
          $('#keepModal' + props.keep.id).modal('hide')
          $('body').removeClass('modal-open')
          $('.modal-backdrop').remove()
        } catch (error) {
          Pop.toast(error)
        }
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
.kimg{
  width: 100%;
  max-height: auto;
}
</style>
