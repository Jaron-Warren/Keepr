<template>
  <!-- Modal -->
  <div class="modal"
       :id="'keepModal' + keep.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="modelTitleId"
       aria-hidden="true"
  >
    <form class="modal-dialog" role="document" @submit.prevent="deleteKeep">
      <div class="modal-content">
        <div class="modal-header bg-dark bcolor bthick">
          <h2 class="modal-title">
            Edit keep Report
          </h2>
          <button type="button" class="close text-light" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body bg-dark">
          <div class="container-fluid">
            <div class="mb-3" v-if="user.isAuthenticated">
              <img :src="account.picture" alt="picture" class="mr-2">
              {{ account.name }}
            </div>
          </div>
        </div>
        <div class="modal-footer bg-dark bcolor bthick">
          <button type="button" class="btn btn-danger" data-dismiss="modal">
            Close
          </button>
          <button type="button" class="btn btn-success" data-toggle data-target>
            Confirm
          </button>
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
      user: computed(() => AppState.user),
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
</style>
