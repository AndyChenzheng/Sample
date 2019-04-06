/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：andy chen                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2019-04-06 18:52:22                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Czar.Cms.Models                                  
*│　类    名：Article                                     
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
	public partial class Article
	{
  		[Key]
		public Int32 Id{get;set;}

		[Required]
		[MaxLength(10)]
		public Int32 CategoryId {get;set;}

		[Required]
		[MaxLength(128)]
		public String Title {get;set;}

		[MaxLength(128)]
		public String ImageUrl {get;set;}

		[MaxLength(2147483647)]
		public String Content {get;set;}

		[Required]
		[MaxLength(10)]
		public Int32 ViewCount {get;set;}

		[Required]
		[MaxLength(10)]
		public Int32 Sort {get;set;}

		[MaxLength(64)]
		public String Author {get;set;}

		[MaxLength(128)]
		public String Source {get;set;}

		[MaxLength(128)]
		public String SeoTitle {get;set;}

		[MaxLength(256)]
		public String SeoKeyword {get;set;}

		[MaxLength(512)]
		public String SeoDescription {get;set;}

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
		public Boolean IsTop {get;set;}

		[Required]
		[MaxLength(1)]
		public Boolean IsSlide {get;set;}

		[Required]
		[MaxLength(1)]
		public Boolean IsRed {get;set;}

		[Required]
		[MaxLength(1)]
		public Boolean IsPublish {get;set;}

		[Required]
		[MaxLength(1)]
		public Boolean IsDeleted {get;set;}


	}
}
