namespace UserMgmtApi.Exceptions
{
    public class EmployeeTypeNotFoundException : BaseException
    {
        public EmployeeTypeNotFoundException() : base("EMPLOYEE_TYPE_NOTFOUND", "Employee type is not available")
        {
        }
    }
}
