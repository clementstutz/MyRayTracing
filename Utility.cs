using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public static class Utility
    {
        public static double epsilon = 0.0001;
        //public static double epsilon = 0.00001;

        //public const double pi = 3.1415926535897931;

        public const double epsilon_0 = 8.85418782 * 0.00001;

        public static double Infinity = 1e10;

        public static double DegToRad(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        public static bool FE(double a, double b) //FE means Float Equality
        {
            //double temp = Math.Abs(a - b);
            double temp = a - b;
            if (temp < 0.0)
            {
                temp *= -1.0;
            }

            if (temp < epsilon)
            {
                return true;
            }

            return false;
        }

        public static bool IsEven(double a)
        {
            if (a % 2 == 0)
                return true;
            else
                return false;
        }

        public static bool IsStringAnInt(string Nombre)
        {
            try
            {
                int.Parse(Nombre);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsStringADouble(string Nombre)
        {
            try
            {
                double.Parse(Nombre);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
