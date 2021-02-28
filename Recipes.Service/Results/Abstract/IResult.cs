using System;
using Recipes.Service.Results.Concrete;

namespace Recipes.Service.Results.Abstract
{
    public interface IResult
    {
        public string Message { get; }
        public Exception Exception { get; }
        public ResultStatus ResultStatus { get; }
    }
}