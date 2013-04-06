using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 用户权限组
    /// </summary>
    public class UserGroup
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string userID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 权限组ID
        /// </summary>
        public string groupID { get; set; }

        /// <summary>
        /// 权限组名称
        /// </summary>
        public string groupName { get; set; }
    }
}