﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

using ContractWeb.Models;

namespace ContractWeb.Common
{
    /// <summary>
    /// 基本工具类
    /// </summary>
    public static class BaseHelper
    {
        /// <summary>
        /// 数据库链接
        /// </summary>
        public static string DBConnStr = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();

        /// <summary>
        /// 当前用户
        /// </summary>
        public static UserInfo user;

    }
}