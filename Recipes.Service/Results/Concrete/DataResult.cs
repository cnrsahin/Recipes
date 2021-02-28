using System;
using Recipes.Service.Results.Abstract;

namespace Recipes.Service.Results.Concrete
{
    public class DataResult<T> : IDataResult<T>
    {
        public T Data { get; }
        public string Message { get; }
        public Exception Exception { get; }
        public ResultStatus ResultStatus { get; }

        public DataResult(ResultStatus resultStatus, T data)
        {
            ResultStatus = resultStatus;
            Data = data;
        }

        public DataResult(ResultStatus resultStatus, string message, T data)
        {
            ResultStatus = resultStatus;
            Message = message;
            Data = data;
        }

        public DataResult(ResultStatus resultStatus, string message, T data, Exception exception)
        {
            ResultStatus = resultStatus;
            Message = message;
            Data = data;
            Exception = exception;
        }
    }
}