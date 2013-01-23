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
        #region 权限组界面
        /// <summary>
        /// 权限组界面
        /// </summary>
        /// <returns></returns>
        public ActionResult PowerGroup()
        {
            ViewBag.menu = 2;
            return View();
        }
        #endregion

        #region 用户管理界面
        /// <summary>
        /// 用户管理界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Users()
        {
            ViewBag.menu = 4;
            return View();
        }
        #endregion

        #region 修改密码界面
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
        #endregion

        #region 个人信息修改界面
        /// <summary>
        /// 个人信息修改界面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditUserInfo()
        {
            ViewBag.menu = 6;
            return View();
        }
        #endregion

        #region 修改密码
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
        #endregion

        #region 获取个人信息
        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <returns></returns>
        public JsonResult getUserInfo()
        {
            var result = new CustomJsonResult();

            result.Data = BaseHelper.getUserInfo();
            return result;
        }
        #endregion

        #region 修改个人信息
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
            int isAccess = dal.edit(info);

            if (isAccess == 1)
                BaseHelper.setUserInfo(info);

            var result = new CustomJsonResult();
            result.Data = isAccess;            

            return result;
        }
        #endregion

        #region 获取用户列表
        /// <summary>
        /// 获取用户列表
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
        #endregion

        #region 获取用户列表
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public JsonResult searchList(string userID, string beginDate, string endDate)
        {
            DaUserInfo dal = new DaUserInfo();
            IList<UserInfo> users = dal.getList(userID, beginDate, endDate);

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";

            result.Data = new { total = users.Count, rows = users };
            return result;
        }
        #endregion

        #region 获取权限组列表
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
        #endregion

        #region 添加权限组
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
        #endregion

        #region 更新权限组
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
        #endregion

        #region 添加用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <param name="name">真实姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="card">身份证</param>
        /// <param name="tel">联系方式</param>
        /// <param name="address">联系地址</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addUser(string userID, string name, string sex, string card, string tel, string address)
        {
            UserInfo en = new UserInfo();
            en.userID = userID;
            en.name = name;
            en.sex = sex;
            en.card = card;
            en.tel = tel;
            en.address = address;

            DaUserInfo dal = new DaUserInfo();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);

            return result;
        }
        #endregion

        #region 修改用户
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="userID">用户名</param>
        /// <param name="name">真实姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="card">身份证</param>
        /// <param name="tel">联系方式</param>
        /// <param name="address">联系地址</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult editUser(string id, string userID, string name, string sex, string card, string tel, string address)
        {
            UserInfo en = new UserInfo();
            en.id = Convert.ToInt32(id);
            en.userID = userID;
            en.name = name;
            en.sex = sex;
            en.card = card;
            en.tel = tel;
            en.address = address;

            DaUserInfo dal = new DaUserInfo();
            var result = new CustomJsonResult();
            result.Data = dal.edit(en);

            return result;
        }
        #endregion

        #region 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public JsonResult deleteUser(string id)
        {
            UserInfo en = new UserInfo();
            en.id = Convert.ToInt32(id);

            DaUserInfo dal = new DaUserInfo();
            var result = new CustomJsonResult();
            result.Data = dal.delete(en);

            return result;
        }
        #endregion
    }
}
