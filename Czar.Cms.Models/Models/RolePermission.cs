/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：andy chen                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2019-04-06 18:52:22                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Czar.Cms.Models                                  
*│　类    名：RolePermission                                     
*└──────────────────────────────────────────────────────────────┘
*/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Czar.Cms.Models
{
	/// <summary>
	/// andy chen
	/// 2019-04-06 18:52:22
	/// 
	/// </summary>
	public partial class RolePermission
	{
  		[Key]
		public Int32 Id{get;set;}

		[Required]
		[MaxLength(10)]
		public Int32 RoleId {get;set;}

		[Required]
		[MaxLength(10)]
		public Int32 MenuId {get;set;}

		[MaxLength(128)]
		public String Permission {get;set;}


	}
}
