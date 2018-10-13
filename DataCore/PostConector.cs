using DataCore.Models;
using DataHelper.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Utils.Helpers;

namespace DataCore
{
    public class PostConector
    {
        public string operationName { get; set; }
        public string operateType { get; set; }
        public JObject body { get; set; }

        [JsonIgnore]
        public AppDb Db { get; set; }

        private OperateTable operationTable;
        private OperateType operate;

        public PostConector(AppDb db = null)
        {
            Db = db;
        }

        public void ParseParameter()
        {
            switch (operationName)
            {
                case "PagePost":
                    operationTable = OperateTable.PagePost;
                    break;
                case "NewsItemPost":
                    operationTable = OperateTable.NewsItemPost;
                    break;
                case "NewsDetailPost":
                    operationTable = OperateTable.NewsDetailPost;
                    break;
                case "MediaPost":
                    operationTable = OperateTable.MediaPost;
                    break;
            }

            switch (operateType)
            {
                case "Insert":
                    operate = OperateType.Insert;
                    break;
                case "SelectAll":
                    operate = OperateType.SelectAll;
                    break;
                case "Select":
                    operate = OperateType.Select;
                    break;
                case "Delete":
                    operate = OperateType.Delete;
                    break;
                case "Update":
                    operate = OperateType.Update;
                    break;
            }
        }
        public object DevExcute()
        {
            try
            {
                ParseParameter();
                var requisiteCmd = GetMethod();
                if (requisiteCmd == null)
                    return null;
                switch (operate)
                {
                    case OperateType.Insert:
                        requisiteCmd.Insert();
                        return null;
                    case OperateType.Select:
                        return requisiteCmd.DevSelect();
                    case OperateType.SelectAll:
                        return requisiteCmd.SelectAll();
                    case OperateType.Update:
                        requisiteCmd.Update();
                        return null;
                    case OperateType.Delete:
                        requisiteCmd.Delete();
                        return null;
                    default:
                        return null;
                }
            }
            catch (Exception e) { Log4netHelper.ErrorFormat("开发者Post Api返回错误,\r\n{0}", e.ToString()); return new PostResult(e.ToString()); }
        }

        public async Task<object> DevExcuteAsync()
        {
            try
            {
                ParseParameter();
                var requisiteCmd = GetMethod();

                if (requisiteCmd == null)
                    return null;
                switch (operate)
                {
                    case OperateType.Insert:
                        await requisiteCmd.InsertAsync();
                        return null;
                    case OperateType.Select:
                        return await requisiteCmd.DevSelectAsync();
                    case OperateType.SelectAll:
                        return await requisiteCmd.SelectAllAsync();
                    case OperateType.Update:
                        await requisiteCmd.UpdateAsync();
                        return null;
                    case OperateType.Delete:
                        await requisiteCmd.DeleteAsync();
                        return null;
                    default:
                        return null;
                }
            }
            catch (Exception e) { Log4netHelper.ErrorFormat("开发者Post Api返回错误,\r\n{0}", e.ToString()); return new PostResult(e.ToString()); }
        }
        public object Excute()
        {
            ParseParameter();
            var requisiteCmd = GetMethod();
            if (requisiteCmd == null)
                return null;
            switch (operate)
            {
                case OperateType.Insert:
                    requisiteCmd.Insert();
                    return null;
                case OperateType.Select:
                    return requisiteCmd.Select();
                case OperateType.SelectAll:
                    return requisiteCmd.SelectAll();
                case OperateType.Update:
                    requisiteCmd.Update();
                    return null;
                case OperateType.Delete:
                    requisiteCmd.Delete();
                    return null;
                default:
                    return null;
            }
        }

        public async Task<object> ExcuteAsync()
        {
            ParseParameter();
            var requisiteCmd = GetMethod();

            if (requisiteCmd == null)
                return null;
            switch (operate)
            {
                case OperateType.Insert:
                    await requisiteCmd.InsertAsync();
                    return null;
                case OperateType.Select:
                    return await requisiteCmd.SelectAsync();
                case OperateType.SelectAll:
                    return await requisiteCmd.SelectAllAsync();
                case OperateType.Update:
                    await requisiteCmd.UpdateAsync();
                    return null;
                case OperateType.Delete:
                    await requisiteCmd.DeleteAsync();
                    return null;
                default:
                    return null;
            }
        }

        public IRequisiteCmd GetMethod()
        {
            string bodyJson = body != null ? body.ToString() : null;
            IRequisiteCmd requisiteCmd = null;
            switch (operationTable)
            {
                case OperateTable.PagePost:
                    var page = new Page();
                    try
                    {
                        page = JsonHelper.JsonDeserilize<Page>(bodyJson);
                    }
                    catch (Exception e) { }
                    PagePost pagePost = new PagePost(page, Db);
                    requisiteCmd = pagePost as IRequisiteCmd;
                    break;
                case OperateTable.NewsItemPost:
                    var newsItem = new NewsItem();
                    try
                    {
                        newsItem = JsonHelper.JsonDeserilize<NewsItem>(bodyJson);
                    }
                    catch (Exception e) { }
                    NewsItemPost newsItemPost = new NewsItemPost(newsItem, Db);
                    requisiteCmd = newsItemPost as IRequisiteCmd;
                    break;
                case OperateTable.NewsDetailPost:
                    var newsDetail = new NewsDetail();
                    try
                    {
                        newsDetail = JsonHelper.JsonDeserilize<NewsDetail>(bodyJson);
                    }
                    catch (Exception e) { }
                    NewsDetailPost newsDetailPost = new NewsDetailPost(newsDetail, Db);
                    requisiteCmd = newsDetailPost as IRequisiteCmd;
                    break;
                case OperateTable.MediaPost:
                    var media = new Media();
                    try
                    {
                        media = JsonHelper.JsonDeserilize<Media>(bodyJson);
                    }
                    catch (Exception e) { }
                    MediaPost mediaPost = new MediaPost(media, Db);
                    requisiteCmd = mediaPost as IRequisiteCmd;
                    break;
                default:
                    break;
            }
            return requisiteCmd;
        }

    }
}
