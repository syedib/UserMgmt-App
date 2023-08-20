namespace UserMgmtApi.Exceptions
{
    public class UserNotFoundExceotion : NotFoundException
    {
        public UserNotFoundExceotion() : base("USER_NOT_FOUND", "User not found")
        {
        }
    }
}
