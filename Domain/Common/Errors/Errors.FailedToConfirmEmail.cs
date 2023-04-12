using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class FailedToConfirmEmail
    {
        public static Error FailedToConfirmUser => Error.Validation(
            code: "User.FailedToConfirmUser",
            description: "Oop! Unable to confirm email!");
    }
}