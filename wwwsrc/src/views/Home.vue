<template>
  <div class="home container-fluid">
    <!-- <img alt="Vue logo" src="../assets/logo.png"> -->

    <div class="row">
      <div class="col-12 mb-3 text-center">
        <div class="d-flex flex-column align-items-center mb-3 mt-3">
          <img src="logo.png" width="150" alt />
          <em class="text-success">Testing from the ground up</em>
        </div>
        <div class="card border-info">
          <div class="card-body readMeSection text-left">
            <h3 class="d-flex align-items-center justify-content-between">
              <span>Welcome</span>
              <button class="btn btn-secondary" @click="runAll">
                <span>Run All Suites</span>
              </button>
            </h3>
            <div>
              <em class="action text-info" @click="toggleReadme">{{readMe ? 'close':'view'}} readme</em>
            </div>
            <div class="instructions" v-if="readMe">
              <p>
                This tool is designed to test very specific routes in your application's backend.
                <strong>If your routes are different or they return the wrong type of data these tests will not work.</strong>
              </p>
              <p>Each test will tell you what route it is testing, what data (if any) should be sent to the API, and what the response should be.</p>
              <p>Once the test passes, it will turn green, and the next test will be called. Each suite of tests will correspond to a controller in your backend. The suites have been designed to test one controller at a time</p>
              <p>Please read the following directions before beginning your tests:</p>
              <ul class="text-left">
                <li class="my-2">
                  <strong>OPEN YOUR INSPECTOR TOOLS</strong>
                </li>
                <li class="my-2">
                  Next, test your Keep controller. When you press 'run tests' it will create a waterfall effect. Each time a test is successful, it will invoke the next test.
                  <strong>If a test fails, the next tests will not run.</strong> This means that if you pass the first test and the remaining tests stay red, that does not mean that all of them are broken; it means that the next test in the line did not pass so the remaining
                  tests were not run.
                </li>
                <li>
                  When you are ready to view the tests and begin testing, click on the word "Welcome" in the upper left
                  corner of this box and then be sure to scroll down.
                </li>
                <li
                  class="my-2"
                >Be sure to read your error messages in the console if a test does not pass.</li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <hr />
      </div>
    </div>

    <auth v-if="usesAuth" />
    <div class="row" v-for="s in suites" :key="s.name">
      <div class="col-12">
        <suite :suite="s" />
        <hr />
      </div>
    </div>
  </div>
</template>

<script>
import Auth from "@/components/Auth.vue";
import Suite from "@/components/Suite.vue";

export default {
  mounted() {
    if (this.usesAuth) {
      this.$store.dispatch("authenticate");
    }
    this.readMe = JSON.parse(window.localStorage.getItem(this.title));
  },
  name: "home",
  data() {
    return {
      title: "AmaZen",
      readMe: false,
      usesAuth: false
    };
  },
  computed: {
    user() {
      return this.$store.state.auth.user;
    },
    suites() {
      return this.$store.state.suites;
    }
  },
  methods: {
    toggleReadme() {
      this.readMe = !this.readMe;
      window.localStorage.setItem(this.title, this.readMe);
    },
    runAll() {
      try {
        Promise.all(this.suites.map(s => s.runTests.call(s)));
      } catch (e) {
        console.error(e);
      }
    }
  },
  components: {
    Auth,
    Suite
  }
};
</script>
