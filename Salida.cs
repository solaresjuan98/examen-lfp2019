using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenFinalLFP
{
    class Salida
    {
        private Token.Tipo tipoToken;
        private int parseInt;

        Salida(Token.Tipo tipo, int parseInt)
        {
            this.tipoToken = tipo;
            this.parseInt = parseInt;

        }


        public Token.Tipo GetTipo()
        {
            return tipoToken;
        }

        public int GetParseInt()
        {
            return parseInt;
        }
    }
}
