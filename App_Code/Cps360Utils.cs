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
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 通用类
/// </summary>
public class Cps360Utils
{
	public Cps360Utils()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 获取当前unix时间戳
    /// </summary>
    /// <returns></returns>
    public static long getCurrentTimestamp()
    {
        long time = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        return time;
    }

    /// <summary>
    /// 检查超时
    /// </summary>
    /// <param name="active_time">请求时的UNIX时间戳</param>
    /// <param name="msg">提示信息</param>
    /// <returns>true：不超时，false：超时</returns>
    public static bool checkActiveTime(long active_time, out string msg)
    {
        long currentTime = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        long diffTime = Math.Abs(currentTime - active_time);   //时间差（s）

        if ((int)diffTime > Cps360Config.TIMEOUT)
        {
            msg = "检查超时";
            return false;
        }
        msg = "成功";
        return true;
    }

    /// <summary>
    /// 获取32位md5加密串
    /// </summary>
    /// <param name="input">需要加密的字符串</param>
    /// <returns>加密后的字符串</returns>
    public static string getMd5Hash(string input)
    {
        MD5 md5Hasher = MD5.Create();

        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
        StringBuilder sBuilder = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        return sBuilder.ToString();
    }
    /// <summary>
    /// 发送POST数据
    /// </summary>
    /// <param name="url">目标url</param>
    /// <param name="data">post数据</param>
    public static void postRequest(string url, byte[] data)
    {
        WebRequest webRequest = WebRequest.Create(url);
        HttpWebRequest httpRequest = webRequest as HttpWebRequest;
        if (httpRequest == null)
        {
            throw new ApplicationException(
                string.Format("Invalid url string: {0}", url)
                );
        }

        httpRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1;360cps sdk;)";
        httpRequest.ContentType = "application/x-www-form-urlencoded";
        httpRequest.Method = "POST";

        httpRequest.ContentLength = data.Length;
        Stream requestStream = httpRequest.GetRequestStream();
        requestStream.Write(data, 0, data.Length);
        requestStream.Close(); 
    }

    /// <summary>
    /// 检查签名
    /// </summary>
    /// <param name="active_time">请求时间的UNIX时间戳</param>
    /// <param name="sign">POST中的sign</param>
    /// <param name="msg">输出的信息</param>
    /// <returns></returns>
    public static bool checkSign(string active_time, string sign, out string msg)
    {
        string s_sign = String.Join("#", new string[] { Cps360Config.BID, active_time, Cps360Config.CP_KEY });
        s_sign = Cps360Utils.getMd5Hash(s_sign);

        if (String.Compare(s_sign, sign) != 0)
        {
            msg = "签名失败";
            return false;
        }
        msg = "成功";
        return true;
    }
    /// <summary>
    /// 构造xml串
    /// </summary>
    /// <param name="xml_fileds">xml字段</param>
    /// <param name="order_list">订单集合</param>
    /// <returns></returns>
    public static string getXml(string[] xml_fileds, List<IDictionary> order_list)
    {
        string ln = "\n";
        string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><orders>" + ln;

        foreach (IDictionary row in order_list)
        {
            xml += "<order>" + ln;

            foreach (string field in xml_fileds)
            {
                xml += "<" + field + ">" + row[field] + "</" + field + ">" + ln;
            }
            xml += "</order>" + ln;
        }
        xml += "</orders>";

        return xml;
    }
    
}
