import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    user: {},
    suites: []
  },
  modules: {
  },
  mutations: {
    setUser(state, payload) {
      state.user = payload
    },
    reloadSuites(state) {
      state.suites = []
    },
    addSuite(state, suite) {
      state.suites.push(suite)
    }

  },
  actions: {
  }
})
