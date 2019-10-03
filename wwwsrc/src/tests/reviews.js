
import { Test, Suite, TestReport } from "../Test.js"
import { api } from "../utils.js"

const PATH = "reviews"
let testData = {
  name: "TEST__PRODUCT",
  description: "TEST__DESCRIPTION",
  rating: 5,
  productId: ""
}

export class ReviewsSuite extends Suite {
  constructor() {
    super("ReviewsController", "This is a test Controller", PATH)
    this.addTests(
      new Test(
        'Can Create a review',
        PATH,
        async () => {
          let res = await api.post('products', {
            name: "TEST__PRODUCT__REVIEWS",
            img: "//placehold.it/200x200",
            description: "ADDING REVIEWS",
            price: 5.10
          })
          this.product = res.data

          testData.productId = res.data.id

          this.review = await this.create(testData)

          if (this.review.name !== testData.name || !this.review.id) {
            throw new Error("Whoops something failed, unable to create review successfully")
          }
          return new TestReport(true, "Successfully created review " + JSON.stringify(this.review))
        },
        'Review',
        'POST request. This should create a new review in your database.',
        'review object { id, name, description, rating, productId }',
      ),
      new Test(
        'Can Edit review by review Id',
        PATH + '/:id',
        async () => {
          this.review.name = "EDITED"
          let res = await this.update(this.review)

          if (res.name != this.review.name) {
            throw new Error("Unable to edit review")
          }

          return new TestReport(true, "Woot able to edit review successfully " + JSON.stringify(this.review))
        },
        'Review',
        'PUT request. This should update one review by its id.'
      ),
      new Test(
        "Get Reviews for Product",
        "products/:id/reviews",
        async () => {
          let reviews = await api.get("products/" + this.product.id + "/reviews")
          if (!Array.isArray(reviews)) {
            throw new Error("Unable to get product reviews")
          }
          return new TestReport(reviews.length > 0, JSON.stringify(reviews))
        },
        "Take note of the path this is calling to the ProductsController",
        "review[]"
      )
    )
  }
}