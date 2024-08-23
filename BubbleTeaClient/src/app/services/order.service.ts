import { Injectable } from '@angular/core';
import { environment } from '../../environments/environments';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from '../models/Order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private apiUrl= environment.apiurl + 'ProductList';

  constructor(private http: HttpClient) { }

  getAll():Observable<Order[]>{
    return this.http.get<Order[]>(this.apiUrl)
  }
  getById(orderId:number): Observable<Order>{
    return this.http.get<Order>(`${this.apiUrl}/${orderId}`);
  }
  create(order:Order): Observable<Order>{
    return this.http.post<Order>(this.apiUrl, order);
  }

  update(order:Order): Observable<Order>{
    return this.http.put<Order>(`${this.apiUrl}/${order.orderID}`, order);
  }

  delete(orderId:number): Observable<Order>{
    return this.http.delete<Order>(`${this.apiUrl}/${orderId}`);
  }
}
