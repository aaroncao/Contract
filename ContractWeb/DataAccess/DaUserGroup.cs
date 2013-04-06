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
    public class DaUserGroup
    {
        #region 获取用户权限列表
        /// <summary>
        /// 获取用户权限列表
        /// </summary>
        /// <returns></returns>
        public IList<UserGroup> getList()
        {
            string strSql = "select a.userID, a.name as userName, a.powergroupID as groupID, b.name as groupName "
                + "from UserInfo a, PowerGroup b where a.powergroupID=b.id";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<UserGroup> list = DynamicBuilder<UserGroup>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 修改权限组
        /// <summary>
        /// 修改权限组
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int update(UserInfo en)
        {
            string strSql = "update UserInfo set powergroupID=@groupID where id=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", en.id),
                new SqlParameter("@groupID", en.powergroupID)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
        #endregion
    }
}