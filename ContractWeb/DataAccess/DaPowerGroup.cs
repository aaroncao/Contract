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
        public IList<PowerGroup> getList()
        {
            string strSql = "select id, name, memo from PowerGroup";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<PowerGroup> list = DynamicBuilder<PowerGroup>.ConvertToList(dr);
            return list;
        }

        /// <summary>
        /// 添加权限组
        /// </summary>
        /// <param name="en">权限组</param>
        /// <returns></returns>
        public int add(PowerGroup en)
        {
            string strSql = "insert into PowerGroup (name, memo) values (@name, @memo)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@name", en.name),
                new SqlParameter("@memo", en.memo)
            };

            return SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
        }

        /// <summary>
        /// 更新权限组
        /// </summary>
        /// <param name="en">权限组</param>
        /// <returns></returns>
        public int update(PowerGroup en)
        {
            string strSql = "update PowerGroup set name=@name, memo=@memo where id=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", en.id),
                new SqlParameter("@name", en.name),
                new SqlParameter("@memo", en.memo)
            };

            return SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
        }

        /// <summary>
        /// 删除权限组
        /// </summary>
        /// <param name="en">权限组</param>
        /// <returns></returns>
        public int delete(PowerGroup en)
        {
            string strSql = "delete from PowerGroup where id=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", en.id)
            };

            return SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
        }
    }
}