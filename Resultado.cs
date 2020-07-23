using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenFinalLFP
{
    class Resultado
    {
        private string Tipo;
        private string Valor;

        public Resultado(String tipo, String valor)
        {
            this.Tipo = tipo;
            this.Valor = valor;
        }

        public string GetTipo()
        {
            return Tipo;
        }

        public string GetVal()
        {
            return Valor;
        }
    }
}
