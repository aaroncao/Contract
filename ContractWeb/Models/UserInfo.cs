using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractWeb.Models
{
    public class UserInfo
    {
        //编号
        int id;

        //登录昵称
        string userID;

        //权限组ID
        int powergroupID;

        //密码
        string password;

        //使用状态
        int state = 1;

        //真实姓名
        string name;

        //性别
        int sex = 1;

        //身份证号
        string card;

        //联系电话
        string tel;

        //地址
        string address;

        //注册日期
        DateTime date;
    }
}