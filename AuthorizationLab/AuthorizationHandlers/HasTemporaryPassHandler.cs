﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationLab.Policies;
using Microsoft.AspNetCore.Authorization;

namespace AuthorizationLab.AuthorizationHandlers
{
    public class HasTemporaryPassHandler : AuthorizationHandler<OfficeEntryRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OfficeEntryRequirement requirement)
        {
            if (!context.User.HasClaim((c => c.Type == "TemporaryBadgeExpiry" && c.Issuer == "https://contoso.com")))
            {
                return Task.CompletedTask;
            }

            var temporaryBadgeExpiry = Convert.ToDateTime(context.User
                .FindFirst(c => c.Type == "TemporaryBadgeExpiry" && c.Issuer == "https://contoso.com").Value);

            if (temporaryBadgeExpiry > DateTime.Now)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
