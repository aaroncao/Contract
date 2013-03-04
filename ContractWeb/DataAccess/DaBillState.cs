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
    public class DaBillState
    {
        /// <summary>
        /// 获取发票状态列表
        /// </summary>
        /// <returns></returns>
        public IList<BillState> getList()
        {
            string strSql = "select id, name from BillState";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<BillState> list = DynamicBuilder<BillState>.ConvertToList(dr);
            return list;
        }
    }
}