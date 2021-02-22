using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngineCrawlerServices.Contract
{
    public interface IServiceResult<T>
    {
        public bool Success { get; }

        public T Result { get; set; }

        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
