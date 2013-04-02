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
    public class DaReceiveBill
    {
        #region 获取发票列表
        /// <summary>
        /// 获取发票列表
        /// </summary>
        /// <returns></returns>
        public IList<ReceiveBill> getList()
        {
            string strSql = "select a.id, a.orderID, b.contractID, "
                + "(select z.name from ContractInfo z where z.contractID=b.contractID) as contractName, "
                + "(select z.money from ContractInfo z where z.contractID=b.contractID) as contractMoney, "
                + "a.type, (select z.name from BillType z where z.id=a.type) as typeName, a.costTargetID as targetID, "
                + "(case when a.type=1 then (select z.target from ADCostTarget z where z.id=a.costTargetID) else (select z.target from MakeCostTarget z where z.id=a.costTargetID) end) as targetName, "
                + "a.money, a.date "
                + "from ReceiveBill a, OrderInfo b where a.orderID=b.orderID";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<ReceiveBill> list = DynamicBuilder<ReceiveBill>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 获取发票列表
        /// <summary>
        /// 获取发票列表
        /// </summary>
        /// <returns></returns>
        public IList<ReceiveBill> getList(string id)
        {
            string strSql = "select a.id, a.orderID, b.contractID, "
                + "(select z.name from ContractInfo z where z.contractID=b.contractID) as contractName, "
                + "(select z.money from ContractInfo z where z.contractID=b.contractID) as contractMoney, "
                + "a.type, (select z.name from BillType z where z.id=a.type) as typeName, a.costTargetID as targetID, "
                + "(case when a.type=1 then (select z.target from ADCostTarget z where z.id=a.costTargetID) else (select z.target from MakeCostTarget z where z.id=a.costTargetID) end) as targetName, "
                + "a.money, a.date "
                + "from ReceiveBill a, orderInfo b where a.orderID=b.orderID and a.orderID=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<ReceiveBill> list = DynamicBuilder<ReceiveBill>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 添加发票
        /// <summary>
        /// 添加发票
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int add(ReceiveBill en)
        {
            string strSql = "insert into ReceiveBill (orderID, type, costTargetID, money, date) values (@orderID, @type, @target, @money, @date)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@orderID", en.orderID),
                new SqlParameter("@type", en.type),
                new SqlParameter("@target", en.targetID),
                new SqlParameter("@money", en.money),
                new SqlParameter("@date", en.date)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
        #endregion
    }
}