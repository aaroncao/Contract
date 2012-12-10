using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 影厅信息
    /// </summary>
    public class CinemaRoom
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 影院ID
        /// </summary>
        public int cinemaID { get; set; }

        /// <summary>
        /// 影院名称
        /// </summary>
        public string cinema { get; set; }

        /// <summary>
        /// 厅名
        /// </summary>
        public string room { get; set; }

        /// <summary>
        /// 类型ID
        /// </summary>
        public int typeID { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }
    }
}