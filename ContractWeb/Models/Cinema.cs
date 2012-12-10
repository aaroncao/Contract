using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 影院
    /// </summary>
    public class Cinema
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 影院名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 地区ID
        /// </summary>
        public int areaID { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public string area { get; set; }

        /// <summary>
        /// 厅数
        /// </summary>
        public int roomNum { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string person { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string tel { get; set; }

        /// <summary>
        /// 影院地址
        /// </summary>
        public string address { get; set; }
    }
}