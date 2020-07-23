using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenFinalLFP
{
    class AnalizadorLexico
    {
        private LinkedList<Token> Salida;

        int NumeroErrores = 0;

        private string AuxLexico;

        private int Estado;

        

        public LinkedList<Token> Escanear(string Entrada)
        {
            Entrada += "#";
            Salida = new LinkedList<Token>();
            char c;


            for (int i = 0; i < Entrada.Length; i++)
            {
                c = Entrada.ElementAt(i);
                switch (Estado)
                {
                    case 0:
                        if (Char.IsLetter(c))
                        {
                            AuxLexico += c;
                            Estado = 1;

                        }
                        else if (Char.IsDigit(c))
                        {
                            AuxLexico += c;
                            Estado = 2;
                        }
                        else if (c.Equals('"'))
                        {

                            AuxLexico = "";
                            Estado = 3;
                        }
                        else if (Char.IsSymbol(c))
                        {
                            Estado = 3;
                            AuxLexico += c;
                        }

                        else if (c.Equals('!'))
                        {
                            AuxLexico += c;
                        }
                        else if (c.CompareTo(' ') == 0)
                        {

                            AuxLexico += "";
                            Estado = 0;
                        }

                        else if (c.CompareTo('+') == 0)
                        {

                            AuxLexico += c;
                            AgregarToken(Token.Tipo.OPERADOR_SUMA);
                        }
                        else if (c.Equals('='))
                        {
                            AuxLexico += c;
                            //AgregarToken(Token.Tipo.SIMBOLO_IGUAL);
                        }
                        else if (c.CompareTo('-') == 0)
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.OPERADOR_RESTA);
                        }
                        else if (c.CompareTo('–') == 0)
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.OPERADOR_RESTA);
                        }
                        else if (c.CompareTo('_') == 0)
                        {
                            AuxLexico += c;

                            AgregarToken(Token.Tipo.GUION_BAJO);
                        }
                        else if (c.CompareTo('+') == 0)
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.OPERADOR_SUMA);
                        }

                        else if (c.CompareTo('/') == 0)
                        {
                         
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.OPERADOR_DIVISION);
                        }
                        else if (c.CompareTo('*') == 0)
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.OPERADOR_MULTIPLICACION);
                        }
                        else if (c.CompareTo('{') == 0)
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.LLAVE_IZQ);
                        }
                        else if (c.CompareTo('}') == 0)
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.LLAVE_DER);
                        }
                        else if (c.CompareTo(':') == 0)
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.DOS_PUNTOS);
                        }
                        else if (c.CompareTo(';') == 0)
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.PUNTO_Y_COMA);
                        }
                        else if (c.CompareTo('\n') == 0)
                        {
                            AuxLexico += c;
                            AuxLexico = "";
                        }
                        else if (c.CompareTo('\t') == 0)
                        {
                            AuxLexico += c;
                            AuxLexico = "";
                            AgregarToken(Token.Tipo.DOS_PUNTOS);
                        }
                        else if (c.CompareTo(',') == 0)
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.COMA);

                        }
                        else if (c.Equals(':'))
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.DOS_PUNTOS);

                        }
                        else if (c.CompareTo('(') == 0)
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.PARENTESIS_IZQ);
                        }
                        else if (c.CompareTo(')') == 0)
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.PARENTESIS_DER);

                        }
                        else if (c.CompareTo('.') == 0)
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.PUNTO);
                        }
                        else if (c.CompareTo('[') == 0)
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.CORCHETE_IZQ);
                        }
                        else if (c.CompareTo(']') == 0)
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.CORCHETE_DER);
                        }
                        else if (c.CompareTo('<') == 0)
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.SIMBOLO_MENOR);
                        }
                        else if (c.CompareTo('>') == 0)
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.SIMBOLO_MAYOR);
                        }
                        else if (c.CompareTo(':') == 0)
                        {
                            AuxLexico += c;
                            AgregarToken(Token.Tipo.DOS_PUNTOS);
                        }
                        else
                        {
                            if (c.CompareTo('#') == 0 && i == Entrada.Length - 1)
                            {
                                //Finaliza el analisis lexico.
                                //MessageBox.Show("El Analisis lexico ha terminado");
                                
                            }
                            else
                            {
                                Console.WriteLine("Error lexico con: " + c);
                                AgregarError(c.ToString());
                                NumeroErrores++;
                                Estado = 0;
                            }
                        }
                        break;
                    case 1:
                        if (Char.IsLetter(c))
                        {
                            AuxLexico += c;
                        } else if (Char.IsDigit(c))
                        {
                            AuxLexico += c;
                        }
                        else if (c.Equals('_'))
                        {
                            AuxLexico += c;

                        }
                        else if (Char.IsDigit(c))
                        {
                            AuxLexico += c;

                        }
                        else
                        {
                            //AgregarToken(Token.Tipo.IDENTIFICADOR);
                            Token.Tipo verificar = ClasificarToken();
                            i -= 1;
                            Estado = 0;
                        }
                        break;
                    case 2:
                        if (Char.IsDigit(c))
                        {
                            AuxLexico += c;

                        }
                        else
                        {
                            AgregarToken(Token.Tipo.NUMERO_ENTERO);
                            i -= 1;
                            Estado = 0;
                        }
                        break;
                    case 3:
                        if (Char.IsSymbol(c))
                        {
                            AuxLexico += c;
                        }
                        else
                        {
                            Token.Tipo verificar = ClasificarToken();
                            i -= 1;
                            Estado = 0;
                        }
                        break;

                }
            }
            return Salida;
        }

        public Token.Tipo ClasificarToken()
        {
            switch (AuxLexico)
            {
                case "var":
                    AgregarToken(Token.Tipo.PR_VAR);
                    return Token.Tipo.PR_VAR;

                case "datos":
                    AgregarToken(Token.Tipo.PR_DATOS);
                    return Token.Tipo.PR_DATOS;

                case "print":
                    AgregarToken(Token.Tipo.PR_PRINT);
                    return Token.Tipo.PR_PRINT;

                case "=":
                    AgregarToken(Token.Tipo.SIGNO_IGUAL);
                    return Token.Tipo.SIGNO_IGUAL;

                case "-":
                    AgregarToken(Token.Tipo.OPERADOR_RESTA);
                    return Token.Tipo.OPERADOR_RESTA;

                case "+":
                    AgregarToken(Token.Tipo.OPERADOR_SUMA);
                    return Token.Tipo.OPERADOR_SUMA;

                case "/":
                    AgregarToken(Token.Tipo.OPERADOR_DIVISION);
                    return Token.Tipo.OPERADOR_DIVISION;

                case "*":
                    AgregarToken(Token.Tipo.OPERADOR_MULTIPLICACION);
                    return Token.Tipo.OPERADOR_MULTIPLICACION;

                case "(":
                    AgregarToken(Token.Tipo.PARENTESIS_IZQ);
                    return Token.Tipo.PARENTESIS_IZQ;

                case ")":
                    AgregarToken(Token.Tipo.PARENTESIS_DER);
                    return Token.Tipo.PARENTESIS_DER;

                case ";":
                    AgregarToken(Token.Tipo.PUNTO_Y_COMA);
                    return Token.Tipo.PUNTO_Y_COMA;

                default:
                    AgregarToken(Token.Tipo.IDENTIFICADOR);
                    return Token.Tipo.IDENTIFICADOR;
            }
        }


        public void AgregarToken(Token.Tipo tipo)
        {
            Salida.AddLast(new Token(tipo, AuxLexico));
            AuxLexico = "";

        }

        public void AgregarError(string error)
        {

        }

        public void ImprimirTokens(LinkedList<Token> lista)
        {
            string contenido = "";

            contenido += "Lista de Tokens reconocidos\n";

            foreach (Token token in lista)
            {
                contenido += "Lexema: " + token.ObtenerValor() + " Tipo: " + token.ObtenerTipoToken() + "\n";
            }

            contenido += "---------------------------------";

            Console.WriteLine(contenido);

        }

        public void Flujoaplicacion()
        {
            if(NumeroErrores == 0)
            {
                //ImprimirTokens(Salida);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
    }
}
