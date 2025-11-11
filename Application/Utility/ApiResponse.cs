using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utility
{
    public class ApiResponse<T>
    {
        public int IsSuccess { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ApiResponse(List<T> data, HttpStatusCode statusCode, string message, int isSuccess)
        {
            Data = data;
            StatusCode = statusCode;
            Message = message;
            IsSuccess = isSuccess;
        }
    }
}
