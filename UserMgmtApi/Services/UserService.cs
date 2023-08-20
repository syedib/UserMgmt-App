using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UserMgmtApi.Data;
using UserMgmtApi.Exceptions;
using UserMgmtApi.Models;
using UserMgmtApi.Dtos;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace UserMgmtApi.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private readonly FileUploadService _fileUploadService;
        public UserService(ApplicationDbContext context, FileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }

        public async Task<int> Createuser(CreateOrUpdateUserModel user) 
        {
            // Validations
            //Email
            if (await _context.UserDetails.AnyAsync(u => u.Email.Value == user.Email)) 
            {
                throw new UserAlreadyExistsException(nameof(user.Email));
            }

            //Passport Number
            if (await _context.UserDetails.AnyAsync(u => u.PassportNo.Value == user.PassportNo))
            {
                throw new UserAlreadyExistsException(nameof(user.PassportNo));
            }

            //Mobile Number
            if (await _context.UserDetails.AnyAsync(u => u.MobileNo.Value == user.MobileNo))
            {
                throw new UserAlreadyExistsException(nameof(user.MobileNo));
            }

            EmployeeType employeeType = EmployeeType.FromValue(user.EmployeeType);

            if (employeeType == null)
            {
                throw new EmployeeTypeNotFoundException();
            }

            var passportFilePath = "";
            byte[] photoData = default;

            if (user.PersonPhoto != null)
            {
                photoData = await _fileUploadService.GetPhotoAsync(user.PersonPhoto);
            }

            if (user.PassportFile != null) 
            {
                passportFilePath = await _fileUploadService.UploadPassportAsync(user.PassportFile, user.PassportNo);
            }

            string dateString = user.PassportExpirtDate + "T10:30:00+00:00";
            DateTimeOffset PassportDateTimeOffset = DateTimeOffset.Parse(dateString);

            UserDetail userDetail = new UserDetail(default(int),
                    user.LocationId,
                    new Name(user.FirstName, user.LastName),
                    employeeType,
                    new MobileNumber(user.MobileNo),
                    new Email(user.Email),
                    new PassportNumber(user.PassportNo),
                    user.Nationality,
                    user.Designation,
                    PassportDateTimeOffset,
                    passportFilePath,
                    photoData
                );

            await _context.UserDetails.AddAsync( userDetail );

            await _context.SaveChangesAsync();

            return userDetail.Id;
        }

        public async Task<int> Updateuser(CreateOrUpdateUserModel user) 
        {
            var existingUser = await _context.UserDetails.FirstOrDefaultAsync(u => u.Id == user.Id);
            
            EmployeeType employeeType = EmployeeType.FromValue(user.EmployeeType);

            if (existingUser == null) 
            {
                throw new UserNotFoundExceotion();
            }

            if(employeeType == null) 
            {
                throw new EmployeeTypeNotFoundException();
            }

            var passportFilePath = "";

            if (user.PassportFile != null)
            {
                passportFilePath = await _fileUploadService.UploadPassportAsync(user.PassportFile, user.PassportNo);
            }

            byte[] photoData = await _fileUploadService.GetPhotoAsync(user.PersonPhoto);

            string dateString = user.PassportExpirtDate + "T10:30:00+00:00";
            DateTimeOffset PassportDateTimeOffset = DateTimeOffset.Parse(dateString);

            existingUser.Update(
                    user.LocationId,
                    new Name(user.FirstName, user.LastName),
                    employeeType,
                    new MobileNumber(user.MobileNo),
                    new Email(user.Email),
                    new PassportNumber(user.PassportNo),
                    user.Nationality,
                    user.Designation,
                    PassportDateTimeOffset,
                    passportFilePath,
                    photoData
                );

            await _context.SaveChangesAsync();

            return existingUser.Id;
        }

        public async Task<UserDto> GetUserById(int userId) 
        {
            UserDetail user = await _context.UserDetails.FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null) 
            {
                throw new UserNotFoundExceotion();
            }

            var fileInfo = _fileUploadService.GetFileDetail(user.PassportFilePath);

            return new UserDto(user.Id, user.Name.Firstname, user.Name.Lastname, user.Email.Value,
                user.EmployeeType.Value, user.EmployeeType.Name, user.MobileNo.Value, user.PassportNo.Value, user.Nationality, user.Designation, user.PassportExpirtDate, fileInfo);
        }

        public async Task<byte[]> GetuserPhoto(int userId) 
        {
            UserDetail user = await _context.UserDetails.FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
            {
                throw new UserNotFoundExceotion();
            }

            return user.PersonPhoto;
        }

        public async Task<(FileStream, string)> DownloadFile(int userId) 
        {
            UserDetail user = await _context.UserDetails.FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
            {
                throw new UserNotFoundExceotion();
            }

            var filePath = user.PassportFilePath;

            try
            {
                
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                var fileInfo = new FileInfo(filePath);

                return (fileStream, "passport.pdf");
            }
            catch (Exception ex)
            {
               
            }

            return (null, "");
        }

        public async Task<UserDto[]> Getusers() 
        {

            var users = await _context.UserDetails
                .OrderBy(u => u.Id)
                .Select(user => 
                    new UserDto(user.Id, 
                                user.Name.Firstname, 
                                user.Name.Lastname, 
                                user.Email.Value,
                                user.EmployeeType.Value,
                                user.EmployeeType.Name, 
                                user.MobileNo.Value, 
                                user.PassportNo.Value, 
                                user.Nationality, 
                                user.Designation, 
                                user.PassportExpirtDate, null)
                ).ToArrayAsync();

            return users;
        }

        public async Task<bool> RemoveUser(int id) 
        {
            var user = _context.UserDetails.FirstOrDefault(e => e.Id == id);

            if (user is null)
            {
                throw new UserNotFoundExceotion();
            }

            _context.UserDetails.Remove(user);
            
            return await _context.SaveChangesAsync() > 0; // Commit the changes to the database
        }
    }
}
