
namespace WorkForcs5;


using System;
using System.Collections.Generic;
using System.Text;

public class Caculation
{
    private const double CONST_PI = Math.PI;
    private const double CONST_E = Math.E;

    private string expression;
    private string result;

    public Caculation(string e)
    {
        expression = e;
    }

    private int Precedence(char op)
    {
        switch (op)
        {
            case '+':
            case '-':
                return 1;
            case '*':
            case '/':
                return 2;
            case '^':
                return 3;
            default:
                return 0;
        }
    }

    private double Function(string func, double num)
    {
        switch (func)
        {
            case "sin": return Math.Sin(num);
            case "cos": return Math.Cos(num);
            case "tan": return Math.Tan(num);
            case "arcsin": return Math.Asin(num);
            case "arccos": return Math.Acos(num);
            case "arctan": return Math.Atan(num);
            case "ln": return Math.Log(num);
            default: return -1;
        }
    }

    private double BasicCaculate(double l, double r, char op)
    {
        switch (op)
        {
            case '+': return l + r;
            case '-': return l - r;
            case '*': return l * r;
            case '/': return l / r;
            case '^': return Math.Pow(l, r);
            default: return -1;
        }
    }

    private string DoubleToString(double x)
    {
        return x.ToString(System.Globalization.CultureInfo.InvariantCulture);
    }

    public void Caculate()
    {
        // 将表达式中的 π 替换为 'P'，其余字符直接转换
        StringBuilder expBuilder = new StringBuilder();
        foreach (char ch in expression)
        {
            if (ch == 'π')
                expBuilder.Append('P');
            else
                expBuilder.Append(ch);
        }
        string exp = expBuilder.ToString();

        Stack<char> ops = new Stack<char>();
        Stack<string> funcs = new Stack<string>();
        Stack<double> nums = new Stack<double>();

        int index = 0;
        while (index < exp.Length)
        {
            char current = exp[index];

            if (current == '(')
            {
                ops.Push('(');
                index++;
            }
            else if (char.IsDigit(current))
            {
                string numStr = "";
                while (index < exp.Length && (char.IsDigit(exp[index]) || exp[index] == '.'))
                {
                    numStr += exp[index];
                    index++;
                }
                double num = double.Parse(numStr, System.Globalization.CultureInfo.InvariantCulture);
                nums.Push(num);
            }
            else if (current == 'e')
            {
                nums.Push(CONST_E);
                index++;
            }
            else if (current == 'P')
            {
                nums.Push(CONST_PI);
                index++;
            }
            else if (current == '+' || current == '-' || current == '*' || current == '/' || current == '^')
            {
                if (ops.Count == 0 || ops.Peek() == '(' || Precedence(current) > Precedence(ops.Peek()))
                {
                    ops.Push(current);
                    index++;
                }
                else if (Precedence(current) <= Precedence(ops.Peek()))
                {
                    double r = nums.Pop();
                    double l = nums.Pop();
                    char op = ops.Pop();
                    nums.Push(BasicCaculate(l, r, op));
                    ops.Push(current);
                    index++;
                }
            }
            else if (current == ')')
            {
                while (ops.Peek() != '(')
                {
                    double r = nums.Pop();
                    double l = nums.Pop();
                    char op = ops.Pop();
                    nums.Push(BasicCaculate(l, r, op));
                }
                ops.Pop(); // 弹出 '('
                index++;

                if (funcs.Count > 0 && ops.Count > 0 && ops.Peek() == 'f')
                {
                    double numX = nums.Pop();
                    string func = funcs.Pop();
                    nums.Push(Function(func, numX));
                    ops.Pop(); // 弹出 'f'
                }
            }
            else if (current == 's' || current == 'c' || current == 't' || current == 'l' || current == 'a')
            {
                string func = "";
                while (index < exp.Length && exp[index] != '(')
                {
                    func += exp[index];
                    index++;
                }
                funcs.Push(func);
                ops.Push('f');
                // 注意：此时 index 指向 '('，外层循环继续时会处理 '('
            }
            else
            {
                // 忽略未知字符（可选：抛出异常）
                index++;
            }
        }

        while (ops.Count > 0)
        {
            double r = nums.Pop();
            double l = nums.Pop();
            char op = ops.Pop();
            nums.Push(BasicCaculate(l, r, op));
        }

        result = DoubleToString(nums.Pop());
    }

    public string GetResult()
    {
        return result;
    }
}
