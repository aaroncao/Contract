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
    public class DaContractBinding
    {
        #region 获取绑定人员
        /// <summary>
        /// 获取绑定人员
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<string> getList(string userID)
        {
            List<string> result = new List<string>();
            string strSql = "select personID from ContractBinding where userID=" + userID + " ";
            DataTable dt = SqlHelper.ExecuteDataset(BaseHelper.DBConnStr, CommandType.Text, strSql).Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                result.Add(row["personID"].ToString());
            }

            return result;
        }
        #endregion


        #region 设置绑定人员
        /// <summary>
        /// 设置绑定人员
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="persons"></param>
        /// <returns></returns>
        public int update(string userID, string[] persons)
        {
            string strSql = "delete from ContractBinding where userID=" + userID + " ";

            for (int i = 0; i < persons.Length; i++)
            {
                strSql += "insert into ContractBinding values (" + userID + ", " + persons[i] + ") ";
            }

            return SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql);
        }
        #endregion
    }
}