using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using ContractWeb.Common;
using ContractWeb.Models;
using ContractWeb.DataAccess;

namespace ContractWeb.Controllers
{
    public class SystemSettingController : Controller
    {
        /// <summary>
        /// 权限组界面
        /// </summary>
        /// <returns></returns>
        public ActionResult PowerGroup()
        {
            ViewBag.menu = 2;
            return View();
        }

        /// <summary>
        /// 获取权限组列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getGroupList()
        {
            DaPowerGroup dal = new DaPowerGroup();
            IList<PowerGroup> areas = dal.getList();

            var result = new CustomJsonResult();
            result.Data = new { total = areas.Count, rows = areas };
            return result;
        }

        /// <summary>
        /// 添加权限组
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addGroup(string name, string memo)
        {
            PowerGroup en = new PowerGroup();
            en.name = name;
            en.memo = memo;

            DaPowerGroup dal = new DaPowerGroup();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }

        /// <summary>
        /// 更新权限组
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult updateGroup(string id, string name, string memo)
        {
            PowerGroup en = new PowerGroup();
            en.id = Convert.ToInt32(id);
            en.name = name;
            en.memo = memo;

            DaPowerGroup dal = new DaPowerGroup();
            var result = new CustomJsonResult();
            result.Data = dal.update(en);
            return result;
        }
    }
}
