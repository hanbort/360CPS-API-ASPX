using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Net;
using System.IO;

public partial class _360Redirect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 获取POST数据
        string bid = Request.Form["bid"];
        string qihoo_id = Request.Form["qihoo_id"];
        string url = Request.Form["url"];
        string from_url = Request.Form["from_url"];
        string active_time = Request.Form["active_time"];   //请求时间的时间戳
        string ext = Request.Form["ext"];
        string qid = Request.Form["qid"];
        string qmail = Request.Form["qmail"];
        string qname = Request.Form["qname"];
        string sign = Request.Form["sign"];
        #endregion

        #region 设置cookie信息
        HttpCookie cookie = new HttpCookie("cpsinfo");
        cookie.Values.Add("id", "360cps");
        cookie.Values.Add("qihoo_id", qihoo_id);
        cookie.Values.Add("ext", ext);
        cookie.Values.Add("qid", qid);
        cookie.Values.Add("qmail", qmail);
        cookie.Values.Add("qname", qname);
        cookie.Expires = DateTime.Now.AddDays(Cps360Config.COOKIE_RD);
        cookie.Domain = Cps360Config.COOKIE_DOMAIN;
        cookie.Path = "/";
        #endregion

        #region 构造签名
        string sign_check = String.Join("#", new string[] { bid, active_time, Cps360Config.CP_KEY, qid, qmail, qname });
        sign_check = Cps360Utils.getMd5Hash(sign_check);
        #endregion

        
        long currentTime = Cps360Utils.getCurrentTimestamp();
        string msg = "";
        //检查超时时间和签名，如果失败，向360发送一个错误通知
        if (!Cps360Utils.checkActiveTime(long.Parse(active_time), out msg) || sign_check != sign)
        {
            string from_ip = Page.Request.UserHostAddress;

            ASCIIEncoding encoding = new ASCIIEncoding();

            string t_sign = Cps360Utils.getMd5Hash(String.Join("#", new string[] { Cps360Config.BID, currentTime.ToString(), Cps360Config.CP_KEY }));
            string postData = String.Join("&", new string[] { "bid=" + Cps360Config.BID, 
                                                            "active_time=" + currentTime, 
                                                            "sign=" + t_sign, 
                                                            "pre_bid=" + bid, 
                                                            "pre_active_time=" + active_time, 
                                                            "pre_sign=" + sign, 
                                                            "qid=" + qid, 
                                                            "qname=" + qname, 
                                                            "qmail=" + qmail, 
                                                            "from_url=" + from_url, 
                                                            "from_ip=" + from_ip });

            byte[] data = encoding.GetBytes(postData);

            Cps360Utils.postRequest(Cps360Config.FAILED_URL, data);
        }
        else
        {
            if (!String.IsNullOrEmpty(qid))
            {
                //实现自动登录
                Cps360Model cpsModel = new Cps360Model();
                cpsModel.autoLogin(qid, qname, qmail);
            }
        }

        #region 完成跳转
        if (String.IsNullOrEmpty(url))
        {
            url = Cps360Config.DEFAULT_GO_URL;
        }
        Response.Redirect(url);
        #endregion
    }
}
