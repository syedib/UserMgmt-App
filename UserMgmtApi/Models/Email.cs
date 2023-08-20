using System.Text.RegularExpressions;
using UserMgmtApi.Base;
using UserMgmtApi.Exceptions;

namespace UserMgmtApi.Models
{
    public class Email : ValueObject<Email>
    {
        public string Value { get; private set; }

        private Email() { }
        public Email(string email) 
        {
            if (string.IsNullOrEmpty(email) || !ValidateEmail(email)) 
            {
                throw new InvalidEmailException();
            }

            Value = email;
        }

        private bool ValidateEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
