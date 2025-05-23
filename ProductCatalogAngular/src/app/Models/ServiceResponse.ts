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