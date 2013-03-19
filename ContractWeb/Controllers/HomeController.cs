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
                BaseHelper.user = info;
                Session["userInfo"] = info;
                //FormsAuthentication.SetAuthCookie(userID, false);
                result.Data = 1;
            }
            else
            {
                result.Data = 0;
            }

            return result;
        }
        #endregion

        #region 获取菜单数据
        /// <summary>
        /// 获取菜单数据
        /// </summary>
        /// <returns></returns>
        public JsonResult getMenuData()
        {
            List<MenuItem> list = BaseHelper.getMenuData();

            var result = new CustomJsonResult();
            result.Data = list;
            return result;
        }
        #endregion

    }
}
