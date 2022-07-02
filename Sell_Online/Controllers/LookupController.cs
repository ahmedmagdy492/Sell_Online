using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sell_Online.Filters;
using Sell_Online.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Controllers
{
    [ApiController]
    [Route("v1/Lookups/")]
    [ExecptionCatcherFilter]
    public class LookupController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public LookupController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("Categories")]
        [Authorize]
        public IActionResult GetCategories()
        {
            return Ok(new { Message = "Success", Data = _categoryService.GetAll() });
        }
    }
}
