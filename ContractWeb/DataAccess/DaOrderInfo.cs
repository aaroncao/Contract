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
        #region 获取订单列表
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <returns></returns>
        public IList<OrderInfo> getList()
        {
            string strSql = "select a.id, a.contractID, (select z.name from ContractInfo z where z.contractID=a.contractID) as contractName, a.orderID, "
                + "(select z.target from ADCostTarget z where z.id=a.costTargetID) as costTargetName, a.costTargetID, "
                + "(select z.customerID from ContractInfo z where z.contractID=a.contractID) as customerID, "
                + "(select x.name from CustomerInfo x, ContractInfo y where x.id=y.customerID and y.contractID=a.contractID) as customerName, "
                + "a.roomNum, a.begintime, a.endtime, a.memo, "
                + "(select z.target from MakeCostTarget z where z.id=a.makeTargetID) as makeTargetName, a.makeTargetID, "
                + "playReport, reportTime, mdate from OrderInfo a";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<OrderInfo> list = DynamicBuilder<OrderInfo>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 搜索订单列表
        /// <summary>
        /// 搜索订单列表
        /// </summary>
        /// <returns></returns>
        public IList<OrderInfo> getList(string orderID, string contractID, string begin, string end, string person, string adTarget, string makeTarget)
        {
            string strSql = "select a.id, a.contractID, b.name as contractName, a.orderID, "
                + "(select z.target from ADCostTarget z where z.id=a.costTargetID) as costTargetName, a.costTargetID, "
                + "b.customerID, (select z.name from CustomerInfo z where z.id=b.customerID) as customerName, "
                + "a.roomNum, a.begintime, a.endtime, a.memo, "
                + "(select z.target from MakeCostTarget z where z.id=a.makeTargetID) as makeTargetName, a.makeTargetID, "
                + "a.playReport, a.reportTime, a.mdate from OrderInfo a, ContractInfo b where a.contractID=b.contractID ";

            string where = "";

            if (orderID.Trim() != "")
                where += "a.orderID like '%" + orderID + "%'";

            if (contractID.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "a.contractID like '%" + contractID + "%'";
            }

            if (begin.Trim() != "" && end.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "(a.endtime>='" + begin + "' and a.begintime<='" + end + "')";
            }

            if (person.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "b.personID='" + person + "'";
            }

            if (adTarget.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "a.costTargetID='" + adTarget + "'";
            }

            if (makeTarget.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "a.makeTargetID='" + makeTarget + "'";
            }

            if (where != "")
                strSql += "and " + where;

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<OrderInfo> list = DynamicBuilder<OrderInfo>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 搜索订单列表(导出报表)
        /// <summary>
        /// 搜索订单列表
        /// </summary>
        /// <returns></returns>
        public DataTable getDataTable(string orderID, string contractID, string begin, string end, string person, string adTarget, string makeTarget)
        {
            string strSql = "select a.contractID as [合同编号], a.orderID as [下单编号], b.name as [合同名称], "
                + "(select z.target from ADCostTarget z where z.id=a.costTargetID) as [广告费结算对象], "
                + "(select z.target from MakeCostTarget z where z.id=a.makeTargetID) as [制作费结算对象], "
                + "a.begintime as [下单时段(起)], a.endtime as [下单时段(止)], a.roomNum as [下单厅数], a.memo as [备注] from OrderInfo a, ContractInfo b "
                + "where a.contractID=b.contractID ";

            string where = "";

            if (orderID.Trim() != "")
                where += "a.orderID like '%" + orderID + "%'";

            if (contractID.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "a.contractID like '%" + contractID + "%'";
            }

            if (begin.Trim() != "" && end.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "(a.endtime>='" + begin + "' and a.begintime<='" + end + "')";
            }

            if (person.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "b.personID='" + person + "'";
            }

            if (adTarget.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "a.costTargetID='" + adTarget + "'";
            }

            if (makeTarget.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "a.makeTargetID='" + makeTarget + "'";
            }

            if (where != "")
                strSql += "and " + where;

            return SqlHelper.ExecuteDataset(BaseHelper.DBConnStr, CommandType.Text, strSql).Tables[0];
        }
        #endregion

        #region 根据编号获取信息
        /// <summary>
        /// 根据编号获取信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public ContractOrder getContractOrder(string id)
        {
            string strSql = "select a.id, a.contractID, b.name as contractName, a.orderID, "
                + "(select z.target from ADCostTarget z where z.id=a.costTargetID) as costTargetName, a.costTargetID, "
                + "b.customerID, (select z.name from CustomerInfo z where z.id=b.customerID) as customerName, "
                + "a.roomNum, a.begintime, a.endtime, a.memo, "
                + "(select z.target from MakeCostTarget z where z.id=a.makeTargetID) as makeTargetName, a.makeTargetID, "
                + "a.playReport, a.reportTime, a.mdate from OrderInfo a, ContractInfo b where a.contractID=b.contractID and a.orderID=@id";

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
        #endregion

        #region 根据编号获取信息
        /// <summary>
        /// 根据编号获取信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public OrderInfo getOrderInfo(string id)
        {
            string strSql = "select a.id, a.contractID, b.name as contractName, a.orderID, "
                + "(select z.target from ADCostTarget z where z.id=a.costTargetID) as costTargetName, a.costTargetID, "
                + "b.customerID, (select z.name from CustomerInfo z where x.id=b.customerID) as customerName, "
                + "a.roomNum, a.begintime, a.endtime, a.memo, "
                + "(select z.target from MakeCostTarget z where z.id=a.makeTargetID) as makeTargetName, a.makeTargetID, "
                + "playReport, reportTime, mdate from OrderInfo a, ContractInfo b where a.contractID=b.contractID and a.id=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<OrderInfo> list = DynamicBuilder<OrderInfo>.ConvertToList(dr);

            if (list != null && list.Count > 0)
                return list[0];
            else
                return null;
        }
        #endregion

        #region 生成当天最大的订单编号
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
        #endregion

        #region 添加订单
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
        #endregion

        #region 修改订单
        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="en">订单信息</param>
        /// <returns></returns>
        public int update(OrderInfo en)
        {
            string strSql = "update OrderInfo set orderID=@orderID, costTargetID=@costTargetID, makeTargetID=@makeTargetID, roomNum=@roomNum, begintime=@begintime, endtime=@endtime, memo=@memo where id=@id ";
 
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@orderID", en.orderID),
                new SqlParameter("@costTargetID", en.costTargetID),
                new SqlParameter("@makeTargetID", en.makeTargetID),
                new SqlParameter("@roomNum", en.roomNum),
                new SqlParameter("@begintime", en.begintime),
                new SqlParameter("@endtime", en.endtime),
                new SqlParameter("@memo", en.memo),
                new SqlParameter("@id", en.id)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
        #endregion
    }
}