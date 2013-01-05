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
        /// 获取影厅列表
        /// </summary>
        /// <param name="id">影院ID</param>
        /// <returns></returns>
        public IList<CinemaRoom> getList(string id)
        {
            string strSql = "select id, cinemaID, (select z.name from Cinema z where z.id=cinemaID) as cinema, "
            + "room, typeID, (select z.type from CinemaRoomType z where z.id=typeID) as type, "
            + "memo from CinemaRoom where cinemaID=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<CinemaRoom> list = DynamicBuilder<CinemaRoom>.ConvertToList(dr);
            return list;
        }

        /// <summary>
        /// 获取影厅列表
        /// </summary>
        /// <param name="ids">影厅ID数组</param>
        /// <returns></returns>
        public IList<CinemaRoom> getList(string[] ids)
        {
            string id = "";
            for (int i = 0; i < ids.Length; i++)
            {
                id += ids[i] + ",";
            }
            if (id.Length > 0)
                id = id.Substring(0, id.Length - 1);

            string strSql = "select id, cinemaID, (select z.name from Cinema z where z.id=cinemaID) as cinema, "
            + "room, typeID, (select z.type from CinemaRoomType z where z.id=typeID) as type, "
            + "memo from CinemaRoom where id in (" + id + ")";

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