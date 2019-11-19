using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    /// <summary>
    /// 管理员类
    /// </summary>
    [Serializable]
    public class Admin
    {
        //登录账号
        public int LoginId { get; set; }
        //登录密码
        public string LoginPwd { get; set; }
        //管理员名称
        public string AdminName { get; set; }
    }
}
