using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using ContractWeb.Common;
using ContractWeb.Models;
using ContractWeb.DataAccess;

namespace ContractWeb.Controllers
{
    public class BusinessController : Controller
    {
        /// <summary>
        /// 合同管理界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Contract()
        {
            return View();
        }

        /// <summary>
        /// 下单
        /// </summary>
        /// <returns></returns>
        public ActionResult Order()
        {
            return View();
        }

        /// <summary>
        /// 客户到账登记界面
        /// </summary>
        /// <returns></returns>
        public ActionResult CPay()
        {
            return View();
        }

        /// <summary>
        /// 开发票登记
        /// </summary>
        /// <returns></returns>
        public ActionResult WriteBill()
        {
            return View();
        }

        /// <summary>
        /// 广告费结算
        /// </summary>
        /// <returns></returns>
        public ActionResult ADCostAccount()
        {
            return View();
        }

        /// <summary>
        /// 制作费结算
        /// </summary>
        /// <returns></returns>
        public ActionResult MakeCostAccount()
        {
            return View();
        }

        /// <summary>
        /// 获取合同列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getContractList()
        {
            DaContractInfo dal = new DaContractInfo();
            IList<ContractInfo> contracts = dal.getList();

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = contracts.Count, rows = contracts };
            return result;
        }

        /// <summary>
        /// 获取合同类型列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getDrpContractTypeList()
        {
            DaContractType dal = new DaContractType();
            IList<ContractType> types = dal.getList();

            var result = new CustomJsonResult();
            result.Data = types;
            return result;
        }

        /// <summary>
        /// 获取渠道列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getDrpChannelList()
        {
            DaChannel dal = new DaChannel();
            IList<Channel> channels = dal.getList();

            var result = new CustomJsonResult();
            result.Data = channels;
            return result;
        }

        /// <summary>
        /// 获取经办人列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getDrpPersonList()
        {
            DaUserInfo dal = new DaUserInfo();
            IList<UserInfo> users = dal.getList();

            var result = new CustomJsonResult();
            result.Data = users;
            return result;
        }

