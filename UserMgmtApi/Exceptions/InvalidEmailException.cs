namespace UserMgmtApi.Exceptions
{
    public class InvalidEmailException : BaseException
    {
        public InvalidEmailException() : base("INVALID_EMAIL", "Invalid Email Address")
        {
        }
    }
}
