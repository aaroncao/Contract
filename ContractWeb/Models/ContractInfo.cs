using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 合同信息
    /// </summary>
    public class ContractInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string contractID { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public int customerID { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string customerName { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// 单厅价格
        /// </summary>
        public double price { get; set; }

        /// <summary>
        /// 每场厅数
        /// </summary>
        public int roomNum { get; set; }

        /// <summary>
        /// 制作费
        /// </summary>
        public double makeCost { get; set; }

        /// <summary>
        /// 回扣金额
        /// </summary>
        public double backMoney { get; set; }

        /// <summary>
        /// 签署金额
        /// </summary>
        public double money { get; set; }

        /// <summary>
        /// 合同类型编号
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 合同类型
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// 渠道类型编号
        /// </summary>
        public int channelID { get; set; }

        /// <summary>
        /// 渠道类型
        /// </summary>
        public string channelName { get; set; }

        /// <summary>
        /// 合同周期起
        /// </summary>
        public DateTime begintime { get; set; }

        /// <summary>
        /// 合同周期止
        /// </summary>
        public DateTime endtime { get; set; }

        /// <summary>
        /// 周期
        /// </summary>
        public double ZQ { get; set; }

        /// <summary>
        /// 经办人ID
        /// </summary>
        public int personID { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        public string personName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 签署日期
        /// </summary>
        public DateTime mDate { get; set; }

        /// <summary>
        /// 发票状态ID
        /// </summary>
        public int billState { get; set; }

        /// <summary>
        /// 发票状态
        /// </summary>
        public string billStateName { get; set; }

        /// <summary>
        /// 合同状态ID
        /// </summary>
        public int state { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public string stateName { get; set; }

        /// <summary>
        /// 合同状态修改时间
        /// </summary>
        public DateTime? editTime { get; set; }
    }
}