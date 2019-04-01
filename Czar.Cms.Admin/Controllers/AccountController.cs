using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Czar.Cms.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace Czar.Cms.Admin.Controllers
{
    public class AccountController : Controller
    {
        //private readonly string CaptchaCodeSessionName = "CaptchaCode";
        //private readonly string MamagerSignInErrorTimes = "ManagerSignErrorTimes";
        //private readonly int MaxErrorTimes = 3;
        //private readonly IManagerService _service;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        //public AccountController(IManagerService service,IHttpContextAccessor httpContextAccessor)
        //{
        //    _service = service;
        //    _httpContextAccessor = httpContextAccessor;
        //}

        private readonly ILogger<AccountController> logger;

        public AccountController(ILogger<AccountController> _logger)
        {
            this.logger = _logger;
        }

        public IActionResult Index()
        {
            this.logger.LogInformation("这是登录的方法");
            return View();
        }
    }
}