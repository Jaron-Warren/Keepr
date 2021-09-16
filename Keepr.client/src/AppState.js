import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  // auth
  user: {},
  account: {},
  // auth
  keeps: {},
  activeKeep: {},
  activeVault: {},
  activeProfile: {},
  activeProfileVaults: {},
  userVaults: {}
})
