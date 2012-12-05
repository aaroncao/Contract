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
        public IList<UserInfo> getUserList()
        {

            string strSql = "select * from UserInfo";
            SqlParameter[] param = { new SqlParameter{"@userID", "sfsd"}};

            IDataReader dr = null;
            dr = SqlHelper.ExecuteReader("", CommandType.Text, strSql, param);
            IList<UserInfo> list = DynamicBuilder<UserInfo>.ConvertToList(dr);

            return list;
        }
    }
}