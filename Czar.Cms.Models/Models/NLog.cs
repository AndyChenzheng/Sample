/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：andy chen                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2019-04-06 18:52:22                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Czar.Cms.Models                                  
*│　类    名：NLog                                     
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
	public partial class NLog
	{
  		[Key]
		public Int32 Id{get;set;}

		[MaxLength(50)]
		public String Application {get;set;}

		[MaxLength(23)]
		public DateTime? Logged {get;set;}

		[MaxLength(50)]
		public String Level {get;set;}

		[MaxLength(512)]
		public String Message {get;set;}

		[MaxLength(250)]
		public String Logger {get;set;}

		[MaxLength(512)]
		public String Callsite {get;set;}

		[MaxLength(512)]
		public String Exception {get;set;}


	}
}
