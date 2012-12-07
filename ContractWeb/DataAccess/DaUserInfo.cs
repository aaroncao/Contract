﻿using System;
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
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public IList<UserInfo> getUserList()
        {
            string strSql = "select id, userID, "
                + "(case state when 1 then '使用' when 0 then '禁止' end) as state, name, "
                + "(case sex when 1 then '男' when 2 then '女' end) as sex, card, tel, address, date from UserInfo";

            IDataReader dr = null;
            dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<UserInfo> list = DynamicBuilder<UserInfo>.ConvertToList(dr);
            return list;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int add(UserInfo en)
        {
            string strSql = "insert into UserInfo (userID, powergroupID, name) values (@userID, @group, @name)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@userID", en.userID),
                new SqlParameter("@group", en.powergroupID),
                new SqlParameter("@name", en.name)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public UserInfo checkUserID(string userID, string pwd)
        {
            UserInfo info = null;

            string strSql = "select id, userID, "
                + "(case state when 1 then '使用' when 0 then '禁止' end) as state, name, "
                + "(case sex when 1 then '男' when 2 then '女' end) as sex, card, tel, address, date from UserInfo "
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
    }
}