namespace ProductCatalogMVC.Response
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public PaginationMetadata Pagination { get; set; }

        public ServiceResponse(bool success, string message, T data = default, PaginationMetadata pagination = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Pagination = pagination;
        }
    }

    public class PaginationMetadata
    {
        public int CurrentPage { get; set; }
        public int LastPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
    }

}
