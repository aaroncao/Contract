using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 制作费结算
    /// </summary>
    public class MakeCostAccount
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
        /// 结算金额
        /// </summary>
        public double money { get; set; }

        /// <summary>
        /// 打款状态
        /// </summary>
        public int state { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime date { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }
    }
}