using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 结算状态
    /// </summary>
    public class AccountState
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string name { get; set; }
    }
}