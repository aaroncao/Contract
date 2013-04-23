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
    public class DaPayList
    {
        #region 获取到账列表
        /// <summary>
        /// 获取到账列表
        /// </summary>
        /// <returns></returns>
        public IList<PayList> getListByContract(string id)
        {
            string strSql = "select a.contractID, a.name, a.money, "
                + "a.channelID, (select z.name from Channel z where z.id=a.channelID) as channelName, "
                + "a.personID, (select z.name from UserInfo z where z.id=a.personID) as personName, "
                + "b.money as payMoney, null as payPercent, b.date "
                + "from ContractInfo a, CustomerPay b where a.contractID=b.contractID and a.contractID=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<PayList> list = DynamicBuilder<PayList>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 搜索到账列表
        /// <summary>
        /// 搜索到账列表
        /// </summary>
        /// <returns></returns>
        public IList<PayList> getList(string contractID, string channel, string begin, string end, string person, string userID)
        {
            string strSql = "select a.contractID, a.name, a.money, "
                + "a.channelID, (select z.name from Channel z where z.id=a.channelID) as channelName, "
                + "a.personID, (select z.name from UserInfo z where z.id=a.personID) as personName, "
                + "b.money as payMoney, null as payPercent, b.date "
                + "from ContractInfo a join ContractBinding c on a.personID=c.personID left join CustomerPay b on a.contractID=b.contractID where c.userID=@id ";

            if (contractID.Trim() != "")
                strSql += " and a.contractID='" + contractID + "' ";

            if (channel.Trim() != "")
                strSql += " and a.channelID='" + channel+ "' ";

            if (person.Trim() != "")
                strSql += " and a.personID='" + person + "' ";

            if (begin.Trim() != "" && end.Trim() != "")
                strSql += " and b.date between '" + begin + "' and '" + end + "' ";
            
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", userID)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<PayList> list = DynamicBuilder<PayList>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 搜索各种到账类型列表
        /// <summary>
        /// 搜索各种到账类型列表
        /// </summary>
        /// <returns></returns>
        public IList<PayList> getList(string contractID, string channel, string begin, string end, string person, int type, string userID)
        {
            string strSql = "select a.contractID, a.money, count(b.money) as total into #ddd "
                + "from ContractInfo a join ContractBinding c on a.personID=c.personID left join CustomerPay b on a.contractID=b.contractID where c.userID=@id ";

            if (contractID.Trim() != "")
                strSql += " and a.contractID='" + contractID + "' ";

            if (channel.Trim() != "")
                strSql += " and a.channelID='" + channel + "' ";

            if (person.Trim() != "")
                strSql += " and a.personID='" + person + "' ";

            if (begin.Trim() != "" && end.Trim() != "")
                strSql += " and b.date between '" + begin + "' and '" + end + "' ";

            strSql += " group by a.contractID, a.money ";

            strSql += "select a.contractID, a.name, a.money, "
                + "a.channelID, (select z.name from Channel z where z.id=a.channelID) as channelName, "
                + "a.personID, (select z.name from UserInfo z where z.id=a.personID) as personName, "
                + "b.money as payMoney, null as payPercent, b.date "
                + "from ContractInfo a, CustomerPay b, #ddd c where a.contractID=b.contractID and a.contractID=c.contractID ";

            switch (type)
            {
                case 0:
                    strSql += " and c.total=0 ";
                    break;
                case 1:
                    strSql += " and c.money<>c.total ";
                    break;
                case 2:
                    strSql += " and c.money=c.total ";
                    break;
            }

            strSql += " drop table #ddd ";            

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", userID)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<PayList> list = DynamicBuilder<PayList>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 获取到账总金额
        /// <summary>
        /// 获取到账总金额
        /// </summary>
        /// <returns></returns>
        public double getMoney()
        {
            string strSql = "select sum(money) from CustomerPay";
            DataTable dt = SqlHelper.ExecuteDataset(BaseHelper.DBConnStr, CommandType.Text, strSql).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
                return Convert.ToDouble(dt.Rows[0][0]);
            else
                return 0;
        }
        #endregion

        #region 搜索到账列表
        /// <summary>
        /// 搜索到账列表
        /// </summary>
        /// <returns></returns>
        public DataTable getDataTable(string contractID, string channel, string begin, string end, string person, int type, string userID)
        {
            string strSql = "";

            if (type == -1)
            {
                strSql = "select a.contractID as [合同编号], a.name as [合同名称], "
                    + "(select z.name from Channel z where z.id=a.channelID) as [渠道归类], "
                    + "(select z.name from UserInfo z where z.id=a.personID) as [业务员], "
                    + "a.money as [签署金额], b.date as [到账日期], b.money as [到账金额], null as [到账比例] "
                    + "from ContractInfo a join ContractBinding c on a.personID=c.personID left join CustomerPay b on a.contractID=b.contractID where c.userID=@id ";

                if (contractID.Trim() != "")
                    strSql += " and a.contractID='" + contractID + "' ";

                if (channel.Trim() != "")
                    strSql += " and a.channelID='" + channel + "' ";

                if (person.Trim() != "")
                    strSql += " and a.personID='" + person + "' ";

                if (begin.Trim() != "" && end.Trim() != "")
                    strSql += " and b.date between '" + begin + "' and '" + end + "' ";
            }
            else
            {
                strSql = "select a.contractID, a.money, count(b.money) as total into #ddd "
                + "from ContractInfo a join ContractBinding c on a.personID=c.personID left join CustomerPay b on a.contractID=b.contractID where c.userID=@id ";

                if (contractID.Trim() != "")
                    strSql += " and a.contractID='" + contractID + "' ";

                if (channel.Trim() != "")
                    strSql += " and a.channelID='" + channel + "' ";

                if (person.Trim() != "")
                    strSql += " and a.personID='" + person + "' ";

                if (begin.Trim() != "" && end.Trim() != "")
                    strSql += " and b.date between '" + begin + "' and '" + end + "' ";

                strSql += " group by a.contractID, a.money ";

                strSql += "select a.contractID as [合同编号], a.name as [合同名称], "
                    + "(select z.name from Channel z where z.id=a.channelID) as [渠道归类], "
                    + "(select z.name from UserInfo z where z.id=a.personID) as [业务员], "
                    + "a.money as [签署金额], b.date as [到账日期], b.money as [到账金额], null as [到账比例] "
                    + "from ContractInfo a, #ddd b where ";

                switch (type)
                {
                    case 0:
                        strSql += " b.total=0 ";
                        break;
                    case 1:
                        strSql += " a.money<>b.total ";
                        break;
                    case 2:
                        strSql += " a.money=b.total ";
                        break;
                }

                strSql += " drop table #ddd "; 
            }

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", userID)
            };

            return SqlHelper.ExecuteDataset(BaseHelper.DBConnStr, CommandType.Text, strSql, param).Tables[0];
        }
        #endregion
    }
}