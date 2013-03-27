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

        #region 系统菜单配置
        /// <summary>
        /// 系统菜单配置
        /// </summary>
        public static MenuItem[] sysMenu = new MenuItem[] {
            new MenuItem(0, "平台首页", 0, "Index", "Home"),
            new MenuItem(2, "操作岗位管理", 0, "PowerGroup", "UserManage"),
            new MenuItem(3, "管理员修改密码", 0, "EditUserPwd", "UserManage"),
            new MenuItem(2000, "退出系统", 0, "Exit", "Home"),


            new MenuItem(4, "系统用户管理", 1, "Users", "UserManage"),            
            new MenuItem(5, "个人密码修改", 1, "EditPwd", "UserManage"),
            new MenuItem(6, "个人信息修改", 1, "EditUserInfo", "UserManage"),


            new MenuItem(7, "渠道类别设置", 2, "Channel", "BasicSetting"),
            new MenuItem(8, "客户有效期设置", 2, "Validity", "BasicSetting"), 
            new MenuItem(33, "权限组设定", 2, "PowerSetting", "BasicSetting"),
            new MenuItem(9, "地区设定", 2, "Area", "BasicSetting"),
            new MenuItem(10, "客户状态设定", 2, "State", "BasicSetting"),
            new MenuItem(11, "广告费结算对象设定", 2, "ADCost", "BasicSetting"),
            new MenuItem(12, "制作费结算对象设定", 2, "MakeCost", "BasicSetting"),
            new MenuItem(14, "影院信息管理", 2, "Cinema", "BasicSetting"),
            new MenuItem(15, "影厅信息管理", 2, "CinemaRoom", "BasicSetting"),
            

            new MenuItem(13, "客户资料管理", 3, "Customer", "BasicSetting"),
            new MenuItem(34, "合同信息录入", 3, "Contract", "Business"),
            new MenuItem(18, "下单", 3, "Order", "Business"),
            new MenuItem(19, "客户到账登记", 3, "CPay", "Business"),
            new MenuItem(20, "开发票登记", 3, "WriteBill", "Business"),
            new MenuItem(21, "广告费结算", 3, "ADCostAccount", "Business"),
            new MenuItem(22, "制作费结算", 3, "MakeCostAccount", "Business"),
            new MenuItem(23, "收发票登记", 3, "ReceiveBill", "Business"),

            new MenuItem(26, "合同信息查询", 4, "Contract", "Select"),
            new MenuItem(27, "下单信息查询", 4, "Order", "Select"),
            new MenuItem(29, "广告费结算查询", 4, "ADCost", "Select"),
            new MenuItem(30, "制作费结算查询", 4, "MakeCost", "Select"),
            new MenuItem(32, "影院投放统计", 4, "Putin", "Select"),
            new MenuItem(38, "影院广告情况统计", 4, "AdvList", "Select")
        };
        #endregion

        #region 获取菜单权限
        /// <summary>
        /// 获取菜单权限
        /// </summary>
        public static List<MenuItem> getMenuData(string userID)
        {
            List<MenuItem> menus = new List<MenuItem>();

            if (!string.IsNullOrEmpty(userID))
            {
                //取权限
                string strSql = "select b.moduleID from UserInfo a, PowerGroupPower b where a.powergroupID=b.groupID and a.userID=@userID";

                SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("@userID", userID)
                };

                DataTable dt = SqlHelper.ExecuteDataset(DBConnStr, CommandType.Text, strSql, param).Tables[0];

                //配数据
                for (int i = 0; i < sysMenu.Length; i++)
                {
                    if (sysMenu[i].id == 0 || sysMenu[i].id >= 2000)
                        menus.Add(sysMenu[i]);

                    if (dt.Select("moduleID=" + sysMenu[i].id).Length > 0)
                        menus.Add(sysMenu[i]);
                }

                //排序
                //menus.Sort(new MenuCompare());
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
                info.password = cookie.Values["password"];
                info.powergroupID = cookie.Values["powergroupID"];
            }

            return info;
        }
        #endregion
    }

    /// <summary>
    /// 菜单排序
    /// </summary>
    public class MenuCompare : IComparer<MenuItem>
    {
        public int Compare(MenuItem t1, MenuItem t2)
        {
            if (t1.type > t2.type)
                return 1;

            if (t1.id > t2.id)
                return 1;
            else
                return -1;
        }

    }
}