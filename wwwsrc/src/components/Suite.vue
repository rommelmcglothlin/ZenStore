<template>
  <div class="card p-3" :class="suite.success ? 'border-success' : 'border-danger'">
    <div>
      <small class="mr-3">status: {{passing.length}}/{{suite.tests.length}}</small>
      <em class="action text-info" @click="toggleSuite">{{showTests ? 'close':'view'}} suite</em>
    </div>
    <h3 class="d-flex align-items-center justify-content-between">
      <span class="mr-4">{{suite.name}}</span>
      <button class="btn btn-secondary" @click="suite.runTests.call(suite)">
        <span>Run Tests</span>
        <i v-if="suite.running" class="fa fa-fw fa-spin fa-spinner ml-2"></i>
        <i
          v-else
          class="fa fa-fw ml-2 text-light"
          :class="suite.success ? 'fa-check-circle': 'fa-times-circle'"
        ></i>
      </button>
    </h3>
    <p>These tests use the following base URL: api/{{suite.path}}</p>
    <tests v-if="showTests" :tests="suite.tests" />
  </div>
</template>

<script>
import Tests from "./tests.vue";
export default {
  props: ["suite"],
  data() {
    return {
      showTests: true
    };
  },
  computed: {
    passing() {
      return this.suite.tests.filter(t => t.success);
    }
  },
  mounted() {
    this.showTests = JSON.parse(
      window.localStorage.getItem(`suite::${this.suite.name}`)
    );
  },
  methods: {
    toggleSuite() {
      this.showTests = !this.showTests;
      window.localStorage.setItem(`suite::${this.suite.name}`, this.showTests);
    }
  },
  components: {
    Tests
  }
};
</script>