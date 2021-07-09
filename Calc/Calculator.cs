using System;
using System.Collections.Generic;

namespace Calc
{
    public class Calculator
    {
        static int EvaluateExpression(char[] exp)
        {
            Stack<int> vStack = new Stack<int>();
            Stack<char> opStack = new Stack<char>();

            opStack.Push('(');

            int pos = 0;
            while (pos <= exp.Length)
            {
                if (pos == exp.Length || exp[pos] == ')')
                {
                    ProcessClosingParenthesis(vStack, opStack);
                    pos++;
                }
                else if (exp[pos] >= '0' && exp[pos] <= '9')
                {
                    pos = ProcessInputNumber(exp, pos, vStack);
                }
                else
                {
                    ProcessInputOperator(exp[pos], vStack, opStack);
                    pos++;
                }

            }

            return vStack.Pop(); 
        }

        static void ProcessClosingParenthesis(Stack<int> vStack, Stack<char> opStack)
        {
            while (opStack.Peek() != '(')
                ExecuteOperation(vStack, opStack);

            opStack.Pop();
        }

        static int ProcessInputNumber(char[] exp, int pos, Stack<int> vStack)
        {
            int value = 0;
            while (pos < exp.Length && exp[pos] >= '0' && exp[pos] <= '9')
                value = checked(10 * value + (int)(exp[pos++] - '0'));
            vStack.Push(value);
            return pos;
        }

        static void ProcessInputOperator(char op, Stack<int> vStack, Stack<char> opStack)
        {
            while (opStack.Count > 0 &&
            OperatorCausesEvaluation(op, opStack.Peek()))
                ExecuteOperation(vStack, opStack);
            opStack.Push(op);
        }

        static bool OperatorCausesEvaluation(char op, char prevOp)
        {
            bool evaluate = false;
            switch (op)
            {
                case '+':
                case '-':
                    evaluate = (prevOp != '(');
                    break;
                case '*':
                case '/':
                    evaluate = (prevOp == '*' || prevOp == '/');
                    break;
                case ')':
                    evaluate = true;
                    break;
            }
            return evaluate;
        }

        static void ExecuteOperation(Stack<int> vStack, Stack<char> opStack)
        {
            int rightOperand = vStack.Pop();
            int leftOperand = vStack.Pop();
            char op = opStack.Pop();
            int result = 0;
            switch (op)
            {
                case '+':
                    result = leftOperand + rightOperand;
                    break;
                case '-':
                    result = leftOperand - rightOperand;
                    break;
                case '*':
                    result = leftOperand * rightOperand;
                    break;
                case '/':
                    result = leftOperand / rightOperand;
                    break;
            }
            vStack.Push(result);
        }

        public static string Calculate(string line)
        {
            char[] exp = line.ToCharArray();
            try
            {
                return Convert.ToString(EvaluateExpression(exp));
            }
            catch (DivideByZeroException)
            {
                return ("Деление на ноль!");
            }
            catch (OverflowException)
            {
                return ("Слишком большое число!");
            }
            catch (Exception)
            {
                return ("Проверьте правильность выражения!");
            }
        }
    }
}