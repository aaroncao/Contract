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
    public class DaCustomerState
    {
        /// <summary>
        /// 获取状态列表
        /// </summary>
        /// <returns></returns>
        public IList<CustomerState> getList()
        {
            string strSql = "select id, state, memo from CustomerState";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<CustomerState> list = DynamicBuilder<CustomerState>.ConvertToList(dr);
            return list;
        }

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="en">状态</param>
        /// <returns></returns>
        public int add(CustomerState en)
        {
            string strSql = "insert into CustomerState (state, memo) values (@state, @memo)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@state", en.state),
                new SqlParameter("@memo", en.memo)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }

        /// <summary>
        /// 删除状态
        /// </summary>
        /// <param name="en">状态</param>
        /// <returns></returns>
        public int delete(CustomerState en)
        {
            string strSql = "delete from CustomerState where id=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", en.id)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }

    }
}