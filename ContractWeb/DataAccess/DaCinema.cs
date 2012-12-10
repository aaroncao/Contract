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
    }
}