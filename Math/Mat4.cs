using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Mat4
    {
        double[,] mat;
        int size = 4;

        public int Size
        {
            get { return size; }
        }

        public Mat4(Mat4 a)
        {
            mat = new double[4, 4];
            mat[0, 0] = a[0, 0];
            mat[0, 1] = a[0, 1];
            mat[0, 2] = a[0, 2];
            mat[0, 3] = a[0, 3];

            mat[1, 0] = a[1, 0];
            mat[1, 1] = a[1, 1];
            mat[1, 2] = a[1, 2];
            mat[1, 3] = a[1, 3];

            mat[2, 0] = a[2, 0];
            mat[2, 1] = a[2, 1];
            mat[2, 2] = a[2, 2];
            mat[2, 3] = a[2, 3];

            mat[3, 0] = a[3, 0];
            mat[3, 1] = a[3, 1];
            mat[3, 2] = a[3, 2];
            mat[3, 3] = a[3, 3];
        }

        public Mat4(double m00 = 1.0, double m01 = 0.0, double m02 = 0.0, double m03 = 0.0,
                    double m10 = 0.0, double m11 = 1.0, double m12 = 0.0, double m13 = 0.0,
                    double m20 = 0.0, double m21 = 0.0, double m22 = 1.0, double m23 = 0.0,
                    double m30 = 0.0, double m31 = 0.0, double m32 = 0.0, double m33 = 1.0)
        {
            mat = new double[4, 4];
            mat[0, 0] = m00;
            mat[0, 1] = m01;
            mat[0, 2] = m02;
            mat[0, 3] = m03;

            mat[1, 0] = m10;
            mat[1, 1] = m11;
            mat[1, 2] = m12;
            mat[1, 3] = m13;

            mat[2, 0] = m20;
            mat[2, 1] = m21;
            mat[2, 2] = m22;
            mat[2, 3] = m23;

            mat[3, 0] = m30;
            mat[3, 1] = m31;
            mat[3, 2] = m32;
            mat[3, 3] = m33;
        }

        /*public Mat4 Identity()
        {
            mat[0, 0] = 1;
            mat[0, 1] = 0;
            mat[0, 2] = 0;
            mat[0, 3] = 0;

            mat[1, 0] = 0;
            mat[1, 1] = 1;
            mat[1, 2] = 0;
            mat[1, 3] = 0;

            mat[2, 0] = 0;
            mat[2, 1] = 0;
            mat[2, 2] = 1;
            mat[2, 3] = 0;

            mat[3, 0] = 0;
            mat[3, 1] = 0;
            mat[3, 2] = 0;
            mat[3, 3] = 1;

            return this;
        }*/

        public double this[int r, int c]
        {
            get { return mat[r, c]; }
            set { mat[r, c] = value; }
        }

        public static bool operator ==(Mat4 a, Mat4 b)
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (!Utility.FE(a[x, y], b[x, y]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool operator !=(Mat4 a, Mat4 b)
        {
            return !(a == b);
        }

        public static Mat4 operator *(Mat4 a, Mat4 b)
        {
            Mat4 temp = new Mat4();

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    temp[r, c] = a[r, 0] * b[0, c] +
                                 a[r, 1] * b[1, c] +
                                 a[r, 2] * b[2, c] +
                                 a[r, 3] * b[3, c];
                }
            }
            return temp;
        }

        public static Mat4 operator *(Mat4 a, double b)
        {
            Mat4 temp = new Mat4(a);

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    temp[r, c] *= b;
                }
            }
            return temp;
        }

        public Mat4 TransposeMatrix()
        {
            Mat4 temp = new Mat4();

            for (int l = 0; l < 4; l++)
            {
                for (int r = 0; r < 4; r++)
                {
                    temp[l, r] = this[r, l];
                }
            }
            return temp;
        }
        
        public Mat4 Transpose() // ne fonctionne pas !!!!!
        {
            Mat4 temp = TransposeMatrix();
            this.mat = temp.mat;
            return this;
        }

        public Mat3 Sub(int x, int y)
        {
            Mat3 sub = new Mat3();

            int i = 0;

            for (int r = 0; r < 4; r++)
            {
                int j = 0;
                if (r == x)
                    continue;

                for (int c = 0; c < 4; c++)
                {
                    if (c == y)
                    {
                        continue;
                    }

                    sub[i, j] = this.mat[r, c];
                    j++;
                }
                i++;
            }
            return sub;
        }

        public double Minor(int x, int y)
        {
            Mat3 sub = this.Sub(x, y);
            return sub.Det();
        }

        public double Cofactor(int r, int c)
        {
            double temp = this.Minor(r, c);

            if (((r + c) % 2) != 0)
            {
                temp = -temp;
            }

            return temp;
        }

        public double Det()
        {
            double minor00 = this[1, 1] * (this[2, 2] * this[3, 3] - this[2, 3] * this[3, 2]) -
                             this[2, 1] * (this[1, 2] * this[3, 3] - this[1, 3] * this[3, 2]) +
                             this[3, 1] * (this[1, 2] * this[2, 3] - this[1, 3] * this[2, 2]);

            double minor01 = this[0, 1] * (this[2, 2] * this[3, 3] - this[2, 3] * this[3, 2]) -
                             this[2, 1] * (this[0, 2] * this[3, 3] - this[0, 3] * this[3, 2]) +
                             this[3, 1] * (this[0, 2] * this[2, 3] - this[0, 3] * this[2, 2]);

            double minor02 = this[0, 1] * (this[1, 2] * this[3, 3] - this[1, 3] * this[3, 2]) -
                             this[1, 1] * (this[0, 2] * this[3, 3] - this[0, 3] * this[3, 2]) +
                             this[3, 1] * (this[0, 2] * this[1, 3] - this[0, 3] * this[1, 2]);

            double minor03 = this[0, 1] * (this[1, 2] * this[2, 3] - this[1, 3] * this[2, 2]) -
                             this[1, 1] * (this[0, 2] * this[2, 3] - this[0, 3] * this[2, 2]) +
                             this[2, 1] * (this[0, 2] * this[1, 3] - this[0, 3] * this[1, 2]);

            double temp = this[0, 0] * minor00 -
                          this[1, 0] * minor01 +
                          this[2, 0] * minor02 -
                          this[3, 0] * minor03;

            return temp;
        }

        public Mat4 Adjugate()
        {
            Mat4 adj = new Mat4();

            //Grab all 16 sub matrices of 3x3 and get the determinate of each, then determine if positive or negative valud
            for (int l = 0; l < 4; l++)
            {
                for (int r = 0; r < 4; r++)
                {
                    adj[l, r] = this.Sub(l, r).Det();
                    if (((l + r) % 2) != 0)
                    {
                        adj[l, r] *= -1.0;
                    }
                }
            }
            return adj;
        }

        public Mat4 Inverse_with_adjugate()
        {
            Mat4 inverse = new Mat4();
            double det = Det();
            //Console.WriteLine(det);

            if (!Utility.FE(det, 0.0))
            {
                inverse = this.Adjugate().TransposeMatrix() * (1.0 / det);
            }
            return inverse;
        }


        public Mat4 Inverse()
        {
            Mat4 inverse = new Mat4();
            double det = Det();
            //Console.WriteLine(det);

            if (!Utility.FE(det, 0.0))
            {
                for (int l = 0; l < 4; l++)
                {
                    for (int r = 0; r < 4; r++)
                    {
                        inverse[l, r] = Cofactor(r, l);
                    }
                }
                //Console.WriteLine(inverse + "\n");
                inverse.TransposeMatrix();
                //Console.WriteLine(inverse + "\n");
                inverse = inverse * (1.0 / det);
            }
            return inverse;
        }

        public Mat4 Inverse_10_percent_faster()
        {
            Mat4 inverse = new Mat4();

            double minor00 = this[1, 1] * (this[2, 2] * this[3, 3] - this[2, 3] * this[3, 2]) -
                             this[2, 1] * (this[1, 2] * this[3, 3] - this[1, 3] * this[3, 2]) +
                             this[3, 1] * (this[1, 2] * this[2, 3] - this[1, 3] * this[2, 2]);

            double minor01 = this[0, 1] * (this[2, 2] * this[3, 3] - this[2, 3] * this[3, 2]) -
                             this[2, 1] * (this[0, 2] * this[3, 3] - this[0, 3] * this[3, 2]) +
                             this[3, 1] * (this[0, 2] * this[2, 3] - this[0, 3] * this[2, 2]);

            double minor02 = this[0, 1] * (this[1, 2] * this[3, 3] - this[1, 3] * this[3, 2]) -
                             this[1, 1] * (this[0, 2] * this[3, 3] - this[0, 3] * this[3, 2]) +
                             this[3, 1] * (this[0, 2] * this[1, 3] - this[0, 3] * this[1, 2]);

            double minor03 = this[0, 1] * (this[1, 2] * this[2, 3] - this[1, 3] * this[2, 2]) -
                             this[1, 1] * (this[0, 2] * this[2, 3] - this[0, 3] * this[2, 2]) +
                             this[2, 1] * (this[0, 2] * this[1, 3] - this[0, 3] * this[1, 2]);

            double det = this[0, 0] * minor00 -
                          this[1, 0] * minor01 +
                          this[2, 0] * minor02 -
                          this[3, 0] * minor03;

            //Console.WriteLine(det);

            if (!Utility.FE(det, 0.0))
            {
                for (int l = 0; l < 4; l++)
                {
                    for (int r = 0; r < 4; r++)
                    {
                        Mat3 sub = new Mat3();
                        int i = 0;
                        for (int ll = 0; ll < 4; ll++)
                        {
                            int j = 0;
                            if (ll != l)
                            {
                                for (int rr = 0; rr < 4; rr++)
                                {
                                    if (rr != r)
                                    {
                                        sub[i, j] = this.mat[ll, rr];
                                        j++;
                                    }
                                }
                                i++;
                            }
                        }

                        double minor = sub[0, 0] * (sub[1, 1] * sub[2, 2] - sub[1, 2] * sub[2, 1]) -
                                       sub[1, 0] * (sub[0, 1] * sub[2, 2] - sub[0, 2] * sub[2, 1]) +
                                       sub[2, 0] * (sub[0, 1] * sub[1, 2] - sub[0, 2] * sub[1, 1]);

                        double cofactor = minor;
                        if (((l + r) % 2) != 0)
                        {
                            cofactor = -minor;
                        }
                        inverse[l, r] = cofactor;
                    }
                }
                //Console.WriteLine(inverse + "\n");
                Mat4 transpose = new Mat4(inverse);
                for (int l = 0; l < 4; l++)
                {
                    for (int r = 0; r < 4; r++)
                    {
                        inverse[l, r] = transpose[r, l];
                    }
                }
                //Console.WriteLine(inverse + "\n");
                inverse = inverse * (1.0 / det);
            }
            return inverse;
        }




        public static Mat4 TranslateMatrix(double x, double y, double z)
        {
            Mat4 temp = new Mat4();
            temp[0, 3] = x;
            temp[1, 3] = y;
            temp[2, 3] = z;
            return temp;
        }

        public Mat4 Translate(double x, double y, double z)
        {
            Mat4 temp = TranslateMatrix(x, y, z);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat4 TranslateMatrix(Point p)
        {
            Mat4 temp = new Mat4();
            temp[0, 3] = p.x;
            temp[1, 3] = p.y;
            temp[2, 3] = p.z;
            return temp;
        }

        public Mat4 Translate(Point p)
        {
            Mat4 temp = TranslateMatrix(p);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat4 TranslateMatrix(Vector v)
        {
            Mat4 temp = new Mat4();
            temp[0, 3] = v.x;
            temp[1, 3] = v.y;
            temp[2, 3] = v.z;
            return temp;
        }

        public Mat4 Translate(Vector v)
        {
            Mat4 temp = TranslateMatrix(v);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat4 ScaleMatrix(double x, double y, double z)
        {
            Mat4 temp = new Mat4();
            temp[0, 0] = x;
            temp[1, 1] = y;
            temp[2, 2] = z;
            return temp;
        }

        public Mat4 Scale(double x, double y, double z)
        {
            Mat4 temp = ScaleMatrix(x, y, z);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat4 ScaleMatrix(Point p1)
        {
            Mat4 temp = new Mat4();
            temp[0, 0] = p1.x;
            temp[1, 1] = p1.y;
            temp[2, 2] = p1.z;
            return temp;
        }

        public Mat4 Scale(Point p1)
        {
            Mat4 temp = ScaleMatrix(p1);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat4 ScaleMatrix(Vector v1)
        {
            Mat4 temp = new Mat4();
            temp[0, 0] = v1.x;
            temp[1, 1] = v1.y;
            temp[2, 2] = v1.z;
            return temp;
        }

        public Mat4 Scale(Vector v1)
        {
            Mat4 temp = ScaleMatrix(v1);
            this.mat = (this * temp).mat;
            return this;
        }

        //Order here matters, so be careful!
        public static Mat4 RotateMatrix(double x, double y, double z)
        {
            // Ces rotations sont par rapport au repere globale qui rest donc inchangé par les rotations!!
            // on tourne dabord au tour de X, puis de Y puis de Z !!
            Mat4 tempX = RotateXMatrix(x);
            Mat4 tempY = RotateYMatrix(y);
            Mat4 tempZ = RotateZMatrix(z);
            return tempZ * tempY * tempX;
        }

        public Mat4 Rotate(double x, double y, double z)
        {
            Mat4 temp = RotateMatrix(x, y, z);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat4 RotateXMatrix(double x)
        {
            Mat4 temp = new Mat4();
            temp[1, 1] = Math.Cos(x);
            temp[1, 2] = Math.Sin(x) * (-1.0);
            temp[2, 1] = Math.Sin(x);
            temp[2, 2] = Math.Cos(x);
            return temp;
        }

        public Mat4 RotateX(double x)
        {
            Mat4 temp = RotateXMatrix(x);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat4 RotateYMatrix(double y)
        {
            Mat4 temp = new Mat4();
            temp[0, 0] = Math.Cos(y);
            temp[0, 2] = Math.Sin(y);
            temp[2, 0] = Math.Sin(y) * (-1.0);
            temp[2, 2] = Math.Cos(y);
            return temp;
        }

        public Mat4 RotateY(double y)
        {
            Mat4 temp = RotateYMatrix(y);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat4 RotateZMatrix(double z)
        {
            Mat4 temp = new Mat4();
            temp[0, 0] = Math.Cos(z);
            temp[0, 1] = Math.Sin(z) * (-1.0);
            temp[1, 0] = Math.Sin(z);
            temp[1, 1] = Math.Cos(z);
            return temp;
        }

        public Mat4 RotateZ(double z)
        {
            Mat4 temp = RotateZMatrix(z);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat4 ShearMatrix(double xy, double xz, double yx, double yz, double zx, double zy)
        {
            Mat4 temp = new Mat4();
            temp[0, 0] = 1.0;
            temp[0, 1] = xy;
            temp[0, 2] = xz;
            temp[0, 3] = 0.0;
            temp[1, 0] = yx;
            temp[1, 1] = 1.0;
            temp[1, 2] = yz;
            temp[1, 3] = 0.0;
            temp[2, 0] = zx;
            temp[2, 1] = zy;
            temp[2, 2] = 1.0;
            temp[2, 3] = 0.0;
            temp[3, 0] = 0.0;
            temp[3, 1] = 0.0;
            temp[3, 2] = 0.0;
            temp[3, 3] = 1.0;
            return temp;
        }

        public static Mat4 ShearMatrix_faster(double xy, double xz, double yx, double yz, double zx, double zy)
        {
            Mat4 temp = new Mat4();
            temp[0, 1] = xy;
            temp[0, 2] = xz;
            temp[1, 0] = yx;
            temp[1, 2] = yz;
            temp[2, 0] = zx;
            temp[2, 1] = zy;
            return temp;
        }

        public Mat4 Shear(double xy, double xz, double yx, double yz, double zx, double zy)
        {
            Mat4 temp = ShearMatrix(xy, xz, yx, yz, zx, zy);
            this.mat = (this * temp).mat;
            return this;
        }

        public Mat4 Shear_faster(double xy, double xz, double yx, double yz, double zx, double zy)
        {
            Mat4 temp = ShearMatrix_faster(xy, xz, yx, yz, zx, zy);
            this.mat = (this * temp).mat;
            return this;
        }

        public override string ToString()
        {
            return String.Format("|{0,6:0.00}", mat[0, 0]) + "," +
                   String.Format("{0,6:0.00}", mat[0, 1]) + "," +
                   String.Format("{0,6:0.00}", mat[0, 2]) + "," +
                   String.Format("{0,6:0.00}|", mat[0, 3]) + "\n" +

                   String.Format("|{0,6:0.00}", mat[1, 0]) + "," +
                   String.Format("{0,6:0.00}", mat[1, 1]) + "," +
                   String.Format("{0,6:0.00}", mat[1, 2]) + "," +
                   String.Format("{0,6:0.00}|", mat[1, 3]) + "\n" +

                   String.Format("|{0,6:0.00}", mat[2, 0]) + "," +
                   String.Format("{0,6:0.00}", mat[2, 1]) + "," +
                   String.Format("{0,6:0.00}", mat[2, 2]) + "," +
                   String.Format("{0,6:0.00}|", mat[2, 3]) + "\n" +

                   String.Format("|{0,6:0.00}", mat[3, 0]) + "," +
                   String.Format("{0,6:0.00}", mat[3, 1]) + "," +
                   String.Format("{0,6:0.00}", mat[3, 2]) + "," +
                   String.Format("{0,6:0.00}|", mat[3, 3]);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Mat4 other = obj as Mat4;

            if (Utility.FE(this.mat[0, 0], other.mat[0, 0]) &&
                Utility.FE(this.mat[0, 1], other.mat[0, 1]) &&
                Utility.FE(this.mat[0, 2], other.mat[0, 2]) &&
                Utility.FE(this.mat[0, 3], other.mat[0, 3]) &&
                Utility.FE(this.mat[1, 0], other.mat[1, 0]) &&
                Utility.FE(this.mat[1, 1], other.mat[1, 1]) &&
                Utility.FE(this.mat[1, 2], other.mat[1, 2]) &&
                Utility.FE(this.mat[1, 3], other.mat[1, 3]) &&
                Utility.FE(this.mat[2, 0], other.mat[2, 0]) &&
                Utility.FE(this.mat[2, 1], other.mat[2, 1]) &&
                Utility.FE(this.mat[2, 2], other.mat[2, 2]) &&
                Utility.FE(this.mat[2, 3], other.mat[2, 3]) &&
                Utility.FE(this.mat[3, 0], other.mat[3, 0]) &&
                Utility.FE(this.mat[3, 1], other.mat[3, 1]) &&
                Utility.FE(this.mat[3, 2], other.mat[3, 2]) &&
                Utility.FE(this.mat[3, 3], other.mat[3, 3]))
                return true;

            return false;
        }
    }
}
