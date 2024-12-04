using Publications.Auth.Entities.Enums;

namespace Publications.Auth.Entities;

public record Error(ErrorType Type, string Message)
{
    public static Error NotFound(string message) => new(ErrorType.EntityNotFound, message);
    public static Error Conflict(string message) => new(ErrorType.EntityConflict, message);
    public static Error NotAllowed(string message) => new(ErrorType.ActionNotAllowed, message);
    public static Error BadRequest(string message) => new(ErrorType.BadRequest, message);
    public static Error Unauthorized() => new(ErrorType.NotAuthorized, "Пользователь не авторизован в системе");

    public override string ToString() => $"{Type}: {Message}";
}
