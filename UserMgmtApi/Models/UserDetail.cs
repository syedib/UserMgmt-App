using System;

namespace UserMgmtApi.Models
{
    public class UserDetail
    {
        public int Id { get; private set; }
        public int LocationId { get; private set; }
        public Name Name { get; private set; }
        public EmployeeType EmployeeType { get; private set; }
        public MobileNumber MobileNo { get; private set; }
        public PassportNumber PassportNo { get; private set; }
        public Email Email { get; private set; }
        public string Nationality { get; private set; }
        public string Designation { get; private set; }
        public DateTimeOffset PassportExpirtDate { get; private set; }
        public string PassportFilePath { get; private set; }
        public byte[] PersonPhoto { get; private set; }

        public UserDetail(int id, 
            int locationId, 
            Name name, 
            EmployeeType employeeType,
            MobileNumber mobileNo, 
            Email email,
            PassportNumber passportNo, 
            string nationality, 
            string designation, 
            DateTimeOffset passportExpirtDate, 
            string passportFilePath, 
            byte[] personPhoto)
        {
            Id = id;
            LocationId = locationId;
            Name = name;
            EmployeeType = employeeType;
            MobileNo = mobileNo;
            Email = email;
            Nationality = nationality;
            Designation = designation;
            PassportNo = passportNo;
            PassportExpirtDate = passportExpirtDate;
            PassportFilePath = passportFilePath;
            PersonPhoto = personPhoto;
        }

        public void Update(
            int locationId,
            Name name,
            EmployeeType employeeType,
            MobileNumber mobileNo,
            Email email,
            PassportNumber passportNo,
            string nationality,
            string designation,
            DateTimeOffset passportExpirtDate,
            string passportFilePath,
            byte[] personPhoto) 
        {
            LocationId = locationId;
            Name = name;
            EmployeeType = employeeType;
            MobileNo = mobileNo;
            Email = email;
            Nationality = nationality;
            Designation = designation;
            PassportNo = passportNo;
            PassportExpirtDate = passportExpirtDate;
            PassportFilePath = passportFilePath;
            PersonPhoto = personPhoto;
        }

        private UserDetail() { }
    }
}



