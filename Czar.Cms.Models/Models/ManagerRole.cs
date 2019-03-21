/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：andy chen                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2019-03-21 22:06:27                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Czar.Cms.Models                                  
*│　类    名：ManagerRole                                     
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Czar.Cms.Models
{
	/// <summary>
	/// andy chen
	/// 2019-03-21 22:06:27
	/// 
	/// </summary>
	public partial class ManagerRole
	{
      		[Key]
		public Int32 Id{get;set;}

		[Required]
		[MaxLength(64)]
		public String RoleName {get;set;}

		[Required]
		[MaxLength(10)]
		public Int32 RoleType {get;set;}

		[Required]
		[MaxLength(1)]
		public Boolean IsSystem {get;set;}

		[Required]
		[MaxLength(10)]
		public Int32 AddManagerId {get;set;}

		[Required]
		[MaxLength(23)]
		public DateTime AddTime {get;set;}

		[MaxLength(10)]
		public Int32? ModifyManagerId {get;set;}

		[MaxLength(23)]
		public DateTime? ModifyTime {get;set;}

		[Required]
		[MaxLength(1)]
		public Boolean IsDelete {get;set;}

		[MaxLength(128)]
		public String Remark {get;set;}



	}

}
