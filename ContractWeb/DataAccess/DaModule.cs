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
    public class DaModule
    {
        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <returns></returns>
        public IList<Module> getList()
        {
            string strSql = "select id, name, [check] from SystemModule";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<Module> list = DynamicBuilder<Module>.ConvertToList(dr);
            return list;
        }
    }
}