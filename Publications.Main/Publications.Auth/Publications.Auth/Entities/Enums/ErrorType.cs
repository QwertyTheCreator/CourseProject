namespace Publications.Auth.Entities.Enums;

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
