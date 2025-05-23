import { Routes } from '@angular/router';
import { LoginComponent } from './Componants/login/login.component';
import { ProductsComponent } from './Componants/products/products.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'products', component: ProductsComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' }
];
