using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenFinalLFP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string entrada;
        string lista;

        private void Button1_Click(object sender, EventArgs e)
        {
            entrada = txtEntrada.Text;
            //lista = txtTokens.Text;

            AnalizadorLexico AnalisisLexico = new AnalizadorLexico();
            LinkedList<Token> ListaTokens = AnalisisLexico.Escanear(entrada);
            //Console.WriteLine("Analisis lexico iniciado");
            AnalisisLexico.Flujoaplicacion();

            AnalizadorSintactico parser = new AnalizadorSintactico();
            parser.Parsear(ListaTokens);

            Interprete interprete = new Interprete(ListaTokens);

        }

        private void BtnBorrar_Click(object sender, EventArgs e)
        {
            txtEntrada.Text = "";
        }
    }
}
