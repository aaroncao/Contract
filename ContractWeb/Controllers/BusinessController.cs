using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using ContractWeb.Common;
using ContractWeb.Models;
using ContractWeb.DataAccess;

namespace ContractWeb.Controllers
{
    //业务管理
    [AuthorizeFilterAttribute]
    public class BusinessController : Controller
    {
        /* ============ 界面 ============ */

        #region 合同录入界面
        /// <summary>
        /// 合同录入界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Contract()
        {
            ViewBag.menu = 34;
            return View();
        }
        #endregion

        #region 下单界面
        /// <summary>
        /// 下单界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Order()
        {
            ViewBag.menu = 18;
            return View();
        }
        #endregion

        #region 客户到账登记界面
        /// <summary>
        /// 客户到账登记界面
        /// </summary>
        /// <returns></returns>
        public ActionResult CPay()
        {
            ViewBag.menu = 19;
            return View();
        }
        #endregion

        #region 开发票登记界面
        /// <summary>
        /// 开发票登记
        /// </summary>
        /// <returns></returns>
        public ActionResult WriteBill()
        {
            ViewBag.menu = 20;
            return View();
        }
        #endregion

        #region 广告费结算
        /// <summary>
        /// 广告费结算
        /// </summary>
        /// <returns></returns>
        public ActionResult ADCostAccount()
        {
            ViewBag.menu = 21;
            return View();
        }
        #endregion

        #region 制作费结算
        /// <summary>
        /// 制作费结算
        /// </summary>
        /// <returns></returns>
        public ActionResult MakeCostAccount()
        {
            ViewBag.menu = 22;
            return View();
        }
        #endregion

        #region 收发票登记
        /// <summary>
        /// 收发票登记
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceiveBill()
        {
            ViewBag.menu = 23;
            return View();
        }
        #endregion

        #region 下单概括修改
        /// <summary>
        /// 下单概括修改
        /// </summary>
        /// <returns></returns>
        public ActionResult EditOrder()
        {
            return View();
        }
        #endregion

        /* ============ 操作 ============ */
        
        #region 获取合同列表
        /// <summary>
        /// 获取合同列表
        /// </summary>
        /// <returns></returns>
        public JsonResult Contract_getList()
        {
            DaContractInfo dal = new DaContractInfo();
            IList<ContractInfo> contracts = dal.getList(BaseHelper.getCookie().id.ToString());

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = contracts.Count, rows = contracts };
            return result;
        }
        #endregion

        #region 添加合同
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
        public JsonResult Contract_add(string id, string name, string version, string price, string num,
            string money, string make, string back, string type, string channel, string begintime, string endtime,
            string zq, string person, string mDate, string memo)
        {
            ContractInfo en = new ContractInfo();
            en.contractID = id;
            en.name = name;
            en.version = version;

            if (price.Trim() != "")
                en.price = Convert.ToDouble(price);

            if (num.Trim() != "")
                en.roomNum = Convert.ToInt32(num);

            if (money.Trim() != "")
                en.money = Convert.ToDouble(money);

            if (make.Trim() != "")
                en.makeCost = Convert.ToDouble(make);

            if (back.Trim() != "")
                en.backMoney = Convert.ToDouble(back);

            if (type.Trim() != "")
                en.type = Convert.ToInt32(type);

            if (channel.Trim() != "")
                en.channelID = Convert.ToInt32(channel);

            if (begintime.Trim() != "")
                en.begintime = Convert.ToDateTime(begintime);

            if (endtime.Trim() != "")
                en.endtime = Convert.ToDateTime(endtime);

            if (zq.Trim() != "")
                en.ZQ = Convert.ToDouble(zq);

            if (person.Trim() != "")
                en.personID = Convert.ToInt32(person);

            if (mDate.Trim() != "")
                en.mDate = Convert.ToDateTime(mDate);

            en.memo = memo;

            DaContractInfo dal = new DaContractInfo();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }
        #endregion



        #region 获取订单列表
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <returns></returns>
        public JsonResult Order_getList()
        {
            DaOrderInfo dal = new DaOrderInfo();
            IList<OrderInfo> orders = dal.getList();

            var result = new CustomJsonResult();
            result.Data = new { total = orders.Count, rows = orders };
            return result;
        }
        #endregion

        #region 根据合同编号获取合同信息填充下单界面
        /// <summary>
        /// 根据合同编号获取合同信息填充下单界面
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public JsonResult Order_getContract(string id)
        {
            DaContractInfo dal = new DaContractInfo();
            ContractInfo en = dal.getEntity(id);

            var result = new CustomJsonResult();
            result.Data = en;
            return result;
        }
        #endregion

