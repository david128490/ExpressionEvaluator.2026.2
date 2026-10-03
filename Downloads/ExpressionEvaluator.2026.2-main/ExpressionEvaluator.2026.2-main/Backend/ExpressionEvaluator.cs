using System;
using System.Collections.Generic;

namespace Backend;

public static class ExpressionEvaluator
{
    public static double Evaluate(string infix)
    {
        string postfix = ToPostfix(infix);
        return EvaluatePostfix(postfix);
    }

    private static string ToPostfix(string infix)
    {
        string postfix = string.Empty;
        Stack<char> stack = new Stack<char>();
        int i = 0;

        while (i < infix.Length)
        {
            char item = infix[i];

            // ✅ Si es dígito o punto decimal → acumular TODO el número
            if (char.IsDigit(item) || item == '.')
            {
                string numero = string.Empty;
                while (i < infix.Length && (char.IsDigit(infix[i]) || infix[i] == '.'))
                {
                    numero += infix[i];
                    i++;
                }
                // Separar números con espacio para que se reconozcan bien
                postfix += numero + " ";
            }
            else if (IsOperator(item))
            {
                if (item == '(')
                {
                    stack.Push(item);
                    i++;
                }
                else if (item == ')')
                {
                    while (stack.Peek() != '(')
                    {
                        postfix += stack.Pop();
                    }
                    stack.Pop(); // Quitar el '('
                    i++;
                }
                else
                {
                    while (stack.Count > 0 && Precedencia(stack.Peek()) >= Precedencia(item))
                    {
                        postfix += stack.Pop();
                    }
                    stack.Push(item);
                    i++;
                }
            }
            else if (char.IsWhiteSpace(item))
            {
                i++; // Saltar espacios
            }
            else
            {
                throw new FormatException($"Carácter no válido: {item}");
            }
        }

        while (stack.Count > 0)
        {
            postfix += stack.Pop();
        }

        return postfix;
    }

    private static double EvaluatePostfix(string postfix)
    {
        Stack<double> stack = new Stack<double>();
        int i = 0;

        while (i < postfix.Length)
        {
            if (char.IsDigit(postfix[i]) || postfix[i] == '.')
            {
                string numero = string.Empty;
                while (i < postfix.Length && (char.IsDigit(postfix[i]) || postfix[i] == '.'))
                {
                    numero += postfix[i];
                    i++;
                }
                // Validar que no tenga más de un punto decimal
                if (numero.Split('.').Length > 2)
                    throw new FormatException($"Número inválido: {numero}");

                stack.Push(double.Parse(numero, System.Globalization.CultureInfo.InvariantCulture));
            }
            else if (IsOperator(postfix[i]))
            {
                double b = stack.Pop();
                double a = stack.Pop();
                char op = postfix[i];

                double res = op switch
{
    '+' => a + b,
    '-' => a - b,
    '*' => a * b,
    '/' => a / b,
    '^' => Math.Pow(a, b),
    _ => throw new InvalidOperationException()
};
                stack.Push(res);
                i++;
            }
            else
            {
                i++; // Saltar espacios
            }
        }
        return stack.Pop();
    }

    private static bool IsOperator(char c)
    {
        return c == '+' || c == '-' || c == '*' || c == '/' || c == '^' || c == '(' || c == ')';
    }

    private static int Precedencia(char op)
{
    if (op == '^') return 3;
    if (op == '*' || op == '/') return 2;
    if (op == '+' || op == '-') return 1;
    return 0;
}
}