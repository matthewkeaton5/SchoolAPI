using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using System.Threading.Tasks;

namespace ActionFilters
{
    public class ValidateUserExistsAttribute 
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ValidateUserExistsAttribute(IRepositoryManager repository,
            ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id = (Guid)context.ActionArguments["id"];
            var user = _repository.User.GetUser(id, trackChanges);
            if (user == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("user", user);
                await next();
            }
        }
    }
}
