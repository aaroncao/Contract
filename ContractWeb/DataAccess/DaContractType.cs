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
    public class DaContractType
    {
        /// <summary>
        /// 获取合同列表
        /// </summary>
        /// <returns></returns>
        public IList<ContractType> getList()
        {
            string strSql = "select id, name from ContractType";
            
            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<ContractType> list = DynamicBuilder<ContractType>.ConvertToList(dr);
            return list;
        }
    }
}