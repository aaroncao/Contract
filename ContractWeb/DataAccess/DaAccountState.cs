using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using ContractWeb.Models;
using ContractWeb.Common;
using ND.Lib.Data.SqlHelper;

namespace ContractWeb.DataAccess
{
    public class DaAccountState
    {
        /// <summary>
        /// 获取状态列表
        /// </summary>
        /// <returns></returns>
        public IList<AccountState> getList()
        {
            string strSql = "select id, name from AccountState";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<AccountState> list = DynamicBuilder<AccountState>.ConvertToList(dr);
            return list;
        }
    }
}