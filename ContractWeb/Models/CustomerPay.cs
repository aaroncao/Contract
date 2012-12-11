using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 客户到账登记
    /// </summary>
    public class CustomerPay
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string contractID { get; set; }

        /// <summary>
        /// 下单编号
        /// </summary>
        public string orderID { get; set; }

        /// <summary>
        /// 到账金额
        /// </summary>
        public double money { get; set; }

        /// <summary>
        /// 到账日期
        /// </summary>
        public DateTime date { get; set; }

        /// <summary>
        /// 回扣
        /// </summary>
        public double backHandler { get; set; }
    }
}