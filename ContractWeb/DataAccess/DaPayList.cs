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
        #region 获取情况列表
        /// <summary>
        /// 获取情况列表
        /// </summary>
        /// <returns></returns>
        public IList<PayList> getList(string id)
        {
            string strSql = "select a.contractID, a.name, "
                + "a.channelID, (select z.name from Channel z where z.id=a.channelID) as channelName, "
                + "a.personID, (select z.name from UserInfo z where z.id=a.personID) as personName, "
                + "a.money, b.money as payMoney from ContractInfo a, CustomerPay b where a.contractID=b.contractID and a.contractID=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<PayList> list = DynamicBuilder<PayList>.ConvertToList(dr);
            return list;
        }
        #endregion
    }
}