using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class AddUser
    {
        public static Error FailedAddingUser => Error.Validation(
            code: "User.FailedAddingUser",
            description: "Failed to add user");
    }
}