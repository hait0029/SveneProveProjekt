import { User } from "./User"

export interface Order {
  "orderID": number
  "orderDate": string
  "userId": number
  user?: User[]

}
