using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 发票(收)
    /// </summary>
    public class ReceiveBill
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 下单编号
        /// </summary>
        public string orderID { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string contractID { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string contractName { get; set; }

        /// <summary>
        /// 签署金额
        /// </summary>
        public double contractMoney { get; set; }

        /// <summary>
        /// 类型编号
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// 结算对象ID
        /// </summary>
        public int targetID { get; set; }

        /// <summary>
        /// 结算对象名称
        /// </summary>
        public string targetName { get; set; }

        /// <summary>
        /// 开票金额
        /// </summary>
        public double money { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime date { get; set; }
    }
}