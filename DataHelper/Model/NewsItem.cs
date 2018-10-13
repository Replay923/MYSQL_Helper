using DataHelper.Attributes;
using DataHelper.Entity;
using System;

namespace DataHelper.Model
{
    [TableName("newsitem")]
    public class NewsItem
    {
        /// <summary>
        /// 主键
        /// typeHash+pageIndex+index
        /// </summary>
        [Identity]
        [PrimaryKey]
        public string id { get; set; }
        public int pageIndex { get; set; }
        /// <summary>
        /// 外键
        /// 记录应用名+新闻类型，Hash[LiveLeakFeatured || LiveLeakFeatured || LiveLeakFeatured]
        /// </summary>
        public string typeHash { get; set; }
        public DateTime time { get; set; }
        public int index { get; set; }
        public string title { get; set; }
        public string titleImg { get; set; }
        public string linkUrl { get; set; }
        public string desc { get; set; }
        public string author { get; set; }
        public bool isDone { get; set; }
        [FieldName(null)]
        public int pageSize { get; set; }
    }
}
