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
    public class DaMakeCostTarget
    {
        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <returns></returns>
        public IList<MakeCostTarget> getList()
        {
            string strSql = "select id, target, memo from MakeCostTarget";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<MakeCostTarget> list = DynamicBuilder<MakeCostTarget>.ConvertToList(dr);
            return list;
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="en">对象</param>
        /// <returns></returns>
        public int add(MakeCostTarget en)
        {
            string strSql = "insert into MakeCostTarget (target, memo) values (@target, @memo)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@target", en.target),
                new SqlParameter("@memo", en.memo)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }

        /// <summary>
        /// 修改对象
        /// </summary>
        /// <param name="en">对象</param>
        /// <returns></returns>
        public int update(MakeCostTarget en)
        {
            string strSql = "update MakeCostTarget set target=@target, memo=@memo where id=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", en.id),
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
        public int delete(MakeCostTarget en)
        {
            string strSql = "delete from MakeCostTarget where id=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", en.id)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
    }
}