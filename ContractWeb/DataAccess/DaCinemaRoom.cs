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
    public class DaCinemaRoom
    {
        /// <summary>
        /// 获取影厅列表
        /// </summary>
        /// <returns></returns>
        public IList<CinemaRoom> getList()
        {
            string strSql = "select id, cinemaID, (select z.name from Cinema z where z.id=cinemaID) as cinema, "
            + "room, typeID, (select z.type from CinemaRoomType z where z.id=typeID) as type, "
            + "memo from CinemaRoom";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<CinemaRoom> list = DynamicBuilder<CinemaRoom>.ConvertToList(dr);
            return list;
        }

        /// <summary>
        /// 新增影厅
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int add(CinemaRoom en)
        {
            string strSql = "insert into CinemaRoom (cinemaID, room, typeID, memo) "
                + "values (@cinemaID, @room, @typeID, @memo)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@cinemaID", en.cinemaID),
                new SqlParameter("@room", en.room),
                new SqlParameter("@typeID", en.typeID),
                new SqlParameter("@memo", en.memo)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
    }
}