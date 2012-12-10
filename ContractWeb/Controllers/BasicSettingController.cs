using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using ContractWeb.Common;
using ContractWeb.Models;
using ContractWeb.DataAccess;

namespace ContractWeb.Controllers
{
    public class BasicSettingController : Controller
    {
        /// <summary>
        /// 渠道类别界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Channel()
        {
            return View();
        }

        /// <summary>
        /// 客户有效期界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Validity()
        {
            DaCustomerInfo dal = new DaCustomerInfo();
            ViewBag.days = dal.getCustomerValidity();

            return View();
        }

        /// <summary>
        /// 地区界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Area()
        {
            return View();
        }

        /// <summary>
        /// 客户状态界面
        /// </summary>
        /// <returns></returns>
        public ActionResult State()
        {
            return View();
        }

        /// <summary>
        /// 广告费结算对象界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ADCost()
        {
            return View();
        }

        /// <summary>
        /// 制作费结算对象界面
        /// </summary>
        /// <returns></returns>
        public ActionResult MakeCost()
        {
            return View();
        }

        /// <summary>
        /// 客户资料界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Customer()
        {
            return View();
        }

        /// <summary>
        /// 影院信息界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Cinema()
        {
            return View();
        }

        /// <summary>
        /// 影厅信息界面
        /// </summary>
        /// <returns></returns>
        public ActionResult CinemaRoom()
        {
            return View();
        }

        /// <summary>
        /// 获取渠道列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getChannelList()
        {
            DaChannel dal = new DaChannel();
            IList<Channel> channels = dal.getList();

            var result = new CustomJsonResult();
            result.Data = new { total = channels.Count, rows = channels };
            return result;
        }

        /// <summary>
        /// 添加渠道类别
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addChannel(string name, string memo)
        {
            Channel en = new Channel();
            en.name = name;
            en.memo = memo;

            DaChannel dal = new DaChannel();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }

        /// <summary>
        /// 设置客户有效期
        /// </summary>
        /// <param name="days">天数</param>
        /// <returns></returns>
        public JsonResult setValidity(string days)
        {
            DaCustomerInfo dal = new DaCustomerInfo();
            var result = new CustomJsonResult();
            result.Data = dal.setCustomerValidity(days);

            return result;
        }

        /// <summary>
        /// 获取地区列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getAreaList()
        {
            DaAreaInfo dal = new DaAreaInfo();
            IList<AreaInfo> areas = dal.getList();

            var result = new CustomJsonResult();
            result.Data = new { total = areas.Count, rows = areas };
            return result;
        }

        /// <summary>
        /// 获取地区列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getDrpAreaList()
        {
            DaAreaInfo dal = new DaAreaInfo();
            IList<AreaInfo> areas = dal.getList();

            var result = new CustomJsonResult();
            result.Data = areas;
            return result;
        }

        /// <summary>
        /// 添加地区
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addArea(string name, string memo)
        {
            AreaInfo en = new AreaInfo();
            en.name = name;
            en.memo = memo;

            DaAreaInfo dal = new DaAreaInfo();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }

        /// <summary>
        /// 获取客户状态列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getStateList()
        {
            DaCustomerState dal = new DaCustomerState();
            IList<CustomerState> states = dal.getList();

            var result = new CustomJsonResult();
            result.Data = new { total = states.Count, rows = states };
            return result;
        }

        /// <summary>
        /// 获取客户状态列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getdrpStateList()
        {
            DaCustomerState dal = new DaCustomerState();
            IList<CustomerState> states = dal.getList();

            var result = new CustomJsonResult();
            result.Data = states;
            return result;
        }

        /// <summary>
        /// 添加客户状态
        /// </summary>
        /// <param name="state">名称</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addState(string state, string memo)
        {
            CustomerState en = new CustomerState();
            en.state = state;
            en.memo = memo;

            DaCustomerState dal = new DaCustomerState();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }

        /// <summary>
        /// 获取广告费结算对象列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getADCostList()
        {
            DaADCostTarget dal = new DaADCostTarget();
            IList<ADCostTarget> targets = dal.getList();

            var result = new CustomJsonResult();
            result.Data = new { total = targets.Count, rows = targets };
            return result;
        }

        /// <summary>
        /// 添加广告费结算对象
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addADCost(string name, string memo)
        {
            ADCostTarget en = new ADCostTarget();
            en.target = name;
            en.memo = memo;

            DaADCostTarget dal = new DaADCostTarget();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }

