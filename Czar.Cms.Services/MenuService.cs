/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：andy chen                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2019-03-24 16:16:14                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Services                                  
*│　类    名： MenuService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using Czar.Cms.IRepository;
using Czar.Cms.IServices;
using Czar.Cms.ViewModels;
using Czar.Cms.ViewModels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Czar.Cms.Core.Extensions;

namespace Czar.Cms.Services
{
    public class MenuService: IMenuService
    {
        private readonly IMenuRepository _repository;

        public MenuService(IMenuRepository repository)
        {
            _repository = repository;
        }

        public TableDataModel LoadData(MenuRequestModel model)
        {
            string conditions = " where IsDelete=0";
            if (!model.Key.IsNullOrWhiteSpace())
            {
                conditions += $" and DisplayName like '%@Key%'";
            }

            return new TableDataModel
            {
                count = _repository.RecordCount(conditions),
                data=_repository.GetListPaged(model.Page,model.Limit,conditions,"Id desc",new {Key=model.Key}).ToList(),

            };

           
        }
    }
}