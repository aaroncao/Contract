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
        #region 渠道类别界面
        /// <summary>
        /// 渠道类别界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Channel()
        {
            ViewBag.menu = 7;
            return View();
        }
        #endregion

        #region 客户有效期界面
        /// <summary>
        /// 客户有效期界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Validity()
        {
            DaCustomerInfo dal = new DaCustomerInfo();
            ViewBag.days = dal.getCustomerValidity();
            ViewBag.menu = 8;

            return View();
        }
        #endregion

        #region 权限组设定
        /// <summary>
        /// 权限组设定
        /// </summary>
        /// <returns></returns>
        public ActionResult PowerSetting()
        {
            ViewBag.menu = 33;
            return View();
        }
        #endregion

        #region 地区界面
        /// <summary>
        /// 地区界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Area()
        {
            ViewBag.menu = 9;
            return View();
        }
        #endregion

        #region 客户状态界面
        /// <summary>
        /// 客户状态界面
        /// </summary>
        /// <returns></returns>
        public ActionResult State()
        {
            ViewBag.menu = 10;
            return View();
        }
        #endregion

        #region 广告费结算对象界面
        /// <summary>
        /// 广告费结算对象界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ADCost()
        {
            ViewBag.menu = 11;
            return View();
        }
        #endregion

        #region 制作费结算对象界面
        /// <summary>
        /// 制作费结算对象界面
        /// </summary>
        /// <returns></returns>
        public ActionResult MakeCost()
        {
            ViewBag.menu = 12;
            return View();
        }
        #endregion

        #region 客户资料界面
        /// <summary>
        /// 客户资料界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Customer()
        {
            ViewBag.menu = 13;
            return View();
        }
        #endregion

        #region 影院信息界面
        /// <summary>
        /// 影院信息界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Cinema()
        {
            ViewBag.menu = 14;
            return View();
        }
        #endregion

        #region 影厅信息界面
        /// <summary>
        /// 影厅信息界面
        /// </summary>
        /// <returns></returns>
        public ActionResult CinemaRoom()
        {
            ViewBag.menu = 15;
            return View();
        }
        #endregion




        #region 获取渠道列表
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
        #endregion

        #region 添加渠道类别
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
        #endregion

        #region 修改渠道类别
        /// <summary>
        /// 修改渠道类别
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult updateChannel(string id, string name, string memo)
        {
            Channel en = new Channel();
            en.id = Convert.ToInt32(id);
            en.name = name;
            en.memo = memo;

            DaChannel dal = new DaChannel();
            var result = new CustomJsonResult();
            result.Data = dal.update(en);
            return result;
        }
        #endregion

        #region 删除渠道类别
        /// <summary>
        /// 删除渠道类别
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public JsonResult deleteChannel(string id)
        {
            Channel en = new Channel();
            en.id = Convert.ToInt32(id);

            DaChannel dal = new DaChannel();
            var result = new CustomJsonResult();
            result.Data = dal.delete(en);
            return result;
        }
        #endregion

        #region 设置客户有效期
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
        #endregion

        #region 获取地区列表
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
        #endregion

        #region 添加地区
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
        #endregion

        #region 修改地区
        /// <summary>
        /// 修改地区
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult updateArea(string id, string name, string memo)
        {
            AreaInfo en = new AreaInfo();
            en.id = Convert.ToInt32(id);
            en.name = name;
            en.memo = memo;

            DaAreaInfo dal = new DaAreaInfo();
            var result = new CustomJsonResult();
            result.Data = dal.update(en);
            return result;
        }
        #endregion

        #region 删除地区
        /// <summary>
        /// 删除地区
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult deleteArea(string id)
        {
            AreaInfo en = new AreaInfo();
            en.id = Convert.ToInt32(id);

            DaAreaInfo dal = new DaAreaInfo();
            var result = new CustomJsonResult();
            result.Data = dal.delete(en);
            return result;
        }
        #endregion

        #region 获取客户状态列表
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
        #endregion

        #region 添加客户状态
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
        #endregion

        #region 修改客户状态
        /// <summary>
        /// 修改客户状态
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="state">名称</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult updateState(string id, string state, string memo)
        {
            CustomerState en = new CustomerState();
            en.id = Convert.ToInt32(id);
            en.state = state;
            en.memo = memo;

            DaCustomerState dal = new DaCustomerState();
            var result = new CustomJsonResult();
            result.Data = dal.update(en);
            return result;
        }
        #endregion

        #region 删除客户状态
        /// <summary>
        /// 删除客户状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult deleteState(string id)
        {
            CustomerState en = new CustomerState();
            en.id = Convert.ToInt32(id);

            DaCustomerState dal = new DaCustomerState();
            var result = new CustomJsonResult();
            result.Data = dal.delete(en);
            return result;
        }
        #endregion

        #region 获取广告费结算对象列表
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
        #endregion

        #region 添加广告费结算对象
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
        #endregion

        #region 修改广告费结算对象
        /// <summary>
        /// 修改广告费结算对象
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult updateADCost(string id, string name, string memo)
        {
            ADCostTarget en = new ADCostTarget();
            en.id = Convert.ToInt32(id);
            en.target = name;
            en.memo = memo;

            DaADCostTarget dal = new DaADCostTarget();
            var result = new CustomJsonResult();
            result.Data = dal.update(en);
            return result;
        }
        #endregion

        #region 删除广告费结算对象
        /// <summary>
        /// 删除广告费结算对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult deleteADCost(string id)
        {
            ADCostTarget en = new ADCostTarget();
            en.id = Convert.ToInt32(id);

            DaADCostTarget dal = new DaADCostTarget();
            var result = new CustomJsonResult();
            result.Data = dal.delete(en);
            return result;
        }
        #endregion

        #region 获取制作费结算对象列表
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
        #endregion

        #region 添加制作费结算对象
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
        #endregion

        #region 修改制作费结算对象
        /// <summary>
        /// 修改制作费结算对象
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult updateMakeCost(string id, string name, string memo)
        {
            MakeCostTarget en = new MakeCostTarget();
            en.id = Convert.ToInt32(id);
            en.target = name;
            en.memo = memo;

            DaMakeCostTarget dal = new DaMakeCostTarget();
            var result = new CustomJsonResult();
            result.Data = dal.update(en);
            return result;
        }
        #endregion

        #region 删除制作费结算对象
        /// <summary>
        /// 删除制作费结算对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult deleteMakeCost(string id)
        {
            MakeCostTarget en = new MakeCostTarget();
            en.id = Convert.ToInt32(id);

            DaMakeCostTarget dal = new DaMakeCostTarget();
            var result = new CustomJsonResult();
            result.Data = dal.delete(en);
            return result;
        }
        #endregion

        #region 获取影院列表
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
        #endregion

        #region 获取地区列表
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
        #endregion

        #region 新增影院
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
        #endregion

        #region 修改影院
        /// <summary>
        /// 修改影院
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="area">地区</param>
        /// <param name="roomNum">厅数</param>
        /// <param name="person">联系人</param>
        /// <param name="tel">联系电话</param>
        /// <param name="address">地址</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult updateCinema(string id, string name, string area, string roomNum, string person, string tel, string address)
        {
            Cinema en = new Cinema();
            en.id = Convert.ToInt32(id);
            en.name = name;
            en.areaID = Convert.ToInt32(area);
            en.roomNum = Convert.ToInt32(roomNum);
            en.person = person;
            en.tel = tel;
            en.address = address;

            DaCinema dal = new DaCinema();
            var result = new CustomJsonResult();
            result.Data = dal.update(en);
            return result;
        }
        #endregion

        #region 删除影院
        /// <summary>
        /// 删除影院
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult deleteCinema(string id)
        {
            Cinema en = new Cinema();
            en.id = Convert.ToInt32(id);

            DaCinema dal = new DaCinema();
            var result = new CustomJsonResult();
            result.Data = dal.delete(en);
            return result;
        }
        #endregion

        #region 搜索影院
        /// <summary>
        /// 搜索影院
        /// </summary>
        /// <param name="name">影院名称</param>
        /// <param name="area">所属地区</param>
        /// <returns></returns>
        public JsonResult searchCinema(string sName, string sArea)
        {
            Cinema en = new Cinema();
            en.name = sName;
            en.area = sArea;

            DaCinema dal = new DaCinema();
            var result = new CustomJsonResult();
            result.Data = dal.getList(sName, sArea);
            return result;
        }
        #endregion

        #region 获取影厅列表
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
        #endregion

        #region 根据影院ID获取影厅列表
        /// <summary>
        /// 根据影院ID获取影厅列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult getDrpCinemaRoomList(string id)
        {
            DaCinemaRoom dal = new DaCinemaRoom();
            IList<CinemaRoom> rooms = dal.getList(id);

            var result = new CustomJsonResult();
            result.Data = rooms;
            return result;
        }
        #endregion

        #region 获取影院列表
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
        #endregion

        #region 获取影厅类型列表
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
        #endregion

        #region 添加影厅
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
        #endregion

        #region 修改影厅
        /// <summary>
        /// 修改影厅
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="cinema">所属影院</param>
        /// <param name="type">影厅类型</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult updateCinemaRoom(string id, string name, string cinema, string type, string memo)
        {
            CinemaRoom en = new CinemaRoom();
            en.id = Convert.ToInt32(id);
            en.room = name;
            en.cinemaID = Convert.ToInt32(cinema);
            en.typeID = Convert.ToInt32(type);
            en.memo = memo;

            DaCinemaRoom dal = new DaCinemaRoom();
            var result = new CustomJsonResult();
            result.Data = dal.update(en);
            return result;
        }
        #endregion

        #region 删除影厅
        /// <summary>
        /// 删除影厅
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult deleteCinemaRoom(string id)
        {
            CinemaRoom en = new CinemaRoom();
            en.id = Convert.ToInt32(id);

            DaCinemaRoom dal = new DaCinemaRoom();
            var result = new CustomJsonResult();
            result.Data = dal.delete(en);
            return result;
        }
        #endregion

        #region 搜索影厅
        /// <summary>
        /// 搜索影厅
        /// </summary>
        /// <param name="cinema">影院名称</param>
        /// <returns></returns>
        public JsonResult searchCinemaRoom(string cinema)
        {
            DaCinemaRoom dal = new DaCinemaRoom();
            var result = new CustomJsonResult();

            if (cinema.Trim() != "")
            result.Data = dal.getList(cinema);
            else
                result.Data = dal.getList();

            return result;
        }
        #endregion

        #region 获取客户列表
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
        #endregion

        #region 获取客户状态列表
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
        #endregion

        #region 获取客户渠道类型数据
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
        #endregion

        #region 获取业务员列表
        /// <summary>
        /// 获取业务员列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getdrpUserList()
        {
            DaUserInfo dal = new DaUserInfo();
            IList<UserInfo> users = dal.getList();

            var result = new CustomJsonResult();
            result.Data = users;
            return result;
        }
        #endregion

        #region 新增客户
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
            en.salesmanID = BaseHelper.getCookie().id;
            en.stateID = state;

            DaCustomerInfo dal = new DaCustomerInfo();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }
        #endregion

        #region 修改客户
        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="id">编号</param>
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
        public JsonResult updateCustomer(string id, string name, string type, string person, string tel, string officeTel, string eMail, string fex, string address, string state)
        {
            CustomerInfo en = new CustomerInfo();
            en.id = Convert.ToInt32(id);
            en.name = name;
            en.channelTypeID = Convert.ToInt32(type);
            en.person = person;
            en.tel = tel;
            en.officeTel = officeTel;
            en.email = eMail;
            en.fex = fex;
            en.address = address;
            en.stateID = state;

            DaCustomerInfo dal = new DaCustomerInfo();
            var result = new CustomJsonResult();
            result.Data = dal.update(en);
            return result;
        }
        #endregion

        #region 删除客户
        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult deleteCustomer(string id)
        {
            CustomerInfo en = new CustomerInfo();
            en.id = Convert.ToInt32(id);

            DaCustomerInfo dal = new DaCustomerInfo();
            var result = new CustomJsonResult();
            result.Data = dal.delete(en);
            return result;
        }
        #endregion

        #region 搜索客户
        /// <summary>
        /// 搜索客户
        /// </summary>
        /// <param name="name">客户名称</param>
        /// <param name="type">客户类型</param>
        /// <param name="salesman">业务员</param>
        /// <returns></returns>
        public JsonResult searchCustomer(string name, string type, string salesman)
        {
            CustomerInfo en = new CustomerInfo();
            en.name = name;
            
            if (type.Trim() != "")
                en.channelTypeID = Convert.ToInt32(type);

            if (salesman.Trim() != "")
                en.salesmanID = Convert.ToInt32(salesman);

            DaCustomerInfo dal = new DaCustomerInfo();
            var result = new CustomJsonResult();
            result.Data = dal.getList(en);
            return result;
        }
        #endregion

        #region 获取权限模块列表
        /// <summary>
        /// 获取权限模块列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getModuleList()
        {
            DaModule dal = new DaModule();
            IList<Module> modules = dal.getList();

            var result = new CustomJsonResult();
            result.Data = new { total = modules.Count, rows = modules };
            return result;
        }
        #endregion

        #region 获取权限组列表
        /// <summary>
        /// 获取权限组列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getDrpGroupList()
        {
            DaPowerGroup dal = new DaPowerGroup();
            IList<PowerGroup> groups = dal.getList();

            var result = new CustomJsonResult();
            result.Data = groups;
            return result;
        }
        #endregion

        #region 获取权限组的权限
        /// <summary>
        /// 获取权限组的权限
        /// </summary>
        /// <param name="id">权限组ID</param>
        /// <returns></returns>
        public JsonResult getShowPower(string id, string t)
        {
            DaPowerGroupPower dal = new DaPowerGroupPower();
            IList<PowerGroupPower> groups = dal.getList(id);

            var result = new CustomJsonResult();
            result.Data = new { total = groups.Count, rows = groups };
            return result;
        }
        #endregion

        #region 添加权限
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public JsonResult addPower(string id, string modules)
        {
            if (modules.Trim() == "")
            {
                return null;
            }

            List<PowerGroupPower> addList = new List<PowerGroupPower>();
            string[] moduless = modules.Split(',');

            DaPowerGroupPower dal = new DaPowerGroupPower();
            IList<PowerGroupPower> groups = dal.getList(id);

            for (int i = 0; i < moduless.Length; i++)
            {
                bool flag = true;

                for (int j = 0; j < groups.Count; j++)
                {
                    if (moduless[i] == groups[i].moduleID.ToString())
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    PowerGroupPower en = new PowerGroupPower();
                    en.groupID = Convert.ToInt32(id);
                    en.moduleID = Convert.ToInt32(moduless[i]);
                    en.power = 0;

                    addList.Add(en);
                }
            }

            dal = new DaPowerGroupPower();
            dal.add(addList);

            var result = new CustomJsonResult();
            result.Data = 1;
            return result;
        }
        #endregion

        #region 删除权限
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public JsonResult removePower(string id, string modules)
        {
            List<PowerGroupPower> list = new List<PowerGroupPower>();
            string[] moduless = modules.Split(',');

            for (int i = 0; i < moduless.Length; i++)
            {
                PowerGroupPower en = new PowerGroupPower();
                en.groupID = Convert.ToInt32(id);
                en.moduleID = Convert.ToInt32(moduless[i]);

                list.Add(en);
            }

            DaPowerGroupPower dal = new DaPowerGroupPower();
            var result = new CustomJsonResult();
            result.Data = dal.delete(list);
            return result;
        }
        #endregion

        #region 保存权限
        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public JsonResult savePower(string id, string modules, string powers)
        {
            List<PowerGroupPower> list = new List<PowerGroupPower>();
            string[] moduless = modules.Split(',');
            string[] powerss = powers.Split(',');

            for (int i = 0; i < moduless.Length; i++)
            {
                PowerGroupPower en = new PowerGroupPower();
                en.groupID = Convert.ToInt32(id);
                en.moduleID = Convert.ToInt32(moduless[i]);
                en.power = Convert.ToInt32(powerss[i]);

                list.Add(en);
            }

            DaPowerGroupPower dal = new DaPowerGroupPower();
            var result = new CustomJsonResult();
            result.Data = dal.update(list);
            return result;
        }
        #endregion

        #region 获取权限类型列表
        /// <summary>
        /// 获取权限类型列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getDrpGroupTypeList()
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


    }
}
