using System;
using System.Collections.Generic;
using System.Linq;
using KonnektuTask.API.Models;
using KonnektuTask.API.Models.Request;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KonnektuTask.API.Factories
{
    public class ResponseFactory
    {
        public BaseResponse CreateSuccessfullyResponse(string sourceId, object responseData)
        {
            return new BaseResponse()
            {
                Success = true,
                Warnings = new List<FieldWarning>(),
                SourceId = sourceId,
                Response = responseData
            };
        }

        public BaseResponse CreateFailureResponse(string sourceId = null, 
            object responseData = null,
            ModelStateDictionary model = null)
        {
            var warnings = new List<FieldWarning>();

            foreach (var (key, value) in model)
            {
                if (value.ValidationState != ModelValidationState.Invalid) continue;
                warnings.AddRange(
                    value.Errors.Select(error => 
                        new FieldWarning() {FieldName = key.Split('.').Last(), Message = error.ErrorMessage}));
            }
            
            return new BaseResponse()
            {
                Success = false,
                Warnings = warnings,
                SourceId = sourceId,
                Response = responseData
            };
        }
    }
}