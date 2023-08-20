using Ardalis.SmartEnum;
using System.Drawing;

namespace UserMgmtApi.Models
{
    public sealed class EmployeeType : SmartEnum<EmployeeType>
    {
        public static readonly EmployeeType PERMANENT = new EmployeeType(nameof(PERMANENT), "Permanent", 1);
        public static readonly EmployeeType CONTRACTOR = new EmployeeType(nameof(CONTRACTOR), "Contractor", 2);

        public string DisplayName { get; }

        protected EmployeeType(string name, string displayName, int value) : base(name, value)
        {
            DisplayName = displayName;
        }
    }
}



