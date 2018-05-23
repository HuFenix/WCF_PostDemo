using LoginService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LoginService.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Post()
        {
            var tid = "29760856-b194-498a-9f48-2e9d79f5402b";
            var userName = "Fenix1";
 
           
            var url = "http://localhost:41002/Service1.svc";
            string method = "TestA";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url + "/" + method);
            request.Method = "POST";
            request.Headers.Add("tID", tid);
            request.ContentType = "application/json";
            Stream requestStram = request.GetRequestStream();
            requestStram.Close();
            HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string ReqResult = reader.ReadToEnd();
            return Json(ReqResult);            
        }

        public ActionResult Post2(string name, string pwd, string redirect_url, string client_id = null)
        {
            var tid = "29760856-b194-498a-9f48-2e9d79f5402b";
            var userName = "Fenix1";



            //记录用户相关session
            UserLoginModel _user = new UserLoginModel { LoginName = userName, TenantId = tid };
            Session["user"] = _user;
            //单点登录校验cookie
            HttpCookie cookie = new HttpCookie("usercookieTid");
            cookie.HttpOnly = true;
            cookie.Expires = DateTime.Now.AddYears(100);
            cookie.Value = tid;
            Response.Cookies.Add(cookie);



            //WCFService.Service1Client WcfSer = new WCFService.Service1Client();

            string result = "";  
            var content = JsonConvert.SerializeObject(new { Name = name, Pwd = pwd });
            var url = "http://localhost:41002/Service1.svc";
            string method = "TestB";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url + "/" + method);
            request.Method = "POST";
            request.Headers.Add("tID", tid);
            request.ContentType = "application/json";

            #region 添加Post 参数
            byte[] data = Encoding.UTF8.GetBytes(content);
            request.ContentLength = data.Length;
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion  

            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容  
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return Json(result);
        }

        

    }
}