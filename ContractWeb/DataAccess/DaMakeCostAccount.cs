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
    public class DaMakeCostAccount
    {
        /// <summary>
        /// 添加结算
        /// </summary>
        /// <param name="en">结算实体</param>
        /// <returns></returns>
        public int add(MakeCostAccount en)
        {
            string strSql = "insert into MakeCostAccount (orderID, money, state, date, memo) values (@orderID, @money, @state, @date, @memo)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@orderID", en.orderID),
                new SqlParameter("@money", en.money),
                new SqlParameter("@state", en.state),
                new SqlParameter("@date", en.date),
                new SqlParameter("@memo", en.memo)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
    }
}