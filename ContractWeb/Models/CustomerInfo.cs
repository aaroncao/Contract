using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 客户
    /// </summary>
    public class CustomerInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 渠道类型编号
        /// </summary>
        public int channelTypeID { get; set; }

        /// <summary>
        /// 渠道类型
        /// </summary>
        public string channelType { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string person { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string tel { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        public string officeTel { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string fex { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 业务员编号
        /// </summary>
        public int salesmanID { get; set; }

        /// <summary>
        /// 业务员
        /// </summary>
        public string salesman { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 录入日期
        /// </summary>
        public DateTime mDate { get; set; }

        /// <summary>
        /// 状态ID
        /// </summary>
        public string stateID { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string state { get; set; }
    }
}