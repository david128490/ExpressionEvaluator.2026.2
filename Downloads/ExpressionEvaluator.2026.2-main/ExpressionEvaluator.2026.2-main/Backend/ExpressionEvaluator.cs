using System;
using System.Text.RegularExpressions;

namespace Backend
{
    public static class ExpressionEvaluator
    {
        public static double Evaluate(string expresion)
        {
            if (string.IsNullOrWhiteSpace(expresion))
                throw new ArgumentException("La expresión está vacía");

            expresion = expresion.Replace(" ", "");

            if (!Regex.IsMatch(expresion, @"^[0-9+\-*/.()^]+$"))
                throw new ArgumentException("Caracteres no permitidos");

            try
            {
                // Paso 1: Resolver primero TODO lo que está entre paréntesis
                expresion = ResolverParentesis(expresion);

                // Paso 2: Resolver potencias ^
                expresion = ResolverPotencias(expresion);

                // Paso 3: Calcular el resultado final
                using var tabla = new System.Data.DataTable();
                return Convert.ToDouble(tabla.Compute(expresion, ""));
            }
            catch
            {
                throw new ArgumentException("Expresión inválida o con errores de sintaxis");
            }
        }

        // Resuelve todo lo que está dentro de paréntesis, de adentro hacia afuera
        private static string ResolverParentesis(string expr)
        {
            while (expr.Contains("("))
            {
                // Buscar el paréntesis más interno
                var coincidencia = Regex.Match(expr, @"\(([^()]+)\)");
                if (!coincidencia.Success) break;

                string contenido = coincidencia.Groups[1].Value;
                double resultado = EvaluarSimple(contenido);

                // Reemplazar (contenido) por el resultado
                expr = expr.Replace(coincidencia.Value, resultado.ToString());
            }
            return expr;
        }

        // Resuelve potencias a^b
        private static string ResolverPotencias(string expr)
        {
            while (expr.Contains("^"))
            {
                // Buscar número^número
                var coincidencia = Regex.Match(expr, @"(-?\d+\.?\d*)\^(-?\d+\.?\d*)");
                if (!coincidencia.Success) break;

                double b = double.Parse(coincidencia.Groups[1].Value);
                double e = double.Parse(coincidencia.Groups[2].Value);
                double res = Math.Pow(b, e);

                expr = expr.Replace(coincidencia.Value, res.ToString());
            }
            return expr;
        }

        // Evalúa una expresión SIN paréntesis ni potencias
        private static double EvaluarSimple(string expr)
        {
            // Primero potencias dentro del contenido
            expr = ResolverPotencias(expr);

            using var tabla = new System.Data.DataTable();
            return Convert.ToDouble(tabla.Compute(expr, ""));
        }
    }
}