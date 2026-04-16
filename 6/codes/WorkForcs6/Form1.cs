namespace WorkForcs6;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
public partial class Form1 : Form
{
    private static readonly HttpClient _httpClient;

    static Form1()
    {
        var handler = new HttpClientHandler
        {
            AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate,
            UseCookies = true,
            // 忽略 SSL 证书验证错误（解决某些网站的证书问题）
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
        _httpClient = new HttpClient(handler);
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        _httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.8,en-US;q=0.5,en;q=0.3");
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public Form1()
    {
        InitializeComponent();
        // 给一个默认测试 URL，该页面公开且包含多种电话/邮箱
        txtUrl.Text = "https://www.114.tel/"; // 默认测试网址 

    }

    private async void btnFetch_Click(object sender, EventArgs e)
    {
        string url = txtUrl.Text.Trim();
        if (string.IsNullOrEmpty(url))
        {
            MessageBox.Show("请输入 URL", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
            !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            url = "http://" + url;

        btnFetch.Enabled = false;
        lstPhones.Items.Clear();
        lstEmails.Items. Clear();
        lblStatus.Text = "正在获取网页...";
        lblStatus.ForeColor = System.Drawing.Color.Blue;

        try
        {
            // 如果用户提供了 Cookie（通过一个额外的文本框），可以添加
            if (!string.IsNullOrEmpty(txtCookie.Text))
            {
                _httpClient.DefaultRequestHeaders.Remove("Cookie");
                _httpClient.DefaultRequestHeaders.Add("Cookie", txtCookie.Text);
            }

            string html = await _httpClient.GetStringAsync(url);

            // 显示 HTML 长度，便于调试
            lblStatus.Text = $"已获取 {html.Length} 字符，正在解析...";

            // 提取电话号码（多种格式）
            var phones = ExtractPhoneNumbers(html);
            // 提取邮箱
            var emails = ExtractEmails(html);

            // 显示结果
            this.Invoke((MethodInvoker)delegate {
                foreach (var phone in phones)
                    lstPhones.Items.Add(phone);
                foreach (var email in emails)
                    lstEmails.Items.Add(email);

                grpPhones.Text = $"电话号码 ({phones.Count})";
                grpEmails.Text = $"邮箱地址 ({emails.Count})";
            });

            lblStatus.Text = $"完成！找到 {phones.Count} 个电话，{emails.Count} 个邮箱。";
            lblStatus.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception ex)
        {
            lblStatus.Text = "错误：" + ex.Message;
            lblStatus.ForeColor = System.Drawing.Color.Red;
            MessageBox.Show($"获取失败：\n{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnFetch.Enabled = true;
        }
    }

    /// <summary>
    /// 提取多种格式的电话号码：
    /// 1. 标准11位手机号 (1开头第二位3-9)
    /// 2. 带短横线或空格的手机号 (如 138-1234-5678 或 138 1234 5678)
    /// 3. 固定电话带区号 (如 010-12345678, (010)12345678, 010 12345678)
    /// 4. 带分机号 (如 010-12345678-1234)
    /// 5. 400/800 号码
    /// </summary>
    private HashSet<string> ExtractPhoneNumbers(string text)
    {
        var phones = new HashSet<string>();

        // 正则1: 11位手机号（连续）
        Regex mobile1 = new Regex(@"\b1[3-9]\d{9}\b");
        foreach (Match m in mobile1.Matches(text))
            phones.Add(m.Value);

        // 正则2: 带分隔符的手机号 如 138-1234-5678, 138 1234 5678, 138.1234.5678
        Regex mobile2 = new Regex(@"\b1[3-9]\d[\s\-\.]?\d{4}[\s\-\.]?\d{4}\b");
        foreach (Match m in mobile2.Matches(text))
            phones.Add(m.Value.Replace(" ", "").Replace("-", "").Replace(".", ""));

        // 正则3: 固定电话（区号-号码） 如 010-12345678, 0755-1234567, (010)12345678, 010 12345678
        Regex landline = new Regex(@"(?:(?:0\d{2,3}[-\s]?)?\(?0\d{2,3}\)?[-\s]?\d{7,8}|\d{7,8})(?:[-\s]?\d{1,6})?");
        // 为了避免匹配到手机号的一部分，我们需要更精确的固定电话正则：
        Regex landline2 = new Regex(@"\b(?:0\d{2,3}[-\s]?|\(0\d{2,3}\)[-\s]?)?\d{7,8}\b");
        foreach (Match m in landline2.Matches(text))
        {
            string num = m.Value;
            // 过滤掉长度不符合的（手机号已经处理过，但这里可能会重复，用HashSet自动去重）
            if (num.Length >= 10 && num.Length <= 12)
                phones.Add(num);
        }

        // 正则4: 400/800 号码
        Regex special = new Regex(@"\b[48]00[-\s]?\d{3}[-\s]?\d{4}\b");
        foreach (Match m in special.Matches(text))
            phones.Add(m.Value.Replace(" ", "").Replace("-", ""));

        // 可选：匹配带有国际区号的 +86 手机号
        Regex intlMobile = new Regex(@"\+86[-\s]?1[3-9]\d{9}");
        foreach (Match m in intlMobile.Matches(text))
            phones.Add(m.Value.Replace("+86", "").Replace(" ", "").Replace("-", ""));

        return phones;
    }

    /// <summary>
    /// 提取邮箱地址（标准格式，支持下划线、点、加号）
    /// </summary>
    private HashSet<string> ExtractEmails(string text)
    {
        var emails = new HashSet<string>();
        Regex emailRegex = new Regex(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b");
        foreach (Match m in emailRegex.Matches(text))
            emails.Add(m.Value);
        return emails;
    }
}