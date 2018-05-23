using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginService.Models
{
    public class UserLoginModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 所属租户Id
        /// </summary>
        public string TenantId { get; set; }

        //public string PassWord { get; set; }
    }
}