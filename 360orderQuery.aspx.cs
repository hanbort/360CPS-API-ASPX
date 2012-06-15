using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

public partial class _360orderQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string active_time = Request.Form["active_time"];
        string sign = Request.Form["sign"];
        string order_ids = Request.Form["order_ids"];   //以逗号分割多个订单
        string start_time = Request.Form["start_time"];
        string end_time = Request.Form["end_time"];
        string last_order_id = Request.Form["last_order_id"];
        string updstart_time = Request.Form["updstart_time"];
        string updend_time = Request.Form["updend_time"];

        #region 检查超时
        string msg = "";
        if (!Cps360Utils.checkActiveTime(long.Parse(active_time), out msg))
        {
            Response.Write(msg);
        }
        #endregion

        #region 检查签名
        if (!Cps360Utils.checkSign(active_time, sign, out msg))
        {
            Response.Write(msg);
        }
        #endregion

        #region 通过订单号批量查询
        Cps360Model cpsModel = new Cps360Model();
        List<IDictionary> order_list = new List<IDictionary>();
        if (!String.IsNullOrEmpty(order_ids))
        {
            string[] orderid_arr = order_ids.Split(new char[] { ',' });
            order_list = cpsModel.getOrdersByIds(orderid_arr, Cps360Config.MAX_NUM);
        }
        else if (!String.IsNullOrEmpty(start_time) && !String.IsNullOrEmpty(end_time))
        {
            DateTime s_time = DateTime.Parse(start_time);
            DateTime e_time = DateTime.Parse(end_time);
            order_list = cpsModel.getOrdersByTime(s_time, e_time, last_order_id, Cps360Config.MAX_NUM);
        }
        else if (!String.IsNullOrEmpty(updstart_time) && !String.IsNullOrEmpty(updend_time))
        {
            DateTime s_time = DateTime.Parse(updstart_time);
            DateTime e_time = DateTime.Parse(updend_time);
            order_list = cpsModel.getOrdersByUpdtime(s_time, e_time, last_order_id, Cps360Config.MAX_NUM);
        }

        //xml数据字段
        string[] xml_fields = new string[] { 
                                                "bid", 
                                                "qid", 
                                                "qihoo_id", 
                                                "order_id", 
                                                "order_time", 
                                                "order_updtime",
                                                "server_price",
                                                "order_price",
                                                "coupon",
                                                "total_price",
                                                "total_comm",
                                                "commission",
                                                "p_info",
                                                "ext",
                                                "status"};
        string xml = Cps360Utils.getXml(xml_fields, order_list);

        Response.Write(xml);
        #endregion
    }
}
