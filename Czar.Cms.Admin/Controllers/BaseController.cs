using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Czar.Cms.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Czar.Cms.Admin.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        protected string ToErrorString(ModelStateDictionary modelState,string split)
        {
            if (split.IsNullOrEmpty())
            {
                split = "||";
            }
            StringBuilder errinfo=new StringBuilder();
            foreach (var s in modelState.Values)
            {
                foreach (var p in s.Errors)
                {
                    errinfo.AppendFormat("{0}{1}", p.ErrorMessage, split);
                }
                
            }

            if (errinfo.Length > split.Length)
            {
                errinfo.Remove(errinfo.Length - 2, split.Length);
            }
            return errinfo.ToString();
        }
    }
}