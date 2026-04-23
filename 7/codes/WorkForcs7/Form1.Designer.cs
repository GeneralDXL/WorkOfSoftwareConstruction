namespace WorkForcs7;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.TextBox txtKeyword;
    private System.Windows.Forms.Button btnSearch;
    private System.Windows.Forms.TextBox txtBaiduResult;
    private System.Windows.Forms.TextBox txtBingResult;
    private System.Windows.Forms.Label lblKeyword;
    private System.Windows.Forms.Label lblBaidu;
    private System.Windows.Forms.Label lblBing;
    private System.Windows.Forms.Label lblStatus;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region
    private void InitializeComponent()
    {
        txtKeyword = new TextBox();
        btnSearch = new Button();
        txtBaiduResult = new TextBox();
        txtBingResult = new TextBox();
        lblKeyword = new Label();
        lblBaidu = new Label();
        lblBing = new Label();
        lblStatus = new Label();
        SuspendLayout();
        // 
        // txtKeyword
        // 
        txtKeyword.Location = new Point(115, 17);
        txtKeyword.Margin = new Padding(5, 4, 5, 4);
        txtKeyword.Name = "txtKeyword";
        txtKeyword.Size = new Size(469, 30);
        txtKeyword.TabIndex = 1;
        // 
        // btnSearch
        // 
        btnSearch.Location = new Point(611, 16);
        btnSearch.Margin = new Padding(5, 4, 5, 4);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(141, 35);
        btnSearch.TabIndex = 2;
        btnSearch.Text = "搜索";
        btnSearch.UseVisualStyleBackColor = true;
        btnSearch.Click += btnSearch_Click;
        // 
        // txtBaiduResult
        // 
        txtBaiduResult.Location = new Point(19, 99);
        txtBaiduResult.Margin = new Padding(5, 4, 5, 4);
        txtBaiduResult.Multiline = true;
        txtBaiduResult.Name = "txtBaiduResult";
        txtBaiduResult.ScrollBars = ScrollBars.Vertical;
        txtBaiduResult.Size = new Size(732, 252);
        txtBaiduResult.TabIndex = 4;
        // 
        // txtBingResult
        // 
        txtBingResult.Location = new Point(19, 395);
        txtBingResult.Margin = new Padding(5, 4, 5, 4);
        txtBingResult.Multiline = true;
        txtBingResult.Name = "txtBingResult";
        txtBingResult.ScrollBars = ScrollBars.Vertical;
        txtBingResult.Size = new Size(732, 252);
        txtBingResult.TabIndex = 6;
        // 
        // lblKeyword
        // 
        lblKeyword.AutoSize = true;
        lblKeyword.Location = new Point(19, 21);
        lblKeyword.Margin = new Padding(5, 0, 5, 0);
        lblKeyword.Name = "lblKeyword";
        lblKeyword.Size = new Size(82, 24);
        lblKeyword.TabIndex = 0;
        lblKeyword.Text = "关键字：";
        // 
        // lblBaidu
        // 
        lblBaidu.AutoSize = true;
        lblBaidu.Location = new Point(19, 71);
        lblBaidu.Margin = new Padding(5, 0, 5, 0);
        lblBaidu.Name = "lblBaidu";
        lblBaidu.Size = new Size(100, 24);
        lblBaidu.TabIndex = 3;
        lblBaidu.Text = "百度结果：";
        // 
        // lblBing
        // 
        lblBing.AutoSize = true;
        lblBing.Location = new Point(19, 367);
        lblBing.Margin = new Padding(5, 0, 5, 0);
        lblBing.Name = "lblBing";
        lblBing.Size = new Size(100, 24);
        lblBing.TabIndex = 5;
        lblBing.Text = "必应结果：";
        // 
        // lblStatus
        // 
        lblStatus.AutoSize = true;
        lblStatus.Location = new Point(19, 671);
        lblStatus.Margin = new Padding(5, 0, 5, 0);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(46, 24);
        lblStatus.TabIndex = 7;
        lblStatus.Text = "就绪";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(11F, 24F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(776, 721);
        Controls.Add(lblStatus);
        Controls.Add(txtBingResult);
        Controls.Add(lblBing);
        Controls.Add(txtBaiduResult);
        Controls.Add(lblBaidu);
        Controls.Add(btnSearch);
        Controls.Add(txtKeyword);
        Controls.Add(lblKeyword);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Margin = new Padding(5, 4, 5, 4);
        MaximizeBox = false;
        Name = "Form1";
        Text = "搜索引擎摘要提取器";
        ResumeLayout(false);
        PerformLayout();
    }
    #endregion
}