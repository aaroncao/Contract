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
    public class DaAreaInfo
    {
        /// <summary>
        /// 获取地区列表
        /// </summary>
        /// <returns></returns>
        public IList<AreaInfo> getList()
        {
            string strSql = "select id, name, memo from AreaInfo";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<AreaInfo> list = DynamicBuilder<AreaInfo>.ConvertToList(dr);
            return list;
        }

        /// <summary>
        /// 添加地区
        /// </summary>
        /// <param name="en">地区</param>
        /// <returns></returns>
        public int add(AreaInfo en)
        {
            string strSql = "insert into AreaInfo (name, memo) values (@name, @memo)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@name", en.name),
                new SqlParameter("@memo", en.memo)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }

        /// <summary>
        /// 删除地区
        /// </summary>
        /// <param name="en">地区</param>
        /// <returns></returns>
        public int delete(AreaInfo en)
        {
            string strSql = "delete from AreaInfo where id=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", en.id)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
    }
}