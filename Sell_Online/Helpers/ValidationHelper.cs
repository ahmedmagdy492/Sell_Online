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
        public static GeneralResponse ValidateInput(ModelStateDictionary.ValueEnumerable model)
        {
            var errorModel = new GeneralResponse
            {
                ErrorCode = Guid.NewGuid().ToString(),
                ValidationErrors = new List<ErrorModel>()
            };

            foreach(var item in model)
            {
                foreach(var error in item.Errors)
                {
                    errorModel.ValidationErrors.Add(new ErrorModel
                    {
                        Message = error.ErrorMessage
                    });
                }
            }

            return errorModel;
        }
    }
}
