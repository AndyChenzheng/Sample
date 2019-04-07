/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：andy chen                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2019-04-06 18:52:22                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Services                                  
*│　类    名： ManagerService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using Czar.Cms.IRepository;
using Czar.Cms.IServices;
using Czar.Cms.Models;
using Czar.Cms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Czar.Cms.Core.Helper;

namespace Czar.Cms.Services
{
    public class ManagerService: IManagerService
    {
        private readonly IManagerRepository _repository;
        private readonly IManagerRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IManagerLogRepository _managerLogRepository;

        public ManagerService(IManagerRepository repository,IManagerRoleRepository roleRepository,
            IMapper mapper,IManagerLogRepository managerLogRepository)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _managerLogRepository = managerLogRepository;
        }

        public Manager SignIn(LoginModel model)
        {
            model.Password = AESEncryptHelper.Encode(model.Password.Trim(),CzarCmsKeys.AesEncryptKey);
            model.UserName = model.UserName.Trim();
            string conditions = " where IsDelete=0";
            conditions += $" and (UserName=@UserName or Mobile=@UserName or Email=@UserName) and Password=@Password";
            var manager = _repository.GetList(conditions, model).FirstOrDefault();
            if (manager != null)
            {
                manager.LoginLastIp = model.Ip;
                manager.LoginCount += 1;
                manager.LoginLastTime=DateTime.Now;
                _repository.Update(manager);
                _managerLogRepository.Insert(new ManagerLog()
                {
                    ActionType = CzarCmsEnums.ActionEnum.SignIn.ToString(),
                    AddManageId = manager.Id,
                    AddManagerNickName = manager.NickName,
                    AddTime = DateTime.Now,
                    AddIp = model.Ip,
                    Remark = "用户登录"
                        
                });
            }

            return manager;
        }
    }
}