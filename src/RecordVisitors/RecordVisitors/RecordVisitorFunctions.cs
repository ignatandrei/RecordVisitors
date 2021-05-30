﻿using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace RecordVisitors
{
    class RecordVisitorFunctions : IRecordVisitorFunctions
    {
        public IRequestRecorded GetUrl(HttpContext context)
        {
            var req = context?.Request;
            if (req == null)
                return null;

            if(req.Method !=  HttpMethods.Get)
                return null;

            var url = req.Path.Value;
            if (url == null)
                return null;
            var rr = new RequestRecorded();
            rr.URL = url;
            rr.AdditionalData = null;            
            return rr;
        }

        public Claim GetUser(HttpContext cnt)
        {


            string name = cnt.User?.Identity?.Name;
            if (name != null)
            {
                return new Claim("user", name);
            }
            return cnt.User?.Claims.FirstOrDefault();

        }

    }
}
