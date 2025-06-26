using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public static OperationResult Ok() => new OperationResult { Success = true };
        public static OperationResult Fail(string message)
            => new OperationResult { Success = false, Message = message };
    }

    public class OperationResult<T> : OperationResult
    {
        public T? Data { get; set; }
        public static OperationResult<T> OK(T data, string? message = null)
            => new OperationResult<T> { Success = true, Data = data, Message = message };

        public static OperationResult<T> Fail(string message)
            => new OperationResult<T> { Success = false, Message = message, Data = default };
    }
}
