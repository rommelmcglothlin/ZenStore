import { auth } from '../utils'

export default {
  state: {
    tests: {
      canRegister: {
        success: false,
        name: 'Can Register A User',
        routeInfo: {
          path: '/register',
          data: 'User object {email, password, username}',
          response: 'User object',
          description: 'Post request, allows users to register an account.'
        }
      },
      canLogin: {
        success: false,
        name: 'Can  Login',
        routeInfo: {
          path: '/login',
          data: 'Credentials {email, password}',
          response: 'User object',
          description: 'Post request. Allows users to login'
        }
      },
      canLogout: {
        success: false,
        name: 'Can Logout',
        routeInfo: {
          path: '/logout',
          description: 'Delete request. Allows users to logout.'
        }
      },
      canA: {
        success: false,
        name: 'Can Authenticate',
        routeInfo: {
          path: '/authenticate',
          response: 'User object, if successful',
          description: 'Get request. Authenticates current user.'
        }
      }
    },
    user: {}
  },
  mutations: {
    setAuthState(state, prop) {
      console.log(state.tests[prop])
      state.tests[prop].success = true
    },
    setUser(state, payload) {
      state.user = payload
    }
  },
  actions: {
    register({ commit, dispatch }, payload) {
      auth.post('register', payload)
        .then(res => {
          commit('setUser', payload)
          commit('setAuthState', 'canRegister')
          dispatch('logout')
        })
        .catch(err => { console.error(err) })
    },
    logout({ commit, dispatch }) {
      auth.delete('logout')
        .then(res => {
          commit('setAuthState', 'canLogout')
          dispatch('login', null)
        })
        .catch(err => { console.error(err) })
    },
    removeUser({ commit }) {
      commit('setUser', {})
    },

    login({ commit, dispatch, state }, user) {
      if (!user) {
        user = state.user
      }
      auth.post('login', user)
        .then(res => {
          commit('setAuthState', 'canLogin')
          commit("setUser", res.data)
          user = res.data
          dispatch('authenticate')
        })
        .catch(err => { console.error(err) })
    },
    authenticate({ commit, dispatch }) {
      auth.get('authenticate')
        .then(res => {
          commit('setAuthState', 'canA')
          commit('setUser', res.data)
        })
        .catch(err => { console.error(err) })
    }
  }
}