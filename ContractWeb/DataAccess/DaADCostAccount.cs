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
    public class DaADCostAccount
    {
        #region 添加结算
        /// <summary>
        /// 添加结算
        /// </summary>
        /// <param name="en">结算实体</param>
        /// <returns></returns>
        public int add(ADCostAccount en)
        {
            string strSql = "insert into ADCostAccount (orderID, money, state, date, memo) values (@orderID, @money, @state, @date, @memo)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@orderID", en.orderID),
                new SqlParameter("@money", en.money),
                new SqlParameter("@state", en.state),
                new SqlParameter("@date", en.date),
                new SqlParameter("@memo", en.memo)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
        #endregion

        #region 获取结算列表
        /// <summary>
        /// 获取结算列表
        /// </summary>
        /// <returns></returns>
        public IList<ADCost> getList()
        {
            string strSql = "select a.id, a.orderID, b.contractID, c.name, "
                + "(select z.name from Channel z where z.id=c.channelID) as channelName, "
                + "(select z.name from UserInfo z where z.id=c.personID) as personName, "
                + "c.money as contractMoney, "
                + "(select z.target from ADCostTarget z where z.id=b.costTargetID) as costTargetName, "
                + "a.money, (select z.name from AccountState z where z.id=a.state) as state, a.date, a.memo from ADCostAccount a, OrderInfo b, ContractInfo c where a.orderID=b.orderID and b.contractID=c.contractID ";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<ADCost> list = DynamicBuilder<ADCost>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 获取结算列表
        /// <summary>
        /// 获取结算列表
        /// </summary>
        /// <returns></returns>
        public IList<ADCost> getList(string id)
        {
            string strSql = "select a.orderID, b.contractID, c.name, "
                + "(select z.name from Channel z where z.id=c.channelID) as channelName, "
                + "(select z.name from UserInfo z where z.id=c.personID) as personName, "
                + "c.money as contractMoney, "
                + "(select z.target from ADCostTarget z where z.id=b.costTargetID) as costTargetName, "
                + "a.money, (select z.name from AccountState z where z.id=a.state) as state, a.date, a.memo from ADCostAccount a, OrderInfo b, ContractInfo c "
                + "where a.orderID=b.orderID and b.contractID=c.contractID and a.orderID=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<ADCost> list = DynamicBuilder<ADCost>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 搜索结算列表
        /// <summary>
        /// 搜索结算列表
        /// </summary>
        /// <returns></returns>
        public IList<ADCost> getList(string id, string target, string channel, string state, string begin, string end)
        {
            string strSql = "select a.id, a.orderID, b.contractID, c.name, "
                + "(select z.name from Channel z where z.id=c.channelID) as channelName, "
                + "(select z.name from UserInfo z where z.id=c.personID) as personName, "
                + "c.money as contractMoney, "
                + "(select z.target from ADCostTarget z where z.id=b.costTargetID) as costTargetName, "
                + "a.money, (select z.name from AccountState z where z.id=a.state) as state, a.date from ADCostAccount a, OrderInfo b, ContractInfo c where a.orderID=b.orderID and b.contractID=c.contractID ";

            string where = "";

            if (id.Trim() != "")
                where += "a.orderID like '%" + id + "%'";

            if (target.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "b.costTargetID=" + target;
            }

            if (channel.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "c.channelID=" + channel;
            }            

            if (state.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "a.state=" + state;
            }

            if (begin.Trim() != "" && end.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "date between '" + begin + "' and '" + end + "'";
            }

            if (where != "")
                strSql += "and " + where;

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<ADCost> list = DynamicBuilder<ADCost>.ConvertToList(dr);
            return list;
        }
        #endregion
    }
}