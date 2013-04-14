using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using ContractWeb.Models;
using ND.Lib.Data.SqlHelper;

namespace ContractWeb.Common
{
    /// <summary>
    /// 基本工具类
    /// </summary>
    public static class BaseHelper
    {
        /// <summary>
        /// 数据库链接
        /// </summary>
        public static string DBConnStr = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();

        #region 获取菜单权限
        /// <summary>
        /// 获取菜单权限
        /// </summary>
        public static List<MenuItem> getMenuData(int id)
        {
            List<MenuItem> menus = new List<MenuItem>();

            if (id != 0)
            {
                //取权限
                string strSql = "select c.id, c.name, c.menuType, c.action, c.controller "
                    + "from UserInfo a, PowerGroupPower b, SystemModule c "
                    + "where a.powergroupID=b.groupID and b.moduleID=c.id and a.id=@id and c.menuType is not null order by c.menuType, c.id ";

                SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("@id", id)
                };

                DataTable dt = SqlHelper.ExecuteDataset(DBConnStr, CommandType.Text, strSql, param).Tables[0];

                //配数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    MenuItem item = new MenuItem(Convert.ToInt32(row["id"]), row["name"].ToString(), Convert.ToInt32(row["menuType"]), row["action"].ToString(), row["controller"].ToString());
                    menus.Add(item);
                }

                HttpContext.Current.Session["menu"] = menus;
            }

            return menus;
        }
        #endregion

        #region 保存Cookie
        /// <summary>
        /// 保存Cookie
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static HttpCookie saveCookie(UserInfo info)
        {
            HttpCookie cookie = new HttpCookie("info");
            TimeSpan ts = new TimeSpan(1, 0, 0, 0, 0);
            cookie.Expires = DateTime.Now.Add(ts);
            cookie.Values.Add("id", info.id.ToString());
            cookie.Values.Add("userID", info.userID);
            cookie.Values.Add("userName", info.name);
            cookie.Values.Add("password", info.password);
            cookie.Values.Add("powergroupID", info.powergroupID);

            return cookie;
        }
        #endregion

        #region 获取Cookie
        /// <summary>
        /// 获取Cookie
        /// </summary>
        /// <returns></returns>
        public static UserInfo getCookie()
        {
            UserInfo info = null;

            if (HttpContext.Current.Request.Cookies["info"] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["info"];

                info = new UserInfo();
                info.id = Convert.ToInt32(cookie.Values["id"]);
                info.userID = cookie.Values["userID"];
                info.name = cookie.Values["userName"];
                info.password = cookie.Values["password"];
                info.powergroupID = cookie.Values["powergroupID"];
            }

            return info;
        }
        #endregion

        
    }
}