        #region 根据合同编号获取已经投放的影厅列表
        /// <summary>
        /// 根据合同编号获取已经投放的影厅列表
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public JsonResult Order_getPutin(string contract)
        {
            DaPutinInfo dal = new DaPutinInfo();
            IList<PutinInfo> list = dal.getListByContract(contract);

            var result = new CustomJsonResult();
            result.Data = new { total = list.Count, rows = list };
            return result;
        }
        #endregion

        #region 获取最新的订单编号
        /// <summary>
        /// 获取最新的订单编号
        /// </summary>
        /// <returns></returns>
        public JsonResult Order_getOrderID()
        {
            DaOrderInfo dal = new DaOrderInfo();
            string id = dal.buildOrderIDofDay();

            var result = new CustomJsonResult();
            result.Data = id;
            return result;
        }
        #endregion

        #region 获取影厅列表
        /// <summary>
        /// 获取影厅列表
        /// </summary>
        /// <param name="id">影院ID</param>
        /// <returns></returns>
        public JsonResult Order_getCinemaRoomList(string id)
        {
            DaCinemaRoom dal = new DaCinemaRoom();
            IList<CinemaRoom> rooms = dal.getList(id);

            var result = new CustomJsonResult();
            result.Data = new { total = rooms.Count, rows = rooms };
            return result;
        }
        #endregion

        #region 添加投放影厅
        /// <summary>
        /// 添加投放影厅
        /// </summary>
        /// <param name="contract">合同编号</param>
        /// <param name="order">订单编号</param>
        /// <param name="ids">影厅ID，逗号隔开</param>
        /// <returns></returns>
        public JsonResult Order_addPutin(string contract, string order, string ids)
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
        #endregion

        #region 添加订单
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
        public JsonResult Order_add(string contractID, string orderID, string adID, string makeID, string num, string begin, string end, string memo)
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
        #endregion



        #region 根据订单编号获取订单信息
        /// <summary>
        /// 根据订单编号获取订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Order_getOrderInfo(string id)
        {
            DaOrderInfo dal = new DaOrderInfo();
            OrderInfo en = dal.getOrderInfo(id);

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = en;
            return result;
        }
        #endregion

        #region 修改订单
        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="id">自动编号</param>
        /// <param name="orderID">订单编号</param>
        /// <param name="adCost">广告费结算对象</param>
        /// <param name="makeCost">制作费结算对象</param>
        /// <param name="num">厅数</param>
        /// <param name="begin">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Order_edit(string id, string orderID, string adCost, string makeCost, string num, string begin, string end, string memo)
        {
            if (num.Trim() == "")
                num = "0";

            OrderInfo en = new OrderInfo();
            en.id = Convert.ToInt32(id);
            en.orderID = orderID;
            en.costTargetID = Convert.ToInt32(adCost);
            en.makeTargetID = Convert.ToInt32(makeCost);
            en.roomNum = Convert.ToInt32(num);
            en.begintime = Convert.ToDateTime(begin);
            en.endtime = Convert.ToDateTime(end);
            en.memo = memo;

            DaOrderInfo dal = new DaOrderInfo();
            var result = new CustomJsonResult();
            result.Data = dal.update(en);
            return result;
        }
        #endregion



        #region 根据订单编号获取合同和订单的复合信息
        /// <summary>
        /// 根据订单编号获取合同和订单的复合信息
        /// </summary>
        /// <returns></returns>
        public JsonResult Order_getContractOrderInfo(string id)
        {
            DaOrderInfo dal = new DaOrderInfo();
            ContractOrder en = dal.getContractOrder(id);

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = en;
            return result;
        }
        #endregion

        #region 根据订单编号获取该订单投放的影厅列表
        /// <summary>
        /// 根据订单编号获取该订单投放的影厅列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Order_getPutinOfOrderList(string id)
        {
            DaPutinInfo dal = new DaPutinInfo();
            IList<PutinInfo> putins = dal.getList(id);

            var result = new CustomJsonResult();
            result.Data = new { total = putins.Count, rows = putins };
            return result;
        }
        #endregion



        #region 根据订单编号获取合同和订单的复合信息填充到广告费结算界面
        /// <summary>
        /// 根据订单编号获取合同和订单的复合信息填充到广告费结算界面
        /// </summary>
        /// <returns></returns>
        public JsonResult ADCostAccount_getContractOrderInfo(string id)
        {
            return Order_getContractOrderInfo(id);
        }
        #endregion

        #region 根据订单编号获取该订单投放的影厅列表填充到广告费结算界面
        /// <summary>
        /// 根据订单编号获取该订单投放的影厅列表填充到广告费结算界面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult ADCostAccount_getPutinOfOrderList(string id)
        {
            return Order_getPutinOfOrderList(id);
        }
        #endregion
        
