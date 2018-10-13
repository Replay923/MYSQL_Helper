using DataHelper.Attributes;
using System;

namespace DataHelper.Entity
{
    /// <summary>
    /// 视图基类，表明了该类为一个sql的查询结果
    /// </summary>
    public class BaseView
    {
        /// <summary>
        /// Id
        /// </summary>
        [Identity]
        [PrimaryKey]
        public virtual string id { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        [FieldName(null)]
        [Check(@"^[a-zA-Z\d]{0,500}$")]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime? ModifyDateTime { get; set; }
    }
}
