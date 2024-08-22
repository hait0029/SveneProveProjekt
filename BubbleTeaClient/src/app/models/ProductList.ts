import { Product } from './Product';
import { Order } from './Order';
export interface ProductList {
"productOrderListID": number
  "quantity": number
  "productId": number
  "orderId": number
  Order?: Order[]
  Product?: Product[]
}
