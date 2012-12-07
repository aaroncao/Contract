using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ContractWeb.Common;
using ContractWeb.Models;
using ContractWeb.DataAccess;

namespace ContractWeb.Controllers
{
    public class UserManageController : Controller
    {
        // GET: /UserManage/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户修改密码界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePwdView()
        {
            ViewBag.name = BaseHelper.user.userID;
            return View();
        }
        
        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <returns></returns>
        public JsonResult getUserList()
        {
            DaUserInfo daUserInfo = new DaUserInfo();
            IList<UserInfo> users = daUserInfo.getUserList();

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";

            result.Data = new { total = users.Count, rows = users };
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

    }
}
