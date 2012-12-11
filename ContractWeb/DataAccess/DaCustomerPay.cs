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
    public class DaCustomerPay
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public IList<CustomerPay> getList()
        {
            // do nothing
            return null;
        }

        /// <summary>
        /// 登记到账
        /// </summary>
        /// <param name="en">实体</param>
        /// <returns></returns>
        public int add(CustomerPay en)
        {
            string strSql = "insert into CustomerPay (contractID, orderID, money, date, backHandler) values (@contractID, @orderID, @money, @date, @back)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@contractID", en.contractID),
                new SqlParameter("@orderID", en.orderID),
                new SqlParameter("@money", en.money),
                new SqlParameter("@date", en.date),
                new SqlParameter("@back", en.backHandler)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
    }
}