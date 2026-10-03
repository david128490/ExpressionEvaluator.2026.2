using System;
using System.Windows.Forms;

namespace Frontend.Windows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                string expresion = txtExpresion.Text;
                
                // Llamada a la lógica del Backend
                double resultado = Backend.EvaluadorExpresiones.Evaluar(expresion);
                
                lblResultado.Text = $"Resultado: {resultado}";
                lblResultado.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblResultado.Text = $"Error: {ex.Message}";
                lblResultado.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtExpresion.Clear();
            lblResultado.Text = "Resultado: —";
            lblResultado.ForeColor = System.Drawing.Color.Black;
        }
    }
}