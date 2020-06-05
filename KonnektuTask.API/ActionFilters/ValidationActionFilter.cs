using KonnektuTask.API.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KonnektuTask.API.ActionFilters
{
    public class ValidationActionFilter : ActionFilterAttribute
    {
        private readonly ResponseFactory _responseFactory;
        public ValidationActionFilter(ResponseFactory responseFactory)
        {
            _responseFactory = responseFactory;
        }
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new BadRequestObjectResult(_responseFactory.CreateFailureResponse(model: context.ModelState));
        }
    }
}