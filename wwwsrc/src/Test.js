import { api } from "./utils"
import store from "./store"
export class Test {
  /**
   * 
   * @param {string} name 
   * @param {string} path 
   * @param {function} testFn 
   * @param {string} description 
   * @param {string} expected 
   * @param {string} payload 
   */
  constructor(name, path, testFn, description = "", expected = "", payload = "") {
    if (typeof testFn != 'function') { throw new Error("Invalid Test Registration") }
    this.success = false
    this.running = false
    this.message = ""
    this.name = name
    this.path = path
    this.routeInfo = {
      path,
      expected,
      description
    }
    this.payload = payload
    this.runTest = testFn
  }
  async execute() {
    try {
      this.message = ""
      this.success = false
      this.running = true
      let res = await this.runTest()
      this.success = res.status
      this.message = res.message
    } catch (e) {
      console.error(e)
      this.message = e.message
    } finally {
      this.running = false
    }
  }
}

export class TestReport {
  constructor(status, message) {
    this.status = status
    this.message = message
  }
}

export class Suite {
  constructor(name, description, path) {
    this.name = name
    this.running = false
    this.success = false
    this.description = description
    this.tests = []
    this.path = path
    store.commit("addSuite", this)
  }

  addTests() {
    this.tests.length = 0
    this.tests.push(...arguments)
  }

  async runTests() {
    this.running = true
    try {
      for (let i = 0; i < this.tests.length; i++) {
        const test = this.tests[i];
        await test.execute.call(test)
      }
      // await Promise.all(this.tests.map(t => t.execute.call(t)))
    } catch (e) {
      console.error(e)
    } finally {
      this.running = false
      this.success = this.tests.find(t => !t.success) ? false : true
    }
  }

  async get() {
    let res = await api.get(this.path)
    return res.data
  }
  async create(payload) {
    let res = await api.post(this.path, payload)
    return res.data
  }
  async getById(id) {
    let res = await api.get(`${this.path}/` + id)
    return res.data
  }
  async update(payload) {
    let res = await api.put(`${this.path}/` + payload.id || payload._id, payload)
    return res.data
  }
  async delete(payload) {
    let res = await api.delete(`${this.path}/` + payload.id || payload._id)
    return res.data
  }


}