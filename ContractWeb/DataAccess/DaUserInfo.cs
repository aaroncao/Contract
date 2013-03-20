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
    public class DaUserInfo
    {
        #region 获取用户列表
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public IList<UserInfo> getList()
        {
            string strSql = "select id, userID, "
                + "(case state when 1 then '使用' when 0 then '禁止' end) as state, name, "
                + "(case sex when 1 then '男' when 2 then '女' end) as sex, card, tel, address, date from UserInfo";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<UserInfo> list = DynamicBuilder<UserInfo>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 获取用户列表
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public IList<UserInfo> getList(string userID, string beginDate, string endDate)
        {
            string strSql = "select id, userID, "
                + "(case state when 1 then '使用' when 0 then '禁止' end) as state, name, "
                + "(case sex when 1 then '男' when 2 then '女' end) as sex, card, tel, address, date from UserInfo ";

            if (userID.Trim() != "" || (beginDate.Trim() != "" && endDate.Trim() != ""))
                strSql += "where ";

            if (userID.Trim() != "")
                strSql += "userID like '%" + userID + "%' ";

            if (beginDate.Trim() != "" && endDate.Trim() != "")
            {
                if (userID.Trim() != "")
                    strSql += "or ";
                strSql += "date between '" + beginDate + "' and '" + endDate + "'";
            }

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<UserInfo> list = DynamicBuilder<UserInfo>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 添加用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="en">新用户</param>
        /// <returns></returns>
        public int add(UserInfo en)
        {
            string strSql = "insert into UserInfo (userID, powergroupID, name, date) values (@userID, @group, @name, getDate())";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@userID", en.userID),
                new SqlParameter("@group", en.powergroupID),
                new SqlParameter("@name", en.name)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
        #endregion

        #region 编辑用户
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="en">用户实体</param>
        /// <returns></returns>
        public int edit(UserInfo en)
        {
            string strSql = "update UserInfo set name=@name, sex=@sex, card=@card, tel=@tel, address=@address where id=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@name", en.name),
                new SqlParameter("@sex", en.sex),
                new SqlParameter("@card", en.card),
                new SqlParameter("@tel", en.tel),
                new SqlParameter("@address", en.address),
                new SqlParameter("@id", en.id)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
        #endregion

        #region 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="en">用户实体</param>
        /// <returns></returns>
        public int delete(UserInfo en)
        {
            string strSql = "delete from UserInfo where id=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", en.id)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="en">用户</param>
        /// <param name="theOld">旧密码</param>
        /// <param name="theNew">新密码</param>
        /// <returns></returns>
        public string[] editPassword(UserInfo en, string theOld, string theNew)
        {
            string strSql = "select id from UserInfo where userID=@userID and password=@pwd";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@userID", en.userID),
                new SqlParameter("@pwd", theOld)
            };

            DataSet ds = SqlHelper.ExecuteDataset(BaseHelper.DBConnStr, CommandType.Text, strSql, param);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ds.Dispose();
                strSql = "update UserInfo set password=@pwd where userID=@userID";

                param = new SqlParameter[]
                {
                    new SqlParameter("@userID", en.userID),
                    new SqlParameter("@pwd", theNew)
                };

                int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
                if (result == 1)
                    return new string[] { "1", "修改成功" };
                else
                    return new string[] { "-1", "修改失败" };
            }
            else
            {
                return new string[] { "-1", "旧密码不对" };
            }
        }
        #endregion

        #region 登录验证
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public UserInfo checkUserID(string userID, string pwd)
        {
            UserInfo info = null;

            string strSql = "select id, userID, powergroupID, password, "
                + "(case state when 1 then '使用' when 0 then '禁止' end) as state, name, "
                + "(case sex when 1 then '男' when 2 then '女' end) as sex, card, tel, address, date "
                + "from UserInfo a "
                + "where userID=@userID and password=@pwd";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@userID", userID),
                new SqlParameter("@pwd", pwd)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<UserInfo> list = DynamicBuilder<UserInfo>.ConvertToList(dr);
            
            if (list != null && list.Count > 0)
            {
                info = list[0];
            }
            
            return info;
        }
        #endregion

    }
}