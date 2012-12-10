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
    public class DaCinemaRoomType
    {
        /// <summary>
        /// 获取影厅类型列表
        /// </summary>
        /// <returns></returns>
        public IList<CinemaRoomType> getList()
        {
            string strSql = "select id, type from CinemaRoomType";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<CinemaRoomType> list = DynamicBuilder<CinemaRoomType>.ConvertToList(dr);
            return list;
        }
    }
}