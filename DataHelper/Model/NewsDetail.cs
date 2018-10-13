using DataHelper.Attributes;
using DataHelper.Entity;

namespace DataHelper.Model
{
    [TableName("newsdetail")]
    public class NewsDetail
    {
        /// <summary>
        /// 主键+外键 NewsItem.id
        /// </summary>
        [Identity]
        [PrimaryKey("id")]
        public string id { get; set; }

        public string title { get; set; }

        public string author { get; set; }

        /// <summary>
        /// 文字中插入 audio/img 标记。例：呵呵呵````Audio_1(id)````啦啦啦````Img_1(id)````LOLOLOL
        /// 以 " ```` "为分隔符
        /// </summary>
        public string contentText { get; set; }

        /// <summary>
        /// Html形式记录，<body>文章<iframe class="iframeStyle">媒体</iframe></body>
        /// </summary>
        public string contentHtml { get; set; }

        /// <summary>
        /// 文字中是否存在媒体，视频/音频或图片。默认为false
        /// </summary>
        public bool existMedia { get; set; }
    }
}
