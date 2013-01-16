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

        /// <summary>
        /// 系统菜单
        /// </summary>
        public static MenuItem[] sysMenu = new MenuItem[] {
            new MenuItem(0, "首页", 0, "Index", "Home"),
            new MenuItem(2, "权限组设置", 0, "PowerGroup", "SystemSetting"),

            new MenuItem(4, "用户管理", 1, "Users", "UserManage"),            
            new MenuItem(5, "用户密码修改", 1, "EditPwd", "UserManage"),
            new MenuItem(6, "用户个人信息修改", 1, "EditUserInfo", "UserManage"),

            new MenuItem(7, "渠道类别设置", 2, "Channel", "BasicSetting"),
            new MenuItem(8, "客户有效期设置", 2, "Validity", "BasicSetting"),
            new MenuItem(33, "权限组设定", 2, "", ""),
            new MenuItem(9, "地区设定", 2, "Area", "BasicSetting"),
            new MenuItem(10, "客户状态设定", 2, "State", "BasicSetting"),
            new MenuItem(11, "广告费结算对象设定", 2, "ADCost", "BasicSetting"),
            new MenuItem(12, "制作费结算对象设定", 2, "MakeCost", "BasicSetting"),
            new MenuItem(13, "客户资料管理", 2, "Customer", "BasicSetting"),
            new MenuItem(14, "影院信息管理", 2, "Cinema", "BasicSetting"),
            new MenuItem(15, "影厅信息管理", 2, "CinemaRoom", "BasicSetting"),

            new MenuItem(34, "合同信息录入", 3, "Contract", "Business"),
            new MenuItem(18, "下单", 3, "Order", "Business"),
            new MenuItem(19, "客户到账登记", 3, "CPay", "Business"),
            new MenuItem(20, "开发票登记", 3, "WriteBill", "Business"),
            new MenuItem(21, "广告费结算", 3, "ADCostAccount", "Business"),
            new MenuItem(22, "制作费结算", 3, "MakeCostAccount", "Business")
        };

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public static UserInfo getUserInfo()
        {
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                UserInfo info = (UserInfo) HttpContext.Current.Session["userInfo"];
                return info;
            }

            return null;
        }

        /// <summary>
        /// 获取菜单权限
        /// </summary>
        public static List<MenuItem> getMenuData()
        {
            if (HttpContext.Current.Session["menu"] != null)
            {
                return (List<MenuItem>)HttpContext.Current.Session["menu"];
            }
            else
            {
                //取权限
                UserInfo info = (UserInfo)HttpContext.Current.Session["userInfo"];
                string strSql = "select b.moduleID from UserInfo a, PowerGroupPower b where a.powergroupID=b.groupID and a.userID=@userID";

                SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("@userID", info.userID)
                };

                DataTable dt = SqlHelper.ExecuteDataset(DBConnStr, CommandType.Text, strSql, param).Tables[0];

                //配数据
                List<MenuItem> menus = new List<MenuItem>();

                for (int i = 0; i < sysMenu.Length; i++)
                {
                    if (sysMenu[i].id == 0)
                        menus.Add(sysMenu[i]);

                    if (dt.Select("moduleID=" + sysMenu[i].id).Length > 0)
                        menus.Add(sysMenu[i]);
                }

                //排序
                menus.Sort(sortMenuItem);

                HttpContext.Current.Session["menu"] = menus;
                return menus;
            }
        }

        /// <summary>
        /// 菜单排序
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        private static int sortMenuItem(MenuItem t1, MenuItem t2)
        {
            if (t1.type > t2.type)
                return 1;
            else
                return 0;
        }
    }
}