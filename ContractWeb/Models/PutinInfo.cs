using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 投放信息
    /// </summary>
    public class PutinInfo
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
        /// 影院编号
        /// </summary>
        public int cinemaID { get; set; }

        /// <summary>
        /// 影院名称
        /// </summary>
        public string cinemaName { get; set; }

        /// <summary>
        /// 影厅编号
        /// </summary>
        public int cinemaRoomID { get; set; }

        /// <summary>
        /// 影厅名称
        /// </summary>
        public string cinemaRoomName { get; set; }

        /// <summary>
        /// 影厅属性ID
        /// </summary>
        public int roomTypeID { get; set; }

        /// <summary>
        /// 影厅属性
        /// </summary>
        public string roomType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }
    }
}


