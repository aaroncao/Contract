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
    }
}