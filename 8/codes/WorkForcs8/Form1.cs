namespace WorkForcs8;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

public partial class Form1 : Form
{
    // 数据库连接字符串（SQLite文件放在程序运行目录）
    private static readonly string connStr = "Data Source=words.db;Version=3;";

    private List<Word> words;               // 单词列表
    private int currentIndex = 0;           // 当前单词索引
    private int completedCount = 0;         // 已完成单词数
    private int correctCount = 0;           // 正确次数
    private bool isCurrentChecked = false;  // 当前单词是否已作答
    private bool isAllFinished = false;     // 是否已完成全部单词
    private string currentCorrectAnswer;    // 当前正确答案
    public Form1()
    {
        InitializeComponent();
        InitializeDatabase();
        LoadWords();
        ShowCurrentWord();
    }
    // 1. 初始化数据库：不存在则创建表并插入示例数据
    private void InitializeDatabase()
    {
        using (var conn = new SQLiteConnection(connStr))
        {
            conn.Open();

            // 建表（初次创建）
            string createTable = @"
            CREATE TABLE IF NOT EXISTS Words (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                WordCode TEXT,
                English TEXT NOT NULL,
                Chinese TEXT NOT NULL
            )";
            new SQLiteCommand(createTable, conn).ExecuteNonQuery();

            // 如果表已存在但没有 WordCode 列，则动态添加（兼容旧表）
            try
            {
                string addColumn = "ALTER TABLE Words ADD COLUMN WordCode TEXT";
                new SQLiteCommand(addColumn, conn).ExecuteNonQuery();
            }
            catch (SQLiteException) { /* 列已存在，忽略 */ }

            // 插入示例数据（仅当表为空时）
            string check = "SELECT COUNT(*) FROM Words";
            long count = (long)new SQLiteCommand(check, conn).ExecuteScalar();
            if (count == 0)
            {
                string insert = @"
                INSERT INTO Words (WordCode, English, Chinese) VALUES
                ('0001', 'apple', '苹果'),
                ('0002', 'book', '书'),
                ('0003', 'cat', '猫')
            ";
                new SQLiteCommand(insert, conn).ExecuteNonQuery();
            }
        }
    }

