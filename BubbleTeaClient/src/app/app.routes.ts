import { Routes } from '@angular/router';
import { HomeComponent } from './Frontpage/home/home.component';
import { MenuComponent } from './Menu/menu/menu.component';
import { AboutComponent } from './About/about/about.component';
import { ProductsComponent } from './Product/products/products.component';
import { CartComponent } from './shoppingcart/cart/cart.component';
import { LoginComponent } from './Login/login.component';

export const routes: Routes = [
    {path:'', component:HomeComponent},
    {path:'menu', component:MenuComponent},
    {path:'about', component:AboutComponent},
    {path:'category/:categoryID',component:ProductsComponent},
    {path:'cart',component:CartComponent},
    {path:'Login',component:LoginComponent}

];
