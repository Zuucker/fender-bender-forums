namespace Domain.Errors
{
    public enum ApiErrors
    {
        UserNotFound = 0,
        EmailAlreadyInUse = 1,
        ProvidedWrongPassword = 2,
        CantAccessDifferentUser = 3,
        PasswordsDontMatch = 4,
        UsernameAlreadyInUse = 5,
        Unauthorized = 6,
        EmptyList = 7,
        PostNotFound = 8,
        FileNotFound = 9,
        WrongInteraction = 10,
        OfferNotFound = 11,
    }
}
