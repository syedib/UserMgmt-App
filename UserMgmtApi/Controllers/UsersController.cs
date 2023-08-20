using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using System.Threading.Tasks;
using UserMgmtApi.Dtos;
using UserMgmtApi.Services;

namespace UserMgmtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateUser([FromForm] CreateOrUpdateUserModel user)
        {
            var result = await _userService.Createuser(user);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromForm] CreateOrUpdateUserModel updatedUser)
        {
            var result = await _userService.Updateuser(updatedUser);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserById(id);

            return Ok(result);
        }

        //[HttpGet("{userId}/fileinfo")]
        //public async Task<IActionResult> GetFileInfoByUserId(int userId)
        //{
        //    var result = await _userService.GetFileInfo(userId);

        //    return Ok(result);
        //}

        [HttpGet("{userId}/photo")]
        public async Task<IActionResult> GetUserPhoto(int userId)
        {
            // Retrieve the user's photo from the database
            var userPhoto = await _userService.GetuserPhoto(userId);

            if (userPhoto == null)
            {
                return NotFound("User not found");
            }

            // Serve the user's photo as a file stream
            return File(userPhoto, "image/jpeg");
        }

        [HttpGet("{userId}/DownloadFile")]
        public async Task<IActionResult> DownloadFile(int userId)
        {
            // Retrieve the user's photo from the database
            var (fileStream, filename) = await _userService.DownloadFile(userId);

            if (fileStream == null)
            {
                return NotFound("File not found");
            }

            // Serve the user's photo as a file stream
            return File(fileStream, "application/pdf", filename);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.Getusers();
            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveUser(int id) 
        {
            var result = await _userService.RemoveUser(id);
            return Ok(result);
        }
    }
}
