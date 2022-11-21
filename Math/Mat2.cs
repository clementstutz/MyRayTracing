using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Mat2
    {
        double[,] mat;
        int size = 2;

        public int Size
        {
            get { return size; }
        }

        public Mat2(Mat2 a)
        {
            mat = new double[2, 2];
            mat[0, 0] = a[0, 0];
            mat[0, 1] = a[0, 1];

            mat[1, 0] = a[1, 0];
            mat[1, 1] = a[1, 1];
        }

        public Mat2(double m00 = 1.0, double m01 = 0.0,
                    double m10 = 0.0, double m11 = 1.0)
        {
            mat = new double[2, 2];

            mat[0, 0] = m00;
            mat[0, 1] = m01;

            mat[1, 0] = m10;
            mat[1, 1] = m11;
        }

        public double this[int l, int r]
        {
            get { return mat[l, r]; }
            set { mat[l, r] = value; }
        }

        public static bool operator ==(Mat2 a, Mat2 b)
        {
            for (int l = 0; l < 2; l++)
            {
                for (int r = 0; r < 2; r++)
                {
                    if (!Utility.FE(a[l, r], b[l, r]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool operator !=(Mat2 a, Mat2 b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return String.Format("|{0,6:0.00}", mat[0, 0]) + "," +
                   String.Format("{0,6:0.00}|", mat[0, 1]) + "\n" +
                   String.Format("|{0,6:0.00}", mat[1, 0]) + "," +
                   String.Format("{0,6:0.00}|", mat[1, 1]);
        }

        public static Mat2 operator *(Mat2 a, Mat2 b)
        {
            Mat2 temp = new Mat2();

            for (int l = 0; l < 2; l++)
            {
                for (int r = 0; r < 2; r++)
                {
                    temp[l, r] = a[l, 0] * b[0, r] + a[l, 1] * b[1, r];
                }
            }
            return temp;
        }

        public static Mat2 operator *(Mat2 a, double b)
        {
            Mat2 temp = new Mat2(a);

            for (int l = 0; l < 2; l++)
            {
                for (int r = 0; r < 2; r++)
                {
                    temp[l, r] *= b;
                }
            }
            return temp;
        }

        public Mat2 Transpose()
        {
            Mat2 temp = new Mat2(this);

            for (int l = 0; l < 2; l++)
            {
                for (int r = 0; r < 2; r++)
                {
                    this[l, r] = temp[r, l];
                }
            }
            return this;
        }

        public double Det()
        {
            return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
        }

        public Mat2 Inverse()
        {
            Mat2 inverse = new Mat2(this);

            double det = this.Det();
            //Console.WriteLine(det);

            if (!Utility.FE(det, 0.0))
            {
                inverse[0, 0] = this[1, 1];
                inverse[1, 1] = this[0, 0];
                inverse[0, 1] = -inverse[0, 1];
                inverse[1, 0] = -inverse[1, 0];
                inverse = inverse * (1.0 / det);
            }
            return inverse;
        }

        public Mat2 Inverse_10_percent_faster()
        {
            Mat2 inverse = new Mat2(this);

            double det = this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
            //Console.WriteLine(det);

            if (!Utility.FE(det, 0.0))
            {
                inverse[0, 0] = this[1, 1];
                inverse[1, 1] = this[0, 0];
                inverse[0, 1] = -inverse[0, 1];
                inverse[1, 0] = -inverse[1, 0];
                inverse = inverse * (1.0 / det);
            }
            return inverse;
        }

        public static Mat2 TranslateMatrix(double x, double y)
        {
            Mat2 temp = new Mat2();
            temp[0, 1] = x;
            temp[1, 1] = y;
            return temp;
        }

        public static Mat2 ScaleMatrix(double x, double y)
        {
            Mat2 temp = new Mat2();
            temp[0, 0] = x;
            temp[1, 1] = y;
            return temp;
        }

        public static Mat2 RotateMatrix(double x)
        {
            Mat2 temp = new Mat2();
            temp[0, 0] = (double)Math.Cos(x);
            temp[0, 1] = (double)Math.Sin(x) * (-1.0);
            temp[1, 0] = (double)Math.Sin(x);
            temp[1, 1] = (double)Math.Cos(x);
            return temp;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Mat2 other = obj as Mat2;

            if (Utility.FE(this.mat[0, 0], other.mat[0, 0]) &&
                Utility.FE(this.mat[0, 1], other.mat[0, 1]) &&
                Utility.FE(this.mat[1, 0], other.mat[1, 0]) &&
                Utility.FE(this.mat[1, 1], other.mat[1, 1]))
                return true;

            return false;
        }
    }
}
