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
    public class DaContractBinding
    {
        #region 设置权限
        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int update(List<PowerGroupPower> list)
        {
            SqlParameter[] param = null;
            string strSql = "update PowerGroupPower set power=@power where groupID=@group and moduleID=@module";

            for (int i = 0; i < list.Count; i++)
            {
                param = new SqlParameter[]
                {
                    new SqlParameter("@power", list[i].power),
                    new SqlParameter("@group", list[i].groupID),
                    new SqlParameter("@module", list[i].moduleID)
                };

                SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            }

            return 1;
        }
        #endregion
    }
}