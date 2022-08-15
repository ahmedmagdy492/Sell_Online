using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sell_Online.DTO;
using Sell_Online.Filters;
using Sell_Online.Helpers;
using Sell_Online.IServices;
using Sell_Online.Mappers;
using Sell_Online.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Controllers
{
    [ApiController]
    [Route("v1/Users")]
    [ExecptionCatcherFilter]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Profile")]
        [Authorize]
        public IActionResult GetUserProfile()
        {
            var userId = User.Claims.ToList()[0].Value;

            var user = _userService.GetUserBy(u => u.UserID == userId, "PhoneNumbers").FirstOrDefault();

            if (user == null)
                return NotFound(new { Message = "User ID is Not Found or is Invalid" });

            return Ok(new { Message = "Success", Data = new List<object> { user.GetUserBasicInfo() } });
        }


        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.ValidateInput(ModelState.Values));

            string userId = User.Claims.ToList()[0].Value;
            var user = _userService.GetUserBy(u => u.UserID == userId).FirstOrDefault();

            if (user == null)
                return NotFound(new { Message = "User ID is Not Found or is Invalid" });

            var updateUserResult = await _userService.UpdateUser(UserMapper.MapUpdateUser(user, updateUserDTO));

            if (!updateUserResult)
                return BadRequest(new { Message = "User Data is not Updated due to a problem" });

            return Ok(new { Message = "User Data has been Updated Successfully" });
        }
    }
}
