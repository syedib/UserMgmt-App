namespace UserMgmtApi.Exceptions
{
    public class InvalidMobileNumberException : BaseException
    {
        public InvalidMobileNumberException() : base("INVALID_MOBILE_NUMBER", "Invalid Mobile Number")
        {
        }
    }

    public class InvalidPassportNumberException : BaseException
    {
        public InvalidPassportNumberException() : base("INVALID_PASSPORT_NUMBER", "Invalid Passport Number")
        {
        }
    }
}
