using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 发票状态
    /// </summary>
    public class BillState
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string name { get; set; }
    }
}