using UserMgmtApi.Base;
using UserMgmtApi.Exceptions;

namespace UserMgmtApi.Models
{
    public class MobileNumber : ValueObject<MobileNumber>
    {
        private MobileNumber()
        {
            
        }
        public string Value { get; protected set; }
        public MobileNumber(string mobileNumber) 
        {
            if (string.IsNullOrEmpty(mobileNumber)) 
            {
                throw new InvalidMobileNumberException();
            }

            Value = mobileNumber;
        }
    }
}
