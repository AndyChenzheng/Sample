/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：andy chen                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2019-03-21 22:06:27                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Czar.Cms.Models                                  
*│　类    名：Manager                                     
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
	public partial class Manager
	{
      		[Key]
		public Int32 Id{get;set;}

		[Required]
		[MaxLength(10)]
		public Int32 RoleId {get;set;}

		[Required]
		[MaxLength(32)]
		public String UserName {get;set;}

		[Required]
		[MaxLength(128)]
		public String Password {get;set;}

		[MaxLength(256)]
		public String Avatar {get;set;}

		[MaxLength(32)]
		public String NickName {get;set;}

		[MaxLength(16)]
		public String Mobile {get;set;}

		[MaxLength(128)]
		public String Email {get;set;}

		[MaxLength(10)]
		public Int32? LoginCount {get;set;}

		[MaxLength(64)]
		public String LoginLastIp {get;set;}

		[MaxLength(23)]
		public DateTime? LoginLastTime {get;set;}

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
		public Boolean IsLock {get;set;}

		[Required]
		[MaxLength(1)]
		public Boolean IsDelete {get;set;}

		[MaxLength(128)]
		public String Remark {get;set;}



	}

}
