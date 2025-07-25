using System;
using TestTask.Core.Exeption;

namespace TestTask.Core
{
    public class Result<T> : IEquatable<Result<T>>
    {
        private Result(T value, bool success, string error, int row)
        {
            Value = value;
            Success = success;
            Error = error;
            Row = row;
        }

        public T Value { get; }

        public bool Success { get; }

        public string Error { get; }

        public int Row { get; }

        public Result<T2> ToError<T2>()
        {
            if (Success)
            {
                throw new BusinessLogicException("Should be error.");
            }

            return Result<T2>.CreateFail(Error, Row);
        }

        public static Result<T> CreateSuccess(T value, int row) => new(value, true, string.Empty, row);

        public static Result<T> CreateFail(string error, int row)
        {
            if (string.IsNullOrEmpty(error))
            {
                throw new BusinessLogicException("Error cannot be empty.");
            }

            return new Result<T>(default, false, error, row);
        }

        public override string ToString()
        {
            if (!Success)
            {
                return Row + ". " + Error;
            }

            return Row + ". " + Value.ToString();
        }

        public override bool Equals(object obj) => Equals(obj as Result<T>);

        public bool Equals(Result<T> other)
        {
            if (other == null)
            {
                return false;
            }

            if (other.Value == null && Value == null)
            {
                return other != null
                         && other.Success == Success
                         && other.Error == Error
                         && other.Row == Row;
            }

            return other != null
                    && other.Value.Equals(Value)
                    && other.Success == Success
                    && other.Error == Error
                    && other.Row == Row;
        }

        public override int GetHashCode() => HashCode.Combine(Success, Error, Row);
    }
}
