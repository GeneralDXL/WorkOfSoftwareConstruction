namespace WorkForcs7;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
public partial class Form1 : Form
{
    // 共享的 HttpClient 实例，避免端口耗尽
    private static readonly HttpClient _httpClient = new HttpClient();

    public Form1()
    {
        InitializeComponent();
        // 设置 HttpClient 默认请求头，模拟浏览器行为
        _httpClient.DefaultRequestHeaders.Add("User-Agent",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36 Edg/120.0.0.0");
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8");
    }

    // 搜索按钮点击事件
    private async void btnSearch_Click(object sender, EventArgs e)
    {
        string keyword = txtKeyword.Text.Trim();
        if (string.IsNullOrEmpty(keyword))
        {
            MessageBox.Show("请输入搜索关键字。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        // 禁用按钮，清空之前的结果
        btnSearch.Enabled = false;
        txtBaiduResult.Clear();
        txtBingResult.Clear();
        lblStatus.Text = "正在搜索中，请稍候...";

        try
        {
            // 对关键字进行 URL 编码
            string encodedKeyword = Uri.EscapeDataString(keyword);

            // 创建百度与必应的搜索任务，并行执行
            Task<string> baiduTask = SearchAndExtractAsync(
                $"https://www.baidu.com/s?wd={encodedKeyword}",
                ExtractBaiduSnippets
            );
            Task<string> bingTask = SearchAndExtractAsync(
                $"https://cn.bing.com/search?q={encodedKeyword}",
                ExtractBingSnippets
            );

            // 等待两个任务全部完成
            string[] results = await Task.WhenAll(baiduTask, bingTask);

            // 更新 UI 显示结果
            txtBaiduResult.Text = results[0];
            txtBingResult.Text = results[1];
            lblStatus.Text = "搜索完成。";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            lblStatus.Text = "搜索出错。";
        }
        finally
        {
            btnSearch.Enabled = true;
        }
    }

    /// <summary>
    /// 异步搜索并提取摘要文本（前200字）
    /// </summary>
    /// <param name="url">完整的搜索URL</param>
    /// <param name="extractFunc">从HTML中提取摘要文本的函数</param>
    /// <returns>提取到的文本（最多200字符）</returns>
    private async Task<string> SearchAndExtractAsync(string url, Func<string, string> extractFunc)
    {
        try
        {
            // 异步获取HTML内容
            string html = await _httpClient.GetStringAsync(url);
            // 解析HTML并提取摘要文本（此操作在异步方法中同步执行，因为HTML不大，不会明显阻塞）
            string rawText = extractFunc(html);
            // 截取前200个字符
            return TruncateText(rawText, 200);
        }
        catch (HttpRequestException ex)
        {
            return $"网络请求失败：{ex.Message}";
        }
        catch (Exception ex)
        {
            return $"处理失败：{ex.Message}";
        }
    }

    /// <summary>
    /// 从百度搜索结果页中提取所有结果摘要的合并文本
    /// </summary>
    private string ExtractBaiduSnippets(string html)
    {
        var doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);

        // 百度搜索结果摘要通常位于 class="c-abstract" 的 div 中
        var nodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'c-abstract')]");
        if (nodes == null || nodes.Count == 0)
        {
            // 降级方案：提取页面所有可见文本（移除脚本、样式等）
            return ExtractVisibleText(doc);
        }

        string result = string.Join(" ", nodes.Select(n => n.InnerText.Trim()));
        return string.IsNullOrWhiteSpace(result) ? "未提取到有效摘要信息。" : result;
    }

    /// <summary>
    /// 从必应搜索结果页中提取所有结果摘要的合并文本
    /// </summary>
    private string ExtractBingSnippets(string html)
    {
        var doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);

        // 必应搜索结果的摘要常见位置：
        // 1. class="b_caption" 下的 p 标签
        // 2. 直接 class="snippet_text" 的元素
        var bCaptionParagraphs = doc.DocumentNode.SelectNodes("//div[contains(@class, 'b_caption')]//p");
        var snippetTexts = doc.DocumentNode.SelectNodes("//*[contains(@class, 'snippet_text')]");

        List<string> snippets = new List<string>();

        if (bCaptionParagraphs != null)
        {
            snippets.AddRange(bCaptionParagraphs.Select(p => p.InnerText.Trim()));
        }
        if (snippetTexts != null)
        {
            snippets.AddRange(snippetTexts.Select(s => s.InnerText.Trim()));
        }

        if (snippets.Count > 0)
        {
            return string.Join(" ", snippets);
        }

        // 降级方案：提取页面所有可见文本
        return ExtractVisibleText(doc);
    }

    /// <summary>
    /// 从 HtmlDocument 中提取所有可见文本（移除脚本、样式、标签）
    /// </summary>
    private string ExtractVisibleText(HtmlAgilityPack.HtmlDocument doc)
    {
        // 移除 script 和 style 节点
        var nodesToRemove = doc.DocumentNode.SelectNodes("//script|//style");
        if (nodesToRemove != null)
        {
            foreach (var node in nodesToRemove)
            {
                node.Remove();
            }
        }

        // 获取纯文本，多个空白字符合并为一个空格
        string text = doc.DocumentNode.InnerText;
        text = System.Text.RegularExpressions.Regex.Replace(text, @"\s+", " ");
        return text.Trim();
    }

    /// <summary>
    /// 截取字符串前 length 个字符（中英文均按一个字符计）
    /// </summary>
    private string TruncateText(string text, int length)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        return text.Length <= length ? text : text.Substring(0, length);
    }
}