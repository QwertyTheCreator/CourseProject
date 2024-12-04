using Publications.Auth.Entities;

namespace Publications.Auth.Models;

public class Result
{
    protected Result(bool succeeded, IEnumerable<Error> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; }

    public bool Failed => !Succeeded;

    public Error[] Errors { get; }

    public static Result Success() => new(true, Array.Empty<Error>());

    public static Result Failure(IEnumerable<Error> errors) => new(false, errors);

    public static Result Failure(params Error[] errors) => new(false, errors);

    public static implicit operator Result(Error error) => Failure(error);
}

public class Result<T> : Result
{
    private Result(bool succeeded, IEnumerable<Error> errors, T? data) : base(succeeded, errors)
    {
        Data = data;
    }

    public T? Data { get; }

    public static Result<T> Success(T data) => new(true, Array.Empty<Error>(), data);

    public static new Result<T> Failure(IEnumerable<Error> errors) => new(false, errors, default);

    public static new Result<T> Failure(params Error[] errors) => new(false, errors, default);

    public static implicit operator Result<T>(T data) => Success(data);
    public static implicit operator Result<T>(Error error) => Failure(error);
}
