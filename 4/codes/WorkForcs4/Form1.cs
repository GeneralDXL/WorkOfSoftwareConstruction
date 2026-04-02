using System;
using System.IO;
using System.Windows.Forms;


namespace WorkForcs4;

public partial class Form1:Form
{
    private TextBox textBoxFile1;
    private TextBox textBoxFile2;
    private Button btnBrowse1;
    private Button btnBrowse2;
    private Button btnMerge;
    private Label lblStatus;

    public Form1()
    {
        SetupControls();
    }

    private void SetupControls()
    {
        // 窗体基本设置
        this.Text = "文本文件合并工具";
        this.Size = new System.Drawing.Size(600, 250);
        this.StartPosition = FormStartPosition.CenterScreen;

        // 文件1路径框
        textBoxFile1 = new TextBox
        {
            Location = new System.Drawing.Point(20, 20),
            Size = new System.Drawing.Size(400, 23),
            ReadOnly = true
        };
        // 浏览按钮1
        btnBrowse1 = new Button
        {
            Text = "浏览...",
            Location = new System.Drawing.Point(430, 18),
            Size = new System.Drawing.Size(100, 25)
        };
        btnBrowse1.Click += BtnBrowse1_Click;

        // 文件2路径框
        textBoxFile2 = new TextBox
        {
            Location = new System.Drawing.Point(20, 60),
            Size = new System.Drawing.Size(400, 23),
            ReadOnly = true
        };
        // 浏览按钮2
        btnBrowse2 = new Button
        {
            Text = "浏览...",
            Location = new System.Drawing.Point(430, 58),
            Size = new System.Drawing.Size(100, 25)
        };
        btnBrowse2.Click += BtnBrowse2_Click;

        // 合并按钮
        btnMerge = new Button
        {
            Text = "合并文件",
            Location = new System.Drawing.Point(20, 110),
            Size = new System.Drawing.Size(100, 30)
        };
        btnMerge.Click += BtnMerge_Click;

        // 状态标签
        lblStatus = new Label
        {
            Text = "请选择两个文本文件",
            Location = new System.Drawing.Point(20, 160),
            Size = new System.Drawing.Size(500, 40),
            ForeColor = System.Drawing.Color.DarkBlue
        };

        // 将控件添加到窗体
        this.Controls.Add(textBoxFile1);
        this.Controls.Add(btnBrowse1);
        this.Controls.Add(textBoxFile2);
        this.Controls.Add(btnBrowse2);
        this.Controls.Add(btnMerge);
        this.Controls.Add(lblStatus);
    }

    // 选择第一个文件
    private void BtnBrowse1_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog ofd = new OpenFileDialog())
        {
            ofd.Title = "选择第一个文本文件";
            ofd.Filter = "文本文件|*.txt|所有文件|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxFile1.Text = ofd.FileName;
            }
        }
    }

    // 选择第二个文件
    private void BtnBrowse2_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog ofd = new OpenFileDialog())
        {
            ofd.Title = "选择第二个文本文件";
            ofd.Filter = "文本文件|*.txt|所有文件|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxFile2.Text = ofd.FileName;
            }
        }
    }

    // 合并文件
    private void BtnMerge_Click(object sender, EventArgs e)
    {
        // 检查是否已选择两个文件
        if (string.IsNullOrWhiteSpace(textBoxFile1.Text) || string.IsNullOrWhiteSpace(textBoxFile2.Text))
        {
            lblStatus.Text = "错误：请先选择两个文本文件。";
            lblStatus.ForeColor = System.Drawing.Color.Red;
            return;
        }

        // 检查文件是否存在
        if (!File.Exists(textBoxFile1.Text))
        {
            lblStatus.Text = $"错误：文件不存在 - {textBoxFile1.Text}";
            lblStatus.ForeColor = System.Drawing.Color.Red;
            return;
        }
        if (!File.Exists(textBoxFile2.Text))
        {
            lblStatus.Text = $"错误：文件不存在 - {textBoxFile2.Text}";
            lblStatus.ForeColor = System.Drawing.Color.Red;
            return;
        }

        try
        {
            // 读取两个文件的内容
            string content1 = File.ReadAllText(textBoxFile1.Text);
            string content2 = File.ReadAllText(textBoxFile2.Text);

            // 合并内容（可以自定义分隔符，这里简单拼接，并在两个文件内容之间加一个换行）
            string mergedContent = content1 + Environment.NewLine + Environment.NewLine + content2;

            // 确定目标目录：可执行程序目录下的 Data 子目录
            string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            string dataDir = Path.Combine(exeDir, "Data");
            // 如果目录不存在则创建
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }

            // 生成合并后的文件名（包含时间戳，避免覆盖）
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string mergedFileName = $"MergedFile_{timestamp}.txt";
            string mergedFilePath = Path.Combine(dataDir, mergedFileName);

            // 写入合并后的文件
            File.WriteAllText(mergedFilePath, mergedContent);

            lblStatus.Text = $"成功！合并文件已保存到：{mergedFilePath}";
            lblStatus.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception ex)
        {
            lblStatus.Text = $"合并时发生错误：{ex.Message}";
            lblStatus.ForeColor = System.Drawing.Color.Red;
        }
    }
}

