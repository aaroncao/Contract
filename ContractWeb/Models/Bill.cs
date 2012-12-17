using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 发票(开)
    /// </summary>
    public class Bill
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
        /// 开票金额
        /// </summary>
        public double money { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime date { get; set; }
    }
}