        /// <summary>
        /// 添加合同
        /// </summary>
        /// <param name="id">合同编号</param>
        /// <param name="name">合同名称</param>
        /// <param name="version">版本</param>
        /// <param name="price">单厅价格</param>
        /// <param name="num">每场厅数</param>
        /// <param name="money">签署金额</param>
        /// <param name="make">制作费</param>
        /// <param name="back">优惠</param>
        /// <param name="type">合同类型</param>
        /// <param name="channel">渠道归类</param>
        /// <param name="begintime">合同周期起</param>
        /// <param name="endtime">合同周期止</param>
        /// <param name="zq">周期</param>
        /// <param name="person">经办人</param>
        /// <param name="mDate">签署日期</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addContract(string id, string name, string version, string price, string num, 
            string money, string make, string back, string type, string channel, string begintime, string endtime, 
            string zq, string person, string mDate, string memo)
        {
            ContractInfo en = new ContractInfo();
            en.contractID = id;
            en.name = name;
            en.version = version;
            en.price = Convert.ToDouble(price);
            en.roomNum = Convert.ToInt32(num);
            en.money = Convert.ToDouble(money);
            en.makeCost = Convert.ToDouble(make);
            en.backMoney = Convert.ToDouble(back);
            en.type = Convert.ToInt32(type);
            en.channelID = Convert.ToInt32(channel);
            en.begintime = Convert.ToDateTime(begintime);
            en.endtime = Convert.ToDateTime(endtime);
            en.ZQ = Convert.ToDouble(zq);
            en.personID = Convert.ToInt32(person);
            en.mDate = Convert.ToDateTime(mDate);
            en.memo = memo;

            DaContractInfo dal = new DaContractInfo();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }

        /// <summary>
        /// 根据编号获取合同
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public JsonResult getContract(string id)
        {
            DaContractInfo dal = new DaContractInfo();
            ContractInfo en = dal.getEntity(id);

            var result = new CustomJsonResult();
            result.Data = en;
            return result;
        }

        /// <summary>
        /// 添加到账登记
        /// </summary>
        /// <param name="id">合同编号</param>
        /// <param name="money">到账金额</param>
        /// <param name="date">到账日期</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addPay(string id, string money, string date)
        {
            CustomerPay en = new CustomerPay();
            en.contractID = id;
            en.money = Convert.ToDouble(money);
            en.date = Convert.ToDateTime(date);

            DaCustomerPay dal = new DaCustomerPay();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }

        /// <summary>
        /// 获取发票列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getBillList()
        {
            DaBill dal = new DaBill();
            IList<Bill> bills = dal.getList();

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = bills.Count, rows = bills };
            return result;
        }

        /// <summary>
        /// 获取开票类型列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getDrpBillTypeList()
        {
            DaBillType dal = new DaBillType();
            IList<BillType> types = dal.getList();

            var result = new CustomJsonResult();
            result.Data = types;
            return result;
        }

        /// <summary>
        /// 添加开票登记
        /// </summary>
        /// <param name="id">合同编号</param>
        /// <param name="type">开票类型</param>
        /// <param name="money">开票金额</param>
        /// <param name="date">开票日期</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addBill(string id, string type, string money, string date)
        {
            Bill en = new Bill();
            en.contractID = id;
            en.type = Convert.ToInt32(type);
            en.money = Convert.ToDouble(money);
            en.date = Convert.ToDateTime(date);

            DaBill dal = new DaBill();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }

        /// <summary>
        /// 根据订单编号获取合同订单信息
        /// </summary>
        /// <returns></returns>
        public JsonResult getContractOrder(string id)
        {
            DaOrderInfo dal = new DaOrderInfo();
            ContractOrder en = dal.getContractOrder(id);

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = en;
            return result;
        }

        /// <summary>
        /// 根据订单编号获取投放列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult getPutinList(string id)
        {
            DaPutinInfo dal = new DaPutinInfo();
            IList<PutinInfo> putins = dal.getList(id);

            var result = new CustomJsonResult();
            result.Data = new { total = putins.Count, rows = putins };
            return result;
        }

        /// <summary>
        /// 添加广告费结算
        /// </summary>
        /// <param name="id">订单编号</param>
        /// <param name="money">结算金额</param>
        /// <param name="state">打款状态</param>
        /// <param name="date">日期</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addADCostAccount(string id, string money, string state, string date, string memo)
        {
            ADCostAccount en = new ADCostAccount();
            en.orderID = id;
            en.money = Convert.ToDouble(money);
            en.state = Convert.ToInt32(state);
            en.date = Convert.ToDateTime(date);
            en.memo = memo;

            DaADCostAccount dal = new DaADCostAccount();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }

        /// <summary>
        /// 添加制作费结算
        /// </summary>
        /// <param name="id">订单编号</param>
        /// <param name="money">结算金额</param>
        /// <param name="state">打款状态</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addMakeCostAccount(string id, string money, string state, string date)
        {
            MakeCostAccount en = new MakeCostAccount();
            en.orderID = id;
            en.money = Convert.ToDouble(money);
            en.state = Convert.ToInt32(state);
            en.date = Convert.ToDateTime(date);

            DaMakeCostAccount dal = new DaMakeCostAccount();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }

        /// <summary>
        /// 获取广告费结算对象列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getDrpADTargetList()
        {
            DaADCostTarget dal = new DaADCostTarget();
            IList<ADCostTarget> targets = dal.getList();

            var result = new CustomJsonResult();
            result.Data = targets;
            return result;
        }

        /// <summary>
        /// 获取制作费结算对象列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getDrpMakeTargetList()
        {
            DaMakeCostTarget dal = new DaMakeCostTarget();
            IList<MakeCostTarget> targets = dal.getList();

            var result = new CustomJsonResult();
            result.Data = targets;
            return result;
        }

        /// <summary>
        /// 获取订单编号
        /// </summary>
        /// <returns></returns>
        public JsonResult buildOrderID()
        {
            DaOrderInfo dal = new DaOrderInfo();
            string id = dal.buildOrderIDofDay();

            var result = new CustomJsonResult();
            result.Data = id;
            return result;
        }

        /// <summary>
        /// 获取影院列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getDrpCinemeList()
        {
            DaCinema dal = new DaCinema();
            IList<Cinema> cinemas = dal.getList();

            var result = new CustomJsonResult();
            result.Data = cinemas;
            return result;
        }

        /// <summary>
        /// 获取影厅列表
        /// </summary>
        /// <param name="id">影院ID</param>
        /// <returns></returns>
        public JsonResult getCinemaRoomList(string id)
        {
            DaCinemaRoom dal = new DaCinemaRoom();
            IList<CinemaRoom> rooms = dal.getList(id);

            var result = new CustomJsonResult();
            result.Data = new { total = rooms.Count, rows = rooms };
            return result;
        }

        /// <summary>
        /// 添加投放影厅
        /// </summary>
        /// <param name="contract">合同编号</param>
        /// <param name="order">订单编号</param>
        /// <param name="ids">影厅ID，逗号隔开</param>
        /// <returns></returns>
        public JsonResult addPutin(string contract, string order, string ids)
        {
            string[] id = ids.Split(',');

            DaCinemaRoom dal = new DaCinemaRoom();
            IList<CinemaRoom> rooms = dal.getList(id);

            List<PutinInfo> list = new List<PutinInfo>();
            for (int i = 0; i < rooms.Count; i++)
            {
                PutinInfo en = new PutinInfo();
                en.contractID = contract;
                en.orderID = order;
                en.cinemaID = rooms[i].cinemaID;
                en.cinemaRoomID = rooms[i].id;
                en.roomTypeID = rooms[i].typeID;

                list.Add(en);
            }

            DaPutinInfo dal1 = new DaPutinInfo();
            var result = new CustomJsonResult();
            result.Data = dal1.add(list);
            return result;
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getOrderList()
        {
            DaOrderInfo dal = new DaOrderInfo();
            IList<OrderInfo> orders = dal.getList();

            var result = new CustomJsonResult();
            result.Data = new { total = orders.Count, rows = orders };
            return result;
        }

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="contractID">合同编号</param>
        /// <param name="orderID">订单编号</param>
        /// <param name="adID">广告费结算对象编号</param>
        /// <param name="makeID">制作费结算对象编号</param>
        /// <param name="num">投放厅数</param>
        /// <param name="begin">下单开始时间</param>
        /// <param name="end">下单结束时间</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult addOrder(string contractID, string orderID, string adID, string makeID, string num, string begin, string end, string memo)
        {
            if (num.Trim() == "")
                num = "0";

            OrderInfo en = new OrderInfo();
            en.contractID = contractID;
            en.orderID = orderID;
            en.costTargetID = Convert.ToInt32(adID);
            en.makeTargetID = Convert.ToInt32(makeID);
            en.roomNum = Convert.ToInt32(num);
            en.begintime = Convert.ToDateTime(begin);
            en.endtime = Convert.ToDateTime(end);
            en.memo = memo;

            DaOrderInfo dal = new DaOrderInfo();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }
    }
}
