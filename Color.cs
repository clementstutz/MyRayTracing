using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Color
    {
        public static Color red = new Color(1.0, 0.0, 0.0);
        public static Color green = new Color(0.0, 1.0, 0.0);
        public static Color blue = new Color(0.0, 0.0, 1.0);
        public static Color cyan = new Color(0.0, 1.0, 1.0);
        public static Color magenta = new Color(1.0, 0.0, 1.0);
        public static Color yellow = new Color(1.0, 1.0, 0.0);
        public static Color white = new Color(1.0, 1.0, 1.0);
        public static Color grey = new Color(0.5, 0.5, 0.5);
        public static Color black = new Color(0.0, 0.0, 0.0);

        public double r;
        public double g;
        public double b;

        public Color Randomize()
        {
            new Random();
            this.r = Random.Instance.random.NextDouble();
            this.g = Random.Instance.random.NextDouble();
            this.b = Random.Instance.random.NextDouble();

            return this;
        }

        public Color(Color color)
        {
            this.r = color.r;
            this.g = color.g;
            this.b = color.b;
        }

        public Color(double r = 0.0, double g = 0.0, double b = 0.0)    //couleur par défaut
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public static Color operator +(Color a, Color b)
        {
            Color temp = new Color();
            temp.r = a.r + b.r;
            temp.g = a.g + b.g;
            temp.b = a.b + b.b;
            return temp;
        }

        public static Color operator -(Color a, Color b)
        {
            Color temp = new Color();
            temp.r = a.r - b.r;
            temp.g = a.g - b.g;
            temp.b = a.b - b.b;
            return temp;
        }

        public static Color operator *(Color a, double b)
        {
            Color temp = new Color();
            temp.r = a.r * b;
            temp.g = a.g * b;
            temp.b = a.b * b;
            return temp;
        }

        public static Color operator *(double a, Color b)
        {
            Color temp = new Color();
            temp.r = a * b.r;
            temp.g = a * b.g;
            temp.b = a * b.b;
            return temp;
        }

        public static Color operator *(Color a, Color b) // Hadamard Product.
        {
            Color temp = new Color();
            temp.r = a.r * b.r;
            temp.g = a.g * b.g;
            temp.b = a.b * b.b;
            return temp;
        }

        /*public override bool Equals(object obj)
        {
            return obj is Color color &&
                   r == color.r &&
                   g == color.g &&
                   b == color.b;
        }*/
        public override bool Equals(object obj)
        {
            var color = obj as Color;
            return color != null &&
                   Utility.FE(r, color.r) &&
                   Utility.FE(g, color.g) &&
                   Utility.FE(b, color.b);
        }

        public override string ToString()
        {
            return "(" + this.r.ToString() + "," +
                         this.g.ToString() + "," +
                         this.b.ToString() + ")";

        }
    }
}
