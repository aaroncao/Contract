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
    public class DaChannel
    {
        /// <summary>
        /// 获取渠道类别列表
        /// </summary>
        /// <returns></returns>
        public IList<Channel> getList()
        {
            string strSql = "select id, name, memo from Channel";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<Channel> list = DynamicBuilder<Channel>.ConvertToList(dr);
            return list;
        }

        /// <summary>
        /// 添加渠道类别
        /// </summary>
        /// <param name="en">渠道</param>
        /// <returns></returns>
        public int add(Channel en)
        {
            string strSql = "insert into Channel (name, memo) values (@name, @memo)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@name", en.name),
                new SqlParameter("@memo", en.memo)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }

        /// <summary>
        /// 删除渠道类别
        /// </summary>
        /// <param name="en">渠道</param>
        /// <returns></returns>
        public int delete(Channel en)
        {
            string strSql = "delete from Channel where id=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", en.id)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
    }
}