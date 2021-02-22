using System;
using System.Collections.Generic;
using System.Text;
using SearchEngineCrawlerServices.Contract;

namespace SearchEngineCrawlerServices.Dto
{
    public class ServiceResult<T> : IServiceResult<T>
    {
        public ServiceResult(T result)
        {
            Result = result;
            Success = true;
        }

        public ServiceResult(IEnumerable<string> errorMessages)
        {
            ErrorMessages = errorMessages;
            Success = false;
        }

        public bool Success { get; }

        public T Result { get; set; }

        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
