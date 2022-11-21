using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RT
{
    public class Mat3
    {
        double[,] mat;
        int size = 3;

        public int Size
        {
            get { return size; }
        }

        public Mat3(Mat3 a)
        {
            mat = new double[3, 3];
            mat[0, 0] = a[0, 0];
            mat[0, 1] = a[0, 1];
            mat[0, 2] = a[0, 2];

            mat[1, 0] = a[1, 0];
            mat[1, 1] = a[1, 1];
            mat[1, 2] = a[1, 2];

            mat[2, 0] = a[2, 0];
            mat[2, 1] = a[2, 1];
            mat[2, 2] = a[2, 2];
        }

        public Mat3(double m00 = 1.0, double m01 = 0.0, double m02 = 0.0,
                    double m10 = 0.0, double m11 = 1.0, double m12 = 0.0,
                    double m20 = 0.0, double m21 = 0.0, double m22 = 1.0)
        {
            mat = new double[3, 3];
            mat[0, 0] = m00;
            mat[0, 1] = m01;
            mat[0, 2] = m02;

            mat[1, 0] = m10;
            mat[1, 1] = m11;
            mat[1, 2] = m12;

            mat[2, 0] = m20;
            mat[2, 1] = m21;
            mat[2, 2] = m22;
        }

        public double this[int l, int r]
        {
            get { return mat[l, r]; }
            set { mat[l, r] = value; }
        }

        public static bool operator ==(Mat3 a, Mat3 b)
        {
            for (int l = 0; l < 3; l++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (!Utility.FE(a[l, y], b[l, y]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool operator !=(Mat3 a, Mat3 b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return String.Format("|{0,6:0.00}", mat[0, 0]) + "," +
                   String.Format("{0,6:0.00}", mat[0, 1]) + "," +
                   String.Format("{0,6:0.00}|", mat[0, 2]) + "\n" +

                   String.Format("|{0,6:0.00}", mat[1, 0]) + "," +
                   String.Format("{0,6:0.00}", mat[1, 1]) + "," +
                   String.Format("{0,6:0.00}|", mat[1, 2]) + "\n" +

                   String.Format("|{0,6:0.00}", mat[2, 0]) + "," +
                   String.Format("{0,6:0.00}", mat[2, 1]) + "," +
                   String.Format("{0,6:0.00}|", mat[2, 2]);
        }

        public static Mat3 operator *(Mat3 a, Mat3 b)
        {
            Mat3 temp = new Mat3();

            for (int l = 0; l < 3; l++)
            {
                for (int r = 0; r < 3; r++)
                {
                    temp[l, r] = a[l, 0] * b[0, r] +
                                 a[l, 1] * b[1, r] +
                                 a[l, 2] * b[2, r];
                }
            }
            return temp;
        }

        public static Mat3 operator *(Mat3 a, double b)
        {
            Mat3 temp = new Mat3(a);

            for (int l = 0; l < 3; l++)
            {
                for (int r = 0; r < 3; r++)
                {
                    temp[l, r] *= b;
                }
            }
            return temp;
        }

        

        public Mat2 Sub(int x, int y)
        {
            Mat2 sub = new Mat2();

            int i = 0;

            for (int ll = 0; ll < 3; ll++)
            {
                int j = 0;
                if (ll == x)
                    continue;

                for (int rr = 0; rr < 3; rr++)
                {
                    if (rr == y)
                    {
                        continue;
                    }

                    sub[i, j] = this.mat[ll, rr];
                    j++;
                }
                i++;
            }
            return sub;
        }

        public double Minor(int l, int r)
        {
            Mat2 sub = this.Sub(l, r);
            return sub.Det();
        }

        public double Cofactor(int l, int r)
        {
            double temp = this.Minor(l, r);

            if (((l + r) % 2) != 0)
            {
                temp = -temp;
            }

            return temp;
        }

        public double Det()
        {
            return this[0, 0] * (this[1, 1] * this[2, 2] - this[1, 2] * this[2, 1]) -
                   this[1, 0] * (this[0, 1] * this[2, 2] - this[0, 2] * this[2, 1]) +
                   this[2, 0] * (this[0, 1] * this[1, 2] - this[0, 2] * this[1, 1]);
        }

        public Mat3 Inverse()
        {
            Mat3 inverse = new Mat3();

            double det = Det();
            //Console.WriteLine(det);

            if (!Utility.FE(det, 0.0))
            {
                for (int l = 0; l < 3; l++)
                {
                    for (int r = 0; r < 3; r++)
                    {
                        inverse[l, r] = Cofactor(l, r);
                    }
                }
                inverse.Transpose();
                inverse = inverse * (1.0 / det);
            }

            return inverse;
        }

        public Mat3 Inverse_10_percent_faster()
        {
            Mat3 inverse = new Mat3();

            double det = this[0, 0] * (this[1, 1] * this[2, 2] - this[1, 2] * this[2, 1]) -
                         this[1, 0] * (this[0, 1] * this[2, 2] - this[0, 2] * this[2, 1]) +
                         this[2, 0] * (this[0, 1] * this[1, 2] - this[0, 2] * this[1, 1]);
            //Console.WriteLine(det);

            if (!Utility.FE(det, 0.0))
            {
                for (int l = 0; l < 3; l++)
                {
                    for (int r = 0; r < 3; r++)
                    {
                        Mat2 sub = new Mat2();
                        int i = 0;
                        for (int ll = 0; ll < 3; ll++)
                        {
                            int j = 0;
                            if (ll == l)
                                continue;

                            for (int rr = 0; rr < 3; rr++)
                            {
                                if (rr == r)
                                {
                                    continue;
                                }

                                sub[i, j] = this.mat[ll, rr];
                                j++;
                            }
                            i++;
                        }

                        double minor = sub[0, 0] * sub[1, 1] - sub[0, 1] * sub[1, 0];

                        if (((l + r) % 2) != 0)
                        {
                            minor = -minor;
                        }
                        inverse[l, r] = minor;
                    }
                }

                Mat3 transpose = new Mat3(inverse);
                for (int l = 0; l < 3; l++)
                {
                    for (int r = 0; r < 3; r++)
                    {
                        inverse[l, r] = transpose[r, l];
                    }
                }
                inverse = inverse * (1.0 / det);
            }
            return inverse;
        }





        public Mat3 TransposeMatrix()   // return a new matrix
        {
            Mat3 temp = new Mat3(this);

            for (int l = 0; l < 3; l++)
            {
                for (int r = 0; r < 3; r++)
                {
                    temp[l, r] = this[r, l];
                }
            }
            return temp;
        }

        public Mat3 Transpose()
        {
            Mat3 temp = new Mat3(this);

            for (int l = 0; l < 3; l++)
            {
                for (int r = 0; r < 3; r++)
                {
                    this[l, r] = temp[r, l];
                }
            }
            return this;
        }

        // ne devrait pas exister !?!
        public static Mat3 TranslateMatrix(double x, double y, double z)
        {
            Mat3 temp = new Mat3();
            temp[0, 2] = x;
            temp[1, 2] = y;
            temp[2, 2] = z;
            return temp;
        }

        // ne devrait pas exister !?!
        public Mat3 Translate(double x, double y, double z)
        {
            Mat3 temp = TranslateMatrix(x, y, z);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat3 ScaleMatrix(double x, double y, double z)
        {
            Mat3 temp = new Mat3();
            temp[0, 0] = x;
            temp[1, 1] = y;
            temp[2, 2] = z;
            return temp;
        }

        public Mat3 Scale(double x, double y, double z)
        {
            Mat3 temp = ScaleMatrix(x, y, z);
            this.mat = (this * temp).mat;
            return this;
        }

        //Order here matters, so be careful!
        public static Mat3 RotateMatrix(double x, double y, double z)
        {
            Console.WriteLine("MAT3.CS : public static Mat3 Rotate(...) : ATTENTION a l'ordre de tempX * tempY * tempZ;!!!");
            Mat3 tempX = RotateXMatrix(x);
            Mat3 tempY = RotateYMatrix(y);
            Mat3 tempZ = RotateZMatrix(z);
            return tempZ * tempY * tempX;
        }

        public Mat3 Rotate(double x, double y, double z)
        {
            Mat3 temp = RotateMatrix(x, y, z);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat3 RotateXMatrix(double x)
        {
            Mat3 temp = new Mat3();
            temp[1, 1] = (double)Math.Cos(x);
            temp[1, 2] = (double)Math.Sin(x) * (-1.0);
            temp[2, 1] = (double)Math.Sin(x);
            temp[2, 2] = (double)Math.Cos(x);
            return temp;
        }

        public Mat3 RotateX(double x)
        {
            Mat3 temp = RotateXMatrix(x);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat3 RotateYMatrix(double y)
        {
            Mat3 temp = new Mat3();
            temp[0, 0] = (double)Math.Cos(y);
            temp[0, 2] = (double)Math.Sin(y);
            temp[2, 0] = (double)Math.Sin(y) * (-1.0);
            temp[2, 2] = (double)Math.Cos(y);
            return temp;
        }

        public Mat3 RotateY(double x)
        {
            Mat3 temp = RotateYMatrix(x);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat3 RotateZMatrix(double z)
        {
            Mat3 temp = new Mat3();
            temp[0, 0] = (double)Math.Cos(z);
            temp[0, 1] = (double)Math.Sin(z) * (-1.0);
            temp[1, 0] = (double)Math.Sin(z);
            temp[1, 1] = (double)Math.Cos(z);
            return temp;
        }

        public Mat3 RotateZ(double x)
        {
            Mat3 temp = RotateZMatrix(x);
            this.mat = (this * temp).mat;
            return this;
        }

        // ne devrait pas exister ???
        public static Mat3 ShearMatrix(double xy, double xz, double yx, double yz, double zx, double zy)
        {
            Mat3 temp = new Mat3();
            temp[0, 1] = xy;
            temp[0, 2] = xz;
            temp[1, 0] = yx;
            temp[1, 2] = yz;
            temp[2, 0] = zx;
            temp[2, 1] = zy;
            return temp;
        }

        // ne devrait pas exister ???
        public Mat3 Shear(double xy, double xz, double yx, double yz, double zx, double zy)
        {
            Mat3 temp = ShearMatrix(xy, xz, yx, yz, zx, zy);
            this.mat = (this * temp).mat;
            return this;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Mat3 other = obj as Mat3;

            if (Utility.FE(this.mat[0, 0], other.mat[0, 0]) &&
                Utility.FE(this.mat[0, 1], other.mat[0, 1]) &&
                Utility.FE(this.mat[0, 2], other.mat[0, 2]) &&
                Utility.FE(this.mat[1, 0], other.mat[1, 0]) &&
                Utility.FE(this.mat[1, 1], other.mat[1, 1]) &&
                Utility.FE(this.mat[1, 2], other.mat[1, 2]) &&
                Utility.FE(this.mat[2, 0], other.mat[2, 0]) &&
                Utility.FE(this.mat[2, 1], other.mat[2, 1]) &&
                Utility.FE(this.mat[2, 2], other.mat[2, 2]))
                return true;

            return false;
        }
    }
}
