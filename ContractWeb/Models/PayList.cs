using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 客户到账情况
    /// </summary>
    public class PayList
    {
        /// <summary>
        /// 合同编号
        /// </summary>
        public string contractID { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 签署金额
        /// </summary>
        public double money { get; set; }

        /// <summary>
        /// 渠道类型编号
        /// </summary>
        public int channelID { get; set; }

        /// <summary>
        /// 渠道类型
        /// </summary>
        public string channelName { get; set; }

        /// <summary>
        /// 经办人ID
        /// </summary>
        public int personID { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        public string personName { get; set; }

        /// <summary>
        /// 到账金额
        /// </summary>
        public double payMoney { get; set; }

        /// <summary>
        /// 到账比例
        /// </summary>
        public string payPercent { get; set; }
    }
}