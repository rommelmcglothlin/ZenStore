import store from '../store'
import { ValuesSuite } from './values'
import { ProductsSuite } from "./products"
import { ReviewsSuite } from "./reviews"
import { OrdersSuite } from "./orders"

export function loadSuites() {
  store.commit("reloadSuites")
  new ProductsSuite()
  new ReviewsSuite()
  new OrdersSuite()
}


