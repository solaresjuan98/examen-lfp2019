using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenFinalLFP
{
    class Interprete
    {
        int indice;
        Token TokenActual = null;
        Boolean ErrorSintactico = false;
        LinkedList<Token> ListaToken = new LinkedList<Token>();
        LinkedList<Simbolo> TablaSimbolo = new LinkedList<Simbolo>();


        public Interprete(LinkedList<Token> tokens)
        {
            this.ListaToken = tokens;
            indice = 0;
            TokenActual = ListaToken.ElementAt(indice);
            //Llamada al no terminal inicial
            Inicio(true);


        }



        private void Inicio(Boolean bandera)
        {
            //Inicio -> Lista
            Lista(bandera);
        }

        private void Lista(Boolean bandera)
        {
            /*
             * Lista -> Declaracion ListaP
             *        | Asignacion ListaP
             *        | Imprimir ListaP
             *        | MostrarDatos ListaP
             * */

            //ListaP();
            //Console.WriteLine("Lista");
            if (TokenActual.ObtenerTipoToken() == Token.Tipo.PR_VAR)
            {
                Declaracion(bandera);
                ListaP(bandera);

            }
            else if (TokenActual.ObtenerTipoToken() == Token.Tipo.IDENTIFICADOR)
            {
                Asignacion(bandera);
                ListaP(bandera);
            }
            else if (TokenActual.ObtenerTipoToken() == Token.Tipo.PR_PRINT)
            {
                Imprimir();
                ListaP(bandera);

            }
            else if (TokenActual.ObtenerTipoToken() == Token.Tipo.PR_DATOS)
            {
                MostrarDatos();
                ListaP(bandera);
            }




        }

        private void ListaP(Boolean bandera)
        {
            /*
              * ListaP -> Declaracion ListaP
              *        | Asignacion ListaP
              *        | Imprimir ListaP
              *        | MostrarDatos ListaP
              *        | epsilon
              * */

            //Console.WriteLine("ListaP");
            if (TokenActual.ObtenerTipoToken() == Token.Tipo.PR_VAR)
            {
                //ListaP -> Declaracion ListaP
                Declaracion(true);
                ListaP(bandera);
            }
            else if (TokenActual.ObtenerTipoToken() == Token.Tipo.IDENTIFICADOR)
            {
                //ListaP -> Asignacion ListaP
                Asignacion(true);
                ListaP(bandera);
            }
            else if (TokenActual.ObtenerTipoToken() == Token.Tipo.PR_PRINT)
            {
                //ListaP -> Imprimir ListaP
                Imprimir();
                ListaP(bandera);
            }
            else if (TokenActual.ObtenerTipoToken() == Token.Tipo.PR_DATOS)
            {
                //ListaP -> Imprimir ListaP
                MostrarDatos();
                ListaP(bandera);
            }
            else
            {
                //ListaP -> epsilon
                //Console.WriteLine(">>>> Analisis terminado\n");
                Console.WriteLine("\n");

                foreach (Simbolo simbolo in TablaSimbolo)
                {
                    //Console.WriteLine(">> " + simbolo.GetTipo() + " " + simbolo.GetNombre() + "--" + simbolo.GetValor());
                }
            }


        }

        private void Declaracion(Boolean bandera)
        {
            //Declaracion -> var id igual expresion ;
            string var = PR_VAR();
            //Console.WriteLine(">> " + var);
            ArrayList listaNombre = new ArrayList();
            ID(listaNombre);
           

            Resultado valor = Igual(var);
            //Console.WriteLine(">>" + valor.GetTipo()+" "+valor.GetVal());
            Parea(Token.Tipo.PUNTO_Y_COMA);


            if (bandera)
            {
                //Console.WriteLine("Tabla de simbolos");

                foreach (Object id in listaNombre)
                {
                    //Console.WriteLine(">>" + Convert.ToString(id));
                    Simbolo simbolo = new Simbolo(var, Convert.ToString(id), valor);
                    //Simbolo simbolo = new Simbolo(valor.GetTipo(), Convert.ToString(id) , valor.GetVal());
                    
                    //Console.WriteLine(">>" + simbolo.GetTipo() + " " + simbolo.GetNombre() + " " + simbolo.GetValorString());
                    TablaSimbolo.AddLast(simbolo);
                }
            }

        }

        //asigna (modificar el valor que estaba anteriormente)
        private void Asignacion(Boolean bandera)
        {
            //Asignacion -> id igual expresion ;
            string var = "var"; //de por sí ya es de tipo 'var'
            ArrayList listaNombre = new ArrayList();
            ID(listaNombre);
            Resultado valor = Igual(var);
            PuntoyComa();
            //Console.WriteLine(ExpresionAritmetica);
            //ExpresionAritmetica = "";
            

            if (bandera)
            {
                //Console.WriteLine("Tabla de simbolos");

                foreach (Object id in listaNombre)
                {
                    //int indice = TablaSimbolo.ElementAt(sim);
                    //Console.WriteLine(">>" + Convert.ToString(id));
                    Simbolo simbolo = new Simbolo(var, Convert.ToString(id), valor);
                    //Simbolo simbolo = new Simbolo(valor.GetTipo(), Convert.ToString(id) , valor.GetVal());

                    //Console.WriteLine(">>" + simbolo.GetTipo() + " " + simbolo.GetNombre() + " " + simbolo.GetValorString());
                    TablaSimbolo.AddFirst(simbolo);
                    //TablaSimbolo.ElementAt(int.Parse(simbolo.ToString()));
                }
            }

        }

        private void Imprimir()
        {
            //imprimir -> print ( expresion ) ;
            PR_IMPRIMIR();
            Parea(Token.Tipo.PARENTESIS_IZQ);
            //Expresion();
            if(TokenActual.ObtenerTipoToken() == Token.Tipo.IDENTIFICADOR)
            { 
                ID_();

            } else if(TokenActual.ObtenerTipoToken() == Token.Tipo.PARENTESIS_IZQ || TokenActual.ObtenerTipoToken() == Token.Tipo.NUMERO_ENTERO)
            {
                Expresion();
            }
            //Resultado resultado = nam
            //ObtenerValorSimbolo(resultado.GetVal());
            Parea(Token.Tipo.PARENTESIS_DER);
            PuntoyComa();
        }

        private void ID_()
        {
            if(TokenActual.ObtenerTipoToken() == Token.Tipo.IDENTIFICADOR)
            {
                ObtenerValorSimbolo(TokenActual.ObtenerValor());//Modificar el valor que viene
                Parea(Token.Tipo.IDENTIFICADOR);
            }
        }

        public Resultado ObtenerValorSimbolo(string nombre)
        {

            foreach (Simbolo simbolo in TablaSimbolo)
            {
                if (simbolo.GetNombre().Equals(nombre))
                {
                    //se imprime al ejecutar la sentencia print();
                    Console.WriteLine(">> " + simbolo.GetValorString());
                    //Console.WriteLine(TablaSimbolo.ElementAt(indice));
                    return simbolo.GetValor();
                }

            }

            return new Resultado("Error", "");
        }


        //ESTE METODO SOLO BUSCA EL VALOR QUE TIENE LA VARIABLE EN LA TABLA DE SIMBOLOS
        public Resultado BuscarValorSimbolo(string nombre)
        {
            foreach (Simbolo simbolo in TablaSimbolo)
            {
                if (simbolo.GetNombre().Equals(nombre))
                {
                    //se imprime al ejecutar la sentencia print();
                    //Console.WriteLine(">> " + simbolo.GetValorString());
                    return simbolo.GetValor();
                }

            }

            return new Resultado("Error", "");
        }

        private void MostrarDatos()
        {
            //MostrarDatos -> datos ( ) ; 
            PR_MOSTRAR();
            Parea(Token.Tipo.PARENTESIS_IZQ);
            Console.WriteLine("Juan Antonio Solares - 201800496");
            //Expresion();
            Parea(Token.Tipo.PARENTESIS_DER);
            PuntoyComa();
        }

        private string PR_VAR()
        {
            if (TokenActual.ObtenerTipoToken() == Token.Tipo.PR_VAR)
            {
                Parea(Token.Tipo.PR_VAR);
                //ID();
                return "var";
            }
            else
            {
                Console.WriteLine("Error sintactico: se esperaba PR_VAR en lugar de " + TokenActual.ObtenerTipoToken());
                ErrorSintactico = true;
                return "";
            }
        }

        private void ID(ArrayList listaNombre)
        {
            if (TokenActual.ObtenerTipoToken() == Token.Tipo.IDENTIFICADOR)
            {
                //Console.WriteLine("ID correcto");
                listaNombre.Add(TokenActual.ObtenerValor());
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
            if (TokenActual.ObtenerTipoToken() == Token.Tipo.PR_PRINT)
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

        private Resultado Igual(string tipo)
        {
            if (TokenActual.ObtenerTipoToken() == Token.Tipo.SIGNO_IGUAL)
            {
                Parea(Token.Tipo.SIGNO_IGUAL);
                return Expresion();
            }
            else
            {
                Console.WriteLine("Error sintactico: se esperaba = en lugar de " + TokenActual.ObtenerTipoToken());
                ErrorSintactico = true;
                return new Resultado("var", "0");
            }
        }


        private Resultado Expresion()
        {
            //Expresion -> E
            return E();
        }

        private Resultado E()
        {
            //E -> T EP

            Resultado operando1 = T();
            Resultado resultado = EP(operando1);

            //Console.WriteLine("El valor operado es: " + resultado.GetVal());
            ObtenerValorSimbolo(resultado.GetVal());
            return resultado;
        }

        private Resultado T()
        {
            // T -> F TP
            Resultado operando1 = F();
            Resultado resultado = TP(operando1);
            //TP();

            return resultado;
        }

        private Resultado EP(Resultado operando1)
        {
            if (TokenActual.ObtenerTipoToken() == Token.Tipo.OPERADOR_SUMA)
            {
                // EP -> + T EP
                //ExpresionAritmetica += TokenActual.ObtenerValor();
                Parea(Token.Tipo.OPERADOR_SUMA);
                Resultado operando2 = T();
                Resultado resultado = new Resultado("Error", "-");
                //T();
                //EP();
                switch (operando1.GetTipo())
                {
                    case "var":
                        switch (operando2.GetTipo())
                        {
                            case "var":
                                int valor1 = int.Parse(operando1.GetVal());
                                int valor2 = int.Parse(operando2.GetVal());
                                resultado = new Resultado("var", Convert.ToString(valor1 + valor2));
                                break;
                        }
                        break;
                }

                return EP(resultado);

            }
            else if (TokenActual.ObtenerTipoToken() == Token.Tipo.OPERADOR_RESTA)
            {
                // EP -> - T EP
                //ExpresionAritmetica += TokenActual.ObtenerValor();
                Parea(Token.Tipo.OPERADOR_RESTA);
                //T();
                //EP();
                Resultado operando2 = T();
                Resultado resultado = new Resultado("Error", "-");

                switch (operando1.GetTipo())
                {
                    case "var":
                        switch (operando2.GetTipo())
                        {
                            case "var":
                                int valor1 = int.Parse(operando1.GetVal());
                                int valor2 = int.Parse(operando2.GetVal());
                                resultado = new Resultado("var", Convert.ToString(valor1-valor2));
                                break;
                        }
                        break;
                }
                return EP(resultado);
            }
            else
            {
                return operando1;
                //EPSILON
                //NO HACEMOS NADA 
            }
        }

        private Resultado F()
        {
            if (TokenActual.ObtenerTipoToken() == Token.Tipo.NUMERO_ENTERO)
            {
                //Console.WriteLine("Viene un numero entero de valor" +TokenActual.ObtenerValor());
                Resultado temp = new Resultado("var", TokenActual.ObtenerValor());
                //Console.WriteLine("numero: " + TokenActual.ObtenerValor());
                //ExpresionAritmetica += TokenActual.ObtenerValor();
                Parea(Token.Tipo.NUMERO_ENTERO);

                return temp;

            }
            else if (TokenActual.ObtenerTipoToken() == Token.Tipo.PARENTESIS_IZQ)
            {

                //F -> (E)
                //ExpresionAritmetica += TokenActual.ObtenerValor();
                Parea(Token.Tipo.PARENTESIS_IZQ);
                //Resultado par_izq = F();
                Resultado temp = E();
                //ExpresionAritmetica += TokenActual.ObtenerValor();
                Parea(Token.Tipo.PARENTESIS_DER);
                //Resultado par_der = F();
                return temp;
            }

            else
            {
                Resultado valorid = BuscarValorSimbolo(TokenActual.ObtenerValor());
                //Resultado valorid = new Resultado("var", TokenActual.ObtenerValor());
                //ExpresionAritmetica += TokenActual.ObtenerValor();
                Parea(Token.Tipo.IDENTIFICADOR);
                return valorid;
            }
        }
        
        

        private Resultado TP(Resultado operando1)
        {
            if (TokenActual.ObtenerTipoToken() == Token.Tipo.OPERADOR_MULTIPLICACION)
            {
                // TP -> * F TP
                //ExpresionAritmetica += TokenActual.ObtenerValor();
                Parea(Token.Tipo.OPERADOR_MULTIPLICACION);
                Resultado operando2 = F();
                Resultado resultado = new Resultado("Error", "-");
                //F();
                //TP();
                switch (operando1.GetTipo())
                {
                    case "var":
                        switch (operando2.GetTipo())
                        {
                            case "var":
                                int valor1 = int.Parse(operando1.GetVal());
                                int valor2 = int.Parse(operando2.GetVal());
                                resultado = new Resultado("var", Convert.ToString(valor1 * valor2));
                                break;
                        }
                        break;
                }

                return EP(resultado);

            }
            else if (TokenActual.ObtenerTipoToken() == Token.Tipo.OPERADOR_DIVISION)
            {
                // TP -> / F TP
                //ExpresionAritmetica += TokenActual.ObtenerValor();
                Parea(Token.Tipo.OPERADOR_DIVISION);
                Resultado operando2 = F();
                Resultado resultado = new Resultado("Error", "-");
                //F();
                //TP();
                switch (operando1.GetTipo())
                {
                    case "var":
                        switch (operando2.GetTipo())
                        {
                            case "var":
                                int valor1 = int.Parse(operando1.GetVal());
                                int valor2 = int.Parse(operando2.GetVal());
                                resultado = new Resultado("var", Convert.ToString(valor1 / valor2));
                                break;
                        }
                        break;
                }

                return EP(resultado);

            }
            else
            {
                return operando1;
                //TP -> EPSILON
                //NO HACEMOS NADA 
            }
        }

        private void PuntoyComa()
        {
            if (TokenActual.ObtenerTipoToken() == Token.Tipo.PUNTO_Y_COMA)
            {
                //ExpresionAritmetica += "\n";
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
                if (indice < ListaToken.Count() - 1)
                {
                    indice++;
                    TokenActual = ListaToken.ElementAt(indice);
                    if (TokenActual.ObtenerTipoToken() == Token.Tipo.PUNTO_Y_COMA)
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
                if (TokenActual.ObtenerTipoToken() == tipo)
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
