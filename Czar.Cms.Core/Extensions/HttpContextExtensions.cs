using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Czar.Cms.Core.Extensions
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientUserIp(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }

            return ip;
        }

        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return request.Headers.ContainsKey("X-Requested-With") &&
                   request.Headers["X-Requested-With"].Equals("XMLHttpRequest");

        }
    }

   
}