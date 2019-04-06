/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：andy chen                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2019-04-06 18:52:22                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Czar.Cms.Models                                  
*│　类    名：ArticleCategory                                     
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
	public partial class ArticleCategory
	{
  		[Key]
		public Int32 Id{get;set;}

		[Required]
		[MaxLength(128)]
		public String Title {get;set;}

		[Required]
		[MaxLength(10)]
		public Int32 ParentId {get;set;}

		[MaxLength(128)]
		public String ClassList {get;set;}

		[MaxLength(10)]
		public Int32? ClassLayer {get;set;}

		[Required]
		[MaxLength(10)]
		public Int32 Sort {get;set;}

		[MaxLength(128)]
		public String ImageUrl {get;set;}

		[MaxLength(128)]
		public String SeoTitle {get;set;}

		[MaxLength(256)]
		public String SeoKeywords {get;set;}

		[MaxLength(512)]
		public String SeoDescription {get;set;}

		[Required]
		[MaxLength(1)]
		public Boolean IsDeleted {get;set;}


	}
}
