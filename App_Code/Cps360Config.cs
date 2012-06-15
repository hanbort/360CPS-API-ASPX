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

/// <summary>
///360Cps配置类
/// </summary>
public class Cps360Config
{
    /// <summary>
    /// 密钥,由360cps系统分配，必须修改
    /// </summary>
    public const string CP_KEY = "";
    /// <summary>
    /// 网站域名，必须修改
    /// </summary>
    public const string COOKIE_DOMAIN = "example.com";
    /// <summary>
    /// cookie的有效期长,由商务上确定，一般是30天
    /// </summary>
    public const double COOKIE_RD = 30.00;
    /// <summary>
    /// 错误报告地址
    /// </summary>
    public const string FAILED_URL = "http://open.union.360.cn/gofailed";
    /// <summary>
    /// 360分配给商家的bid，必须修改
    /// </summary>
    public const string BID = "";
    /// <summary>
    /// 默认跳转的url
    /// </summary>
    public const string DEFAULT_GO_URL="http://domain.com";
    /// <summary>
    /// 超时时间，默认为900s
    /// </summary>
    public const int TIMEOUT = 900;
    /// <summary>
    /// 查询返回最大记录条数
    /// </summary>
    public const int MAX_NUM = 2000;

    public Cps360Config()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
}
