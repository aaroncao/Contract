using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

using ContractWeb.Common;
using ContractWeb.Models;
using ContractWeb.DataAccess;

namespace ContractWeb.Controllers
{
    public class SelectController : Controller
    {
        #region 合同信息查询界面
        /// <summary>
        /// 合同信息查询界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Contract()
        {
            ViewBag.menu = 26;
            return View();
        }
        #endregion

        #region 合同修改界面
        /// <summary>
        /// 合同修改界面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditContract()
        {
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
            return View();
        }
        #endregion

        #region 客户到账情况界面
        /// <summary>
        /// 客户到账情况界面
        /// </summary>
        /// <returns></returns>
        public ActionResult CPayList()
        {
            return View();
        }
        #endregion

        #region 开发票登记界面
        /// <summary>
        /// 开发票登记界面
        /// </summary>
        /// <returns></returns>
        public ActionResult WriteBill()
        {
            return View();
        }
        #endregion

        #region 开具发票情况
        /// <summary>
        /// 开具发票情况
        /// </summary>
        /// <returns></returns>
        public ActionResult BillList()
        {
            return View();
        }
        #endregion

        #region 修改开票状态
        /// <summary>
        /// 修改开票状态
        /// </summary>
        /// <returns></returns>
        public ActionResult EditBillState()
        {
            return View();
        }
        #endregion

        #region 修改合同状态
        /// <summary>
        /// 修改合同状态
        /// </summary>
        /// <returns></returns>
        public ActionResult EditContractState()
        {
            return View();
        }
        #endregion

        #region 下单信息查询界面
        /// <summary>
        /// 下单信息查询界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Order()
        {
            ViewBag.menu = 27;
            return View();
        }
        #endregion

        #region 广告费结算查询界面
        /// <summary>
        /// 广告费结算查询界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ADCost()
        {
            ViewBag.menu = 29;
            return View();
        }
        #endregion

        #region 制作费结算查询界面
        /// <summary>
        /// 制作费结算查询界面
        /// </summary>
        /// <returns></returns>
        public ActionResult MakeCost()
        {
            ViewBag.menu = 30;
            return View();
        }
        #endregion

        #region 广告费结算情况
        /// <summary>
        /// 广告费结算情况
        /// </summary>
        /// <returns></returns>
        public ActionResult ADCostList()
        {
            return View();
        }
        #endregion

        #region 制作费结算情况
        /// <summary>
        /// 制作费结算情况
        /// </summary>
        /// <returns></returns>
        public ActionResult MakeCostList()
        {
            return View();
        }
        #endregion

        #region 发票收到情况
        /// <summary>
        /// 发票收到情况
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceiveBillList()
        {
            return View();
        }
        #endregion

        public void outputContract()
        {
            DaContractInfo dal = new DaContractInfo();
            //Stream  NPOIHelper.ExportDataTableToExcel(dal.getDataTable());
        }





        #region 获取合同状态列表
        /// <summary>
        /// 获取合同状态列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getDrpContractStateList()
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
        public JsonResult getDrpBillStateList()
        {
            DaBillState dal = new DaBillState();
            IList<BillState> types = dal.getList();

            var result = new CustomJsonResult();
            result.Data = types;
            return result;
        }
        #endregion

        #region 获取合同列表
        /// <summary>
        /// 获取合同列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getContractList()
        {
            DaContractInfo dal = new DaContractInfo();
            IList<ContractInfo> contracts = dal.getList();

            double money = 0.0;
            foreach(ContractInfo en in contracts)
                money += en.money;

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = contracts.Count, rows = contracts, money = money };
            return result;
        }
        #endregion

