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
    public class DaContractState
    {
        /// <summary>
        /// 获取合同状态列表
        /// </summary>
        /// <returns></returns>
        public IList<ContractState> getList()
        {
            string strSql = "select id, name from ContractState";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<ContractState> list = DynamicBuilder<ContractState>.ConvertToList(dr);
            return list;
        }
    }
}