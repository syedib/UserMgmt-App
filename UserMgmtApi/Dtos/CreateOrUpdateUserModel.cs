using Microsoft.AspNetCore.Http;
using System;

namespace UserMgmtApi.Dtos
{
    public class CreateOrUpdateUserModel
    {
        public int LocationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int EmployeeType { get; set; }
        public string MobileNo { get; set; }
        public string PassportNo { get; set; }
        public string Nationality { get; set; }
        public string Designation { get; set; }
        public string PassportExpirtDate { get; set; }
        public IFormFile PersonPhoto { get; set; }
        public IFormFile PassportFile { get; set; }
        public int Id { get; set; }
    }
}
