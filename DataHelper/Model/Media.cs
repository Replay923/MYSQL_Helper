using DataHelper.Attributes;
using DataHelper.Entity;

namespace DataHelper.Model
{
    /// <summary>
    /// 用来记录动态改变的 媒体信息
    /// </summary>
    [TableName("media")]
    public class Media
    {
        /// <summary>
        /// 主键
        /// 文章id+_index 
        /// </summary>
        [Identity]
        [PrimaryKey("id")]
        public string id { get; set; }
        /// <summary>
        /// 文章id
        /// </summary>
        public string newsId { get; set; }
        /// <summary>
        /// 下标
        /// </summary>
        public int index { get; set; }
        /// <summary>
        /// 资源地址
        /// </summary>
        public string sourceUrl { get; set; }

        /// <summary>
        /// 类型，1：视频/音频。2：图片
        /// </summary>
        public int type { get; set; }
    }
}
