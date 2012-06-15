using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Cps模型类
/// </summary>
public class Cps360Model
{
	public Cps360Model()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public void autoLogin(string qid, string qname, string qmail)
    {
        //
        //TODO: 实现自动登录的逻辑
        //
    }

    /// <summary>
    /// 根据订单号数组获取订单信息
    /// </summary>
    /// <param name="orderid_arr">订单号数组</param>
    /// <param name="max">返回的最大条数</param>
    /// <returns></returns>
    public List<IDictionary> getOrdersByIds(string[] orderid_arr, int max)
    {
        //
        //TODO: 根据请求参数，返回符合条件的订单（订单格式请参照360CPS系统的开发文档）
        //

        //返回的订单形式如下：
        List<IDictionary> order_list = new List<IDictionary>();

        IDictionary order_info = new Dictionary<string, string>();
        order_info.Add("bid", "1001");
        order_info.Add("qid", "10001");
        order_info.Add("qihoo_id", "36010");
        order_info.Add("ext", "ac001");
        order_info.Add("order_id", "201201123918");
        order_info.Add("order_time", "2011-07-01 12:11:12");
        order_info.Add("order_updtime", "2011-07-01 12:11:12");
        order_info.Add("total_comm", "11.20");
        order_info.Add("commission", "2001,10%,11.20,56.00,2|2002,10%,6.60,66.00,1");
        order_info.Add("server_price", "0.00");
        order_info.Add("order_price", "112.00");
        order_info.Add("coupon", "0.00");
        order_info.Add("total_price", "112.00");
        order_info.Add("p_info", "2001,某某鞋子,1001,12.00,2,鞋_男鞋,http%3A%2F%2F360.cn%2Fp%3Fid%3D1|2001,某某凉鞋,1002,15.00,1,鞋_男鞋,http%3A%2F%2F360.cn%2Fp%3Fid%3D1");

        order_list.Add(order_info);

        return order_list;
    }
    /// <summary>
    /// 根据订单下单时间获取订单信息
    /// </summary>
    /// <param name="start_time">开始下单时间</param>
    /// <param name="end_time">结束下单时间</param>
    /// <param name="lastOrderId">最后一次查询的订单号</param>
    /// <param name="max">返回的最大条数</param>
    /// <returns></returns>
    public List<IDictionary> getOrdersByTime(DateTime start_time, DateTime end_time, string lastOrderId, int max)
    {
        //
        //TODO: 根据请求参数，返回符合条件的订单（订单格式请参照360CPS系统的开发文档）
        //      查询时结果必须按照订单正序排序
        //

        //返回的订单形式如下：
        List<IDictionary> order_list = new List<IDictionary>();

        IDictionary order_info = new Dictionary<string, string>();
        order_info.Add("bid", "1001");
        order_info.Add("qid", "10001");
        order_info.Add("qihoo_id", "36010");
        order_info.Add("ext", "ac001");
        order_info.Add("order_id", "201201123918");
        order_info.Add("order_time", "2011-07-01 12:11:12");
        order_info.Add("order_updtime", "2011-07-01 12:11:12");
        order_info.Add("total_comm", "11.20");
        order_info.Add("commission", "2001,10%,11.20,56.00,2|2002,10%,6.60,66.00,1");
        order_info.Add("server_price", "0.00");
        order_info.Add("order_price", "112.00");
        order_info.Add("coupon", "0.00");
        order_info.Add("total_price", "112.00");
        order_info.Add("p_info", "2001,某某鞋子,1001,12.00,2,鞋_男鞋,http%3A%2F%2F360.cn%2Fp%3Fid%3D1|2001,某某凉鞋,1002,15.00,1,鞋_男鞋,http%3A%2F%2F360.cn%2Fp%3Fid%3D1");

        order_list.Add(order_info);

        return order_list;
    }

    public List<IDictionary> getOrdersByUpdtime(DateTime start_time, DateTime end_time, string lastOrderId, int max)
    {
        //
        //TODO: 根据请求参数，返回符合条件的订单（订单格式请参照360CPS系统的开发文档）
        //      查询时结果必须按照订单正序排序
        //

        //返回的订单形式如下：
        List<IDictionary> order_list = new List<IDictionary>();

        IDictionary order_info = new Dictionary<string, string>();
        order_info.Add("bid", "1001");
        order_info.Add("qid", "10001");
        order_info.Add("qihoo_id", "36010");
        order_info.Add("ext", "ac001");
        order_info.Add("order_id", "201201123918");
        order_info.Add("order_time", "2011-07-01 12:11:12");
        order_info.Add("order_updtime", "2011-07-01 12:11:12");
        order_info.Add("total_comm", "11.20");
        order_info.Add("commission", "2001,10%,11.20,56.00,2|2002,10%,6.60,66.00,1");
        order_info.Add("server_price", "0.00");
        order_info.Add("order_price", "112.00");
        order_info.Add("coupon", "0.00");
        order_info.Add("total_price", "112.00");
        order_info.Add("p_info", "2001,某某鞋子,1001,12.00,2,鞋_男鞋,http%3A%2F%2F360.cn%2Fp%3Fid%3D1|2001,某某凉鞋,1002,15.00,1,鞋_男鞋,http%3A%2F%2F360.cn%2Fp%3Fid%3D1");

        order_list.Add(order_info);

        return order_list;
    }

    public List<IDictionary> getCheckedList(string billMonth, string lastOrderId, int max)
    {
        //
        //TODO: 根据请求参数，返回符合条件的对账订单（订单格式请参照360CPS系统的开发文档）
        //      查询时结果必须按照订单正序排序
        //

        //返回的订单形式如下：
        List<IDictionary> order_list = new List<IDictionary>();

        IDictionary order_info = new Dictionary<string, string>();
        order_info.Add("order_id", "201201123918");
        order_info.Add("order_time", "2011-07-01 12:11:12");
        order_info.Add("order_updtime", "2011-07-01 12:11:12");
        order_info.Add("server_price", "0.00");
        order_info.Add("order_price", "112.00");
        order_info.Add("coupon", "0.00");
        order_info.Add("total_price", "112.00");
        order_info.Add("total_comm", "11.20");
        order_info.Add("commission", "2001,10%,11.20,56.00,2|2002,10%,6.60,66.00,1");

        order_list.Add(order_info);
        return order_list;
    }
}
