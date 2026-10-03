using System;
using System.Text.RegularExpressions;

namespace Backend
{
    public static class EvaluadorExpresiones
    {
        public static double Evaluar(string expresion)
        {
            if (string.IsNullOrWhiteSpace(expresion))
                throw new ArgumentException("La expresión está vacía");

            // Eliminar espacios
            expresion = expresion.Replace(" ", "");

            // Validar caracteres permitidos
            if (!Regex.IsMatch(expresion, @"^[0-9+\-*/.()]+$"))
                throw new ArgumentException("La expresión contiene caracteres no permitidos");

            try
            {
                // Evaluación simple usando DataTable (alternativa segura para expresiones matemáticas)
                System.Data.DataTable tabla = new System.Data.DataTable();
                var valor = tabla.Compute(expresion, "");
                return Convert.ToDouble(valor);
            }
            catch
            {
                throw new ArgumentException("La expresión no es válida o tiene errores de sintaxis");
            }
        }
    }
}