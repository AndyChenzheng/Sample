using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample05.Models
{
    public class ContentWithComment
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string ContentString { get; set; }

        /// <summary>
        /// 状态 1 正常 0 删除
        /// </summary>
        public int Status { get; set; } = 1;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public override string ToString()
        {
            return
                $"ID={this.Id},Title={this.Title},ContentString={this.ContentString},Status={this.Status},AddTime={this.AddTime.ToString()},ModifyTime={(this.ModifyTime == null ? string.Empty : this.ModifyTime.ToString())},Comment的条数是{this.Comments.Count()}";
        }
    }
}