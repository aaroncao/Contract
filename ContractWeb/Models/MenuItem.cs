using System;
using System.Collections.Generic;
using System.Web;

namespace ContractWeb.Models
{
    /// <summary>
    /// 菜单项
    /// </summary>
    public class MenuItem
    {
        
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 归类
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string control { get; set; }

        /// <summary>
        /// 动作
        /// </summary>
        public string action { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="action"></param>
        /// <param name="control"></param>
        public MenuItem(int id, string name, int type, string action, string control)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.action = action;
            this.control = control;            
        }
    }
}