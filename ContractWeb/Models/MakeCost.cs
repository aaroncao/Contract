using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 制作费结算查询
    /// </summary>
    public class MakeCost
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string orderID { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string contractID { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 渠道类型
        /// </summary>
        public string channelName { get; set; }

        /// <summary>
        /// 广告版本
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// 签署金额
        /// </summary>
        public double contractMoney { get; set; }

        /// <summary>
        /// 结算金额
        /// </summary>
        public double money { get; set; }

        /// <summary>
        /// 打款状态
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime date { get; set; }
    }
}