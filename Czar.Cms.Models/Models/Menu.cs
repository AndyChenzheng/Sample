/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：andy chen                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2019-04-06 18:52:22                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Czar.Cms.Models                                  
*│　类    名：Menu                                     
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
	public partial class Menu
	{
  		[Key]
		public Int32 Id{get;set;}

		[Required]
		[MaxLength(10)]
		public Int32 ParentId {get;set;}

		[Required]
		[MaxLength(32)]
		public String Name {get;set;}

		[MaxLength(128)]
		public String DisplayName {get;set;}

		[MaxLength(128)]
		public String IconUrl {get;set;}

		[MaxLength(128)]
		public String LinkUrl {get;set;}

		[MaxLength(10)]
		public Int32? Sort {get;set;}

		[MaxLength(256)]
		public String Permission {get;set;}

		[Required]
		[MaxLength(1)]
		public Boolean IsDisplay {get;set;}

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


	}
}
