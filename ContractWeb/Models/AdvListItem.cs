using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 影院广告情况
    /// </summary>
    public class AdvListItem
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 影院编号
        /// </summary>
        public int cinemaID { get; set; }

        /// <summary>
        /// 影院名称
        /// </summary>
        public string cinemaName { get; set; }

        /// <summary>
        /// 广告版本
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// 下单时间(起)
        /// </summary>
        public DateTime begintime { get; set; }

        /// <summary>
        /// 下单时间(止)
        /// </summary>
        public DateTime endtime { get; set; }

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
        public string contractName { get; set; }
    }
}