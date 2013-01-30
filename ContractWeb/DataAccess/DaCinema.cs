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
    public class DaCinema
    {
        /// <summary>
        /// 获取影院列表
        /// </summary>
        /// <returns></returns>
        public IList<Cinema> getList()
        {
            string strSql = "select id, name, "
                +"areaID, (select z.name from AreaInfo z where z.id=areaID) as area, "
                +"roomNum, person, tel, address from Cinema";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<Cinema> list = DynamicBuilder<Cinema>.ConvertToList(dr);
            return list;
        }

        /// <summary>
        /// 获取影院列表
        /// </summary>
        /// <returns></returns>
        public IList<Cinema> getList(string name, string area)
        {
            string strSql = "select id, name, "
                + "areaID, (select z.name from AreaInfo z where z.id=areaID) as area, "
                + "roomNum, person, tel, address from Cinema ";

            if (name.Trim() != "" || area.Trim() != "")
                strSql += "where ";

            if (name.Trim() != "")
                strSql += "name like '%" + name + "%' ";

            if (area.Trim() != "")
            {
                if (name.Trim() != "")
                    strSql += "or ";
                strSql += "area=" + area;
            }

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<Cinema> list = DynamicBuilder<Cinema>.ConvertToList(dr);
            return list;
        }

        /// <summary>
        /// 新增影院
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int add(Cinema en)
        {
            string strSql = "insert into Cinema (name, areaID, roomNum, person, tel, address) "
                + "values (@name, @areaID, @roomNum, @person, @tel, @address)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@name", en.name),
                new SqlParameter("@areaID", en.areaID),
                new SqlParameter("@roomNum", en.roomNum),
                new SqlParameter("@person", en.person),
                new SqlParameter("@tel", en.tel),
                new SqlParameter("@address", en.address)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }

        /// <summary>
        /// 修改影院
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int update(Cinema en)
        {
            string strSql = "update Cinema set name=@name, areaID=@areaID, roomNum=@roomNum, person=@person, tel=@tel, address=@address where id=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", en.id),
                new SqlParameter("@name", en.name),
                new SqlParameter("@areaID", en.areaID),
                new SqlParameter("@roomNum", en.roomNum),
                new SqlParameter("@person", en.person),
                new SqlParameter("@tel", en.tel),
                new SqlParameter("@address", en.address)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }

        /// <summary>
        /// 删除影院
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int delete(Cinema en)
        {
            string strSql = "delete from Cinema where id=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", en.id)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }

        
    }
}