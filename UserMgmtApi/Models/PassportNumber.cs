using UserMgmtApi.Base;
using UserMgmtApi.Exceptions;

namespace UserMgmtApi.Models
{
    public class PassportNumber : ValueObject<PassportNumber>
    {
        private PassportNumber()
        {
            
        }
        public string Value { get; protected set; }
        public PassportNumber(string passportNumber)
        {
            if (string.IsNullOrEmpty(passportNumber))
            {
                throw new InvalidPassportNumberException();
            }

            Value = passportNumber;
        }
    }
}
