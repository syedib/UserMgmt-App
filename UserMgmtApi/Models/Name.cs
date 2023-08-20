using UserMgmtApi.Base;

namespace UserMgmtApi.Models
{
    public class Name : ValueObject<Name>
    {
        private Name()
        {
            
        }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public Name(string firstName, string lastname)
        {
            Firstname = firstName;
            Lastname = lastname;
        }
    }
}



