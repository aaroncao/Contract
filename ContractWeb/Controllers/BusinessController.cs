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
        /// 广告费用结算
        /// </summary>
        /// <returns></returns>
        public ActionResult ADCostAccount()
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
        /// 根据编号获取订单
        /// </summary>
        /// <returns></returns>
        public JsonResult getOrder()
        {
            DaBill dal = new DaBill();
            IList<Bill> bills = dal.getList();

            var result = new CustomJsonResult();
            result.dateFormat = "yyyy-MM-dd";
            result.Data = new { total = bills.Count, rows = bills };
            return result;
        }
    }
}
