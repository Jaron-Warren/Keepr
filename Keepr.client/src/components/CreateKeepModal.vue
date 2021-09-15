<template>
  <!-- Modal -->
  <div class="modal"
       id="createNewkeep"
       tabindex="-1"
       role="dialog"
       aria-labelledby="modelTitleId"
       aria-hidden="true"
  >
    <form class="modal-dialog" role="document" @submit.prevent="createkeep">
      <div class="modal-content">
        <div class="modal-header">
          <h2 class="modal-title">
            New Keep
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
                     v-model="state.newkeep.name"
                     minlength="4"
                     maxlength="20"
                     required
              >
              <div class="form-group">
                <label class="pr-2" for="img">Image URL:</label>
                <input type="text"
                       class="form-control"
                       placeholder="URL..."
                       v-model="state.newkeep.img"
                       minlength="10"
                       required
                >
              </div>
              <div class="form-group">
                <label class="pr-2" for="description">Description</label>
                <textarea type="text"
                          class="form-control"
                          placeholder="keep description..."
                          v-model="state.newkeep.description"
                          rows="8"
                          minlength="20"
                          maxlength="300"
                          required
                />
              </div>
              <div class="form-group">
                <label class="pr-2" for="tags">Tags:</label>
                <input type="text"
                       class="form-control"
                       placeholder="tags..."
                       v-model="state.newkeep.tags"
                       minlength="2"
                       maxlength="50"
                       required
                >
              </div>
              <i class="text-muted text-center">* seperate tags with a comma</i>
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
      </div>
    </form>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { keepsService } from '../services/KeepsService'
import $ from 'jquery'

export default {
  setup() {
    const state = reactive({
      newkeep: {}
    })
    return {
      state,
      // account: computed(() => AppState.account),
      async createkeep() {
        await keepsService.create(state.newkeep)
        state.newkeep = {}
        $('#createNewkeep').modal('hide')
        $('body').removeClass('modal-open')
        $('.modal-backdrop').remove()
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
