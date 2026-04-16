namespace WorkForcs6;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private TextBox txtUrl;
    private Button btnFetch;
    private GroupBox grpPhones;
    private ListBox lstPhones;
    private GroupBox grpEmails;
    private ListBox lstEmails;
    private Label lblStatus;
    private TextBox txtCookie;   
    private Label lblCookie;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }
    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
        this.txtUrl = new TextBox();
        this.btnFetch = new Button();
        this.grpPhones = new GroupBox();
        this.lstPhones = new ListBox();
        this.grpEmails = new GroupBox();
        this.lstEmails = new ListBox();
        this.lblStatus = new Label();
        this.txtCookie = new TextBox();
        this.lblCookie = new Label();
        this.SuspendLayout();


        // txtUrl
        this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.txtUrl.Location = new System.Drawing.Point(12, 12);
        this.txtUrl.Name = "txtUrl";
        this.txtUrl.Size = new System.Drawing.Size(468, 23);
        this.txtUrl.TabIndex = 0;

        // btnFetch
        this.btnFetch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btnFetch.Location = new System.Drawing.Point(486, 11);
        this.btnFetch.Name = "btnFetch";
        this.btnFetch.Size = new System.Drawing.Size(100, 25);
        this.btnFetch.TabIndex = 1;
        this.btnFetch.Text = "获取并解析";
        this.btnFetch.UseVisualStyleBackColor = true;
        this.btnFetch.Click += new System.EventHandler(this.btnFetch_Click);

        // grpPhones
        this.grpPhones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.grpPhones.Location = new System.Drawing.Point(12, 70);
        this.grpPhones.Name = "grpPhones";
        this.grpPhones.Size = new System.Drawing.Size(574, 150);
        this.grpPhones.TabIndex = 2;
        this.grpPhones.TabStop = false;
        this.grpPhones.Text = "电话号码 (0)";
        this.grpPhones.Controls.Add(this.lstPhones);

        // lstPhones
        this.lstPhones.Dock = System.Windows.Forms.DockStyle.Fill;
        this.lstPhones.FormattingEnabled = true;
        this.lstPhones.ItemHeight = 17;
        this.lstPhones.Location = new System.Drawing.Point(3, 19);
        this.lstPhones.Name = "lstPhones";
        this.lstPhones.Size = new System.Drawing.Size(568, 158);
        this.lstPhones.TabIndex = 0;
        this.lstPhones.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended; // 可多选复制

        // grpEmails
        this.grpEmails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.grpEmails.Location = new System.Drawing.Point(12, 230);
        this.grpEmails.Name = "grpEmails";
        this.grpEmails.Size = new System.Drawing.Size(574, 150);
        this.grpEmails.TabIndex = 3;
        this.grpEmails.TabStop = false;
        this.grpEmails.Text = " 邮箱地址 (0)";
        this.grpEmails.Controls.Add(this.lstEmails);

        // lstEmails
        this.lstEmails.Dock = System.Windows.Forms.DockStyle.Fill;
        this.lstEmails.FormattingEnabled = true;
        this.lstEmails.ItemHeight = 17;
        this.lstEmails.Location = new System.Drawing.Point(3, 19);
        this.lstEmails.Name = "lstEmails";
        this.lstEmails.Size = new System.Drawing.Size(568, 158);
        this.lstEmails.TabIndex = 0;
        this.lstEmails.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;

        // lblStatus
        this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.lblStatus.AutoSize = false;
        this.lblStatus.Location = new System.Drawing.Point(12, 395);
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Size = new System.Drawing.Size(574, 23);
        this.lblStatus.TabIndex = 4;
        this.lblStatus.Text = "就绪";
        this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        // txtCookie
        this.txtCookie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.txtCookie.Location = new System.Drawing.Point(61, 41);
        this.txtCookie.Name = "txtCookie";
        this.txtCookie.Size = new System.Drawing.Size(525, 23);
        this.txtCookie.TabIndex = 5;
        this.txtCookie.Text = "";
        // lblCookie
        this.lblCookie.AutoSize = true;
        this.lblCookie.Location = new System.Drawing.Point(12, 44);
        this.lblCookie.Name = "lblCookie";
        this.lblCookie.Size = new System.Drawing.Size(43, 17);
        this.lblCookie.TabIndex = 6;
        this.lblCookie.Text = "Cookie:";

        // Form1
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(598, 430);
        this.Controls.Add(this.lblCookie);
        this.Controls.Add(this.txtCookie);
        this.Controls.Add(this.lblStatus);
        this.Controls.Add(this.grpEmails);
        this.Controls.Add(this.grpPhones);
        this.Controls.Add(this.btnFetch);
        this.Controls.Add(this.txtUrl);
        this.MinimumSize = new System.Drawing.Size(600, 450);
        this.Name = "Form1";
        this.Text = "网页信息提取工具 - 电话号码 & 邮箱（支持多种格式）";
        this.ResumeLayout(false);
        this.PerformLayout();
    }
    #endregion
}
