using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 合同状态
    /// </summary>
    public class ContractState
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