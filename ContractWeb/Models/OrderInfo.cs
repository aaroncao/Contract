using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public class OrderInfo
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
        /// 订单编号
        /// </summary>
        public string orderID { get; set; }

        /// <summary>
        /// 广告费结算对象名称
        /// </summary>
        public string costTargetName { get; set; }

        /// <summary>
        /// 广告费结算对象ID
        /// </summary>
        public int costTargetID { get; set; }

        /// <summary>
        /// 下单厅数
        /// </summary>
        public int roomNum { get; set; }

        /// <summary>
        /// 下单开始时间
        /// </summary>
        public DateTime begintime { get; set; }

        /// <summary>
        /// 下单结束时间
        /// </summary>
        public DateTime endtime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 制作费用结算对象名称
        /// </summary>
        public string makeTargetName { get; set; }

        /// <summary>
        /// 制作费用结算对象ID
        /// </summary>
        public int makeTargetID { get; set; }

        /// <summary>
        /// 场次报告
        /// </summary>
        public string playReport { get; set; }

        /// <summary>
        /// 场次报告时间
        /// </summary>
        public DateTime reportTime { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime mdate { get; set; }
    }
}