using System;
using System.Drawing;
using System.Windows.Forms;
using Backend;

namespace Frontend.Windows
{
    public class Form1 : Form
    {
        readonly TextBox pantalla = null!;

        public Form1()
        {
            Text = "Functions Evaluator";
            Size = new Size(520, 420);
            BackColor = Color.Black;
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Arial", 11f);

            // Pantalla de salida
            pantalla = new TextBox
            {
                ReadOnly = true,
                BackColor = Color.Green,
                ForeColor = Color.White,
                Font = new Font("Consolas", 14f, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(450, 50),
                TextAlign = HorizontalAlignment.Right
            };
            Controls.Add(pantalla);

            // Botones: fila 1
            AgregarBoton("7", 20, 90, 70, 60, Color.White, Escribir);
            AgregarBoton("8", 100, 90, 70, 60, Color.White, Escribir);
            AgregarBoton("9", 180, 90, 70, 60, Color.White, Escribir);
            AgregarBoton("(", 260, 90, 70, 60, Color.Orange, Escribir);
            AgregarBoton(")", 340, 90, 70, 60, Color.Orange, Escribir);
            AgregarBoton("Delete", 420, 90, 110, 60, Color.Orange, BorrarUno);

            // Fila 2
            AgregarBoton("4", 20, 160, 70, 60, Color.White, Escribir);
            AgregarBoton("5", 100, 160, 70, 60, Color.White, Escribir);
            AgregarBoton("6", 180, 160, 70, 60, Color.White, Escribir);
            AgregarBoton("*", 260, 160, 70, 60, Color.Orange, Escribir);
            AgregarBoton("/", 340, 160, 70, 60, Color.Orange, Escribir);
            AgregarBoton("Clear", 420, 160, 110, 60, Color.Orange, LimpiarTodo);

            // Fila 3
            AgregarBoton("1", 20, 230, 70, 60, Color.White, Escribir);
            AgregarBoton("2", 100, 230, 70, 60, Color.White, Escribir);
            AgregarBoton("3", 180, 230, 70, 60, Color.White, Escribir);
            AgregarBoton("+", 260, 230, 70, 60, Color.Orange, Escribir);
            AgregarBoton("-", 340, 230, 70, 60, Color.Orange, Escribir);
            AgregarBoton("^", 420, 230, 110, 60, Color.Orange, Escribir);

            // Fila 4
            AgregarBoton("0", 20, 300, 150, 60, Color.White, Escribir);
            AgregarBoton(".", 180, 300, 70, 60, Color.White, Escribir);
            AgregarBoton("=", 260, 300, 270, 60, Color.Orange, Calcular);
        }

        private void AgregarBoton(string texto, int x, int y, int ancho, int alto, Color color, EventHandler accion)
        {
            var btn = new Button
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(ancho, alto),
                BackColor = color,
                ForeColor = Color.Black,
                Font = new Font("Arial", 12f, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btn.Click += accion;
            Controls.Add(btn);
        }

        private void Escribir(object? sender, EventArgs e)
        {
            if (sender is Button btn) pantalla.Text += btn.Text;
        }

        private void BorrarUno(object? sender, EventArgs e)
        {
            if (pantalla.Text.Length > 0)
                pantalla.Text = pantalla.Text[..^1];
        }

        private void LimpiarTodo(object? sender, EventArgs e)
        {
            pantalla.Text = "";
        }

        private void Calcular(object? sender, EventArgs e)
        {
            try
            {
                string expresion = pantalla.Text;
                double resultado = ExpressionEvaluator.Evaluate(expresion);
                pantalla.Text = $"{expresion}={resultado}";
            }
            catch
            {
                pantalla.Text = "Error";
            }
        }
    }
}