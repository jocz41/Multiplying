using System;
using System.Text;

namespace Practica3
{
    class JuegoMultiplicar
    {
        private byte op1, op2, res, aciertos, fallos, nPreg, tpo;

        #region Constructores
        public JuegoMultiplicar()
        {
            Op1 = 0;
            Op2 = 0;
            Res = 0;
            nPreg = 0;
            Aciertos = 0;
            Fallos = 0;
            tpo = 0;
        }

        public JuegoMultiplicar (byte tiempo)
        {
            Tpo = tiempo;
            Aciertos = 0;
            Fallos = 0;
        }
        #endregion

        #region Propiedades
        public byte Op1
        {
            get => op1;
            set
            {
                if (value > 0 && value < 10)
                    op1 = value;
            }
        }

        public byte Op2
        {
            get => op2;
            set
            {
                if (value > 0 && value < 10)
                    op2 = value;
            }
        }

        public byte Res
        {
            get => res;
            set => res = value;
        }

        public byte Aciertos
        {
            get => aciertos;
            set
            {
                if (value >= 0)
                    aciertos = value;
            }
        }

        public byte Fallos
        {
            get => fallos;
            set
            {
                if (value >= 0)
                    fallos = value;
            }
        }

        public byte NPreg
        {
            get => nPreg;
            set
            {
                if (value > 0 && value < 11)
                    nPreg = value;
                else
                {
                    //PruebaJuego.imprimirError("\nERROR. Nº de preguntas no válido.\n");
                    PruebaJuego.imprimirError("\nERROR. Number of questions out of range.\n");
                    PruebaJuego.esperaCorta();
                }
            }
        }

        public byte Tpo
        {
            get => tpo;
            set
            {
                if (value > 2 && value < 11)
                    tpo = value;
                else
                {
                    //PruebaJuego.imprimirError("\nERROR. Tiempo no válido.\n");
                    PruebaJuego.imprimirError("\nERROR. Time out of range.\n");
                    PruebaJuego.esperaCorta();
                }
            }
        }
        #endregion

        #region Métodos
        public bool comprobarResultado (byte n)
        {
            bool acierto = (n == Res);

            if (acierto)
            {
                haAcertado();
            }
            else
            {
                haFallado(0);
            }

            return acierto;
        }

        public void generarOperandos ()
        {
            Random rnd = new Random();
            int n1, n2;

            n1 = rnd.Next(1, 10);
            Op1 = Convert.ToByte(n1);
            n2 = rnd.Next(1,10);
            Op2 = Convert.ToByte(n2);
            Res = Convert.ToByte(n1 * n2);

            nPreg = Convert.ToByte(NPreg - 1);
        }

        public void reiniciarJuego()
        {
            Aciertos = 0;
            Fallos = 0;
        }

        public string responderIntervalo()
        {
            ConsoleKeyInfo ck = default(ConsoleKeyInfo);
            DateTime inicio = DateTime.Now;
            TimeSpan diferencia;
            StringBuilder s = new StringBuilder();

            do
            {
                diferencia = DateTime.Now - inicio;

                if (Console.KeyAvailable)
                {
                    ck = Console.ReadKey(true);

                    if (ck.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                    else if (ck.Key == ConsoleKey.Backspace && s.ToString().Length > 0)
                    {
                        s.Remove(s.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                    else
                    {
                        s.Append(ck.KeyChar.ToString());
                        Console.Write(ck.KeyChar.ToString());
                    }
                }
            }while (diferencia.Seconds < Tpo && ck.Key != ConsoleKey.Enter);

            if (diferencia.Seconds >= Tpo)
            {
                s = new StringBuilder("-1");
            }

            return s.ToString();
        }

        public void haAcertado()
        {
            //PruebaJuego.imprimirVerde("\nMuy bien! Respuesta correcta.\n");
            PruebaJuego.imprimirVerde("\nVery good! Correct answer.\n");
            Aciertos = Convert.ToByte(Aciertos + 1);
        }
        
        public void haFallado(int n)
        {
            if (n == -1)
            {
                //PruebaJuego.imprimirError("\nSe agotó el tiempo. La respuesta era " + Res + "\n");
                PruebaJuego.imprimirError("\nTime is up. The answer was " + Res + "\n");
                Fallos = Convert.ToByte(Fallos + 1);
            }
            else
            {
                //PruebaJuego.imprimirError("\nRespuesta incorrecta. ");
                PruebaJuego.imprimirError("\nWrong answer. ");
                //Console.Write("La respuesta era: ");
                Console.Write("The answer was: ");
                PruebaJuego.imprimirError("" + Res + "\n");
                Fallos = Convert.ToByte(Fallos + 1);
            }
        }

        public void mostrarResultado()
        {
            //Console.WriteLine("\n------- Resultado -------");
            //PruebaJuego.imprimirVerde("  Aciertos: ");
            //Console.Write(Aciertos);
            //PruebaJuego.imprimirError(" Fallos: ");
            //Console.Write(Fallos + "\n");

            Console.WriteLine("\n------- Results -------");
            PruebaJuego.imprimirVerde("  Right: ");
            Console.Write(Aciertos);
            PruebaJuego.imprimirError(" Wrong: ");
            Console.Write(Fallos + "\n");
        }
        #endregion
    }
}
