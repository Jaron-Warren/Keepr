<template>
  <!-- Modal -->
  <div class="modal"
       :id="'keepModal' + keep.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="modelTitleId"
       aria-hidden="true"
  >
    <form class="modal-dialog modal-dialog-centered modal-xl" role="document">
      <div class="modal-content bg-light">
        <div class="modal-body">
          <div class="conatiner-fluid">
            <div class="row">
              <div class="col-lg-6">
                <img class="kimg rounded" :src="keep.img" :alt="keep.name">
              </div>
              <div class="col-lg-6 d-flex flex-column">
                <div>
                  <button type="button" class="close ml-auto" data-dismiss="modal" aria-label="Close">
                    <span class="f-24" aria-hidden="true" title="close">&times;</span>
                  </button>
                </div>
                <div class="mt-2 mb-4 d-flex justify-content-center">
                  <img src="src\assets\img\eye-plus.png" alt="views" title="views">
                  <span v-if="currentKeep.id == keep.id">&ensp;{{ currentKeep.views }} &ensp;</span>
                  <span v-if="!currentKeep.id == keep.id">&ensp;0 &ensp;</span>
                  <img src="src\assets\img\k_title.png" alt="keeps" title="keeps" height="24" width="24">
                  <span>&ensp;{{ keep.keeps }} &ensp;</span>
                  <img src="src\assets\img\share-variant.png" title="shares" alt="share">
                  <span>&ensp;{{ keep.shares }} &ensp;</span>
                </div>
                <h2 class="modal-title border-bottom d-flex justify-content-center">
                  {{ keep.name }}
                </h2>
                <div class="d-flex flex-grow-1 flex-column">
                  <div class="my-auto py-3 d-flex justify-content-center">
                    {{ keep.description }}
                  </div>
                  <div class="my-auto py-3 border-top border-bottom d-flex justify-content-center" v-if="keep.tags">
                    {{ keep.tags }}
                  </div>
                  <div class="my-auto py-4 d-flex justify-content-center">
                    <router-link :to="{ name: 'Profile', params: {id: keep.creator?.id} }" @click="closeModal()">
                      <img class="rounded" :src="keep.creator.picture" alt="author's picture" height="48" width="48">
                      {{ keep.creator?.name }}
                    </router-link>
                  </div>
                </div>
                <div class="mt-auto justify-content-around d-flex" v-if="account.email">
                  <!-- Default dropup button -->
                  <div class="btn-group dropup">
                    <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                      Add to Vault
                    </button>
                    <div class="dropdown-menu">
                      <!-- Dropdown menu links -->
                      <button class="dropdown-item" type="button" v-for="v in userVaults" :key="v.id">
                        <span @click="addToVault(keep.id, v.id)">{{ v.name }}</span>
                      </button>
                    </div>
                  </div>
                  <button type="button"
                          class="btn"
                          title="Delete Keep"
                          data-dismiss="modal"
                          v-if="account?.id == keep.creatorId"
                          @click="deleteKeep()"
                  >
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
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'
import $ from 'jquery'
import { vaultsService } from '../services/VaultsService'

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
      currentKeep: computed(() => AppState.activeKeep),
      userVaults: computed(() => AppState.userVaults),
      async addToVault(keepid, vaultid) {
        try {
          await vaultsService.addKeep(keepid, vaultid)
        } catch (error) {
          Pop.toast(error)
        }
      },
      async deleteKeep() {
        try {
          if (await Pop.confirm('Confirm Delete', 'this can\'t be undone!', 'warning')) {
            await keepsService.delete(props.keep.id)
          }
        } catch (error) {
          Pop.toast(error)
        }
      },
      closeModal() {
        // modal close
        $('#keepModal' + props.keep.id).modal('hide')
        $('body').removeClass('modal-open')
        $('.modal-backdrop').remove()
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
