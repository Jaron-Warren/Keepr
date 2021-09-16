<template>
  <!-- Modal -->
  <div class="modal"
       id="createNewVault"
       tabindex="-1"
       role="dialog"
       aria-labelledby="modelTitleId"
       aria-hidden="true"
  >
    <form class="modal-dialog" role="document" @submit.prevent="createVault()">
      <div class="modal-content">
        <div class="modal-header">
          <h2 class="modal-title">
            New Vault
          </h2>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <div class="container-fluid">
            <div class="form-group">
              <label class="pr-2" for="name">Name:</label>
              <input type="text"
                     class="form-control"
                     placeholder="name..."
                     v-model="state.newVault.name"
                     minlength="4"
                     maxlength="20"
                     required
              >
            </div>
            <div class="form-group">
              <label class="pr-2" for="description">Description</label>
              <textarea type="text"
                        class="form-control"
                        placeholder="vault description..."
                        v-model="state.newVault.description"
                        rows="8"
                        minlength="20"
                        maxlength="300"
                        required
              />
            </div>
            <div>
              <input type="checkbox"
                     name="private"
                     v-model="state.newVault.isPrivate"
              >
              <label for="private">&ensp;Private?</label>
            </div>
            <i class="text-muted">Private vaults can only be seen by you</i>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-danger" data-dismiss="modal">
              Close
            </button>
            <button type="submit" class="btn btn-primary">
              Create
            </button>
          </div>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import { computed } from '@vue/runtime-core'
import { router } from '../router'
import $ from 'jquery'

export default {
  setup() {
    const state = reactive({
      newVault: {
        isPrivate: false
      }
    })
    return {
      state,
      account: computed(() => AppState.account),
      async createVault() {
        await vaultsService.create(state.newVault)
        state.newVault = {
          isPrivate: false
        }
        // router.push({ name: 'Vault', params: { id: createdvault.id } })
        $('#createNewVault').modal('hide')
        $('body').removeClass('modal-open')
        $('.modal-backdrop').remove()
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