    // 2. 从数据库加载所有单词到内存集合
    private void LoadWords()
    {
        words = new List<Word>();
        using (var conn = new SQLiteConnection(connStr))
        {
            conn.Open();
            string sql = "SELECT Id, WordCode, English, Chinese FROM Words";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        words.Add(new Word
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            WordCode = reader["WordCode"].ToString(),
                            English = reader["English"].ToString(),
                            Chinese = reader["Chinese"].ToString()
                        });
                    }
                }
            }
        }
        if (words.Count > 0) currentIndex = 0;
        Shuffle(words);
    }

    // 显示当前单词
    private void ShowCurrentWord()
    {
        if (words.Count == 0)
        {
            txtChinese.Text = "词库为空";
            return;
        }
        // 重置当前单词状态
        currentCorrectAnswer = words[currentIndex].English;
        txtChinese.Text = words[currentIndex].Chinese;
        txtInput.Clear();
        txtResult.Clear();
        HideAnswerArea();
        isCurrentChecked = false;
        txtInput.ReadOnly = false;
        btnConfirm.Enabled = true;
        txtInput.Focus();

        // 更新进度显示
        UpdateProgress();
    }

    // 更新进度标签
    private void UpdateProgress()
    {
        lblProgress.Text = $"已完成：{completedCount}/{words.Count}，" +
                           $"正确率：{(completedCount == 0 ? 0 : correctCount * 100 / completedCount)}%";
    }

    // 检查答案（确认按钮 / 回车）
    private void CheckAnswer()
    {
        if (isCurrentChecked || isAllFinished) return;   // 已答过或全部完成

        string userAnswer = txtInput.Text.Trim();
        bool isCorrect = userAnswer.Equals(currentCorrectAnswer,
                            StringComparison.OrdinalIgnoreCase);

        // 记录结果
        completedCount++;
        if (isCorrect) correctCount++;

        // 显示结果到文本框
        txtResult.Text = isCorrect ? "正确" : "错误";

        // 如果错误，显示加密正确答案框和查看按钮
        if (!isCorrect)
        {
            ShowAnswerArea();
        }
        else
        {
            HideAnswerArea();
        }

        // 禁用输入和确认，等待点击“下一个”
        txtInput.ReadOnly = true;
        btnConfirm.Enabled = false;
        isCurrentChecked = true;
        UpdateProgress();

        // 检查是否全部单词都已答完
        if (completedCount == words.Count)
        {
            isAllFinished = true;
            btnNext.Text = "重新开始";
            MessageBox.Show("所有单词已背完！", "提示",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    // 显示答案区（加密文本框 + 查看按钮）
    private void ShowAnswerArea()
    {
        txtAnswer.Text = currentCorrectAnswer;      // 存储正确答案
        txtAnswer.PasswordChar = '*';               // 加密显示
        txtAnswer.Visible = true;
        btnViewAnswer.Visible = true;
        btnViewAnswer.Text = "查看答案";
    }

    // 隐藏答案区
    private void HideAnswerArea()
    {
        txtAnswer.Visible = false;
        txtAnswer.PasswordChar = '\0';
        btnViewAnswer.Visible = false;
    }

    // 按钮：查看/隐藏正确答案
    private void btnViewAnswer_Click(object sender, EventArgs e)
    {
        if (txtAnswer.PasswordChar == '*')
        {
            txtAnswer.PasswordChar = '\0';      // 显示明文
            btnViewAnswer.Text = "隐藏答案";
        }
        else
        {
            txtAnswer.PasswordChar = '*';       // 重新掩码
            btnViewAnswer.Text = "查看答案";
        }
    }

    // 按钮：下一个单词
    private void btnNext_Click(object sender, EventArgs e)
    {
        // 未作答时不能跳转
        if (!isCurrentChecked && !isAllFinished)
        {
            MessageBox.Show("请先回答当前单词！", "提示",
                           MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // 如果全部完成，点击“重新开始”则重置
        if (isAllFinished)
        {
            ResetRound();
            return;
        }

        // 切换到下一个单词
        if (currentIndex < words.Count - 1)
        {
            currentIndex++;
        }
        else
        {
            currentIndex = 0;   // 应不会发生（全部完成时会提前拦截）
        }
        ShowCurrentWord();
    }

    // 重置一轮
    private void ResetRound()
    {
        Shuffle(words);
        currentIndex = 0;
        completedCount = 0;
        correctCount = 0;
        isAllFinished = false;
        btnNext.Text = "下一个";
        ShowCurrentWord();
    }

    // 打乱列表
    private void Shuffle<T>(IList<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    // 回车键触发确认
    private void txtInput_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            CheckAnswer();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }

    private void btnConfirm_Click(object sender, EventArgs e)
    {
        CheckAnswer();
    }

    // 加载时聚焦输入框
    private void Form1_Load(object sender, EventArgs e)
    {
        txtInput.Focus();
    }


    private void btnImport_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog ofd = new OpenFileDialog())
        {
            ofd.Filter = "CSV 文件 (*.csv)|*.csv|所有文件 (*.*)|*.*";
            ofd.Title = "选择单词 CSV 文件";
            if (ofd.ShowDialog() != DialogResult.OK) return;

            lblImportProgress.Visible = true;
            progressBarImport.Visible = true;
            btnImport.Enabled = false;

            var worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = false
            };
            worker.DoWork += (s, ev) =>
            {
                ev.Result = ImportLargeCsv(ofd.FileName, worker);
            };
            worker.ProgressChanged += (s, ev) =>
            {
                progressBarImport.Value = ev.ProgressPercentage;
                lblImportProgress.Text = $"正在导入... {ev.ProgressPercentage}%";
            };
            worker.RunWorkerCompleted += (s, ev) =>
            {
                btnImport.Enabled = true;
                lblImportProgress.Visible = false;
                progressBarImport.Visible = false;

                if (ev.Error != null)
                {
                    MessageBox.Show($"导入失败：{ev.Error.Message}", "错误",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int count = (int)ev.Result;
                    MessageBox.Show($"成功导入 {count} 个单词！", "导入完成",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadWords();
                    ResetRound();
                }
            };
            worker.RunWorkerAsync();
        }
    }
    private int ImportLargeCsv(string filePath, BackgroundWorker worker)
    {
        int totalCount = 0;
        int totalLines = 0;

        // 快速统计有效行数（跳过表头）
        using (var reader = new StreamReader(filePath, Encoding.UTF8))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true
        }))
        {
            while (csv.Read()) totalLines++;
        }

        if (totalLines == 0) return 0;

        // 正式导入
        using (var reader = new StreamReader(filePath, Encoding.UTF8))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            BadDataFound = null
        }))
        {
            csv.Read();
            csv.ReadHeader();

            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                SQLiteTransaction transaction = conn.BeginTransaction();
                try
                {
                    string sql = "INSERT INTO Words (WordCode, English, Chinese) VALUES (@code, @en, @ch)";
                    using (var cmd = new SQLiteCommand(sql, conn, transaction))
                    {
                        cmd.Parameters.Add("@code", System.Data.DbType.String);
                        cmd.Parameters.Add("@en", System.Data.DbType.String);
                        cmd.Parameters.Add("@ch", System.Data.DbType.String);

                        int batchSize = 5000;
                        while (csv.Read())
                        {
                            string code = csv.GetField(0);
                            string english = csv.GetField(1);
                            string chinese = csv.GetField(2);

                            if (string.IsNullOrWhiteSpace(english) || string.IsNullOrWhiteSpace(chinese))
                                continue;

                            cmd.Parameters["@code"].Value = code;
                            cmd.Parameters["@en"].Value = english;
                            cmd.Parameters["@ch"].Value = chinese;
                            cmd.ExecuteNonQuery();
                            totalCount++;

                            if (totalCount % batchSize == 0)
                            {
                                transaction.Commit();
                                transaction = conn.BeginTransaction();
                                cmd.Transaction = transaction;
                            }

                            if (totalCount % 100 == 0)
                            {
                                int percent = (int)((totalCount / (double)totalLines) * 100);
                                worker.ReportProgress(percent);
                            }
                        }
                    }
                    transaction.Commit();
                    worker.ReportProgress(100);
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }
        return totalCount;
    }

}



