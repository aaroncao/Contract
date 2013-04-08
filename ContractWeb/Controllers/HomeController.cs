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
    //系统通用
    public class HomeController : Controller
    {
        /* ============ 界面 ============ */

        #region 登录界面
        /// <summary>
        /// 登录界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        #endregion

        #region 平台首页
        /// <summary>
        /// 平台首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.menu = 0;
            ViewBag.Message = "欢迎使用系统平台";
            return View();
        }
        #endregion

        #region 退出系统
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        public ActionResult Exit()
        {
            HttpCookie cookie = new HttpCookie("info");
            cookie.Expires = DateTime.Now.AddMonths(-1);
            cookie.Values.Clear();
            Response.AppendCookie(cookie);
            Response.Cookies.Add(cookie);

            return View();
        }
        #endregion

        /* ============ 操作 ============ */

        #region 登录验证
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public JsonResult logOn(string userID, string password, string t)
        {
            DaUserInfo daUser = new DaUserInfo();
            UserInfo info = daUser.checkUserID(userID, password);
            var result = new CustomJsonResult();            

            if (info != null)
            {
                Session["userInfo"] = info;
                Response.AppendCookie(BaseHelper.saveCookie(info)); 

                result.Data = 1;
            }
            else
            {
                result.Data = 0;
            }

            return result;
        }
        #endregion

        #region 获取用户名
        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        public JsonResult getUserName(string t)
        {
            UserInfo en = BaseHelper.getCookie();

            var result = new CustomJsonResult();
            result.Data = en.name;
            return result;
        }
        #endregion

        #region 获取菜单数据
        /// <summary>
        /// 获取菜单数据
        /// </summary>
        /// <returns></returns>
        public JsonResult getMenuData(string t)
        {
            UserInfo info = BaseHelper.getCookie();
            List<MenuItem> list = BaseHelper.getMenuData(info.userID);

            var result = new CustomJsonResult();
            result.Data = list;
            return result;
        }
        #endregion

        /* ============ 下拉框取数 ============ */

        #region 获取业务员列表
        /// <summary>
        /// 获取业务员列表
        /// </summary>
        /// <returns></returns>
        public JsonResult drpPersonList()
        {
            DaUserInfo dal = new DaUserInfo();
            IList<UserInfo> users = dal.getList();

            var result = new CustomJsonResult();
            result.Data = users;
            return result;
        }
        #endregion

        #region 获取权限组列表
        /// <summary>
        /// 获取权限组列表
        /// </summary>
        /// <returns></returns>
        public JsonResult drpGroupList()
        {
            DaPowerGroup dal = new DaPowerGroup();
            IList<PowerGroup> groups = dal.getList();

            var result = new CustomJsonResult();
            result.Data = groups;
            return result;
        }
        #endregion

        #region 获取权限类型列表
        /// <summary>
        /// 获取权限类型列表
        /// </summary>
        /// <returns></returns>
        public JsonResult drpGroupOperTypeList()
        {
            IList<ModuleType> types = new List<ModuleType>();
            types.Add(new ModuleType(0, "禁止"));
            types.Add(new ModuleType(1, "可读"));
            types.Add(new ModuleType(2, "可写"));

            var result = new CustomJsonResult();
            result.Data = types;
            return result;
        }
        #endregion

        #region 获取地区列表
        /// <summary>
        /// 获取地区列表
        /// </summary>
        /// <returns></returns>
        public JsonResult drpAreaList()
        {
            DaAreaInfo dal = new DaAreaInfo();
            IList<AreaInfo> areas = dal.getList();

            var result = new CustomJsonResult();
            result.Data = areas;
            return result;
        }
        #endregion

        #region 获取影院列表
        /// <summary>
        /// 获取影院列表
        /// </summary>
        /// <returns></returns>
        public JsonResult drpCinemaList()
        {
            DaCinema dal = new DaCinema();
            IList<Cinema> cinemas = dal.getList();

            var result = new CustomJsonResult();
            result.Data = cinemas;
            return result;
        }
        #endregion

        #region 获取影厅类型列表
        /// <summary>
        /// 获取影厅类型列表
        /// </summary>
        /// <returns></returns>
        public JsonResult drpCinemaRoomTypeList()
        {
            DaCinemaRoomType dal = new DaCinemaRoomType();
            IList<CinemaRoomType> types = dal.getList();

            var result = new CustomJsonResult();
            result.Data = types;
            return result;
        }
        #endregion

        #region 根据影院ID获取影厅列表
        /// <summary>
        /// 根据影院ID获取影厅列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult drpCinemaRoomList(string id)
        {
            DaCinemaRoom dal = new DaCinemaRoom();
            IList<CinemaRoom> rooms = dal.getList(id);

            var result = new CustomJsonResult();
            result.Data = rooms;
            return result;
        }
        #endregion

        #region 获取客户渠道类型数据
        /// <summary>
        /// 获取客户渠道类型数据
        /// </summary>
        /// <returns></returns>
        public JsonResult drpChannelTypeList()
        {
            DaCustomerInfo dal = new DaCustomerInfo();
            IList<ChannelType> types = dal.getChannelTypeList();

            var result = new CustomJsonResult();
            result.Data = types;
            return result;
        }
        #endregion

        #region 获取客户状态列表
        /// <summary>
        /// 获取客户状态列表
        /// </summary>
        /// <returns></returns>
        public JsonResult drpCustomerStateList()
        {
            DaCustomerState dal = new DaCustomerState();
            IList<CustomerState> states = dal.getList();

            var result = new CustomJsonResult();
            result.Data = states;
            return result;
        }
        #endregion

        #region 获取合同类型列表
        /// <summary>
        /// 获取合同类型列表
        /// </summary>
        /// <returns></returns>
        public JsonResult drpContractTypeList()
        {
            DaContractType dal = new DaContractType();
            IList<ContractType> types = dal.getList();

            var result = new CustomJsonResult();
            result.Data = types;
            return result;
        }
        #endregion

        #region 获取渠道列表
        /// <summary>
        /// 获取渠道列表
        /// </summary>
        /// <returns></returns>
        public JsonResult drpChannelList()
        {
            DaChannel dal = new DaChannel();
            IList<Channel> channels = dal.getList();

            var result = new CustomJsonResult();
            result.Data = channels;
            return result;
        }
        #endregion

        #region 获取广告费结算对象列表
        /// <summary>
        /// 获取广告费结算对象列表
        /// </summary>
        /// <returns></returns>
        public JsonResult drpADTargetList()
        {
            DaADCostTarget dal = new DaADCostTarget();
            IList<ADCostTarget> targets = dal.getList();

            var result = new CustomJsonResult();
            result.Data = targets;
            return result;
        }
        #endregion

        #region 获取制作费结算对象列表
        /// <summary>
        /// 获取制作费结算对象列表
        /// </summary>
        /// <returns></returns>
        public JsonResult drpMakeTargetList()
        {
            DaMakeCostTarget dal = new DaMakeCostTarget();
            IList<MakeCostTarget> targets = dal.getList();

            var result = new CustomJsonResult();
            result.Data = targets;
            return result;
        }
        #endregion

        #region 获取开票类型列表
        /// <summary>
        /// 获取开票类型列表
        /// </summary>
        /// <returns></returns>
        public JsonResult drpBillTypeList()
        {
            DaBillType dal = new DaBillType();
            IList<BillType> types = dal.getList();

            var result = new CustomJsonResult();
            result.Data = types;
            return result;
        }
        #endregion

        #region 获取合同状态列表
        /// <summary>
        /// 获取合同状态列表
        /// </summary>
        /// <returns></returns>
        public JsonResult drpContractStateList()
        {
            DaContractState dal = new DaContractState();
            IList<ContractState> types = dal.getList();

            var result = new CustomJsonResult();
            result.Data = types;
            return result;
        }
        #endregion

        #region 获取发票状态列表
        /// <summary>
        /// 获取发票状态列表
        /// </summary>
        /// <returns></returns>
        public JsonResult drpBillStateList()
        {
            DaBillState dal = new DaBillState();
            IList<BillState> types = dal.getList();

            var result = new CustomJsonResult();
            result.Data = types;
            return result;
        }
        #endregion

        #region 获取打款状态列表
        /// <summary>
        /// 获取渠道列表
        /// </summary>
        /// <returns></returns>
        public JsonResult drpAccountStateList()
        {
            DaAccountState dal = new DaAccountState();
            IList<AccountState> states = dal.getList();

            var result = new CustomJsonResult();
            result.Data = states;
            return result;
        }
        #endregion
    }
}
