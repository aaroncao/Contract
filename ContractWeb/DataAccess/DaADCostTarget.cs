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
    public class DaADCostTarget
    {
        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <returns></returns>
        public IList<ADCostTarget> getList()
        {
            string strSql = "select id, target, memo from ADCostTarget";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<ADCostTarget> list = DynamicBuilder<ADCostTarget>.ConvertToList(dr);
            return list;
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="en">对象</param>
        /// <returns></returns>
        public int add(ADCostTarget en)
        {
            string strSql = "insert into ADCostTarget (target, memo) values (@target, @memo)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@target", en.target),
                new SqlParameter("@memo", en.memo)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="en">对象</param>
        /// <returns></returns>
        public int delete(ADCostTarget en)
        {
            string strSql = "delete from ADCostTarget where id=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", en.id)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
    }
}