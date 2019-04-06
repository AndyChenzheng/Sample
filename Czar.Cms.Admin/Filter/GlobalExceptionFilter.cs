/**
 *全局异常过滤器
 */

using System.Net;
using Czar.Cms.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NLog;

namespace Czar.Cms.Admin.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext context)
        {
            logger.Error(context.Exception);

            var result = new BaseResult()
            {
                ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode,
                ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg,
            };
            context.Result=new ObjectResult(result);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.ExceptionHandled = true;
        }
    }
}