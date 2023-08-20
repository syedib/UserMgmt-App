namespace UserMgmtApi.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string code, string message) : base(code, message)
        {
        }
    }
}
