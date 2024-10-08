import { Component } from '@angular/core';
import { Product } from '../../models/Product';
import { ProductService } from '../../services/product.service';
import { Category } from '../../models/Category';
import { CategoryService } from '../../services/category.service';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { cartItems } from '../../models/CartItems';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [RouterModule,
    FormsModule,
    CommonModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent {
  categories: Category[] = []
  category: Category = {
    categoryID: 0, categoryName: '', product: []
  }
  product: Product = {
    productID: 0, name: '', price: 0, categoryId: 0,
    category: {
      categoryID: 0, categoryName: "",
    },
    
  };
constructor(private productService: ProductService,private categoryService: CategoryService,private cartService: CartService, private route: ActivatedRoute,) {}

  ngOnInit(): void {
    this.categoryService.getAll()
    .subscribe(x => this.categories = x);

  console.warn("category id is", this.route.snapshot.paramMap.get('categoryID'));
  this.route.params.subscribe((params) => {

    this.categoryService.getById(parseInt(params["categoryID"])).subscribe(x => this.category = x);
  })
}

addToCart(product: Product): void {

  // this.productService.getById(product.productId).subscribe(t => this.product = t)
  let item: cartItems = {
    productId: product.productID,
    productName: product.name,
    price: product.price,
    quantity: 1
  };
  this.cartService.addToBasket(item);
}
}
