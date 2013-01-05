using System;
using System.Collections.Generic;
using System.Web;

using ContractWeb.Models;

namespace ContractWeb.Models
{
    /// <summary>
    /// 合同和订单综合实体
    /// </summary>
    public class ContractOrder
    {
        /// <summary>
        /// 合同
        /// </summary>
        public ContractInfo contract { get; set; }

        /// <summary>
        /// 订单
        /// </summary>
        public OrderInfo order { get; set; }
    }
}