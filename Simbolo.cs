using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenFinalLFP
{
    class Simbolo
    {
        private string Tipo;
        private string Nombre;
        private Resultado Valor;
        

        public Simbolo(string tipo, string nombre, Resultado valor)
        {
            this.Tipo = tipo;
            this.Nombre = nombre;
            this.Valor = valor;
        }

        public string GetTipo()
        {
            return Tipo;
        }

        public string GetNombre()
        {
            return Nombre;
        }

        public Resultado GetValor()
        {
            return Valor;
        }

        public String GetValorString()
        {
            return Valor.GetVal();
        }

    }
}
