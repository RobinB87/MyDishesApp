﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyDishesApp.WebApi.Authorization
{
    public class UserMustBeDishManagerRequirementHandler
        : AuthorizationHandler<UserMustBeDishManagerRequirement>
    {
        private readonly IDishRepository _dishInfoRepository;
        private readonly IUserInfoService _userInfoService;

        public UserMustBeDishManagerRequirementHandler(IDishRepository dishInfoRepository, IUserInfoService userInfoService)
        {
            _dishInfoRepository = dishInfoRepository;
            _userInfoService = userInfoService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserMustBeDishManagerRequirement requirement)
        {
            if (_userInfoService.Role == requirement.Role)
            {
                context.Succeed(requirement);
                return Task.FromResult(0);
            }

            var filterContext = context.Resource as AuthorizationFilterContext;
            if (filterContext == null)
            {
                context.Fail();
                return Task.FromResult(0);
            }

            var dishId = filterContext.RouteData.Values["dishId"].ToString();

            if (!Guid.TryParse(dishId, out Guid dishIdAsGuid))
            {
                context.Fail();
                return Task.FromResult(0);
            }

            context.Succeed(requirement);
            return Task.FromResult(0);
        }
    }
}