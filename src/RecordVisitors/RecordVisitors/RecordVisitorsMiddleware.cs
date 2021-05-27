﻿using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecordVisitors
{


    public class RecordVisitorsMiddleware : IMiddleware
    {
        private readonly IRecordVisitorFunctions recordVisitorFunctions;
        private readonly IUsersRepository retrieveUsersRepository;

        public RecordVisitorsMiddleware(IRecordVisitorFunctions recordVisitorFunctions,IUsersRepository retrieveUsersRepository)
        {
            this.recordVisitorFunctions = recordVisitorFunctions;
            this.retrieveUsersRepository = retrieveUsersRepository;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var claim = recordVisitorFunctions.GetUser(context);
            if (claim == null)
                return;
            UserComing?.Invoke(this, claim);
            await retrieveUsersRepository.SaveClaim(claim);
            await next(context);
        }

        public event EventHandler<Claim> UserComing;

    }
}
