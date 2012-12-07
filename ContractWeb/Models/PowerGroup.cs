using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 权限组实体
    /// </summary>
    public class PowerGroup
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 组名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }
    }
}