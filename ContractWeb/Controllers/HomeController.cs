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
    public class HomeController : Controller
    {
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
            HttpCookie cookie = Request.Cookies["info"];
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);

            return View();
        }
        #endregion

        #region 登录验证
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public JsonResult LogOn(string userID, string password)
        {
            DaUserInfo daUser = new DaUserInfo();
            UserInfo info = daUser.checkUserID(userID, password);
            var result = new CustomJsonResult();            

            if (info != null)
            {
                Session["userInfo"] = info;
                //FormsAuthentication.SetAuthCookie(userID, false);
                Response.AppendCookie(saveCookie(info)); 

                result.Data = 1;
            }
            else
            {
                result.Data = 0;
            }

            return result;
        }
        #endregion

        #region 保存Cookie
        /// <summary>
        /// 保存Cookie
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public HttpCookie saveCookie(UserInfo info)
        {
            HttpCookie cookie = new HttpCookie("info");
            TimeSpan ts = new TimeSpan(1, 0, 0, 0, 0);
            cookie.Expires = DateTime.Now.Add(ts);
            cookie.Values.Add("userID", info.userID);
            cookie.Values.Add("powergroupID", info.powergroupID);

            return cookie;
        }
        #endregion

        #region 获取Cookie
        /// <summary>
        /// 获取Cookie
        /// </summary>
        /// <returns></returns>
        public UserInfo getCookie()
        {
            UserInfo info = null;

            if (Request.Cookies["info"] != null)
            {
                info = new UserInfo();
                info.userID = Request.Cookies["info"].Values["userID"];
                info.powergroupID = Request.Cookies["info"].Values["powergroupID"];
            }

            return info;
        }
        #endregion

        #region 获取菜单数据
        /// <summary>
        /// 获取菜单数据
        /// </summary>
        /// <returns></returns>
        public JsonResult getMenuData()
        {
            UserInfo info = getCookie();
            List<MenuItem> list = BaseHelper.getMenuData(info.userID);

            var result = new CustomJsonResult();
            result.Data = list;
            return result;
        }
        #endregion
    }
}
