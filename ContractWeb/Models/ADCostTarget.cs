using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 广告费结算对象
    /// </summary>
    public class ADCostTarget
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 对象名称
        /// </summary>
        public string target { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }

    }
}