<template>
  <div class="row">
    <div class="col-12 col-sm-6 mx-auto">
      <div class="card p-3">
        <form
          v-if="!user.email"
          class="text-left"
          @submit.prevent="newAccount ? register() : login()"
        >
          <div class="form-group" v-if="newAccount">
            <label for="username">Username:</label>
            <input
              autocomplete="username"
              type="text"
              v-model="newUser.username"
              placeholder="name"
              class="form-control"
            />
          </div>
          <div class="form-group">
            <label for="username">Email:</label>
            <input
              type="email"
              autocomplete="email"
              v-model="newUser.email"
              placeholder="email"
              class="form-control"
            />
          </div>
          <div class="form-group">
            <label for="Password">Password:</label>
            <input
              autocomplete="current-password"
              type="password"
              v-model="newUser.password"
              placeholder="password"
              class="form-control"
            />
          </div>
          <button type="submit" class="btn btn-info my-2">{{newAccount ? 'Register' : 'Login'}}</button>
          <div>
            <hr />
            <div
              class="text-danger text-right action muted"
              @click="newAccount = !newAccount"
            >{{newAccount ? 'Already registered? Login': 'Create new Account'}}</div>
          </div>
        </form>
        <div class="col-6 offset-lg-3 my-3" v-if="user.email">
          <h1 class="mb-2 welcome">Welcome, {{user.username}}</h1>
          <button @click="logout" class="btn btn-info">Logout</button>
        </div>
      </div>
    </div>
    <div class="col-12">
      <hr />
    </div>
  </div>
</template>

<script>
export default {
  name: "AuthForm",
  data() {
    return {
      newAccount: true,
      newUser: {
        email: "",
        password: "",
        username: ""
      }
    };
  },
  computed: {
    user() {
      return this.$store.state.auth.user;
    }
  },
  methods: {
    register() {
      this.$store.dispatch("register", this.newUser);
    },
    login() {
      let test = {
        email: this.newUser.email,
        password: this.newUser.password
      };
      this.$store.dispatch("login", test);
    },
    logout() {
      this.$store.dispatch("logout");
      this.$store.dispatch("removeUser");
    }
  }
};
</script>
