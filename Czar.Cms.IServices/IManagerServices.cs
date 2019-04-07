/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：andy chen                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2019-04-06 18:52:22                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.IServices                                   
*│　接口名称： IManagerRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;
using Czar.Cms.Models;
using Czar.Cms.ViewModels;

namespace Czar.Cms.IServices
{
    public interface IManagerService
    {
        /// <summary>
        /// 登录操作,成功则写日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Manager SignIn(LoginModel model);

    }
}