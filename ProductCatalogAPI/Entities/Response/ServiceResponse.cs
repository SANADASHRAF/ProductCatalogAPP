using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public PaginationMetadata Pagination { get; set; }


        public ServiceResponse(bool success, string message, T data = default, PaginationMetadata pagination = null)
        {
            Success = success;
            Message = message;
            Data = data ?? new object();
            Pagination = pagination;
        }
       
        public ServiceResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data ?? new object();
        }
    }
}
