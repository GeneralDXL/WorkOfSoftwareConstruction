namespace WorkForcs5;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        // 可以在这里进行一些初始化操作
    }
    private void AppendToExpression(string s)
    {
        textExpression.AppendText(s);
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        textExpression.Clear();
        textResult.Clear();
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
        if (textExpression.Text.Length > 0)
        {
            textExpression.Text = textExpression.Text.Substring(0, textExpression.Text.Length - 1);
        }
    }

    private bool AreParenthesesBalanced(string expression)
    {
        int balance = 0;
        foreach (char ch in expression)
        {
            if (ch == '(') balance++;
            else if (ch == ')') balance--;
            if (balance < 0) return false;
        }
        return balance == 0;
    }

    private void btnEqual_Click(object sender, EventArgs e)
    {
        string exp = textExpression.Text;
        if (!AreParenthesesBalanced(exp))
        {
            MessageBox.Show("括号不匹配，请检查表达式。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        try
        {
            Caculation caculation = new Caculation(exp);
            caculation.Caculate();
            string result = caculation.GetResult();
            textResult.Text = result;
        }
        catch (Exception ex)
        {
            MessageBox.Show("计算过程中发生错误: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void Button_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        if (btn != null)
        {
            string textToAppend = btn.Tag?.ToString() ?? btn.Text;
            if (textToAppend == "sin" || textToAppend == "cos" || textToAppend == "tan" || textToAppend == "arcsin" || textToAppend == "arccos" || textToAppend == "arctan" || textToAppend == "ln")
            {
                AppendToExpression(textToAppend + "(");
            }
            else
                AppendToExpression(btn.Text);
        }
    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void textExpression_TextChanged(object sender, EventArgs e)
    {

    }
}
