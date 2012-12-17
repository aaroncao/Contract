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
    public class DaBillType
    {
        /// <summary>
        /// 获取开票类型列表
        /// </summary>
        /// <returns></returns>
        public IList<BillType> getList()
        {
            string strSql = "select id, name from BillType";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<BillType> list = DynamicBuilder<BillType>.ConvertToList(dr);
            return list;
        }
    }
}