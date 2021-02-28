using System;
using Recipes.Service.Results.Abstract;

namespace Recipes.Service.Results.Concrete
{
    public class Result : IResult
    {
        public string Message { get; }
        public Exception Exception { get; }
        public ResultStatus ResultStatus { get; }

        public Result(ResultStatus resultStatus)
        {
            ResultStatus = resultStatus;
        }

        public Result(ResultStatus resultStatus, string message)
        {
            ResultStatus = resultStatus;
            Message = message;
        }

        public Result(ResultStatus resultStatus, string message, Exception exception)
        {
            ResultStatus = resultStatus;
            Message = message;
            Exception = exception;
        }
    }
}