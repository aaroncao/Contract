using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using ContractWeb.Common;
using ContractWeb.Models;
using ContractWeb.DataAccess;

namespace ContractWeb.Controllers
{
    //用户管理
    [AuthorizeFilterAttribute]
    public class UserManageController : Controller
    {
        /* ============ 界面 ============ */

        #region 操作岗位管理界面
        /// <summary>
        /// 操作岗位管理界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Group()
        {
            ViewBag.menu = 2;
            return View();
        }
        #endregion

        #region 管理员修改密码界面
        /// <summary>
        /// 管理员修改密码界面
        /// </summary>
        /// <returns></returns>
        public ActionResult UserPwd()
        {
            ViewBag.menu = 3;
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

        #region 设置角色界面
        /// <summary>
        /// 设置角色界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Role()
        {
            return View();
        }
        #endregion

        #region 修改个人密码界面
        /// <summary>
        /// 修改密码界面
        /// </summary>
        /// <returns></returns>
        public ActionResult MyPwd()
        {
            ViewBag.menu = 5;
            return View();
        }
        #endregion

        #region 修改个人信息界面
        /// <summary>
        /// 修改个人信息界面
        /// </summary>
        /// <returns></returns>
        public ActionResult MyInfo()
        {
            ViewBag.userInfo = BaseHelper.getCookie();
            ViewBag.menu = 6;
            return View();
        }
        #endregion    

        /* ============ 操作 ============ */

        #region 获取操作岗位管理列表
        /// <summary>
        /// 获取操作岗位管理列表
        /// </summary>
        /// <returns></returns>
        public JsonResult Group_getList()
        {
            DaPowerGroup dal = new DaPowerGroup();
            IList<PowerGroup> areas = dal.getList();

            var result = new CustomJsonResult();
            result.Data = new { total = areas.Count, rows = areas };
            return result;
        }
        #endregion

        #region 添加操作岗位
        /// <summary>
        /// 添加操作岗位
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Group_add(string name, string memo)
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

        #region 更新操作岗位
        /// <summary>
        /// 更新操作岗位
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Group_edit(string id, string name, string memo)
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

        #region 删除操作岗位
        /// <summary>
        /// 删除操作岗位
        /// </summary>
        /// <param name="id">权限组ID</param>
        /// <returns></returns>
        public JsonResult Group_remove(string id)
        {
            PowerGroup en = new PowerGroup();
            en.id = Convert.ToInt32(id);

            DaPowerGroup dal = new DaPowerGroup();
            var result = new CustomJsonResult();
            result.Data = dal.delete(en);
            return result;
        }
        #endregion



        #region 管理员修改密码
        /// <summary>
        /// 管理员修改密码
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        public JsonResult UserPwd_edit(string id, string newPwd)
        {
            DaUserInfo dal = new DaUserInfo();
            string[] re = dal.editPassword(Convert.ToInt32(id), newPwd);

            var result = new CustomJsonResult();
            result.Data = new { isSuccess = re[0], msg = re[1] };
            return result;
        }
        #endregion



        #region 获取用户列表
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public JsonResult Users_getList()
        {
            DaUserInfo dal = new DaUserInfo();
            IList<UserInfo> users = dal.getList();

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";

            result.Data = new { total = users.Count, rows = users };
            return result;
        }
        #endregion

        #region 搜索用户列表
        /// <summary>
        /// 搜索用户列表
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public JsonResult Users_search(string userID, string beginDate, string endDate)
        {
            DaUserInfo dal = new DaUserInfo();
            IList<UserInfo> users = dal.getList(userID, beginDate, endDate);

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";

            result.Data = new { total = users.Count, rows = users };
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
        public JsonResult Users_add(string userID, string name, string sex, string card, string tel, string address)
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
        public JsonResult Users_edit(string id, string userID, string name, string sex, string card, string tel, string address)
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
        public JsonResult Users_remove(string id)
        {
            UserInfo en = new UserInfo();
            en.id = Convert.ToInt32(id);

            DaUserInfo dal = new DaUserInfo();
            var result = new CustomJsonResult();
            result.Data = dal.delete(en);

            return result;
        }
        #endregion

        #region 设置用户的启用/禁用状态
        /// <summary>
        /// 设置用户的启用/禁用状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public JsonResult Users_editOpen(string id, string state)
        {
            UserInfo en = new UserInfo();
            en.id = Convert.ToInt32(id);
            en.state = state;

            DaUserInfo dal = new DaUserInfo();
            var result = new CustomJsonResult();
            result.Data = dal.updateOpen(en);

            return result;
        }
        #endregion



        #region 获取用户角色列表
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <returns></returns>
        public JsonResult Users_getRoleList()
        {
            DaUserGroup dal = new DaUserGroup();
            IList<UserGroup> users = dal.getList();

            var result = new CustomJsonResult();
            result.Data = new { total = users.Count, rows = users };
            return result;
        }
        #endregion

        #region 修改权限组
        /// <summary>
        /// 修改权限组
        /// </summary>
        /// <param name="id"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public JsonResult Users_editRole(string id, string groupID)
        {
            DaUserGroup dal = new DaUserGroup();
            UserInfo en = new UserInfo();
            en.id = Convert.ToInt32(id);
            en.powergroupID = groupID;

            var result = new CustomJsonResult();
            result.Data = dal.update(en);
            return result;
        }
        #endregion



        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        public JsonResult MyPwd_edit(string oldPwd, string newPwd)
        {
            DaUserInfo dal = new DaUserInfo();
            string[] re = dal.editPassword(BaseHelper.getCookie(), oldPwd, newPwd);

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
        public JsonResult MyInfo_getList()
        {
            DaUserInfo dal = new DaUserInfo();
            UserInfo info = BaseHelper.getCookie();
            var result = new CustomJsonResult();

            result.Data = dal.checkUserID(info.userID, info.password);
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
        public JsonResult MyInfo_edit(string id, string name, string sex, string card, string tel, string address)
        {
            UserInfo info = BaseHelper.getCookie().clone();
            info.userID = id.Trim();
            info.name = name.Trim();
            info.sex = sex.Trim();
            info.card = card.Trim();
            info.tel = tel.Trim();
            info.address = address.Trim();

            DaUserInfo dal = new DaUserInfo();
            int isAccess = dal.edit(info);

            if (isAccess == 1)
            {
                Session["info"] = info;
                BaseHelper.saveCookie(info);
            }

            var result = new CustomJsonResult();
            result.Data = isAccess;            

            return result;
        }
        #endregion
        
    }
}
