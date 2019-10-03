<template>
  <div>
    <div
      class="card mb-1"
      v-for="test in tests"
      :class="test.success ? 'border-success' : 'border-danger'"
      :key="test.name"
    >
      <div class="card-header text-light" :class="test.success ? 'bg-success' : 'bg-danger'">
        <h4>{{test.name}}</h4>
      </div>
      <div class="card-body text-left">
        <p class="alert" :class="test.success ? 'alert-success' : 'alert-danger'" v-if="test.message">
          <b>Message:</b>
          {{test.message}}
        </p>
        <p v-for="(value, key) in test.routeInfo" :key="key">
          <b>{{key}}:</b>
          {{value}}
        </p>
      </div>
      <div class="card-footer text-right">
        <button class="btn btn-secondary" :disabled="test.running" @click="test.execute()">
          <span>Run</span>
          <i v-if="test.running" class="fa fa-fw fa-spin fa-spinner ml-2"></i>
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "keepTests",
  props: ["tests"],
  data() {
    return {};
  },
  computed: {},
  methods: {},
  components: {}
};
</script>
