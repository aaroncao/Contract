using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using ContractWeb.Models;
using ContractWeb.DataAccess;
using ContractWeb.Common;

namespace ContractWeb.Controllers
{
    //系统通用
    public class HomeController : Controller
    {
        /* ============ 界面 ============ */

        #region 登录界面
        /// <summary>
        /// 登录界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        #endregion

        #region 平台首页
        /// <summary>
        /// 平台首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.menu = 0;
            ViewBag.Message = "欢迎使用系统平台";
            return View();
        }
        #endregion

        #region 退出系统
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        public ActionResult Exit()
        {
            HttpCookie cookie = new HttpCookie("info");
            cookie.Expires = DateTime.Now.AddMonths(-1);
            cookie.Values.Clear();
            Response.AppendCookie(cookie);
            Response.Cookies.Add(cookie);

            return View();
        }
        #endregion

        /* ============ 操作 ============ */

        #region 登录验证
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public JsonResult logOn(string userID, string password, string t)
        {
            DaUserInfo daUser = new DaUserInfo();
            UserInfo info = daUser.checkUserID(userID, password);
            var result = new CustomJsonResult();            

            if (info != null)
            {
                Session["userInfo"] = info;
                Response.AppendCookie(BaseHelper.saveCookie(info)); 

                result.Data = 1;
            }
            else
            {
                result.Data = 0;
            }

            return result;
        }
        #endregion

        #region 获取用户名
        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        public JsonResult getUserName(string t)
        {
            UserInfo en = BaseHelper.getCookie();

            var result = new CustomJsonResult();
            result.Data = en.name;
            return result;
        }
        #endregion

        #region 获取菜单数据
        /// <summary>
        /// 获取菜单数据
        /// </summary>
        /// <returns></returns>
        public JsonResult getMenuData(string t)
        {
            UserInfo info = BaseHelper.getCookie();
            List<MenuItem> list = BaseHelper.getMenuData(info.userID);

            var result = new CustomJsonResult();
            result.Data = list;
            return result;
        }
        #endregion

        #region 获取业务员列表
        /// <summary>
        /// 获取业务员列表
        /// </summary>
        /// <returns></returns>
        public JsonResult drpPersonList()
        {
            DaUserInfo dal = new DaUserInfo();
            IList<UserInfo> users = dal.getList();

            var result = new CustomJsonResult();
            result.Data = users;
            return result;
        }
        #endregion

        #region 获取权限组列表
        /// <summary>
        /// 获取权限组列表
        /// </summary>
        /// <returns></returns>
        public JsonResult drpGroupList()
        {
            DaPowerGroup dal = new DaPowerGroup();
            IList<PowerGroup> groups = dal.getList();

            var result = new CustomJsonResult();
            result.Data = groups;
            return result;
        }
        #endregion
    }
}
