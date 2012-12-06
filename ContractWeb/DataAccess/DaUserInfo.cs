using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using ContractWeb.Models;
using ContractWeb.Common;
using ND.Lib.Data.SqlHelper;

namespace ContractWeb.DataAccess
{
    public class DaUserInfo
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public IList<UserInfo> getUserList()
        {
            string strSql = "select id, userID, powergroupID, state, name, sex, card, tel, address, date from UserInfo";

            IDataReader dr = null;
            dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<UserInfo> list = DynamicBuilder<UserInfo>.ConvertToList(dr);
            return list;
        }
    }
}