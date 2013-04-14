using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    public class ContractBinding
    {
        /// <summary>
        /// 自动编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 当前用户ID
        /// </summary>
        public int userID { get; set; }

        /// <summary>
        /// 绑定用户ID
        /// </summary>
        public int personID { get; set; }
    }
}