using System;

namespace Practica3
{
    class PruebaJuego
    {
        private JuegoMultiplicar juego;

        static void Main(string[] args)
        {
            Console.Title = "Multiplying Game";
            PruebaJuego p = new PruebaJuego();
            p.juego = new JuegoMultiplicar();
            ConsoleKey ck;

            do
            {
                p.menu();
                ck = Console.ReadKey().Key;
                p.seleccionOpcion(ck);                    
            }
            while (ck != ConsoleKey.Escape);
        }

        #region Menú
        private void menu ()
        {
            Console.Clear();
            //imprimirVerde("\nJuego de multiplicar.\n\n");
            //imprimirVerde("1. ");
            //Console.WriteLine("Establecer tiempo máximo para las respuestas");
            //imprimirVerde("2. ");
            //Console.WriteLine("Establecer el número de preguntas");
            //imprimirVerde("3. ");
            //Console.WriteLine("Jugar");
            //imprimirVerde("Esc. ");
            //Console.WriteLine("Salir\n");
            //imprimirVerde("Opción: ");

            imprimirVerde("\nMultiplying Game.\n\n");
            imprimirVerde("1. ");
            Console.WriteLine("Set maximum answer time");
            imprimirVerde("2. ");
            Console.WriteLine("Set number of questions");
            imprimirVerde("3. ");
            Console.WriteLine("Play");
            imprimirVerde("Esc. ");
            Console.WriteLine("Exit\n");
            imprimirVerde("Option: ");
        }

        private void seleccionOpcion (ConsoleKey ck)
        {
            if (!ck.Equals(ConsoleKey.Escape))
            {
                switch (ck)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        leerTiempoLimite();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        leerNumeroPreguntas();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        jugar();
                        break;
                    default:
                        //imprimirError("\n\nERROR. Opción no válida.\n");
                        imprimirError("\n\nERROR. Non-existent option.\n");
                        esperaCorta();
                        break;
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }
        #endregion

        #region Imprimir
        public static void imprimirVerde (string s)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(s);
            Console.ResetColor();
        }

        private void imprimirAzul(string s)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(s);
            Console.ResetColor();
        }

        public static void imprimirError(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(s);
            Console.ResetColor();
        }
        #endregion

        #region Comprobación
        private bool comprobarByte(String s)
        {
            byte n;
            bool correcto = false;

            try
            {
                n = Convert.ToByte(s);
                correcto = true;
            }
            catch (FormatException)
            {
                //imprimirError("\nERROR. Valor no numérico.\n");
                imprimirError("\nERROR. Not a number.\n");
            }
            catch (OverflowException)
            {
                //imprimirError("\nERROR. Valor demasiado extenso o negativo.\n");
                imprimirError("\nERROR. Negative value or out of range.\n");
            }

            return correcto;
        }
        #endregion

        #region Esperas
        public static void esperaLarga()
        {
            System.Threading.Thread.Sleep(2000);
        }

        public static void esperaCorta()
        {
            System.Threading.Thread.Sleep(800);
        }
        #endregion

        #region Métodos
        private void leerTiempoLimite()
        {
            bool tiempoCorrecto = false;

            while (!tiempoCorrecto || juego.Tpo == 0)
            {
                //imprimirAzul("\n\nIntroduzca el tiempo límite (3-10s): ");
                imprimirAzul("\n\nSet time limit (3-10s): ");

                string s = Console.ReadLine();

                tiempoCorrecto = comprobarByte(s);

                if (tiempoCorrecto)
                    juego.Tpo = Convert.ToByte(s);
            }
        }

        private void leerNumeroPreguntas()
        {
            bool nPregCorrecto = false;

            while (!nPregCorrecto || juego.NPreg == 0)
            {
                //imprimirAzul("\n\nIntroduzca el nº de preguntas (1-10): ");
                imprimirAzul("\n\nSet number of questions (1-10): ");


                string s = Console.ReadLine();

                nPregCorrecto = comprobarByte(s);

                if (nPregCorrecto)
                    juego.NPreg = Convert.ToByte(s);
            }
        }

        private void jugar()
        {
            if (juego.Tpo == 0 || juego.NPreg == 0)
            {
                //imprimirError("\n\nERROR. Primero debe establecer un tiempo límite y un número de preguntas.");
                imprimirError("\n\nERROR. Settings not stablished.");
                esperaLarga();
            }
            else
            {
                Console.Clear();

                while (juego.NPreg > 0)
                {
                    string s = "";
                    byte respuesta = 0;

                    juego.generarOperandos();
                    Console.Write("\n" + juego.Op1 + " * " + juego.Op2 + " = ");
                    
                    s = juego.responderIntervalo();

                    if (s.Equals("-1"))
                    {
                        juego.haFallado(-1);
                    }
                    else if (!s.Equals("") && comprobarByte(s))
                    {
                        respuesta = Convert.ToByte(s);
                        juego.comprobarResultado(respuesta);
                    }   
                    else
                    {
                        juego.haFallado(0);
                    }                    
                }

                juego.mostrarResultado();

                //Console.WriteLine("\nPulse cualquier tecla para continuar.");
                Console.WriteLine("\nPress any key to continue.");
                Console.ReadKey();

                juego.reiniciarJuego();
            }
        }
        #endregion

        #region Idioma
        private void idioma (String s)
        {
            if (s.Equals("en"))
            {

            }
            else if (s.Equals("es"))
            {
            }
        }
        #endregion
    }
}
