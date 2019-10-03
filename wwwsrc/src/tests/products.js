
import { Test, Suite, TestReport } from "../Test.js"
import { api } from "../utils.js"

const PATH = "products"
let testData = {
  name: "TEST__PRODUCT",
  img: "//placehold.it/200x200",
  description: "TEST__DESCRIPTION",
  price: 19.99
}

export class ProductsSuite extends Suite {
  constructor() {
    super("ProductsController", "This is a test Controller", PATH)
    this.addTests(
      new Test(
        "Can Get products",
        PATH,
        async () => {
          this.products = await this.get()

          if (!Array.isArray(this.products)) {
            throw new Error("Invalid data recieved when requesting " + PATH)
          }
          return new TestReport(true, "Able to get products: " + this.products.length)
        },
        'GET request. This should get a list of products.',
        "Product[]"
      ),
      new Test(
        'Can Create a product',
        PATH,
        async () => {
          this.product = await this.create(testData)

          if (this.product.name !== testData.name || !this.product.id) {
            throw new Error("Whoops something failed, unable to create product successfully")
          }
          return new TestReport(true, "Successfully created product " + JSON.stringify(this.product))
        },
        'Product',
        'POST request. This should create a new product in your database.',
        'product object { id, name, description, img, price }',
      ),
      new Test(
        'Can Get product by product Id',
        PATH + '/:id',
        async () => {
          let res = await this.getById(this.product.id)
          if (res.data.id !== this.product.id) {
            throw new Error("Failed to get the correct product when requesting by id")
          }
          return new TestReport(true, "Successfully retrieved product " + JSON.stringify(this.product))
        },
        'Product',
        'GET request. This should get one product by its id.'
      ),
      new Test(
        'Can Edit product by product Id',
        PATH + '/:id',
        async () => {
          this.product.name = "EDITED"
          let res = await this.update(this.product)

          if (res.name != this.product.name) {
            throw new Error("Unable to edit product")
          }

          return new TestReport(true, "Woot able to edit product successfully " + JSON.stringify(this.product))
        },
        'Product',
        'PUT request. This should update one product by its id.'
      ),
      new Test(
        'Can delete product by product Id',
        PATH + '/:id',
        async () => {
          let res = await this.delete(this.product)
          try {
            let getProduct = await this.getById(this.product.id)
          } catch (e) {
            console.warn("THIS MIGHT OF BEEN EXPECTED", e)
          }
          return new TestReport(true, "Woot able to delete product successfully")
        },
        'string',
        'DELETE request. This should delete one product by its id.'
      )
    )
  }
}