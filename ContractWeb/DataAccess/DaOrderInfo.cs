using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using ContractWeb.Models;
using ContractWeb.Common;
using ND.Lib.Data.SqlHelper;

namespace ContractWeb.DataAccess
{
    public class DaOrderInfo
    {
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <returns></returns>
        public IList<OrderInfo> getList()
        {
            string strSql = "select a.id, a.contractID, a.orderID, (select z.name from ContractInfo z where z.contractID=a.contractID) as contractName, "
                + "(select z.customerID from ContractInfo z where z.contractID=a.contractID) as customerID, "
                + "(select x.name from CustomerInfo x, ContractInfo y where x.id=y.customerID and y.contractID=a.contractID) as customerName, "
                + "a.costTargetID, (select z.target from ADCostTarget z where z.id=a.costTargetID) as costTargetName, "
                + "a.makeTargetID, (select z.target from MakeCostTarget z where z.id=a.makeTargetID) as makeTargetName, "
                + "a.begintime, a.endtime, a.roomNum, a.memo from OrderInfo a";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<OrderInfo> list = DynamicBuilder<OrderInfo>.ConvertToList(dr);
            return list;
        }

        /// <summary>
        /// 根据编号获取信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public ContractOrder getContractOrder(string id)
        {
            string strSql = "select id, contractID, orderID, "
                + "costTargetID, (select z.target from ADCostTarget z where z.id=costTargetID) as costTargetName, "
                + "makeTargetID, (select z.target from MakeCostTarget z where z.id=makeTargetID) as makeTargetName "
                + "from OrderInfo where orderID=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<OrderInfo> list = DynamicBuilder<OrderInfo>.ConvertToList(dr);

            if (list != null && list.Count > 0)
            {
                ContractOrder en = new ContractOrder();
                en.order = list[0];

                DaContractInfo dal = new DaContractInfo();
                en.contract = dal.getEntity(list[0].contractID);

                return en;
            }
            else
                return null;
        }

        /// <summary>
        /// 生成当天最大的订单编号
        /// </summary>
        public string buildOrderIDofDay()
        {
            string selectID = "XD" + DateTime.Today.ToString("yyyyMMdd");
            string strSql = "select top 1 orderID from OrderInfo where orderID like '" + selectID + "%' order by id desc";

            DataTable dt = SqlHelper.ExecuteDataset(BaseHelper.DBConnStr, CommandType.Text, strSql).Tables[0];

            int auto = 1;
            if (dt.Rows.Count > 0)
            {
                string maxID = dt.Rows[0]["orderID"].ToString();
                auto = Convert.ToInt32(maxID.Substring(maxID.Length - 2, 2));
                auto++;
            }

            return selectID + (auto > 10 ? auto.ToString() : "0" + auto);
        }

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="en">订单信息</param>
        /// <returns></returns>
        public int add(OrderInfo en)
        {
            string strSql = "insert into OrderInfo (contractID, orderID, costTargetID, roomNum, begintime, endtime, memo, makeTargetID, playReport, reportTime, mdate) "
                + "values (@contractID, @orderID, @costTargetID, @roomNum, @begintime, @endtime, @memo, @makeTargetID, @playReport, @reportTime, @mdate)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@contractID", en.contractID),
                new SqlParameter("@orderID", en.orderID),
                new SqlParameter("@costTargetID", en.costTargetID),
                new SqlParameter("@roomNum", en.roomNum),
                new SqlParameter("@begintime", en.begintime),
                new SqlParameter("@endtime", en.endtime),
                new SqlParameter("@memo", en.memo),
                new SqlParameter("@makeTargetID", en.makeTargetID),
                new SqlParameter("@playReport", System.DBNull.Value),
                new SqlParameter("@reportTime", System.DBNull.Value),
                new SqlParameter("@mdate", DateTime.Now)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
    }
}