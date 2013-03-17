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
    public class DaAdvListItem
    {
        #region 搜索投放信息
        /// <summary>
        /// 搜索投放信息
        /// </summary>
        /// <param name="cinema"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IList<AdvListItem> getList(string cinema, string begin, string end)
        {
            string strSql = "select a.id, a.cinemaID, (select z.name from Cinema z where z.id=a.cinemaID) as cinemaName, "
                + "b.version, c.begintime, c.endtime, a.orderID, a.contractID, b.name as contractName "
                + "from PutinInfo a, ContractInfo b, OrderInfo c where a.contractID=b.contractID and b.contractID=c.contractID ";

            string where = "";

            if (cinema.Trim() != "")
                where += "a.cinemaID='" + cinema + "'";

            if (begin.Trim() != "" && end.Trim() != "")
            {
                if (where != "")
                    where += " or ";
                where += "(c.endtime>='" + begin + "' and c.begintime<='" + end + "')";
            }

            if (where != "")
                strSql += "and (" + where + ")";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<AdvListItem> list = DynamicBuilder<AdvListItem>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 搜索投放信息(导出报表)
        /// <summary>
        /// 搜索投放信息(导出报表)
        /// </summary>
        /// <param name="cinema"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataTable getDataTable(string cinema, string begin, string end)
        {
            string strSql = "select a.contractID as [合同编号], b.name as [合同名称], a.orderID as [下单编号], (select z.name from Cinema z where z.id=a.cinemaID) as [投放影院], "
                + "c.begintime as [下单时间(起)], c.endtime as [下单时间(止)], b.version as [广告版本] "
                + "from PutinInfo a, ContractInfo b, OrderInfo c where a.contractID=b.contractID and b.contractID=c.contractID ";

            string where = "";

            if (cinema.Trim() != "")
                where += "a.cinemaID='" + cinema + "'";

            if (begin.Trim() != "" && end.Trim() != "")
            {
                if (where != "")
                    where += " or ";
                where += "(c.endtime>='" + begin + "' and c.begintime<='" + end + "')";
            }

            if (where != "")
                strSql += "and (" + where + ")";

            return SqlHelper.ExecuteDataset(BaseHelper.DBConnStr, CommandType.Text, strSql).Tables[0];
        }
        #endregion
    }
}