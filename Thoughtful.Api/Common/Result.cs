namespace Thoughtful.Api.Common
{
    public class Result<T>
    {
        public Result() { }
        private Result(T value)
        {
            Body = value;
            Error = null;
            IsSuccess = true;
        }
        private Result(Error error)
        {
            Body = default;
            Error = error;
        }

        public T? Body { get; set; }
        public Error? Error { get; set; }
        public bool IsSuccess { get; set; }

        //public bool IsSuccess => Error == null;
        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Failure(Error error) => new Result<T>(error);
        public static Result<T> Failure(T value) => new Result<T>(value);

        public TResult Map<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure)
        {
            return IsSuccess ? onSuccess(Body!) : onFailure(Error!);
        }
    }
}
