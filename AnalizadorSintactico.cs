using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenFinalLFP
{
    class AnalizadorSintactico
    {

        int indice;
        Token TokenActual = null;
        Boolean ErrorSintactico = false;
        LinkedList<Token> ListaToken = new LinkedList<Token>();
        string ExpresionAritmetica = "";

        /*  GRAMATICA UTILIZADA
         
              Lista -> Declaracion ListaP
                     | Asignacion ListaP
                     | Imprimir ListaP
                     | MostrarDatos ListaP
              
              ListaP -> Declaracion ListaP
                      | Asignacion ListaP
                      | Imprimir ListaP
                      | MostrarDatos ListaP
                      | epsilon
             
            Declaracion -> var id igual expresion;
            Asignacion -> id igual expresion ;
            Imprimir -> print ( expresion ) ;
            MostrarDatos -> datos ( ) ;

                E-> T EP
                EP-> + T EP
                    | - T EP
                    | EPSILON
                    T->F TP
                TP-> * F TP
                    | / F TP
                    | EPSILON
                F->  (E)
                    | NUMERO
                    | id


             */


        public void Parsear(LinkedList<Token> tokens)
        {
            this.ListaToken = tokens;
            indice = 0;
            
            TokenActual = ListaToken.ElementAt(indice);
            //Llamada al no terminal inicial
            Inicio();
            
        }

        private void Inicio()
        {
            //Inicio -> Lista
            Lista();
        }

        private void Lista()
        {
            /*
             * Lista -> Declaracion ListaP
             *        | Asignacion ListaP
             *        | Imprimir ListaP
             *        | MostrarDatos ListaP
             * */

            //ListaP();
            //Console.WriteLine("Lista");
            if(TokenActual.ObtenerTipoToken() == Token.Tipo.PR_VAR)
            {
                Declaracion();
                ListaP();

            }
            else if(TokenActual.ObtenerTipoToken() == Token.Tipo.IDENTIFICADOR)
            {
                Asignacion();
                ListaP();
            }
            else if (TokenActual.ObtenerTipoToken() == Token.Tipo.PR_PRINT)
            {
                Imprimir();
                ListaP();

            }
            else if(TokenActual.ObtenerTipoToken() == Token.Tipo.PR_DATOS)
            {
                MostrarDatos();
                ListaP();
            }




        }

        private void ListaP()
        {
            /*
              * ListaP -> Declaracion ListaP
              *        | Asignacion ListaP
              *        | Imprimir ListaP
              *        | MostrarDatos ListaP
              *        | epsilon
              * */

            //Console.WriteLine("ListaP");
            if(TokenActual.ObtenerTipoToken() == Token.Tipo.PR_VAR)
            {
                //ListaP -> Declaracion ListaP
                Declaracion();
                ListaP();
            }
            else if(TokenActual.ObtenerTipoToken() == Token.Tipo.IDENTIFICADOR)
            {
                //ListaP -> Asignacion ListaP
                Asignacion();
                ListaP();
            }
            else if (TokenActual.ObtenerTipoToken() == Token.Tipo.PR_PRINT)
            {
                //ListaP -> Imprimir ListaP
                Imprimir();
                ListaP();
            }
            else if (TokenActual.ObtenerTipoToken() == Token.Tipo.PR_DATOS)
            {
                //ListaP -> Imprimir ListaP
                MostrarDatos();
                ListaP();
            }
            else
            {
                //ListaP -> epsilon
                //Console.WriteLine(">>>> Analisis terminado");
                //MessageBox.Show("Analisis terminado");
            }
        
           
        }

        private void Declaracion()
        {
            //Declaracion -> var id igual expresion ;
            PR_VAR();
            ID();
            Igual();
            Parea(Token.Tipo.PUNTO_Y_COMA);
            //Console.WriteLine(ExpresionAritmetica);
            ExpresionAritmetica = "";
            //PuntoyComa();

        }

        private void Asignacion()
        {
            //Asignacion -> id igual expresion ;
            ID();
            Igual();
            PuntoyComa();
            //Console.WriteLine(ExpresionAritmetica);
            ExpresionAritmetica = "";
        }

        private void Imprimir()
        {
            //imprimir -> print ( expresion ) ;
            PR_IMPRIMIR();
            Parea(Token.Tipo.PARENTESIS_IZQ);
            Expresion();
            Parea(Token.Tipo.PARENTESIS_DER);
            PuntoyComa();
        }

        private void MostrarDatos()
        {
            //MostrarDatos -> datos ( ) ; 
            PR_MOSTRAR();
            Parea(Token.Tipo.PARENTESIS_IZQ);
            //Console.WriteLine("Juan Antonio Solares - 201800496");
            //Expresion();
            Parea(Token.Tipo.PARENTESIS_DER);
            PuntoyComa();
        }

        private void PR_VAR()
        {
            if(TokenActual.ObtenerTipoToken() == Token.Tipo.PR_VAR)
            {
                Parea(Token.Tipo.PR_VAR);
                //ID();

            }
            else
            {
                Console.WriteLine("Error sintactico: se esperaba PR_VAR en lugar de " + TokenActual.ObtenerTipoToken());
                ErrorSintactico = true;

            }
        }

        private void ID()
        {
            if (TokenActual.ObtenerTipoToken() == Token.Tipo.IDENTIFICADOR)
            {
                //Console.WriteLine("ID correcto");
                Parea(Token.Tipo.IDENTIFICADOR);
            }
            else
            {
                Console.WriteLine("Error sintactico: se esperaba ID en lugar de " + TokenActual.ObtenerTipoToken());
                ErrorSintactico = true;
            }
        }

        private void PR_IMPRIMIR()
        {
            if(TokenActual.ObtenerTipoToken() == Token.Tipo.PR_PRINT)
            {
                Parea(Token.Tipo.PR_PRINT);
            }
            else
            {
                Console.WriteLine("Error sintactico: se esperaba PRINT en lugar de " + TokenActual.ObtenerTipoToken());
                ErrorSintactico = true;
            }
        }

        private void PR_MOSTRAR()
        {
            if (TokenActual.ObtenerTipoToken() == Token.Tipo.PR_DATOS)
            {
                Parea(Token.Tipo.PR_DATOS);
            }
            else
            {
                Console.WriteLine("Error sintactico: se esperaba datos en lugar de " + TokenActual.ObtenerTipoToken());
                ErrorSintactico = true;
            }
        }

        private void Igual()
        {
            if (TokenActual.ObtenerTipoToken() == Token.Tipo.SIGNO_IGUAL)
            {
                Parea(Token.Tipo.SIGNO_IGUAL);
                Expresion();
            }
            else
            {
                Console.WriteLine("Error sintactico: se esperaba = en lugar de " + TokenActual.ObtenerTipoToken());
                ErrorSintactico = true;
            }
        }


        private void Expresion()
        {
            //Expresion -> E
            E();
        }

        private void E()
        {
            //E -> T EP
            T();
            EP();
        }

        private void T()
        {
            // T -> F TP
            F();
            TP();

        }

        private void EP()
        {
            if(TokenActual.ObtenerTipoToken() == Token.Tipo.OPERADOR_SUMA)
            {
                // EP -> + T EP
                ExpresionAritmetica += TokenActual.ObtenerValor();
                Parea(Token.Tipo.OPERADOR_SUMA);
                T();
                EP();

            }
            else if(TokenActual.ObtenerTipoToken() == Token.Tipo.OPERADOR_RESTA)
            {
                // EP -> - T EP
                ExpresionAritmetica += TokenActual.ObtenerValor();
                Parea(Token.Tipo.OPERADOR_RESTA);
                T();
                EP();

            }
            else
            {
                //EPSILON
                //NO HACEMOS NADA 
            }
        }

        private void F()
        {
            if (TokenActual.ObtenerTipoToken() == Token.Tipo.NUMERO_ENTERO)
            {
                //Console.WriteLine("numero: " + TokenActual.ObtenerValor());
                ExpresionAritmetica += TokenActual.ObtenerValor();
                Parea(Token.Tipo.NUMERO_ENTERO);
               
            }
            else if (TokenActual.ObtenerTipoToken() == Token.Tipo.PARENTESIS_IZQ)
            {

                //F -> (E)
                ExpresionAritmetica += TokenActual.ObtenerValor();
                Parea(Token.Tipo.PARENTESIS_IZQ);
                E();
                ExpresionAritmetica += TokenActual.ObtenerValor();
                Parea(Token.Tipo.PARENTESIS_DER);
            }
            else
            {
                ExpresionAritmetica += TokenActual.ObtenerValor();
                Parea(Token.Tipo.IDENTIFICADOR);
               
            }
        }
           

        private void TP()
        {
            if (TokenActual.ObtenerTipoToken() == Token.Tipo.OPERADOR_MULTIPLICACION)
            {
                // TP -> * F TP
                ExpresionAritmetica += TokenActual.ObtenerValor();
                Parea(Token.Tipo.OPERADOR_MULTIPLICACION);
                F();
                TP();

            }
            else if (TokenActual.ObtenerTipoToken() == Token.Tipo.OPERADOR_DIVISION)
            {
                // TP -> / F TP
                ExpresionAritmetica += TokenActual.ObtenerValor();
                Parea(Token.Tipo.OPERADOR_DIVISION);
                F();
                TP();

            }
            else
            {
                //TP -> EPSILON
                //NO HACEMOS NADA 
            }
        }

        private void PuntoyComa()
        {
            if (TokenActual.ObtenerTipoToken() == Token.Tipo.PUNTO_Y_COMA)
            {
                ExpresionAritmetica += "\n";
                Parea(Token.Tipo.PUNTO_Y_COMA);
            }
            else
            {
                Console.WriteLine("Error sintactico: se esperaba PUNTO Y COMA en lugar de " + TokenActual.ObtenerTipoToken());
                ErrorSintactico = true;
            }
        }


       




        public void Parea(Token.Tipo tipo)
        {
            if (ErrorSintactico)
            {
                if(indice < ListaToken.Count() - 1)
                {
                    indice++;
                    TokenActual = ListaToken.ElementAt(indice);
                    if(TokenActual.ObtenerTipoToken() == Token.Tipo.PUNTO_Y_COMA)
                    {
                        ErrorSintactico = false;
                    }
                }
                else
                {
                    Console.WriteLine(":/");
                }
            }
            else
            {
                if(TokenActual.ObtenerTipoToken() == tipo)
                {
                    if (indice < ListaToken.Count() - 1)
                    {
                        indice++;
                        TokenActual = ListaToken.ElementAt(indice);
                    }
                    else
                    {
                        //Console.WriteLine("Error sintatico en [" + TokenActual.ObtenerTipoToken() + " " + TokenActual.ObtenerValor()+"]");
                        ErrorSintactico = true;

                    }
                }
            }
        }
    }
}
