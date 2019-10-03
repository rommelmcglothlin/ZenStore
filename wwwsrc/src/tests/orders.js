
import { Test, Suite, TestReport } from "../Test.js"
import { api } from "../utils.js"

const PATH = "orders"
let testData = {
  name: "JIMMY__TEST__ORDER",
  products: [],
  total: 0
}

export class OrdersSuite extends Suite {
  constructor() {
    super("OrdersController", "", PATH)
    this.addTests(
      new Test(
        'Can Create a order',
        PATH,
        async () => {
          let p1res = await api.post('products', {
            name: "TEST__PRODUCT__ORDERS",
            img: "//placehold.it/200x200",
            description: "ADDING ORDERS",
            price: 2.00
          })
          let p2res = await api.post('products', {
            name: "TEST__PRODUCT__ORDERS",
            img: "//placehold.it/200x200",
            description: "ADDING ORDERS",
            price: 5.00
          })
          testData.products.push(p2res.data, p1res.data)

          this.order = await this.create(testData)

          if (this.order.name !== testData.name || !this.order.id) {
            throw new Error("Whoops something failed, unable to create order successfully")
          }
          if (this.order.total != 7) {
            throw new Error("Cart total not calculated correctly " + JSON.stringify(this.order))
          }
          return new TestReport(true, "Successfully created order " + JSON.stringify(this.order))
        },
        'Order',
        'POST request. This should create a new order in your database.',
        'order object { id, name, products, shipped, canceled }',
      ),
      new Test(
        'Can Edit order by order Id, Adds an extra Product',
        PATH + '/:id',
        async () => {
          let pRes = await api.post('products', {
            name: "TEST__PRODUCT__ORDERS__ADDON",
            img: "//placehold.it/200x200",
            description: "ADDING ORDERS ADDON",
            price: 1.00
          })
          this.order.name = "EDITED"
          this.order.products.push(pRes.data)
          let res = await this.update(this.order)

          if (res.name != this.order.name || res.total != 8) {
            throw new Error("Unable to edit order correctly")
          }

          return new TestReport(true, "Woot able to edit order successfully " + JSON.stringify(this.order))
        },
        'Order',
        'PUT request. This should update one order by its id.'
      ),
      new Test(
        "Ships Order",
        PATH + "/:id/ship",
        async () => {
          let res = await api.put("orders/" + this.order.id + "/ship")
          this.order = res.data
          if (!res.data.shipped) {
            throw new Error("Unable to ship orders")
          }
          return new TestReport(true, JSON.stringify(this.order))
        },
        "PUT Request: should mark the order as shipped",
        "order"
      ),
      new Test(
        "Can't Edit order once shipped",
        PATH + '/:id',
        async () => {
          this.order.name = "EDITED"
          try {
            let res = await this.update(this.order)
          } catch (e) {
            if (!e.status || e.status == 404) { throw e }
            return new TestReport(true, "Shipped orders can't be edited")
          }
          throw new Error("order edited after shipped")
        },
        'Error',
        'PUT request. This update should fail.'
      ),
      new Test(
        "Non-Cancelable Order",
        PATH + "/:id/cancel",
        async () => {
          try {
            let res = await api.put("orders/" + this.order.id + "/cancel")
          } catch (e) {
            if (!e.status || e.status == 404) { throw e }
            return new TestReport(true, "Unable to cancel order after it is shipped")
          }
          throw new Error("Once an order is shipped it cannot be canceled")
        },
        "PUT Request: Expects shipped order to be uncancelable",
        "order"
      ),

      new Test(
        "Cancel Order",
        PATH + "/:id/cancel",
        async () => {
          this.order.name = "A_NEW_ORDER"
          this.order.products = this.order.products.map(p => p.id)
          this.order = this.create(this.order)

          let res = await api.put("orders/" + this.order.id + "/cancel")
          this.order = res.data
          if (!res.data.canceled) {
            throw new Error("Unable to cancel orders")
          }
          return new TestReport(true, JSON.stringify(this.order))
        },
        "PUT Request: should mark the order as canceled",
        "order"
      ),
      new Test(
        "Can't Edit order once canceled",
        PATH + '/:id',
        async () => {
          this.order.name = "EDITED"
          try {
            let res = await this.update(this.order)
          } catch (e) {
            if (!e.status || e.status == 404) { throw e }
            return new TestReport(true, "Canceled orders can't be edited")
          }
          throw new Error("order edited after canceled")
        },
        'Error',
        'PUT request. This update should fail.'
      ),
      new Test(
        "Non-Shippable Order",
        PATH + "/:id/ship",
        async () => {
          try {
            let res = await api.put("orders/" + this.order.id + "/ship")
          } catch (e) {
            if (!e.status || e.status == 404) { throw e }
            return new TestReport(true, "Unable to ship order after it is canceled")
          }
          throw new Error("Once an order is canceled it cannot be shipped")
        },
        "PUT Request: Expects canceled orders to be non-shippable",
        "order"
      ),

    )
  }
}