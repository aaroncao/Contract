using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Mvc;

using ContractWeb.Common;
using ContractWeb.Models;
using ContractWeb.DataAccess;

namespace ContractWeb.Controllers
{
    //查询与统计
    [AuthorizeFilterAttribute]
    public class SelectController : Controller
    {
        /* ============ 界面 ============ */

        #region 合同信息查询界面
        /// <summary>
        /// 合同信息查询界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ContractList()
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
        public ActionResult OrderList()
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

        #region 影院投放统计
        /// <summary>
        /// 影院投放统计
        /// </summary>
        /// <returns></returns>
        public ActionResult Putin()
        {
            ViewBag.menu = 32;
            return View();
        }
        #endregion

        #region 影院广告情况统计
        /// <summary>
        /// 影院广告情况统计
        /// </summary>
        /// <returns></returns>
        public ActionResult AdvList()
        {
            ViewBag.menu = 33;
            return View();
        }
        #endregion

        /* ============ 操作 ============ */

        #region 获取合同列表
        /// <summary>
        /// 获取合同列表
        /// </summary>
        /// <returns></returns>
        public JsonResult ContractList_getList()
        {
            DaContractInfo dal = new DaContractInfo();
            IList<ContractInfo> contracts = dal.getList(BaseHelper.getCookie().id.ToString());

            double money = 0.0;
            foreach(ContractInfo en in contracts)
                money += en.money;

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = contracts.Count, rows = contracts, money = money };
            return result;
        }
        #endregion

