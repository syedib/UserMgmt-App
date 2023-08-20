using System;

namespace UserMgmtApi.Exceptions
{
    public class BaseException : Exception
    {
        public string Code { get; private set; }
        public BaseException(string code, string message) : base(message) 
        {
            Code = code;
        }
    }
}
