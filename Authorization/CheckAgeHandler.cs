using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.Authorization
{
    public class CheckAgeHandler : AuthorizationHandler<CheckAge>
    {
        private readonly ILogger _logger;

        public CheckAgeHandler(ILogger logger)
        {
            this._logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CheckAge requirement)
        {
            var dateOfBirth =  DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);

            var userEmail = context.User.FindFirst(u => u.Type == ClaimTypes.Name).Value;

            if (dateOfBirth.AddYears(requirement.MinAge) > DateTime.Today)
            {
                _logger.LogInformation("Your age is good , open the gate ");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("Back later  , when you grow up");
            }

            return Task.CompletedTask;
        }
    }
}
