namespace WorkForcs8
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private TextBox txtInput;
        private Button btnConfirm;
        private TextBox txtResult;
        private TextBox txtAnswer;
        private Button btnViewAnswer;
        private Button btnNext;
        private Label lblProgress;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblProgress = new Label();
            txtInput = new TextBox();
            btnConfirm = new Button();
            txtResult = new TextBox();
            txtAnswer = new TextBox();
            btnViewAnswer = new Button();
            btnNext = new Button();
            lblAnswer = new Label();
            lblResult = new Label();
            lblInput = new Label();
            btnImport = new Button();
            progressBarImport = new ProgressBar();
            lblImportProgress = new Label();
            txtChinese = new TextBox();
            lblChinese = new Label();
            SuspendLayout();
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(12, 9);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(229, 24);
            lblProgress.TabIndex = 0;
            lblProgress.Text = "已完成：0/5，正确率：0%";
            // 
            // txtInput
            // 
            txtInput.Location = new Point(241, 157);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(349, 30);
            txtInput.TabIndex = 2;
            txtInput.KeyDown += txtInput_KeyDown;
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(239, 320);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(106, 47);
            btnConfirm.TabIndex = 3;
            btnConfirm.Text = "确认";
            btnConfirm.Click += btnConfirm_Click;
            // 
            // txtResult
            // 
            txtResult.Location = new Point(241, 195);
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.Size = new Size(349, 30);
            txtResult.TabIndex = 4;
            txtResult.TabStop = false;
            // 
            // txtAnswer
            // 
            txtAnswer.Location = new Point(239, 231);
            txtAnswer.Name = "txtAnswer";
            txtAnswer.ReadOnly = true;
            txtAnswer.Size = new Size(349, 30);
            txtAnswer.TabIndex = 5;
            txtAnswer.TabStop = false;
            txtAnswer.Visible = false;
            // 
            // btnViewAnswer
            // 
            btnViewAnswer.Location = new Point(486, 320);
            btnViewAnswer.Name = "btnViewAnswer";
            btnViewAnswer.Size = new Size(102, 47);
            btnViewAnswer.TabIndex = 6;
            btnViewAnswer.Text = "查看答案";
            btnViewAnswer.Visible = false;
            btnViewAnswer.Click += btnViewAnswer_Click;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(352, 320);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(128, 47);
            btnNext.TabIndex = 7;
            btnNext.Text = "下一个";
            btnNext.Click += btnNext_Click;
            // 
            // lblAnswer
            // 
            lblAnswer.AutoSize = true;
            lblAnswer.Location = new Point(137, 234);
            lblAnswer.Name = "lblAnswer";
            lblAnswer.Size = new Size(82, 24);
            lblAnswer.TabIndex = 8;
            lblAnswer.Text = "正确答案";
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Location = new Point(155, 195);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(46, 24);
            lblResult.TabIndex = 9;
            lblResult.Text = "结果";
            // 
            // lblInput
            // 
            lblInput.AutoSize = true;
            lblInput.Location = new Point(155, 157);
            lblInput.Name = "lblInput";
            lblInput.Size = new Size(46, 24);
            lblInput.TabIndex = 10;
            lblInput.Text = "输入";
            // 
            // btnImport
            // 
            btnImport.Location = new Point(723, 21);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(112, 34);
            btnImport.TabIndex = 11;
            btnImport.Text = "导入词库";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // progressBarImport
            // 
            progressBarImport.Location = new Point(37, 420);
            progressBarImport.Name = "progressBarImport";
            progressBarImport.Size = new Size(784, 34);
            progressBarImport.TabIndex = 12;
            progressBarImport.Visible = false;
            // 
            // lblImportProgress
            // 
            lblImportProgress.AutoSize = true;
            lblImportProgress.Location = new Point(42, 383);
            lblImportProgress.Name = "lblImportProgress";
            lblImportProgress.Size = new Size(0, 24);
            lblImportProgress.TabIndex = 13;
            lblImportProgress.Visible = false;
            // 
            // txtChinese
            // 
            txtChinese.Location = new Point(239, 57);
            txtChinese.Multiline = true;
            txtChinese.Name = "txtChinese";
            txtChinese.ReadOnly = true;
            txtChinese.ScrollBars = ScrollBars.Vertical;
            txtChinese.Size = new Size(347, 45);
            txtChinese.TabIndex = 14;
            // 
            // lblChinese
            // 
            lblChinese.AutoSize = true;
            lblChinese.Location = new Point(154, 60);
            lblChinese.Name = "lblChinese";
            lblChinese.Size = new Size(46, 24);
            lblChinese.TabIndex = 15;
            lblChinese.Text = "中文";
            // 
            // Form1
            // 
            ClientSize = new Size(848, 510);
            Controls.Add(lblChinese);
            Controls.Add(txtChinese);
            Controls.Add(lblImportProgress);
            Controls.Add(progressBarImport);
            Controls.Add(btnImport);
            Controls.Add(lblInput);
            Controls.Add(lblResult);
            Controls.Add(lblAnswer);
            Controls.Add(lblProgress);
            Controls.Add(txtInput);
            Controls.Add(btnConfirm);
            Controls.Add(txtResult);
            Controls.Add(txtAnswer);
            Controls.Add(btnViewAnswer);
            Controls.Add(btnNext);
            Name = "Form1";
            Text = "背单词";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();



        }

        #endregion

        private Label lblAnswer;
        private Label lblResult;
        private Label lblInput;
        private Button btnImport;
        private ProgressBar progressBarImport;
        private Label lblImportProgress;
        private TextBox txtChinese;
        private Label lblChinese;
    }
}
