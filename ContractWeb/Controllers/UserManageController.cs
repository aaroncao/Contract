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
        
        // 获取数据
        public JsonResult getUserList()
        {
            DaUserInfo dal = new DaUserInfo();
            IList<UserInfo> list = dal.getUserList();

            var result = new CustomJsonResult();
            result.Data = new { total = list.Count, rows = list };
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

    }
}