        #region 添加广告费结算
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
        public JsonResult ADCostAccount_add(string id, string money, string state, string date, string memo)
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
        #endregion



        #region 根据订单编号获取合同和订单的复合信息填充到制作费结算界面
        /// <summary>
        /// 根据订单编号获取合同和订单的复合信息填充到制作费结算界面
        /// </summary>
        /// <returns></returns>
        public JsonResult MakeCostAccount_getContractOrderInfo(string id)
        {
            return Order_getContractOrderInfo(id);
        }
        #endregion

        #region 根据订单编号获取该订单投放的影厅列表填充到制作费结算界面
        /// <summary>
        /// 根据订单编号获取该订单投放的影厅列表填充到制作费结算界面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult MakeCostAccount_getPutinOfOrderList(string id)
        {
            return Order_getPutinOfOrderList(id);
        }
        #endregion

        #region 添加制作费结算
        /// <summary>
        /// 添加制作费结算
        /// </summary>
        /// <param name="id">订单编号</param>
        /// <param name="money">结算金额</param>
        /// <param name="state">打款状态</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult MakeCostAccount_add(string id, string money, string state, string date)
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
        #endregion

        

        #region 根据合同编号获取合同信息填充到账登记界面
        /// <summary>
        /// 根据合同编号获取合同信息填充到账登记界面
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public JsonResult CPay_getContract(string id)
        {
            return Order_getContract(id);
        }
        #endregion

        #region 添加到账登记
        /// <summary>
        /// 添加到账登记
        /// </summary>
        /// <param name="id">合同编号</param>
        /// <param name="money">到账金额</param>
        /// <param name="date">到账日期</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CPay_add(string id, string money, string date)
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
        #endregion



        #region 根据合同编号获取合同信息填充到开发票界面
        /// <summary>
        /// 根据合同编号获取合同信息填充到开发票界面
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public JsonResult WriteBill_getContract(string id)
        {
            return Order_getContract(id);
        }
        #endregion

        #region 获取开发票列表
        /// <summary>
        /// 获取开发票列表
        /// </summary>
        /// <returns></returns>
        public JsonResult WriteBill_getList()
        {
            DaBill dal = new DaBill();
            IList<Bill> bills = dal.getList();

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = bills.Count, rows = bills };
            return result;
        }
        #endregion        

        #region 添加开票登记
        /// <summary>
        /// 添加开票登记
        /// </summary>
        /// <param name="id">合同编号</param>
        /// <param name="type">开票类型</param>
        /// <param name="money">开票金额</param>
        /// <param name="date">开票日期</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult WriteBill_add(string id, string type, string money, string date)
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
        #endregion

        #region 删除开票登记
        /// <summary>
        /// 删除开票登记
        /// </summary>
        /// <param name="id">自动编号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult WriteBill_remove(string id)
        {
            Bill en = new Bill();
            en.id = Convert.ToInt32(id);

            DaBill dal = new DaBill();
            var result = new CustomJsonResult();
            result.Data = dal.delete(en);
            return result;
        }
        #endregion



        #region 根据订单编号获取合同和订单的复合信息填充到收发票界面
        /// <summary>
        /// 根据订单编号获取合同和订单的复合信息填充到收发票界面
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public JsonResult ReceiveBill_getContractOrderInfo(string id)
        {
            return Order_getContractOrderInfo(id);
        }
        #endregion

        #region 获取收发票列表
        /// <summary>
        /// 获取收发票列表
        /// </summary>
        /// <returns></returns>
        public JsonResult ReceiveBill_getList()
        {
            DaReceiveBill dal = new DaReceiveBill();
            IList<ReceiveBill> bills = dal.getList();

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = bills.Count, rows = bills };
            return result;
        }
        #endregion

        #region 添加收票登记
        /// <summary>
        /// 添加开票登记
        /// </summary>
        /// <param name="orderID">合同编号</param>
        /// <param name="type">开票类型</param>
        /// <param name="money">开票金额</param>
        /// <param name="date">开票日期</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ReceiveBill_add(string orderID, string type, string target, string money, string date)
        {
            ReceiveBill en = new ReceiveBill();
            en.orderID = orderID;
            en.type = Convert.ToInt32(type);
            en.targetID = Convert.ToInt32(target);
            en.money = Convert.ToDouble(money);
            en.date = Convert.ToDateTime(date);

            DaReceiveBill dal = new DaReceiveBill();
            var result = new CustomJsonResult();
            result.Data = dal.add(en);
            return result;
        }
        #endregion
    }
}
