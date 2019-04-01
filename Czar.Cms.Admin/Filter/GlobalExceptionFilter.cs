/**
 *全局异常过滤器
 */
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Czar.Cms.Admin.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
       

        public void OnException(ExceptionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}