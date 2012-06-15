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

public partial class orderCheck : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string active_time = Request.Form["active_time"];
        string sign = Request.Form["sign"];
        string bill_month = Request.Form["bill_month"];
        string last_order_id = Request.Form["last_order_id"];

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

        #region 根据账单月获取对账数据
        Cps360Model cpsModel = new Cps360Model();

        List<IDictionary> order_list = cpsModel.getCheckedList(bill_month, last_order_id, Cps360Config.MAX_NUM);

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
