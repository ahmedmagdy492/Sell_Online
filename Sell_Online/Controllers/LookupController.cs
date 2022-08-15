using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sell_Online.Filters;
using Sell_Online.IServices;
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
        private readonly ICategoryService _categoryService;

        public LookupController(ICategoryService categoryService)
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
