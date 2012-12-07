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
    public class DaPowerGroup
    {
        /// <summary>
        /// 获取权限组列表
        /// </summary>
        /// <returns></returns>
        public IList<PowerGroup> getGroupList()
        {
            string strSql = "select id, name from PowerGroup";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<PowerGroup> list = DynamicBuilder<PowerGroup>.ConvertToList(dr);
            return list;
        }
    }
}