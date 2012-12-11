using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContractWeb.Controllers
{
    public class BusinessController : Controller
    {
        /// <summary>
        /// 客户到账登记界面
        /// </summary>
        /// <returns></returns>
        public ActionResult CPay()
        {
            return View();
        }

    }
}
