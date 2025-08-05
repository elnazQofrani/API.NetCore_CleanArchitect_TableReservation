using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Result
    {
        protected Result(bool isSuccess, Error error = null)
        {
            if (isSuccess && error != null || !isSuccess && error == null)
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }
        public static Result Success() => new(true);
        public static Result<TValue> Success<TValue>(TValue value) => new(value, true);       
        public static Result NotFound<T>(object value) => new(isSuccess: false, error: Error.NotFound<T>(value));

    }

    public class Result<TValue> : Result
    {
        private readonly TValue _value;

        protected internal Result(TValue value, bool isSuccess, Error error = null)
            : base(isSuccess, error) =>
            _value = value;

        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    }



}
