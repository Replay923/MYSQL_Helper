using DataHelper.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using Utils.Helpers;

namespace DataHelper.Model
{
    [TableName("page")]
    public class Page
    {
        /// <summary>
        /// 主键
        ///  hash[ appName+_pageType(LiveLeak_Featured)]
        /// </summary>
        [Identity]
        [PrimaryKey]
        public string hash { get; set; }
        public string appName { get; set; }
        public string pageType { get; set; }
        public Page()
        {}
        public Page(string name, string type)
        {
            this.appName = name;
            this.pageType = type;
            hash = HashHelper.Hash_SHA_512(name + type);
        }
    }
}
