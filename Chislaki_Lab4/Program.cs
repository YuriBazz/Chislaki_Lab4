using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chislaki_Lab4
{
    
    /*
     * За неимением суперметода по поиску порядка погрешности, придется верить на слово:
     *   H        Прямоугольники       Трапеции
     *   0.1        2                   2
     *   0.05       2                   2
     *   0.025      2                   2
     *   
     *   
     *   На самом деле я включал недоделанную версию программы и ручками все сидел высчитывал
     */
    internal class Program
    {
        public static double[] ArrayH = new double[] { 0.1, 0.05, 0.025 };
        public static double[] ArrayTrapetion = new double[3];
        public static double[] ArraySquare = new double[3];
        public static double[] ArrayRungeSquare = new double[3];
        public static double[] ArrayRungeTrapetion = new double[3];

        private static double EInPow(double x)
        {
            return Math.Pow(Math.E, x * x * x);
        }

        public static void Solve()
        {
            for (int i = 0; i < 3; i++)
            {
                int n = (int)Math.Round(1 / ArrayH[i]);
                ArraySquare[i] = Square(ArrayH[i], n);
                ArrayTrapetion[i] = Trapetion(ArrayH[i], n);
            }
            Runge();
        }

        public static void Runge()
        {
            ArrayRungeSquare[0] = double.NaN; // типизация <3
            ArrayRungeTrapetion[0] = double.NaN; // еще один смешной комментарий
            for (int i = 1; i < 3; i++)
            {
                ArrayRungeSquare[i] = (ArraySquare[i] - ArraySquare[i - 1]) / 3;
                ArrayRungeTrapetion[i] = (ArrayTrapetion[i] - ArrayTrapetion[i - 1]) / 3;
            }
                
        }

        private static double Square(double h, int n)
        {
            
            double temp = 0, I = 0;
            for(int i = 0; i < n; i++)
            {
                I += EInPow((2 * temp + h) / 2);
                temp += h;
            }
            //Console.WriteLine("I = {0} при шаге {1} по составной формуле средних прямоугольников",I * h, h);
            return I * h;
        }

        private static double SquareErrorAbs(double h)
        {
            return Math.E * 15 * (1 / 24.0) * h * h;
        }

        private static double Trapetion(double h, int n)
        {

            double temp = 0, I = (EInPow(0) + EInPow(1)) / 2;
            for (int i = 0; i < n - 1 ; i++)
            {
                temp += h;
                I += EInPow(temp);
            }
            //Console.WriteLine("I = {0} при шаге {1} по составной формуле трапеций", I * h, h);
            return I * h;
        }

        private static double TrapetionErrorAbs(double h)
        {
            return SquareErrorAbs(h) * 2;
        }

        static void Main(string[] args)
        {
            Solve();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("I = {0}, по составной формуле средних прямоугольников, с шагом - {1}; погрешность Рунге - {2}",
                    ArraySquare[i], ArrayH[i], ArrayRungeSquare[i]);
                Console.WriteLine("I = {0}, по составной формуле трапеций, с шагом - {1}; погрешность Рунге - {2}",
                    ArrayTrapetion[i], ArrayH[i], ArrayRungeTrapetion[i]);
                Console.WriteLine();
            }
        }
    }
}
