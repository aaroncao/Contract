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

        /// <summary>
        /// 获取合同列表
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
                    where += " or ";
                where += "channelID=" + channel;
            }

            if (type.Trim() != "")
            {
                if (where != "")
                    where += " or ";
                where += "type=" + type;
            }

            if (state.Trim() != "")
            {
                if (where != "")
                    where += " or ";
                where += "state=" + state;
            }

            if (person.Trim() != "")
            {
                if (where != "")
                    where += " or ";
                where += "personID=" + person;
            }

            if (billState.Trim() != "")
            {
                if (where != "")
                    where += " or ";
                where += "billState=" + billState;
            }

            if (billState.Trim() != "")
            {
                if (where != "")
                    where += " or ";
                where += "billState=" + billState;
            }

            if (date.Trim() != "")
            {
                if (where != "")
                    where += " or ";
                where += "mDate='" + date + "'";
            }

            if (where != "")
                strSql += "where " + where;

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<ContractInfo> list = DynamicBuilder<ContractInfo>.ConvertToList(dr);
            return list;
        }

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


        /// <summary>
        /// 添加合同
        /// </summary>
        /// <param name="en">实体</param>
        /// <returns></returns>
        public int add(ContractInfo en)
        {
            string strSql = "insert into ContractInfo (contractID, name, customerID, version, price, roomNum, makeCost, backMoney, money, type, channelID, begintime, endtime, ZQ, personID, memo, mDate, billState) "
                + "values (@contractID, @name, @customerID, @version, @price, @roomNum, @makeCost, @backMoney, @money, @type, @channelID, @begintime, @endtime, @ZQ, @personID, @memo, @mDate, @billState)";

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
    }
}