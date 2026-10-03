using System;
using System.Windows.Forms;
using Backend;

namespace Frontend.Windows;

public partial class Form1 : Form
{
    // ✅ AQUÍ VA EXACTAMENTE ESTA LÍNEA 👇
    private TextBox pantalla = null!;

    public Form1()
    {
        InitializeComponent();
        CrearInterfaz();
    }

    private void CrearInterfaz()
    {
        Text = "Calculadora Evaluadora de Funciones";
        Size = new System.Drawing.Size(420, 580);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;

        // ✅ Aquí la asignamos
        pantalla = new TextBox
        {
            Dock = DockStyle.Top,
            Font = new System.Drawing.Font("Arial", 26),
            TextAlign = HorizontalAlignment.Right,
            ReadOnly = true
        };
        Controls.Add(pantalla);

        Panel panel = new Panel { Dock = DockStyle.Fill };
        Controls.Add(panel);

        string[] botones = {
            "7","8","9","/",
            "4","5","6","*",
            "1","2","3","-",
            ".","0","=","+",
            "C","(",")"
        };

        int fila = 0, col = 0;
        foreach (string t in botones)
        {
            Button btn = new Button
            {
                Text = t,
                Font = new System.Drawing.Font("Arial", 20),
                Size = new System.Drawing.Size(85, 85),
                Location = new System.Drawing.Point(col * 95 + 10, fila * 95 + 10)
            };
            btn.Click += Btn_Click;
            panel.Controls.Add(btn);
            col++;
            if (col > 3) { col = 0; fila++; }
        }
    }

    private void Btn_Click(object? sender, EventArgs e)
    {
        Button btn = (Button)sender!;
        string valor = btn.Text;

        // ✅ Aquí la usamos → desaparece el aviso
        if (valor == "C")
        {
            pantalla.Text = "";
        }
        else if (valor == "=")
        {
            try
            {
                double res = ExpressionEvaluator.Evaluate(pantalla.Text);
                pantalla.Text = res.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Expresión inválida",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                pantalla.Text = "";
            }
        }
        else
        {
            pantalla.Text += valor;
        }
    }
}