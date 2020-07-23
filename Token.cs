using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenFinalLFP
{
    class Token
    {

        public enum Tipo
        {
            PR_VAR,
            NUMERO_ENTERO,
            SIGNO_IGUAL,
            OPERADOR_MULTIPLICACION,
            OPERADOR_SUMA,
            OPERADOR_RESTA,
            OPERADOR_DIVISION,
            PARENTESIS_IZQ,
            PARENTESIS_DER,
            LLAVE_IZQ,
            LLAVE_DER,
            CORCHETE_IZQ,
            CORCHETE_DER,
            DOS_PUNTOS,
            COMA,
            PUNTO_Y_COMA,
            SIMBOLO_MENOR,
            SIMBOLO_MAYOR,
            PUNTO,
            GUION_BAJO,
            IDENTIFICADOR,
            PR_DATOS,
            PR_PRINT

        }


        private Tipo TipoToken;
        private String Valor;

        public Token(Tipo tipo, String valor)
        {
            this.TipoToken = tipo;
            this.Valor = valor;
        }


        public String ObtenerValor()
        {
            return Valor;
        }

        public Tipo ObtenerTipoToken()
        {
            return this.TipoToken;

        }
    }
}
