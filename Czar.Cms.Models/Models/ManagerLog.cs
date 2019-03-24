/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：andy chen                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2019-03-24 16:16:14                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Czar.Cms.Models                                  
*│　类    名：ManagerLog                                     
*└──────────────────────────────────────────────────────────────┘
*/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Czar.Cms.Models
{
	/// <summary>
	/// andy chen
	/// 2019-03-24 16:16:14
	/// 
	/// </summary>
	public partial class ManagerLog
	{
  		[Key]
		public Int32 Id{get;set;}

		[MaxLength(32)]
		public String ActionType {get;set;}

		[Required]
		[MaxLength(10)]
		public Int32 AddManageId {get;set;}

		[MaxLength(64)]
		public String AddManagerNickName {get;set;}

		[Required]
		[MaxLength(23)]
		public DateTime AddTime {get;set;}

		[MaxLength(64)]
		public String AddIp {get;set;}

		[MaxLength(256)]
		public String Remark {get;set;}


	}
}