        /// <summary>
        /// 获取制作费结算对象列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getMakeCostList()
        {
            DaMakeCostTarget dal = new DaMakeCostTarget();
            IList<MakeCostTarget> targets = dal.getList();

            var result = new CustomJsonResult();
            result.Data = new { total = targets.Count, rows = targets };
            return result;
        }

        /// <summary>
        /// 添加制作费结算对象
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addMakeCost(string name, string memo)
        {
            MakeCostTarget en = new MakeCostTarget();
            en.target = name;
            en.memo = memo;

            DaMakeCostTarget dal = new DaMakeCostTarget();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }

        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getCustomerList()
        {
            DaCustomerInfo dal = new DaCustomerInfo();
            IList<CustomerInfo> customers = dal.getList();

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = customers.Count, rows = customers };
            return result;
        }

        /// <summary>
        /// 获取客户渠道类型数据
        /// </summary>
        /// <returns></returns>
        public JsonResult getChannelTypeList()
        {
            DaCustomerInfo dal = new DaCustomerInfo();
            IList<ChannelType> types = dal.getChannelTypeList();

            var result = new CustomJsonResult();
            result.Data = types;
            return result;
        }

        /// <summary>
        /// 新增客户
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">渠道类型</param>
        /// <param name="person">联系人</param>
        /// <param name="tel">联系电话</param>
        /// <param name="officeTel">办公电话</param>
        /// <param name="eMail">E-Mail</param>
        /// <param name="fex">传真</param>
        /// <param name="address">联系电话</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addCustomer(string name, string type, string person, string tel, string officeTel, string eMail, string fex, string address, string state)
        {
            CustomerInfo en = new CustomerInfo();
            en.name = name;
            en.channelTypeID = Convert.ToInt32(type);
            en.person = person;
            en.tel = tel;
            en.officeTel = officeTel;
            en.email = eMail;
            en.fex = fex;
            en.address = address;
            en.stateID = Convert.ToInt32(state);

            DaCustomerInfo dal = new DaCustomerInfo();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }

        /// <summary>
        /// 获取影院列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getCinemaList()
        {
            DaCinema dal = new DaCinema();
            IList<Cinema> cinemas = dal.getList();

            var result = new CustomJsonResult();
            result.Data = new { total = cinemas.Count, rows = cinemas };
            return result;
        }

        /// <summary>
        /// 获取影院列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getDrpCinemaList()
        {
            DaCinema dal = new DaCinema();
            IList<Cinema> cinemas = dal.getList();

            var result = new CustomJsonResult();
            result.Data = cinemas;
            return result;
        }

        /// <summary>
        /// 新增影院
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="area">地区</param>
        /// <param name="roomNum">厅数</param>
        /// <param name="person">联系人</param>
        /// <param name="tel">联系电话</param>
        /// <param name="address">地址</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addCinema(string name, string area, string roomNum, string person, string tel, string address)
        {
            Cinema en = new Cinema();
            en.name = name;
            en.areaID = Convert.ToInt32(area);
            en.roomNum = Convert.ToInt32(roomNum);
            en.person = person;
            en.tel = tel;
            en.address = address;

            DaCinema dal = new DaCinema();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }

        /// <summary>
        /// 获取影厅类型列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getdrpCinemaRoomTypeList()
        {
            DaCinemaRoomType dal = new DaCinemaRoomType();
            IList<CinemaRoomType> types = dal.getList();

            var result = new CustomJsonResult();
            result.Data = types;
            return result;
        }

        /// <summary>
        /// 获取影厅列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getCinemaRoomList()
        {
            DaCinemaRoom dal = new DaCinemaRoom();
            IList<CinemaRoom> rooms = dal.getList();

            var result = new CustomJsonResult();
            result.Data = new { total = rooms.Count, rows = rooms };
            return result;
        }

        /// <summary>
        /// 添加影厅
        /// </summary>
        /// <param name="room">名称</param>
        /// <param name="cinemaID">影院</param>
        /// <param name="typeID">影厅类型</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addCinemaRoom(string room, string cinemaID, string typeID, string memo)
        {
            CinemaRoom en = new CinemaRoom();
            en.room = room;
            en.cinemaID = Convert.ToInt32(cinemaID);
            en.typeID = Convert.ToInt32(typeID);
            en.memo = memo;

            DaCinemaRoom dal = new DaCinemaRoom();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }

    }
}
