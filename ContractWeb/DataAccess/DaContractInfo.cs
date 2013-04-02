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
    public class DaContractInfo
    {
        #region 获取合同列表
        /// <summary>
        /// 获取合同列表
        /// </summary>
        /// <returns></returns>
        public IList<ContractInfo> getList()
        {
            string strSql = "select id, contractID, name, customerID, (select z.name from CustomerInfo z where z.id=customerID) as customerName, "
                + "version, price, roomNum, makeCost, backMoney, money, "
                + "type, (select z.name from ContractType z where z.id=type) as typeName, "
                + "channelID, (select z.name from Channel z where z.id=channelID) as channelName, "
                + "begintime, endtime, ZQ, personID, (select z.name from UserInfo z where z.id=personID) as personName, "
                + "memo, mDate, billState, (select z.name from BillState z where z.id=billState) as billStateName, "
                + "state, (select z.name from ContractState z where z.id=state) as stateName, editTime from ContractInfo";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<ContractInfo> list = DynamicBuilder<ContractInfo>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 获取合同列表(输出报表)
        /// <summary>
        /// 获取合同列表(输出报表)
        /// </summary>
        /// <returns></returns>
        public DataTable getDataTable()
        {
            string strSql = "select id, contractID, name, customerID, (select z.name from CustomerInfo z where z.id=customerID) as customerName, "
                + "version, price, roomNum, makeCost, backMoney, money, "
                + "type, (select z.name from ContractType z where z.id=type) as typeName, "
                + "channelID, (select z.name from Channel z where z.id=channelID) as channelName, "
                + "begintime, endtime, ZQ, personID, (select z.name from UserInfo z where z.id=personID) as personName, "
                + "memo, mDate, billState, (select z.name from BillState z where z.id=billState) as billStateName, "
                + "state, (select z.name from ContractState z where z.id=state) as stateName, editTime from ContractInfo";

            return SqlHelper.ExecuteDataset(BaseHelper.DBConnStr, CommandType.Text, strSql).Tables[0];
        }
        #endregion

        #region 搜索合同列表
        /// <summary>
        /// 搜索合同列表
        /// </summary>
        /// <returns></returns>
        public IList<ContractInfo> getList(string id, string channel, string type, string state, string person, string billState, string date)
        {
            string strSql = "select id, contractID, name, customerID, (select z.name from CustomerInfo z where z.id=customerID) as customerName, "
                + "version, price, roomNum, makeCost, backMoney, money, "
                + "type, (select z.name from ContractType z where z.id=type) as typeName, "
                + "channelID, (select z.name from Channel z where z.id=channelID) as channelName, "
                + "begintime, endtime, ZQ, personID, (select z.name from UserInfo z where z.id=personID) as personName, "
                + "memo, mDate, billState, (select z.name from BillState z where z.id=billState) as billStateName, "
                + "state, (select z.name from ContractState z where z.id=state) as stateName, editTime from ContractInfo ";

            string where = "";

            if (id.Trim() != "")
                where += "contractID='" + id + "'";

            if (channel.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "channelID=" + channel;
            }

            if (type.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "type=" + type;
            }

            if (state.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "state=" + state;
            }

            if (person.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "personID=" + person;
            }

            if (billState.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "billState=" + billState;
            }

            if (billState.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "billState=" + billState;
            }

            if (date.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "mDate='" + date + "'";
            }

            if (where != "")
                strSql += "where " + where;

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<ContractInfo> list = DynamicBuilder<ContractInfo>.ConvertToList(dr);
            return list;
        }
        #endregion

        #region 搜索合同列表（输出报表）
        /// <summary>
        /// 搜索合同列表（输出报表）
        /// </summary>
        /// <returns></returns>
        public DataTable getDataTable(string id, string channel, string type, string state, string person, string billState, string date)
        {
            string strSql = "select contractID as [合同编号], name as [合同名称], (select z.name from CustomerInfo z where z.id=customerID) as [客户名称], "
                + "version as [版本], price as [单厅价格], roomNum as [每场厅数], makeCost as [制作费], backMoney as [优惠], money as [签署金额], "
                + "(select z.name from ContractType z where z.id=type) as [合同类型], "
                + "(select z.name from Channel z where z.id=channelID) as [渠道归类], "
                + "begintime as [合同周期(起)], endtime as [合同周期(止)], ZQ as [周期], (select z.name from UserInfo z where z.id=personID) as [经办人], "
                + "memo as [备注], (select z.name from BillState z where z.id=billState) as [发票状态], "
                + "(select z.name from ContractState z where z.id=state) as [合同状态], editTime as [状态修改时间] from ContractInfo ";

            string where = "";

            if (id.Trim() != "")
                where += "contractID='" + id + "'";

            if (channel.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "channelID=" + channel;
            }

            if (type.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "type=" + type;
            }

            if (state.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "state=" + state;
            }

            if (person.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "personID=" + person;
            }

            if (billState.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "billState=" + billState;
            }

            if (billState.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "billState=" + billState;
            }

            if (date.Trim() != "")
            {
                if (where != "")
                    where += " and ";
                where += "mDate='" + date + "'";
            }

            if (where != "")
                strSql += "where " + where;

            return SqlHelper.ExecuteDataset(BaseHelper.DBConnStr, CommandType.Text, strSql).Tables[0];
        }
        #endregion

        #region 根据合同编号获取合同信息
        /// <summary>
        /// 根据合同编号获取合同信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public ContractInfo getEntity(string id)
        {
            string strSql = "select id, contractID, name, customerID, (select z.name from CustomerInfo z where z.id=customerID) as customerName, "
                + "version, price, roomNum, makeCost, backMoney, money, "
                + "type, (select z.name from ContractType z where z.id=type) as typeName, "
                + "channelID, (select z.name from Channel z where z.id=channelID) as channelName, "
                + "begintime, endtime, ZQ, personID, (select z.name from UserInfo z where z.id=personID) as personName, "
                + "memo, mDate, billState, (select z.name from BillState z where z.id=billState) as billStateName, "
                + "state, (select z.name from ContractState z where z.id=state) as stateName, editTime from ContractInfo "
                + "where contractID=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            IList<ContractInfo> list = DynamicBuilder<ContractInfo>.ConvertToList(dr);

            if (list != null && list.Count > 0)
                return list[0];
            else
                return null;
        }
        #endregion

        #region 添加合同
        /// <summary>
        /// 添加合同
        /// </summary>
        /// <param name="en">实体</param>
        /// <returns></returns>
        public int add(ContractInfo en)
        {
            string strSql = "insert into ContractInfo (contractID, name, customerID, version, price, roomNum, makeCost, backMoney, money, type, channelID, begintime, endtime, ZQ, personID, memo, mDate, billState) "
                + "values (@contractID, @name, @customerID, @version, @price, @roomNum, @makeCost, @backMoney, @money, @type, @channelID, @begintime, @endtime, @ZQ, @personID, @memo, @mDate, @billState)";

            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@contractID", en.contractID);

            if (en.name.Trim() == "")
                param[1] = new SqlParameter("@name", System.DBNull.Value);
            else
                param[1] = new SqlParameter("@name", en.name);

            if (en.customerID == 0)
                param[2] = new SqlParameter("@customerID", System.DBNull.Value);
            else
                param[2] = new SqlParameter("@customerID", en.customerID);

            if (en.version.Trim() == "")
                param[3] = new SqlParameter("@version", System.DBNull.Value);
            else
                param[3] = new SqlParameter("@version", en.version);

            if (en.price == 0.0)
                param[4] = new SqlParameter("@price", System.DBNull.Value);
            else
                param[4] = new SqlParameter("@price", en.price);

            if (en.roomNum == 0)
                param[5] = new SqlParameter("@roomNum", System.DBNull.Value);
            else
                param[5] = new SqlParameter("@roomNum", en.roomNum);

            if (en.makeCost == 0.0)
                param[6] = new SqlParameter("@makeCost", System.DBNull.Value);
            else
                param[6] = new SqlParameter("@makeCost", en.makeCost);

            if (en.backMoney == 0.0)
                param[7] = new SqlParameter("@backMoney", System.DBNull.Value);
            else
                param[7] = new SqlParameter("@backMoney", en.backMoney);

            if (en.money == 0.0)
                param[8] = new SqlParameter("@money", System.DBNull.Value);
            else
                param[8] = new SqlParameter("@money", en.money);

            if (en.type == 0)
                param[9] = new SqlParameter("@type", System.DBNull.Value);
            else
                param[9] = new SqlParameter("@type", en.type);

            if (en.channelID == 0)
                param[10] = new SqlParameter("@channelID", System.DBNull.Value);
            else
                param[10] = new SqlParameter("@channelID", en.channelID);

            if (en.begintime == null)
                param[11] = new SqlParameter("@begintime", System.DBNull.Value);
            else
                param[11] = new SqlParameter("@begintime", en.begintime);

            if (en.endtime == null)
                param[12] = new SqlParameter("@endtime", System.DBNull.Value);
            else
                param[12] = new SqlParameter("@endtime", en.endtime);

            if (en.ZQ == 0.0)
                param[13] = new SqlParameter("@ZQ", System.DBNull.Value);
            else
                param[13] = new SqlParameter("@ZQ", en.ZQ);

            if (en.personID == 0)
                param[14] = new SqlParameter("@personID", System.DBNull.Value);
            else
                param[14] = new SqlParameter("@personID", en.personID);

            if (en.memo.Trim() == "")
                param[15] = new SqlParameter("@memo", System.DBNull.Value);
            else
                param[15] = new SqlParameter("@memo", en.memo);

            if (en.mDate == null)
                param[16] = new SqlParameter("@mDate", System.DBNull.Value);
            else
                param[16] = new SqlParameter("@mDate", en.mDate);

            if (en.billState == 0)
                param[17] = new SqlParameter("@billState", System.DBNull.Value);
            else
                param[17] = new SqlParameter("@billState", en.billState);

            
            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
        #endregion

        #region 修改合同
        /// <summary>
        /// 修改合同
        /// </summary>
        /// <param name="en">实体</param>
        /// <returns></returns>
        public int update(ContractInfo en)
        {
            string strSql = "update ContractInfo set name=@name, customerID=@customerID, version=@version, "
                + "price=@price, roomNum=@roomNum, makeCost=@makeCost, backMoney=@backMoney, money=@money, type=@type, "
                + "channelID=@channelID, begintime=@begintime, endtime=@endtime, ZQ=@ZQ, personID=@personID, memo=@memo, "
                + "mDate=@mDate, billState=@billState, editTime=getDate() where contractID=@id ";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@contractID", en.contractID),
                new SqlParameter("@name", en.name),
                new SqlParameter("@customerID", en.customerID),
                new SqlParameter("@version", en.version),
                new SqlParameter("@price", en.price),
                new SqlParameter("@roomNum", en.roomNum),
                new SqlParameter("@makeCost", en.makeCost),
                new SqlParameter("@backMoney", en.backMoney),
                new SqlParameter("@money", en.money),
                new SqlParameter("@type", en.type),
                new SqlParameter("@channelID", en.channelID),
                new SqlParameter("@begintime", System.DBNull.Value),                    
                new SqlParameter("@endtime", System.DBNull.Value),
                new SqlParameter("@ZQ", en.ZQ),
                new SqlParameter("@personID", en.personID),
                new SqlParameter("@memo", en.memo),
                new SqlParameter("@mDate", System.DBNull.Value),
                new SqlParameter("@billState", en.billState)
            };

            if (en.begintime != null)
                param[11].Value = en.begintime;

            if (en.endtime != null)
                param[12].Value = en.endtime;

            if (en.mDate != null)
                param[16].Value = en.mDate;


            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
        #endregion

        #region 修改开票状态
        /// <summary>
        /// 修改开票状态
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int editBillState(ContractInfo en)
        {
            string strSql = "update ContractInfo set billState=@state where contractID=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", en.contractID),
                new SqlParameter("@state", en.billState)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
        #endregion

        #region 修改合同状态
        /// <summary>
        /// 修改合同状态
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int editContractState(ContractInfo en)
        {
            string strSql = "update ContractInfo set state=@state where contractID=@id";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", en.contractID),
                new SqlParameter("@state", en.state)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }
        #endregion
    }
}