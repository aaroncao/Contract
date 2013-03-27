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
        #region 获取渠道类别列表
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
        #endregion

        #region 添加渠道类别
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
        #endregion

        #region 修改渠道类别
        /// <summary>
        /// 修改渠道类别
        /// </summary>
        /// <param name="en">渠道</param>
        /// <returns></returns>
        public int update(Channel en)
        {
            string strSql = "update Channel set name=@name, memo=@memo where id=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", en.id),
                new SqlParameter("@name", en.name),
                new SqlParameter("@memo", en.memo)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
        #endregion

        #region 删除渠道类别
        /// <summary>
        /// 删除渠道类别
        /// </summary>
        /// <param name="en">渠道</param>
        /// <returns></returns>
        public int delete(Channel en)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", en.id)
            };

            string strSql = "select count(id) from ContractInfo where channelID=@id";
            DataTable dt = SqlHelper.ExecuteDataset(BaseHelper.DBConnStr, CommandType.Text, strSql, param).Tables[0];

            if (dt != null && Convert.ToInt32(dt.Rows[0][0]) > 0)
                return 0;

            strSql = "delete from Channel where id=@id";
            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
        #endregion
    }
}