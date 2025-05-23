import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ProductDto {
  id: number;
  name: string;
  categoryName: string;
  price: number;
  startDate: string;
  duration: string;
  createdByUserName: string;
}

export interface Pagination {
  currentPage: number;
  lastPage: number;
  perPage: number;
  total: number;
}

export interface ServiceResponse<T> {
  success: boolean;
  message: string;
  data: T;
  pagination?: Pagination;
}

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private baseUrl = 'http://vacata3380-001-site1.qtempurl.com/api';

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    }
    return headers;
  }

  getAllProducts(categoryId: number = 0, pageNumber: number = 1, pageSize: number = 10): Observable<ServiceResponse<ProductDto[]>> {
    return this.http.get<ServiceResponse<ProductDto[]>>(
      `${this.baseUrl}/products/GetAllProducts?categoryId=${categoryId}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
      { headers: this.getHeaders() }
    );
  }

  getProductsInCategory(categoryId: number, pageNumber: number = 1, pageSize: number = 10): Observable<ServiceResponse<ProductDto[]>> {
    return this.http.get<ServiceResponse<ProductDto[]>>(
      `${this.baseUrl}/products/GetProductsInCategory?categoryId=${categoryId}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
      { headers: this.getHeaders() }
    );
  }
}


