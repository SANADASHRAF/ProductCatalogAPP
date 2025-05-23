import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ProductsService, ProductDto, ServiceResponse, Pagination } from '../../services/products.service';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  products: ProductDto[] = [];
  pagination: Pagination | null = null;
  categoryId: number = 0;
  pageNumber: number = 1;
  pageSize: number = 10;
  errorMessage: string | null = null;
  successMessage: string | null = null;

  categories = [
    { id: 0, name: 'الكل' },
    { id: 1, name: 'Books' },
    { id: 2, name: 'Clothing' },
    { id: 3, name: 'Games' }
  ];

  constructor(private productsService: ProductsService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    const serviceCall = this.categoryId > 0
      ? this.productsService.getProductsInCategory(this.categoryId, this.pageNumber, this.pageSize)
      : this.productsService.getAllProducts(this.categoryId, this.pageNumber, this.pageSize);

    serviceCall.subscribe({
      next: (response: ServiceResponse<ProductDto[]>) => {
        if (response.success) {
          this.products = response.data;
          this.pagination = response.pagination ?? null;
        } else {
          this.errorMessage = response.message;
        }
      },
      error: (err) => {
        this.errorMessage = 'حدث خطأ أثناء جلب المنتجات';
        console.error(err);
      }
    });
  }

  onCategoryChange(): void {
    this.pageNumber = 1; 
    this.loadProducts();
  }

  changePage(page: number): void {
    this.pageNumber = page;
    this.loadProducts();
  }

  getPages(): number[] {
  const pages: number[] = [];
  if (this.pagination) {
    for (let i = 1; i <= this.pagination.lastPage; i++) {
      pages.push(i);
    }
  }
  return pages;
}


}