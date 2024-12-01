namespace Publications.Main.Domain.Enums;

public enum ErrorType
{
    InternalError,
    NotAuthorized,
    EntityNotFound,
    EntityConflict,
    ValidationFailure,
    BadRequest,
    ActionNotAllowed
}
