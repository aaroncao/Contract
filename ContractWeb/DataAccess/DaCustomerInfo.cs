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
    public class DaCustomerInfo
    {
        #region 获取客户有效期
        /// <summary>
        /// 获取客户有效期
        /// </summary>
        /// <returns></returns>
        public string getCustomerValidity()
        {
            string result = "";
            string strSql = "select validity from CustomerValidity";

            DataSet ds = SqlHelper.ExecuteDataset(BaseHelper.DBConnStr, CommandType.Text, strSql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = ds.Tables[0].Rows[0]["validity"].ToString();
                ds.Dispose();
            }

            return result;
        }
        #endregion

        #region 设置客户有效期
        /// <summary>
        /// 设置客户有效期
        /// </summary>
        /// <param name="days">天数</param>
        /// <returns></returns>
        public int setCustomerValidity(string days)
        {
            string strSql = "if exists (select validity from CustomerValidity) "
                                + "update CustomerValidity set validity=@day "
                                + "else "
                                + "insert into CustomerValidity values (@day) ";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@day", days)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
        #endregion

        #region 获取客户渠道类型列表
        /// <summary>
        /// 获取客户渠道类型列表
        /// </summary>
        /// <returns></returns>
        public IList<ChannelType> getChannelTypeList()
        {
            string strSql = "select id, name from ChannelType";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<ChannelType> list = DynamicBuilder<ChannelType>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 获取客户资料列表
        /// <summary>
        /// 获取客户资料列表
        /// </summary>
        /// <returns></returns>
        public IList<CustomerInfo> getList(string userID)
        {
            string strSql = "select a.id, a.name, "
            + "a.channelTypeID, (select z.name from ChannelType z where z.id=a.channelTypeID) as channelType, "
            + "a.person, a.tel, a.officeTel, a.email, a.fex, a.address, "
            + "a.salesmanID, (select z.name from UserInfo z where z.id=a.salesmanID) as salesman, a.memo, a.mDate, "
            + "a.stateID, (select z.state from CustomerState z where z.id=a.stateID) as state "
            + "from CustomerInfo a, ContractBinding b where a.salesmanID=b.personID and b.userID=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", userID)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<CustomerInfo> list = DynamicBuilder<CustomerInfo>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 获取客户资料列表
        /// <summary>
        /// 获取客户资料列表
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public IList<CustomerInfo> getList(CustomerInfo en, string userID)
        {
            string strSql = "select a.id, a.name, "
                + "a.channelTypeID, (select z.name from ChannelType z where z.id=a.channelTypeID) as channelType, "
                + "a.person, a.tel, a.officeTel, a.email, a.fex, a.address, "
                + "a.salesmanID, (select z.name from UserInfo z where z.id=a.salesmanID) as salesman, a.memo, a.mDate, "
                + "a.stateID, (select z.state from CustomerState z where z.id=a.stateID) as state "
                + "from CustomerInfo a, ContractBinding b where a.salesmanID=b.personID and b.userID=@id ";

            if (en.name.Trim() != "")
                strSql += " and a.name like '%" + en.name + "%' ";

            if (en.channelTypeID != 0)
                strSql += " and a.channelTypeID=" + en.channelTypeID + " "; 

            if (en.salesmanID != 0)
                strSql += " and a.salesmanID=" + en.salesmanID + " ";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", userID)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<CustomerInfo> list = DynamicBuilder<CustomerInfo>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 新增客户
        /// <summary>
        /// 新增客户
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int add(CustomerInfo en)
        {
            string strSql = "insert into CustomerInfo (name, channelTypeID, person, tel, officeTel, email, fex, address, salesmanID, mDate, stateID) "
                + "values (@name, @type, @person, @tel, @officeTel, @email, @fex, @address, @salesmanID, @mDate, @state)";
            
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@name", en.name);
            param[1] = new SqlParameter("@type", en.channelTypeID);

            if (en.person.Trim() == "")
                param[2] = new SqlParameter("@person", System.DBNull.Value);
            else
                param[2] = new SqlParameter("@person", en.person);

            param[3] = new SqlParameter("@tel", en.tel);

            if (en.officeTel.Trim() == "")
                param[4] = new SqlParameter("@officeTel", System.DBNull.Value);
            else
                param[4] = new SqlParameter("@officeTel", en.officeTel);

            if (en.email.Trim() == "")
                param[5] = new SqlParameter("@email", System.DBNull.Value);
            else
                param[5] = new SqlParameter("@email", en.email);

            if (en.fex.Trim() == "")
                param[6] = new SqlParameter("@fex", System.DBNull.Value);
            else
                param[6] = new SqlParameter("@fex", en.fex);

            if (en.address.Trim() == "")
                param[7] = new SqlParameter("@address", System.DBNull.Value);
            else
                param[7] = new SqlParameter("@address", en.address);

            param[8] = new SqlParameter("@salesmanID", en.salesmanID);
            param[9] = new SqlParameter("@mDate", DateTime.Now);

            if (en.stateID.Trim() == "")
                param[10] = new SqlParameter("@state", System.DBNull.Value);
            else
                param[10] = new SqlParameter("@state", en.stateID);

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
        #endregion

        #region 修改客户
        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int update(CustomerInfo en)
        {
            string strSql = "update CustomerInfo set name=@name, channelTypeID=@type, person=@person, tel=@tel, officeTel=@officeTel, email=@email, fex=@fex, address=@address, stateID=@state where id=@id";

            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@name", en.name);
            param[1] = new SqlParameter("@type", en.channelTypeID);

            if (en.person.Trim() == "")
                param[2] = new SqlParameter("@person", System.DBNull.Value);
            else
                param[2] = new SqlParameter("@person", en.person);

            param[3] = new SqlParameter("@tel", en.tel);

            if (en.officeTel.Trim() == "")
                param[4] = new SqlParameter("@officeTel", System.DBNull.Value);
            else
                param[4] = new SqlParameter("@officeTel", en.officeTel);

            if (en.email.Trim() == "")
                param[5] = new SqlParameter("@email", System.DBNull.Value);
            else
                param[5] = new SqlParameter("@email", en.email);

            if (en.fex.Trim() == "")
                param[6] = new SqlParameter("@fex", System.DBNull.Value);
            else
                param[6] = new SqlParameter("@fex", en.fex);

            if (en.address.Trim() == "")
                param[7] = new SqlParameter("@address", System.DBNull.Value);
            else
                param[7] = new SqlParameter("@address", en.address);

            if (en.stateID.Trim() == "")
                param[8] = new SqlParameter("@state", System.DBNull.Value);
            else
                param[8] = new SqlParameter("@state", en.stateID);

            param[9] = new SqlParameter("@id", en.id);

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
        #endregion

        #region 删除客户
        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int delete(CustomerInfo en)
        {
            string strSql = "delete from CustomerInfo where id=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", en.id)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
        #endregion
    }
}