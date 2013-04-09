using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;

using ContractWeb.Models;
using ND.Lib.Data.SqlHelper;


namespace ContractWeb.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        filterContextInfo fcinfo;

        /// <summary>
        /// 在执行操作方法之前由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool islogin = false;

            #region 登录验证

            UserInfo user = BaseHelper.getCookie();
            if (user != null && user.userID.Trim() != "")
                islogin = true;

            if (!islogin)
            {
                filterContext.Result = filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Home", action = "Login" }));
                return;
            }

            #endregion

            fcinfo = new filterContextInfo(filterContext);
            string[] actions = fcinfo.actionName.Split('_');
            string controller = fcinfo.controllerName;

            #region action或controller为空

            if (fcinfo.actionName.Trim() == "" || controller.Trim() == "")
            {
                filterContext.Result = filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Home", action = "Login" }));
                return;
            }

            #endregion

            //取权限 
            string strSql = "select c.power "
                + "from UserInfo a, PowerGroup b, PowerGroupPower c, SystemModule d "
                + "where a.powergroupID=b.ID and b.ID=c.groupID and c.moduleID=d.ID and d.controller=@controller and d.action=@action";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@controller", controller),
                new SqlParameter("@action", actions[0])
            };

            DataTable dt = SqlHelper.ExecuteDataset(BaseHelper.DBConnStr, CommandType.Text, strSql, param).Tables[0];

            //没有设置权限或禁止
            if (dt.Rows.Count == 0 || dt.Rows[0]["power"].ToString() == "0")
            {
                filterContext.Result = new ContentResult { Content = @"抱歉,你不具有当前操作的权限！" };
                return;
            }

            //只读
            int power = Convert.ToInt32(dt.Rows[0]["power"]);
            if (actions.Length > 1 && (actions[1].IndexOf("add") != -1 || actions[1].IndexOf("edit") != -1 || actions[1].IndexOf("remove") != -1) && power < 2)
            {
                filterContext.Result = new ContentResult { Content = @"抱歉,你不具有当前操作的权限！" };
                return;
            }
        }

        /// <summary>
        /// 在执行操作方法后由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        ///  OnResultExecuted 在执行操作结果后由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        /// <summary>
        /// OnResultExecuting 在执行操作结果之前由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

    }

    public class filterContextInfo
    {
        /// <summary>
        /// 获取域名
        /// </summary>
        public string domainName { get; set; }

        /// <summary>
        /// 获取模块名称
        /// </summary>
        public string module { get; set; }

        /// <summary>
        /// 获取 controllerName 名称
        /// </summary>
        public string controllerName { get; set; }

        /// <summary>
        /// 获取ACTION 名称
        /// </summary>
        public string actionName { get; set; }


        public filterContextInfo(ActionExecutingContext filterContext)
        {
            // 获取域名
            domainName = filterContext.HttpContext.Request.Url.Authority;

            //获取模块名称
            //  module = filterContext.HttpContext.Request.Url.Segments[1].Replace('/', ' ').Trim();

            //获取 controllerName 名称
            controllerName = filterContext.RouteData.Values["controller"].ToString();

            //获取ACTION 名称
            actionName = filterContext.RouteData.Values["action"].ToString();
        }
    }

}