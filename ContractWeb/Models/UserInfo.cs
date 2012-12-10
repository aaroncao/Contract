using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 登录昵称
        /// </summary>
        public string userID { get; set; }

        /// <summary>
        /// 权限组ID
        /// </summary>
        public string powergroupID { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 使用状态
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string card { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string tel { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 注册日期
        /// </summary>
        public DateTime date { get; set; }

        /// <summary>
        /// 实体克隆
        /// </summary>
        /// <returns></returns>
        public UserInfo clone()
        {
            UserInfo en = new UserInfo();

            en.id = id;
            en.userID = userID;
            en.powergroupID = powergroupID;
            en.password = password;
            en.state = state;
            en.name = name;
            en.sex = sex;
            en.card = card;
            en.tel = tel;
            en.address = address;
            en.date = date;

            return en;
        }
    }
}