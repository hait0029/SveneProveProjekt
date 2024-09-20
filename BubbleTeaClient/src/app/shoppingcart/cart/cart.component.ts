import { Component, OnInit } from '@angular/core';
import { cartItems } from '../../models/CartItems';
import { ProductList } from '../../models/ProductList';
import { CartService } from '../../services/cart.service';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Product } from '../../models/Product';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { environment } from '../../../environments/environments'; // Import environment for API URL

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [RouterModule, FormsModule, CommonModule],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
})
export class CartComponent implements OnInit {
  cartItems: cartItems[] = [];
  amount: number = 1;

  constructor(
    private cartService: CartService,
    private http: HttpClient // Inject HttpClient
  ) {}

  ngOnInit(): void {
    this.amount = Math.floor(Math.random() * 10) + 1;
    this.cartService.currentBasket.subscribe((items) => (this.cartItems = items));
  }

  getBasketTotal(): number {
    return this.cartItems.reduce((total, item) => total + item.price * item.quantity, 0);
  }

  addToCart(product: Product): void {
    const item: cartItems = {
      productId: product.productID,
      productName: product.name,
      price: product.price,
      quantity: 1,
    };
    this.amount = item.price * item.quantity;
    this.cartService.addToBasket(item);
  }

  clearCart(): void {
    this.cartService.clearBasket();
  }

  updateCart(): void {
    this.cartService.saveBasket(this.cartItems);
  }

  removeItem(item: cartItems): void {
    this.cartItems = this.cartItems.filter((x) => x.productId !== item.productId);
    this.cartService.saveBasket(this.cartItems);
  }

  purchase(): void {
    if (this.cartItems.length === 0) {
      alert('Your cart is empty!');
      return;
    }

    // Prepare the ProductList data
    const productList: ProductList[] = this.cartItems.map(item => ({
      productOrderListID: 0, // Placeholder, should be handled by the backend
      quantity: item.quantity,
      productId: 0,
      orderId: 0 
    }));

    // Wrap data in an object
    const payload = {
      productList: productList
    };

  // Log payload to check its structure
    console.log('Payload:', payload);
  
    // Send the request to the API
    this.http.post(`${environment.apiurl}productlist`, payload).subscribe({
      next: (response) => {
        alert('Purchase successful!');
        this.clearCart(); // Clear the cart after successful purchase
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error during purchase:', error);
        if (error.error && error.error.errors) {
          // Log validation errors if available
          console.error('Validation errors:', error.error.errors);
          alert('Validation error during the purchase.');
        } else {
          alert('There was an error during the purchase.');
        }
      }
    });
  }
}
