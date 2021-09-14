<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
    <button
      class="navbar-toggler"
      type="button"
      data-toggle="collapse"
      data-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
        <div class="d-flex flex-column align-items-center">
          <img
            alt="logo"
            src="../assets/img/k_icon.svg"
            height="45"
          />
        </div>
      </router-link>
      <form onsubmit="" class="mx-auto">
        <input type="text">
      </form>
      <!-- account dropdown -->
      <span class="navbar-text">
        <button
          class="btn btn-secondary text-uppercase"
          @click="login"
          v-if="!user.isAuthenticated"
        >
          Login
        </button>

        <div class="dropdown bg-secondary text-light p-1 rounded" v-else>
          <div
            class="dropdown-toggle"
            @click="state.dropOpen = !state.dropOpen"
          >
            <img
              :src="user.picture"
              alt="user photo"
              height="40"
              class="rounded"
            />
            <span class="mx-3">{{ user.name }}</span>
          </div>
          <div
            class="dropdown-menu p-0 m-0 border-0 list-group w-100 rounded"
            :class="{ show: state.dropOpen }"
            @click="state.dropOpen = false"
          >
            <router-link :to="{ name: 'Profile' }">
              <div class="list-group-item list-group-item-action bg-secondary text-light hoverable">
                Profile
              </div>
            </router-link>
            <div
              class="list-group-item list-group-item-action bg-secondary text-light hoverable"
              @click="logout"
            >
              logout
            </div>
          </div>
        </div>
      </span>
      <!-- account dropdown -->
    </div>
  </nav>
</template>

<script>
import { AuthService } from '../services/AuthService'
import { AppState } from '../AppState'
import { computed, reactive } from 'vue'
export default {
  setup() {
    const state = reactive({
      dropOpen: false
    })
    return {
      state,
      user: computed(() => AppState.user),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style scoped>
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
a:hover {
  text-decoration: none;
}
.nav-link{
  text-transform: uppercase;
}
.nav-item .nav-link.router-link-exact-active{
  color: var(--primary);
}
.homeicon{
  max-height: 100%;
  width: auto;
}
.nvheight{
  max-height: 11vh;
}
</style>
