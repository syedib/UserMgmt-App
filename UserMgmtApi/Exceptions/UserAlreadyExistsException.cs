namespace UserMgmtApi.Exceptions
{
    public class UserAlreadyExistsException : BaseException
    {
        public UserAlreadyExistsException(string UserIdentityField) : base("USER_ALREADY_EXISTS", $"User Already Exists with {UserIdentityField}")
        {
        }
    }
}
