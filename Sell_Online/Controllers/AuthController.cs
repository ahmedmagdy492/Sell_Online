using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sell_Online.DTO;
using Sell_Online.Filters;
using Sell_Online.Helpers;
using Sell_Online.IServices;
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
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IHashingService _sha256Hasher;
        private readonly IConfiguration _configuration;

        public AuthController(
            IAuthService authService,
            IUserService userService,
            IHashingService sha256Hasher,
            IConfiguration configuration
            )
        {
            _authService = authService;
            _userService = userService;
            _sha256Hasher = sha256Hasher;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet("IsValid")]
        public IActionResult CheckTokenValidatiy()
        {
            return Ok();
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDTO loginDTO) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.ExtractErrMsgs(ModelState.Values));

            var hashedPassword = _sha256Hasher.Hash(loginDTO.Password);

            var isAuthenticated = _authService.Authenticate(loginDTO.Email, hashedPassword);

            if (!isAuthenticated)
                return Unauthorized();

            var user = _userService.GetUserBy(u => u.Email == loginDTO.Email).FirstOrDefault();

            JwtGenerator jwtGenerator = new JwtGenerator(_configuration);
            string token = jwtGenerator.GenerateToken(user);

            return Ok(new
            {
                Message = user.IsVerified == false ? "Please Verify your Email" : "Logged In",
                Token = token,
                user.UserID
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserDTO registerUserDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.ExtractErrMsgs(ModelState.Values));

            var user = _userService.GetUserBy(u => u.Email == registerUserDTO.Email);

            if(user.FirstOrDefault() != null)
                return BadRequest(new { ValidationErrors = new List<object>{
                    new {Message = "Email is Already Taken" }}
                });

            registerUserDTO.Password = _sha256Hasher.Hash(registerUserDTO.Password);

            var created = await _userService.CreateUser(Mappers.UserMapper.MapCreateUser(registerUserDTO));

            if (!created)
                return BadRequest(new { Message = "User is not Created due to a problem" });

            // converting from the image from base64 to bytes
            var imageBytes = new Base64Converter().ConvertFromBase64(registerUserDTO.ProfileiImage);

            // saving image on disk
            string fileName = $"{Guid.NewGuid()}.{registerUserDTO.ImageType}";
            var fileSaver = new FileSaver(_configuration);
            fileSaver.SaveFile(fileName, imageBytes, registerUserDTO.ImageType);

            return Created("", new { Message = "User has been Created Successfully" });
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.ExtractErrMsgs(ModelState.Values));

            string userId =  User.Claims.ToList()[0].Value;

            var user = _userService.GetUserBy(u => u.UserID == userId).FirstOrDefault();
            if(user == null)
                return NotFound(new { Message = "User id is not Valid or not Found" });

            var hashedPassword = _sha256Hasher.Hash(changePasswordDTO.CurrentPassword);

            if (user.Password != hashedPassword)
                return BadRequest(new { Message = "Current Password is incorrect", ValidationErrors = new List<object>{ new { Message = "Current Password is incorrect" } }});

            var changePassword = await _userService.ChangePassword(user, _sha256Hasher.Hash(changePasswordDTO.NewPassword));

            if (!changePassword)
                return BadRequest(new { Message = "Password has not changed due to a problem" });

            return Ok( new { Message = "Password has been changed successfully" });
        }

    }
}