        #region 搜索合同列表
        /// <summary>
        /// 搜索合同列表
        /// </summary>
        /// <returns></returns>
        public JsonResult ContractList_searcht(string id, string channel, string type, string state, string person, string billState, string date)
        {
            DaContractInfo dal = new DaContractInfo();
            IList<ContractInfo> contracts = dal.getList(id, channel, type, state, person, billState, date, BaseHelper.getCookie().id.ToString());

            double money = 0.0;
            foreach (ContractInfo en in contracts)
                money += en.money;

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = contracts.Count, rows = contracts, money = money };
            return result;
        }
        #endregion

        #region 导出合同列表
        /// <summary>
        /// 导出合同列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="channel"></param>
        /// <param name="type"></param>
        /// <param name="state"></param>
        /// <param name="person"></param>
        /// <param name="billState"></param>
        /// <param name="date"></param>
        public void ContractList_output(string id, string channel, string type, string state, string person, string billState, string date)
        {
            DaContractInfo dal = new DaContractInfo();
            DataTable dt = dal.getDataTable(id, channel, type, state, person, billState, date, BaseHelper.getCookie().id.ToString());

            if (dt.Rows.Count > 0)
            {
                string filename = "myContractList.xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();

                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                //Response.ContentType = application/vnd.ms-excel;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                Response.Write(tw.ToString());
            }
            else
            {
                Response.Write("无数据可导出！");
            }

            Response.End();
        }
        #endregion



        #region 根据编号获取合同信息
        /// <summary>
        /// 根据编号获取合同信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult ContractList_getContract(string id)
        {
            DaContractInfo dal = new DaContractInfo();
            ContractInfo contract = dal.getEntity(id);

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = contract;
            return result;
        }
        #endregion

        #region 修改合同信息
        /// <summary>
        /// 修改合同信息
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
        public JsonResult ContractList_editContract(string id, string name, string version, string price, string num,
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
        public JsonResult ContractList_getPayList(string id)
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
        public JsonResult ContractList_getBillList(string id)
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
        public JsonResult ContractList_editBillState(string id, string state)
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
        public JsonResult ContractList_editContractState(string id, string state)
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



        #region 获取下单列表
        /// <summary>
        /// 获取下单列表
        /// </summary>
        /// <returns></returns>
        public JsonResult OrderList_getList()
        {
            DaOrderInfo dal = new DaOrderInfo();
            IList<OrderInfo> contracts = dal.getList(BaseHelper.getCookie().id.ToString());

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = contracts.Count, rows = contracts };
            return result;
        }
        #endregion

        #region 搜索下单列表
        /// <summary>
        /// 搜索下单列表
        /// </summary>
        /// <returns></returns>
        public JsonResult OrderList_search(string order, string contract, string begin, string end, string person, string adTarget, string makeTarget)
        {
            DaOrderInfo dal = new DaOrderInfo();
            IList<OrderInfo> contracts = dal.getList(order, contract, begin, end, person, adTarget, makeTarget, BaseHelper.getCookie().id.ToString());

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = contracts.Count, rows = contracts };
            return result;
        }
        #endregion

        #region 导出下单报表
        /// <summary>
        /// 导出下单报表
        /// </summary>
        /// <param name="order"></param>
        /// <param name="contract"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="person"></param>
        /// <param name="adTarget"></param>
        /// <param name="makeTarget"></param>
        public void OrderList_output(string order, string contract, string begin, string end, string person, string adTarget, string makeTarget)
        {
            DaOrderInfo dal = new DaOrderInfo();
            DataTable dt = dal.getDataTable(order, contract, begin, end, person, adTarget, makeTarget);

            if (dt.Rows.Count > 0)
            {
                string filename = "myOrderList.xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();

                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                //Response.ContentType = application/vnd.ms-excel;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                Response.Write(tw.ToString());
            }
            else
            {
                Response.Write("无数据可导出！");
            }

            Response.End();
        }
        #endregion



        #region 获取发票收到情况列表
        /// <summary>
        /// 获取发票收到情况列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult ReceiveBillList_getList(string id)
        {
            DaReceiveBill dal = new DaReceiveBill();
            IList<ReceiveBill> list = dal.getList(id);

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = list.Count, rows = list };
            return result;
        }
        #endregion 

        #region 获取广告结算列表
        /// <summary>
        /// 获取广告结算列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Order_getADCostList(string id)
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
        public JsonResult Order_getMakeCostList(string id)
        {
            DaMakeCostAccount dal = new DaMakeCostAccount();
            IList<MakeCost> list = dal.getList(id);

            var result = new CustomJsonResult();
            result.Data = new { total = list.Count, rows = list };
            return result;
        }
        #endregion 



        #region 获取广告结算列表
        /// <summary>
        /// 获取广告结算列表
        /// </summary>
        /// <returns></returns>
        public JsonResult ADCost_getList()
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

        #region 搜索广告结算列表
        /// <summary>
        /// 搜索广告结算列表
        /// </summary>
        /// <returns></returns>
        public JsonResult ADCost_search(string id, string target, string channel, string state, string begin, string end)
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
        public JsonResult MakeCost_getList()
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

        #region 搜索制作结算列表
        /// <summary>
        /// 搜索制作结算列表
        /// </summary>
        /// <returns></returns>
        public JsonResult MakeCost_search(string id, string contractID, string channel, string state, string begin, string end)
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


        
        #region 获取影院投放统计列表
        /// <summary>
        /// 获取影院投放统计列表
        /// </summary>
        /// <returns></returns>
        public JsonResult Putin_getList(string page, string rows)
        {
            DaPutinInfo dal = new DaPutinInfo();
            IList<PutinListItem> list = dal.getList(page, rows);
            double[] data = dal.getListOfMoney();

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = Convert.ToInt32(data[0]), rows = list, money = data[1] };
            return result;
        }
        #endregion

        #region 搜索影院投放统计列表
        /// <summary>
        /// 搜索影院投放统计列表
        /// </summary>
        /// <param name="cinema"></param>
        /// <param name="room"></param>
        /// <param name="version"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public JsonResult Putin_search(string cinema, string room, string version, string begin, string end, string page, string rows)
        {
            DaPutinInfo dal = new DaPutinInfo();
            IList<PutinListItem> list = dal.getList(cinema, room, version, begin, end, page, rows);
            double[] data = dal.getListOfMoney(cinema, room, version, begin, end);

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = Convert.ToInt32(data[0]), rows = list, money = data[1] };
            return result;
        }
        #endregion

        #region 获取影院广告情况统计列表
        /// <summary>
        /// 获取影院广告情况统计列表
        /// </summary>
        /// <param name="cinema"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public JsonResult AdvList_search(string cinema, string begin, string end)
        {
            DaAdvListItem dal = new DaAdvListItem();
            IList<AdvListItem> list = dal.getList(cinema, begin, end);

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = list.Count, rows = list };
            return result;
        }
        #endregion

        #region 导出影院广告情况统计报表
        /// <summary>
        /// 导出影院广告情况统计报表
        /// </summary>
        /// <param name="cinema"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        public void AdvList_output(string cinema, string begin, string end)
        {
            DaAdvListItem dal = new DaAdvListItem();
            DataTable dt = dal.getDataTable(cinema, begin, end);

            if (dt.Rows.Count > 0)
            {
                string filename = "myAdvList.xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();

                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                //Response.ContentType = application/vnd.ms-excel;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                Response.Write(tw.ToString());
            }
            else
            {
                Response.Write("无数据可导出！");
            }

            Response.End();
        }
        #endregion
    }
}
