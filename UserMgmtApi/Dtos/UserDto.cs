using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json.Serialization;

namespace UserMgmtApi.Dtos
{
    public class UserDto 
    {

        public UserDto(int id, string firstName, string lastName, string email, int employeeType, string employeeTypeName, string mobileNo, string passportNo, string nationality, string designation, DateTimeOffset passportExpirtDate, FileDetail? fileInfo)
        {

            Id = id;
            FirstName = firstName;
            LastName = lastName;    
            Email = email;
            EmployeeType = employeeType;
            EmployeeTypeName = employeeTypeName;
            MobileNo = mobileNo;
            PassportNo = passportNo;
            Nationality = nationality;
            Designation = designation;
            PassportExpirtDate = passportExpirtDate;
            FileInfo = fileInfo;
        }

        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get;  }
        public string Email { get; }
        public int EmployeeType { get; }
        public string EmployeeTypeName { get; }
        public string MobileNo { get; }
        public string PassportNo { get; }
        public string Nationality { get; }
        public string Designation { get; }
        public DateTimeOffset PassportExpirtDate { get; }
        public FileDetail? FileInfo { get; set; }
    }
}