        #region 查询合同
        /// <summary>
        /// 查询合同
        /// </summary>
        /// <returns></returns>
        public JsonResult searchContract(string id, string channel, string type, string state, string person, string billState, string date)
        {
            DaContractInfo dal = new DaContractInfo();
            IList<ContractInfo> contracts = dal.getList(id, channel, type, state, person, billState, date);

            double money = 0.0;
            foreach (ContractInfo en in contracts)
                money += en.money;

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = contracts.Count, rows = contracts, money = money };
            return result;
        }
        #endregion

        #region 根据编号获取合同信息
        /// <summary>
        /// 根据编号获取合同信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult getContractEn(string id)
        {
            DaContractInfo dal = new DaContractInfo();
            ContractInfo contract = dal.getEntity(id);

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = contract;
            return result;
        }
        #endregion

        #region 修改合同
        /// <summary>
        /// 修改合同
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
        public JsonResult editContract(string id, string name, string version, string price, string num,
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
            result.Data = dal.update(en);
            return result;
        }
        #endregion

        #region 获取客户到账情况列表
        /// <summary>
        /// 获取客户到账情况列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult getPayList(string id)
        {
            DaPayList dal = new DaPayList();
            IList<PayList> list = dal.getList(id);

            var result = new CustomJsonResult();
            result.Data = new { total = list.Count, rows = list };
            return result;
        }
        #endregion 

        #region 获取开具发票情况列表
        /// <summary>
        /// 获取开具发票情况列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult getBillList(string id)
        {
            DaBill dal = new DaBill();
            IList<Bill> list = dal.getList(id);

            var result = new CustomJsonResult();
            result.Data = new { total = list.Count, rows = list };
            return result;
        }
        #endregion 

        #region 修改开票状态
        /// <summary>
        /// 修改开票状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public JsonResult changeBillState(string id, string state)
        {
            DaContractInfo dal = new DaContractInfo();
            ContractInfo en = new ContractInfo();
            en.contractID = id;
            en.billState = Convert.ToInt32(state);

            var result = new CustomJsonResult();
            result.Data = dal.editBillState(en);
            return result;
        }
        #endregion

        #region 修改合同状态
        /// <summary>
        /// 修改合同状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public JsonResult changeContractState(string id, string state)
        {
            DaContractInfo dal = new DaContractInfo();
            ContractInfo en = new ContractInfo();
            en.contractID = id;
            en.state = Convert.ToInt32(state);

            var result = new CustomJsonResult();
            result.Data = dal.editContractState(en);
            return result;
        }
        #endregion

        #region 获取打款状态列表
        /// <summary>
        /// 获取渠道列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getDrpAccountStateList()
        {
            DaAccountState dal = new DaAccountState();
            IList<AccountState> states = dal.getList();

            var result = new CustomJsonResult();
            result.Data = states;
            return result;
        }
        #endregion

        #region 获取广告结算列表
        /// <summary>
        /// 获取广告结算列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getADCostList()
        {
            DaADCostAccount dal = new DaADCostAccount();
            IList<ADCost> list = dal.getList();

            double money = 0.0;
            foreach (ADCost en in list)
                money += en.money;

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = list.Count, rows = list, money = money };
            return result;
        }
        #endregion

        #region 查询广告结算
        /// <summary>
        /// 查询广告结算
        /// </summary>
        /// <returns></returns>
        public JsonResult searchADCost(string id, string target, string channel, string state, string begin, string end)
        {
            DaADCostAccount dal = new DaADCostAccount();
            IList<ADCost> list = dal.getList(id, target, channel, state, begin, end);

            double money = 0.0;
            foreach (ADCost en in list)
                money += en.money;

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = list.Count, rows = list, money = money };
            return result;
        }
        #endregion

        #region 获取制作结算列表
        /// <summary>
        /// 获取制作结算列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getMakeCostList()
        {
            DaMakeCostAccount dal = new DaMakeCostAccount();
            IList<MakeCost> list = dal.getList();

            double money = 0.0;
            foreach (MakeCost en in list)
                money += en.money;

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = list.Count, rows = list, money = money };
            return result;
        }
        #endregion

        #region 查询制作结算
        /// <summary>
        /// 查询制作结算
        /// </summary>
        /// <returns></returns>
        public JsonResult searchMakeCost(string id, string contractID, string channel, string state, string begin, string end)
        {
            DaMakeCostAccount dal = new DaMakeCostAccount();
            IList<MakeCost> list = dal.getList(id, contractID, channel, state, begin, end);

            double money = 0.0;
            foreach (MakeCost en in list)
                money += en.money;

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = list.Count, rows = list, money = money };
            return result;
        }
        #endregion

        #region 获取下单列表
        /// <summary>
        /// 获取下单列表
        /// </summary>
        /// <returns></returns>
        public JsonResult getOrderList()
        {
            DaOrderInfo dal = new DaOrderInfo();
            IList<OrderInfo> contracts = dal.getList();

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = contracts.Count, rows = contracts };
            return result;
        }
        #endregion

        #region 查询下单
        /// <summary>
        /// 查询下单
        /// </summary>
        /// <returns></returns>
        public JsonResult searchOrder(string order, string contract, string begin, string end, string person, string adTarget, string makeTarget)
        {
            DaOrderInfo dal = new DaOrderInfo();
            IList<OrderInfo> contracts = dal.getList(order, contract, begin, end, person, adTarget, makeTarget);

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = contracts.Count, rows = contracts };
            return result;
        }
        #endregion

        #region 获取广告结算列表
        /// <summary>
        /// 获取广告结算列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult getADCostList(string id)
        {
            DaADCostAccount dal = new DaADCostAccount();
            IList<ADCost> list = dal.getList(id);

            var result = new CustomJsonResult();
            result.Data = new { total = list.Count, rows = list };
            return result;
        }
        #endregion 

        #region 获取制作结算列表
        /// <summary>
        /// 获取制作结算列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult getMakeCostList(string id)
        {
            DaMakeCostAccount dal = new DaMakeCostAccount();
            IList<MakeCost> list = dal.getList(id);

            var result = new CustomJsonResult();
            result.Data = new { total = list.Count, rows = list };
            return result;
        }
        #endregion 

        #region 获取制作结算列表
        /// <summary>
        /// 获取制作结算列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult getReceiveBillList(string id)
        {
            DaReceiveBill dal = new DaReceiveBill();
            IList<ReceiveBill> list = dal.getList(id);

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = list.Count, rows = list };
            return result;
        }
        #endregion 
    }
}
