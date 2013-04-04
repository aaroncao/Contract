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
    public class DaPowerGroupPower
    {
        #region 获取权限列表
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        public IList<PowerGroupPower> getList(string id)
        {
            string strSql = "select a.id, a.groupID, a.moduleID, b.name as moduleName, a.power, "
                + "(case a.power when 0 then '禁止' when 1 then '可读' when 2 then '可写' end) as powerName "
                + "from PowerGroupPower a, SystemModule b "
                + "where a.moduleID = b.id and a.groupID = @group";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@group", id)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<PowerGroupPower> list = DynamicBuilder<PowerGroupPower>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 添加权限
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int add(List<PowerGroupPower> list)
        {
            SqlParameter[] param = null;
            string strSql = "insert into PowerGroupPower (groupID, moduleID, power) values (@group, @module, @power)";

            for (int i = 0; i < list.Count; i++)
            {
                param = new SqlParameter[]
                {
                    new SqlParameter("@group", list[i].groupID),
                    new SqlParameter("@module", list[i].moduleID),
                    new SqlParameter("@power", list[i].power)
                };

                SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            }

            return 1;
        }
        #endregion

        #region 设置权限
        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int update(List<PowerGroupPower> list)
        {
            SqlParameter[] param = null;
            string strSql = "update PowerGroupPower set power=@power where groupID=@group and moduleID=@module";

            for (int i = 0; i < list.Count; i++)
            {
                param = new SqlParameter[]
                {
                    new SqlParameter("@power", list[i].power),
                    new SqlParameter("@group", list[i].groupID),
                    new SqlParameter("@module", list[i].moduleID)
                };

                SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            }

            return 1;
        }
        #endregion

        #region 删除权限
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int delete(List<PowerGroupPower> list)
        {
            SqlParameter[] param = null;
            string strSql = "delete from PowerGroupPower where groupID=@group and moduleID=@module";

            for (int i = 0; i < list.Count; i++)
            {
                param = new SqlParameter[]
                {
                    new SqlParameter("@group", list[i].groupID),
                    new SqlParameter("@module", list[i].moduleID)
                };

                SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            }

            return 1;
        }
        #endregion
    }
}