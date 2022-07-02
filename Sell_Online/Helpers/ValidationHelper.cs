using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sell_Online.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Helpers
{
    public static class ValidationHelper
    {
        public static object ValidateInput(ModelStateDictionary.ValueEnumerable model)
        {
            var errorModel = new
            {
                ErrorCode = Guid.NewGuid().ToString(),
                ValidationErrors = new List<object>()
            };

            foreach(var item in model)
            {
                foreach(var error in item.Errors)
                {
                    errorModel.ValidationErrors.Add(new
                    {
                        Message = error.ErrorMessage
                    });
                }
            }

            return errorModel;
        }
    }
}
