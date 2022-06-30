using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sell_Online.DTO;
using Sell_Online.Filters;
using Sell_Online.Helpers;
using Sell_Online.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Controllers
{
    [ApiController]
    [Route("v1/Auth/")]
    [ExecptionCatcherFilter]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly Sha256Hasher _sha256Hasher;
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;

        public AuthController(
            AuthService authService,
            Sha256Hasher sha256Hasher,
            IConfiguration configuration,
            IHostEnvironment hostEnvironment
            )
        {
            _authService = authService;
            _sha256Hasher = sha256Hasher;
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDTO loginDTO) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.ValidateInput(ModelState.Values));

            var hashedPassword = _sha256Hasher.Hash(loginDTO.Password);

            var user = _authService.GetUserBy(u => u.Email == loginDTO.Email && u.Password == hashedPassword);

            if (user == null || user.FirstOrDefault() == null)
                return Unauthorized();

            var userObject = user.FirstOrDefault();

            JwtGenerator jwtGenerator = new JwtGenerator(_configuration);
            string token = jwtGenerator.GenerateToken(userObject);

            return Ok(new GeneralResponse
            {
                Message = userObject.IsVerified == false ? "Please Verify your Email" : "Logged In",
                Token = token
            });
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterUserDTO registerUserDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.ValidateInput(ModelState.Values));

            var user = _authService.GetUserBy(u => u.Email == registerUserDTO.Email);

            if(user.FirstOrDefault() != null)
                return BadRequest(new GeneralResponse
                {
                    ValidationErrors = new List<ErrorModel>
                    {
                        new ErrorModel
                        {
                            Message = "Email is Already Taken"
                        }
                    }
                });

            registerUserDTO.Password = _sha256Hasher.Hash(registerUserDTO.Password);

            var created = _authService.CreateUser(Mappers.UserMapper.MapUser(registerUserDTO));

            if (!created)
                return BadRequest(new GeneralResponse
                {
                    Message = "User is not Created due to a problem"
                });

            // TODO: save image on disk

            var imageBytes = new Base64Converter().ConvertFromBase64(registerUserDTO.ProfileiImage);

            string filePath = System.IO.Path.Combine(_configuration["AppSettings:ImagePath"], $"{Guid.NewGuid()}.{registerUserDTO.ImageType}");

            System.IO.File.WriteAllBytes(filePath, imageBytes);

            return Created("", new GeneralResponse
            {
                Message = "User has been Created Successfully"
            });
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public IActionResult ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.ValidateInput(ModelState.Values));

            string userId =  User.Claims.ToList()[0].Value;

            var user = _authService.GetUserBy(u => u.UserID == userId).FirstOrDefault();
            if(user == null)
                return NotFound(new GeneralResponse
                {
                    Message = "User id is not Valid or not Found"
                });

            var hashedPassword = _sha256Hasher.Hash(changePasswordDTO.CurrentPassword);

            if (user.Password != hashedPassword)
                return BadRequest(new GeneralResponse
                {
                    Message = "Current Password is incorrect",
                    ValidationErrors = new List<ErrorModel>
                    {
                        new ErrorModel
                        {
                            Message = "Current Password is incorrect"
                        }
                    }
                });

            var changePassword = _authService.ChangePassword(user, _sha256Hasher.Hash(changePasswordDTO.NewPassword));

            if (!changePassword)
                return BadRequest(new GeneralResponse
                {
                    Message = "Password has not changed due to a problem"
                });

            return Ok(new GeneralResponse
            {
                Message = "Password has been changed successfully"
            });
        }
    }
}
