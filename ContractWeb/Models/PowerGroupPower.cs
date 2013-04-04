using System;
using System.Collections.Generic;
using System.Web;


namespace ContractWeb.Models
{
    /// <summary>
    /// 权限设置
    /// </summary>
    public class  PowerGroupPower
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 权限组ID
        /// </summary>
        public int groupID { get; set; }

        /// <summary>
        /// 模块ID
        /// </summary>
        public int moduleID { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string moduleName { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public int power { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string powerName { get; set; }
    }
}