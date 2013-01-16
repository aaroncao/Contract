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
        /// <summary>
        /// 登录界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.menu = 0;
            ViewBag.Message = "欢迎使用系统平台";
            return View();
        }

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
                FormsAuthentication.SetAuthCookie(userID, false);

                result.Data = 1;
            }
            else
            {
                result.Data = 0;
            }

            return result;
        }

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
    }
}
