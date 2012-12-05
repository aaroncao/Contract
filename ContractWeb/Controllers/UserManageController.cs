using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public JsonResult data()
        {
            var result = new JsonResult();
            result.Data = new { total = 239, rows = new object[] { new { code = "001", name = "Name 1", addr = "Address 11", col4 = "col4 data"} }};
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

    }
}
