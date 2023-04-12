using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class AuthUserIdExists
    {
        public static Error UserIdExists => Error.Validation(
            code: "User.UserIdExists",
            description: "Oop! User Id can not be found!");
    }
}