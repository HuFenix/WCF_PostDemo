using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

namespace WCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    //[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1 : IService1
    {
        private string _tId = "NULL";

        public Service1()
        {
            //todo 从header中拿到tid 赋给全局变量
            var tid = HttpContext.Current.Request.Headers["tID"];
            _tId = tid;
        }

        public string TestA()
        {
           
            string text = "Tid：{0},字符串";
            return string.Format(text, _tId);
        }

        public string TestB(string Name,string Pwd)
        {

            string text = "Tid：{0},name:{1},pwd:{2}";
            return string.Format(text, _tId,Name,Pwd);
        }
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public class ErrorModel
        {
            public bool IsError { get; set; }

            public int ErrorCode { get; set; }

            public string ErrorMsg { get; set; }
        }
    }
}
