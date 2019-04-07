using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Czar.Cms.Admin.Validation;
using Czar.Cms.Core.Extensions;
using Czar.Cms.Core.Helper;
using Czar.Cms.IServices;
using Czar.Cms.ViewModels;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace Czar.Cms.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly string CaptchaCodeSessionName = "CaptchaCode";
        private readonly string ManagerSignInErrorTimes = "ManagerSignErrorTimes";
        private readonly int MaxErrorTimes = 3;
        private readonly IManagerService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AccountController> logger;

        public AccountController(IManagerService service,IHttpContextAccessor httpContextAccessor, ILogger<AccountController> _logger)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            this.logger = _logger;
        }


        public IActionResult Index()
        {
            this.logger.LogInformation("这是登录的方法");
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken,Route("Account/SignIn")]
        public async Task<string> SignInAsync(LoginModel model)
        {
            BaseResult result=new BaseResult(){ReturnUrl = model.ReturnUrl};
            if (!ValidateCaptchaCode(model.CaptchaCode))
            {
                result.ResultCode = ResultCodeAddMsgKeys.SignInCaptchaCodeErrorCode;
                result.ResultMsg = ResultCodeAddMsgKeys.SignInCaptchaCodeErrorMsg;
                return JsonHelper.ObjectToJson(result);
            }

            var ErrorTimes = HttpContext.Session.GetInt32(ManagerSignInErrorTimes);
            if (ErrorTimes == null)
            {
                HttpContext.Session.SetInt32(ManagerSignInErrorTimes, 1);
                ErrorTimes = 1;
            }
            else
            {
                HttpContext.Session.SetInt32(ManagerSignInErrorTimes,ErrorTimes.Value+1);
            }

            if (ErrorTimes > MaxErrorTimes)
            {
                result.ResultCode = ResultCodeAddMsgKeys.SignInErrorTimesOverTimesCode;
                result.ResultMsg = ResultCodeAddMsgKeys.SignInErrorTimesOverTimesMsg;
                return JsonHelper.ObjectToJson(result);
            }
            LoginModelValidation validation=new LoginModelValidation();
            ValidationResult results = validation.Validate(model);
            if (!results.IsValid)
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = results.ToString("||");
            }

            model.Ip = HttpContext.GetClientUserIp();
            var manager = _service.SignIn(model);
            if (manager == null)
            {
                result.ResultCode = ResultCodeAddMsgKeys.SignInPasswordOrUserNameErrorCode;
                result.ResultMsg = ResultCodeAddMsgKeys.SignInPasswordOrUserNameErrorMsg;
            }else if (manager.IsLock)
            {
                result.ResultCode = ResultCodeAddMsgKeys.SignInUserLockedCode;
                result.ResultMsg = ResultCodeAddMsgKeys.SignInUserLockedMsg;
            }
            else
            {
                var claims=new List<Claim>
                {
                    new Claim(ClaimTypes.Name,manager.UserName),
                    new Claim(ClaimTypes.Role,manager.RoleId.ToString()),
                    new Claim("NickName",manager.NickName??"匿名"),
                    new Claim("Id",manager.Id.ToString()),
                    new Claim("LoginCount",manager.LoginCount.ToString()),
                    new Claim("LoginLastIp",manager.LoginLastIp),
                    new Claim("LoginLastTime",manager.LoginLastTime?.ToString("yyyy-MM-dd HH:mm:ss")),
                };
                var clainsIdentity=new ClaimsIdentity(
                    claims,CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(clainsIdentity));
                _httpContextAccessor.HttpContext.Session.SetInt32("Id",manager.Id);
                _httpContextAccessor.HttpContext.Session.SetInt32("RoleId",manager.RoleId);
                _httpContextAccessor.HttpContext.Session.SetString("NickName",manager.NickName??"匿名");
                _httpContextAccessor.HttpContext.Session.SetString("Email",manager.Email??"");
                _httpContextAccessor.HttpContext.Session.SetString("Avatar",manager.Avatar??"/images/userface1.jpg");
                _httpContextAccessor.HttpContext.Session.SetString("Mobile",manager.Mobile??"");
            }


            return JsonHelper.ObjectToJson(result);
        }

        [Route("Account/SignOut")]
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }



        public IActionResult GetCaptchaImage()
        {
            string captchaCode = CaptchaHelper.GenerateCaptchaCode();
            var result = CaptchaHelper.GetImage(116, 36, captchaCode);
            HttpContext.Session.SetString(CaptchaCodeSessionName,captchaCode);
            return new FileStreamResult(new MemoryStream(result.CaptchaByteData),"image/png");
        }

        private bool ValidateCaptchaCode(string userInputCaptcha)
        {
            var isValid = userInputCaptcha.Equals(HttpContext.Session.GetString(CaptchaCodeSessionName),
                StringComparison.OrdinalIgnoreCase);
            HttpContext.Session.Remove(CaptchaCodeSessionName);
            return isValid;
        }
    }
}