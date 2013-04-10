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
    public class DaPutinInfo
    {
        #region 根据订单编号获取投放信息
        /// <summary>
        /// 根据订单编号获取投放信息
        /// </summary>
        /// <returns></returns>
        public IList<PutinInfo> getList(string id)
        {
            string strSql = "select id, contractID, orderID, "
                + "cinemaID, (select z.name from Cinema z where z.id=cinemaID) as cinemaName, "
                + "cinemaRoomID, (select z.room from CinemaRoom z where z.id=cinemaRoomID) as cinemaRoomName, "
                + "roomTypeID, (select z.type from CinemaRoomType z where z.id=roomTypeID) as roomType, memo from PutinInfo where orderID=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<PutinInfo> list = DynamicBuilder<PutinInfo>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 根据合同编号获取投放信息
        /// <summary>
        /// 根据订单编号获取投放信息
        /// </summary>
        /// <returns></returns>
        public IList<PutinInfo> getListByContract(string id)
        {
            string strSql = "select id, contractID, orderID, "
                + "cinemaID, (select z.name from Cinema z where z.id=cinemaID) as cinemaName, "
                + "cinemaRoomID, (select z.room from CinemaRoom z where z.id=cinemaRoomID) as cinemaRoomName, "
                + "roomTypeID, (select z.type from CinemaRoomType z where z.id=roomTypeID) as roomType, memo from PutinInfo where contractID=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<PutinInfo> list = DynamicBuilder<PutinInfo>.ConvertToList(dr);
            return list;
        }
        #endregion


        #region 获取投放信息的金额
        /// <summary>
        /// 获取投放信息
        /// </summary>
        /// <returns></returns>
        public double[] getListOfMoney()
        {
            string strSql = "select count(a.id), sum(b.price * b.ZQ) from PutinInfo a, ContractInfo b, OrderInfo c where a.orderID=c.orderID and b.contractID=c.contractID ";
            DataTable dt = SqlHelper.ExecuteDataset(BaseHelper.DBConnStr, CommandType.Text, strSql).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
                return new double[2] {Convert.ToDouble(dt.Rows[0][0]), Convert.ToDouble(dt.Rows[0][1])};
            else
                return new double[2] {0.0, 0.0};
        }
        #endregion

        #region 获取投放信息
        /// <summary>
        /// 获取投放信息
        /// </summary>
        /// <returns></returns>
        public IList<PutinListItem> getList(string page, string rows)
        {
            string strSql = "EXECUTE ProPageHelper 'PutinInfo a, ContractInfo b, OrderInfo c', "
                + "'a.id, a.cinemaID, (select z.name from Cinema z where z.id=a.cinemaID) as cinemaName, a.cinemaRoomID, (select z.room from CinemaRoom z where z.id=a.cinemaRoomID) as cinemaRoomName, b.version, c.begintime, a.orderID, a.contractID, a.roomTypeID, (select z.type from CinemaRoomType z where z.id=a.roomTypeID) as roomType, b.price, b.ZQ ', "
                + "'a.id', @rows, @page, 1, 0, 0, 'a.orderID=c.orderID and b.contractID=c.contractID' ";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@page", page),
                new SqlParameter("@rows", rows)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<PutinListItem> list = DynamicBuilder<PutinListItem>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 搜索投放信息的金额
        /// <summary>
        /// 搜索投放信息的金额
        /// </summary>
        /// <param name="cinema"></param>
        /// <param name="room"></param>
        /// <param name="version"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public double[] getListOfMoney(string cinema, string room, string version, string begin, string end)
        {
            string strSql = "select count(a.id), sum(b.price * b.ZQ) from PutinInfo a, ContractInfo b, OrderInfo c where a.orderID=c.orderID and b.contractID=c.contractID ";

            #region 条件部分
            string where = "";

            if (cinema.Trim() != "")
                where += "a.cinemaID='" + cinema + "'";

            if (room.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "a.cinemaRoomID='" + room + "'";
            }

            if (version.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "b.version='" + version + "'";
            }

            if (begin.Trim() != "" && end.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "(c.begintime between'" + begin + "' and '" + end + "')";
            }

            if (where != "")
                strSql += "and " + where;
            #endregion

            DataTable dt = SqlHelper.ExecuteDataset(BaseHelper.DBConnStr, CommandType.Text, strSql).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
                return new double[2] { Convert.ToDouble(dt.Rows[0][0]), Convert.ToDouble(dt.Rows[0][1]) };
            else
                return new double[2] { 0.0, 0.0 };
        }
        #endregion
        
        #region 搜索投放信息
        /// <summary>
        /// 搜索投放信息
        /// </summary>
        /// <param name="cinema"></param>
        /// <param name="room"></param>
        /// <param name="version"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IList<PutinListItem> getList(string cinema, string room, string version, string begin, string end, string page, string rows)
        {
            #region 条件部分
            string where = "a.orderID=c.orderID and b.contractID=c.contractID";

            if (cinema.Trim() != "")
                where += " and a.cinemaID=''" + cinema + "''";

            if (room.Trim() != "")
                where += " and a.cinemaRoomID=''" + room + "''";

            if (version.Trim() != "")
                where += " and b.version=''" + version + "''";

            if (begin.Trim() != "" && end.Trim() != "")
                where += " and (c.begintime between''" + begin + "'' and ''" + end + "'')";
            #endregion

            string strSql = "EXECUTE ProPageHelper 'PutinInfo a, ContractInfo b, OrderInfo c', "
                + "'a.id, a.cinemaID, (select z.name from Cinema z where z.id=a.cinemaID) as cinemaName, a.cinemaRoomID, (select z.room from CinemaRoom z where z.id=a.cinemaRoomID) as cinemaRoomName, b.version, c.begintime, a.orderID, a.contractID, a.roomTypeID, (select z.type from CinemaRoomType z where z.id=a.roomTypeID) as roomType, b.price, b.ZQ ', "
                + "'a.id', @rows, @page, 1, 0, 0, '" + where +　"' ";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@page", page),
                new SqlParameter("@rows", rows)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<PutinListItem> list = DynamicBuilder<PutinListItem>.ConvertToList(dr);
            return list;
        }
        #endregion


        #region 添加投放影厅
        /// <summary>
        /// 添加投放影厅
        /// </summary>
        /// <param name="ens">要投放的影厅</param>
        /// <returns></returns>
        public int add(List<PutinInfo> ens)
        {
            string strSql = "";

            for (int i = 0; i < ens.Count; i++)
            {
                PutinInfo en = ens[i];

                strSql += "insert into PutinInfo(contractID, orderID, cinemaID, cinemaRoomID, roomTypeID) "
                    + "values ('" + en.contractID + "', '" + en.orderID + "', " + en.cinemaID + ", " + en.cinemaRoomID + ", " + en.roomTypeID + ") ";
            }

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql);
            return result;
        }
        #endregion
    }
}