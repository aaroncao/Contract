using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using ContractWeb.Common;
using ContractWeb.Models;
using ContractWeb.DataAccess;

namespace ContractWeb.Controllers
{
    public class UserManageController : Controller
    {
        /// <summary>
        /// 用户管理界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Users()
        {
            ViewBag.menu = 4;
            return View();
        }

        /// <summary>
        /// 修改密码界面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPwd()
        {
            if (BaseHelper.getUserInfo() != null)
                ViewBag.userID = BaseHelper.getUserInfo().userID;

            ViewBag.menu = 5;
            return View();
        }

        /// <summary>
        /// 个人信息修改界面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditUserInfo()
        {
            UserInfo info = BaseHelper.getUserInfo();

            if (info != null)
            {
                ViewBag.userID = info.userID;
                ViewBag.name = info.name;
                ViewBag.sex = info.sex;
                ViewBag.card = info.card;
                ViewBag.tel = info.tel;
                ViewBag.address = info.address;
            }

            return View();
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        public JsonResult editPassword(string oldPwd, string newPwd)
        {
            DaUserInfo dal = new DaUserInfo();
            string[] re = dal.editPassword(BaseHelper.getUserInfo(), oldPwd, newPwd);

            var result = new CustomJsonResult();
            result.Data = new { isSuccess = re[0], msg = re[1] };
            return result;
        }       
        
        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <returns></returns>
        public JsonResult getUserList()
        {
            DaUserInfo dal = new DaUserInfo();
            IList<UserInfo> users = dal.getList();

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";

            result.Data = new { total = users.Count, rows = users };
            return result;
        }

        

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="name">真实姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="card">身份证</param>
        /// <param name="tel">联系方式</param>
        /// <param name="address">联系地址</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult editInfo(string name, string sex, string card, string tel, string address)
        {
            UserInfo info = BaseHelper.getUserInfo().clone();
            info.name = name;
            info.sex = sex;
            info.card = card;
            info.tel = tel;
            info.address = address;

            DaUserInfo dal = new DaUserInfo();
            var result = new CustomJsonResult();
            result.Data = dal.edit(info);
            return result;
        }

    }
}
