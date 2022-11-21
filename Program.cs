using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace RT
{
    class Program
    {
        public static void TestInverse_vs_Inverse_10_percent_faster_3x3()
        {
            Mat3 mat = new Mat3(5, 6, 8, 2, 45, 6, 4, 54, 2);
            Console.WriteLine(mat.Det());
            Mat3 mat1 = new Mat3(mat);
            Mat3 mat2 = new Mat3(mat);
            Mat3 mat3 = new Mat3(mat);
            Mat3 res = new Mat3();
            int n = 6000000;
            var timer1 = new Stopwatch();
            var timer2 = new Stopwatch();
            var timer3 = new Stopwatch();

            Console.WriteLine("Start Inverse_faster_i_guess");
            timer1.Start();
            for (int i = 0; i < n; i++)
            {
                res = mat1.Inverse_10_percent_faster();
                //Console.WriteLine(res + "\n");
            }
            Console.WriteLine("End   Inverse_faster_i_guess");
            timer1.Stop();
            TimeSpan timeTaken1 = timer1.Elapsed;
            Console.WriteLine("Inverse_faster_i_guess : " + timeTaken1.ToString(@"m\:ss\.fff"));

            Console.WriteLine("Start Inverse");
            timer2.Start();
            for (int i = 0; i < n; i++)
            {
                res = mat2.Inverse();
                //Console.WriteLine(res + "\n");
            }
            Console.WriteLine("End   Inverse");
            timer2.Stop();
            TimeSpan timeTaken2 = timer2.Elapsed;
            Console.WriteLine("Inverse                : " + timeTaken2.ToString(@"m\:ss\.fff"));

            /*Console.WriteLine("Start Inverse_adjugate");
            timer3.Start();
            for (int i = 0; i < n; i++)
            {
                res = mat3.Inverse_with_adjugate();
                //Console.WriteLine(res + "\n");
            }
            Console.WriteLine("End   Inverse_adjugate");
            timer3.Stop();
            TimeSpan timeTaken3 = timer3.Elapsed;
            Console.WriteLine("Inverse_adjugate       : " + timeTaken3.ToString(@"m\:ss\.fff"));*/
        }

        public static void TestInverse_vs_Inverse_10_percent_faster_4x4()
        {
            Mat4 mat = new Mat4(5, 6, 8, 2, 45, 6, 4, 54, 2, 6, 0, 7, 6, 10, 3, 12);
            Console.WriteLine(mat.Det());
            Mat4 mat1 = new Mat4(mat);
            Mat4 mat2 = new Mat4(mat);
            Mat4 mat3 = new Mat4(mat);
            Mat4 res = new Mat4();
            int n = 2000000;
            var timer1 = new Stopwatch();
            var timer2 = new Stopwatch();
            var timer3 = new Stopwatch();

            Console.WriteLine("Start Inverse_faster_i_guess");
            timer1.Start();
            for (int i = 0; i < n; i++)
            {
                res = mat1.Inverse_10_percent_faster();
                //Console.WriteLine(res + "\n");
            }
            Console.WriteLine("End   Inverse_faster_i_guess");
            timer1.Stop();
            TimeSpan timeTaken1 = timer1.Elapsed;
            Console.WriteLine("Inverse_faster_i_guess : " + timeTaken1.ToString(@"m\:ss\.fff"));

            Console.WriteLine("Start Inverse");
            timer2.Start();
            for (int i = 0; i < n; i++)
            {
                res = mat2.Inverse();
                //Console.WriteLine(res + "\n");
            }
            Console.WriteLine("End   Inverse");
            timer2.Stop();
            TimeSpan timeTaken2 = timer2.Elapsed;
            Console.WriteLine("Inverse                : " + timeTaken2.ToString(@"m\:ss\.fff"));

            Console.WriteLine("Start Inverse_adjugate");
            timer3.Start();
            for (int i = 0; i < n; i++)
            {
                res = mat3.Inverse_with_adjugate();
                //Console.WriteLine(res + "\n");
            }
            Console.WriteLine("End   Inverse_adjugate");
            timer3.Stop();
            TimeSpan timeTaken3 = timer3.Elapsed;
            Console.WriteLine("Inverse_adjugate       : " + timeTaken3.ToString(@"m\:ss\.fff"));
        }


        public static void TestPointConstructor()
        {
            Point t1 = new Point();

            Console.WriteLine("Point Constructor");
            Console.WriteLine(t1.ToString());
        }

        public static void TestVectorConstructor()
        {
            Vector t1 = new Vector();

            Console.WriteLine("\nVector Constructor");
            Console.WriteLine(t1.ToString());
        }

        public static void TestAddition()
        {
            Point p1 = new Point(1.0, 2.0, 3.0);
            Point p2 = new Point(3.0, 2.0, 1.0);
            Vector v1 = new Vector(0.25, 0.25, 0.25);
            Vector v2 = new Vector(0.75, 0.75, 0.75);

            Console.WriteLine("\nAddition");
            Console.WriteLine((p1 + p2).ToString());
            Console.WriteLine((v1 + p1).ToString());
            Console.WriteLine((p1 + v1).ToString());
            Console.WriteLine((v1 + v2).ToString());
        }

        public static void TestSubstraction()
        {
            Point p1 = new Point(1.0, 2.0, 3.0);
            Point p2 = new Point(3.0, 2.0, 1.0);
            Vector v1 = new Vector(0.25, 0.25, 0.25);
            Vector v2 = new Vector(0.75, 0.75, 0.75);

            Console.WriteLine("\nSubstraction");
            Console.WriteLine((p1 - p2).ToString());
            Console.WriteLine((v1 - p1).ToString());
            Console.WriteLine((p1 - v1).ToString());
            Console.WriteLine((v1 - v2).ToString());
        }

        public static void TestNegation()
        {
            Point p1 = new Point(1.0, 1.0, 1.0);
            Vector v1 = new Vector(1.0, 1.0, 1.0);

            Console.WriteLine("\nNegation");
            Console.WriteLine((-p1).ToString());
            Console.WriteLine((-v1).ToString());
            Console.WriteLine(p1.Negate().ToString());
            Console.WriteLine(v1.Negate().ToString());
        }

        public static void TestScalarMultiplication()
        {
            Point p1 = new Point(1.0, 1.0, 1.0);
            Vector v1 = new Vector(1.0, 1.0, 1.0);
            float s = 0.5f;

            Console.WriteLine("\nScalar Multiplication");
            Console.WriteLine((p1 * s).ToString());
            Console.WriteLine((s * p1).ToString());
            Console.WriteLine((v1 * s).ToString());
            Console.WriteLine((s * v1).ToString());
            Console.WriteLine(p1.Scale(s).ToString());
            Console.WriteLine(v1.Scale(s).ToString());
        }

        public static void TestMagnitude()
        {
            Point p1 = new Point(1.0, 1.0, 1.0);
            Vector v1 = new Vector(1.0, 1.0, 1.0);

            Console.WriteLine("\n Magniture");
            Console.WriteLine(p1.Magnitude().ToString());
            Console.WriteLine(v1.Magnitude().ToString());

            Console.WriteLine("\nSquare Magniture");
            Console.WriteLine(p1.SqrtMagnitude().ToString());
            Console.WriteLine(v1.SqrtMagnitude().ToString());

        }

        public static void TestNormalize()
        {
            Point p1 = new Point(1.0, 1.0, 1.0);
            Vector v1 = new Vector(1.0, 1.0, 1.0);

            Console.WriteLine("\nNormalized");
            //Console.WriteLine(p1.Normalized().ToString());
            Console.WriteLine(v1.Normalized().ToString());

            Console.WriteLine("\nNormalize");
            //Console.WriteLine(p1.Normalize().ToString());
            Console.WriteLine(v1.Normalize().ToString());
        }

        public static void TestDot()
        {
            Point p1 = new Point(1.0, 1.0, 0.0);
            Vector v1 = new Vector(-1.0, -1.0, -1.0);

            Console.WriteLine("\nDot Self");
            Console.WriteLine(p1.Dot(p1).ToString());
            Console.WriteLine(p1.Dot(v1).ToString());
            Console.WriteLine(v1.Dot(v1).ToString());
            Console.WriteLine(v1.Dot(p1).ToString());

            Console.WriteLine("\nDot Self Normalized");
            //Console.WriteLine(p1.Normalized().Dot(p1.Normalized()).ToString());
            //Console.WriteLine(p1.Normalized().Dot(v1.Normalized()).ToString());
            Console.WriteLine(v1.Normalized().Dot(v1.Normalized()).ToString());
            //Console.WriteLine(v1.Normalized().Dot(p1.Normalized()).ToString());

            Console.WriteLine("\nDot Static");
            Console.WriteLine(Tuple.Dot(p1, p1).ToString());
            Console.WriteLine(Tuple.Dot(p1, v1).ToString());
            Console.WriteLine(Tuple.Dot(v1, v1).ToString());
            Console.WriteLine(Tuple.Dot(v1, p1).ToString());

            Console.WriteLine("\nDot Static Normalized");
            //Console.WriteLine(Tuple.Dot(p1.Normalized(), p1.Normalized()).ToString());
            //Console.WriteLine(Tuple.Dot(p1.Normalized(), v1.Normalized()).ToString());
            Console.WriteLine(Tuple.Dot(v1.Normalized(), v1.Normalized()).ToString());
            //Console.WriteLine(Tuple.Dot(v1.Normalized(), p1.Normalized()).ToString());
            
        }

        public static void TestCross()
        {
            Vector v1 = new Vector(1.0, 0.0, 0.0);
            Vector v2 = new Vector(0.0, 1.0, 0.0);

            Console.WriteLine("\nCross Self");
            Console.WriteLine(v1.Cross(v2).ToString());
            Console.WriteLine(v2.Cross(v1).ToString());

            Console.WriteLine("\nCross Static");
            Console.WriteLine(Vector.Cross(v1, v2).ToString());
            Console.WriteLine(Vector.Cross(v2, v1).ToString());
        }

        public static void TestTupleEquality()
        {
            Point p1 = new Point(1.0, 0.0, 1.0001);
            Point p2 = new Point(1.0, 0.0, 1.0002);
            Vector v1 = new Vector(1.0, 0.0, 1.0001);
            Vector v2 = new Vector(1.0, 0.0, 1.0002);

            Console.WriteLine("\nTuple Equality");
            Console.WriteLine((p1 == p2).ToString());
            Console.WriteLine((p1 == v1).ToString());
            Console.WriteLine((v1 == v2).ToString());

        }


        public static void TestColorConstructor()
        {
            Color color = new Color();
            Color color2 = new Color(1.0, 0.0, 0.0);
            Color color3 = Color.yellow;

            Console.WriteLine("\nColor Constructor");
            Console.WriteLine(color);
            Console.WriteLine(color2);
            Console.WriteLine(color3);
        }

        public static void TestColorOperators()
        {
            Color color = new Color(0.1, 0.25, 0.5);

            Console.WriteLine("\nColor Operators");
            Console.WriteLine("Addition");
            Console.WriteLine(color + color);

            Console.WriteLine("\n Substraction");
            Console.WriteLine(Color.white - color);

            Console.WriteLine("\n Scalar Multiplication");
            Console.WriteLine(color * 2.0);

            Console.WriteLine("\n Hadamard Product");
            Console.WriteLine(color * Color.red);
            Console.WriteLine(Color.green * color);
        }

        public static void TestCanvas()
        {
            Canvas canvas = new Canvas(100, 100);

            Console.WriteLine("\nCanvas Constructor");
            Console.WriteLine(canvas.ToString());
            canvas.FillCanvas(Color.green);
            Console.WriteLine("\nCanvas Getters");
            Console.WriteLine("Height : " + canvas.GetHeight());
            Console.WriteLine("Width : " + canvas.GetWidth());
            Console.WriteLine("Get pixel : " + canvas.GetPixel(50, 50));
            Console.WriteLine("\nCanvas Setters");
            canvas.SetPixel(50, 50, Color.blue);
            Console.WriteLine("Set pixel : " + canvas.GetPixel(50, 50));
        }

        public static void TestWritePPM()
        {
            Canvas canvas = new Canvas(10, 10);
            canvas.SetPixel(0, 0, Color.red);
            canvas.SetPixel(0, 1, Color.green);
            canvas.SetPixel(1, 0, Color.blue);
            canvas.SetPixel(1, 1, Color.white);
            Save.SaveCanvas(canvas);
            Console.WriteLine("\nCanvas Saved as temp.ppm");
        }


        public static void TestMatrixConstructor()
        {
            Console.WriteLine("\nTest Matrix Constructor");
            Mat2 mat2 = new Mat2();
            Mat3 mat3 = new Mat3();
            Mat4 mat4 = new Mat4();
            Console.WriteLine(mat2.ToString());
            Console.WriteLine(mat3.ToString());
            Console.WriteLine(mat4.ToString());


            Console.WriteLine("\nMatrix Setter");
            mat2[0, 0] = -15.0;
            mat3[1, 2] = -15.0;
            mat4[3, 2] = -15.0;
            Console.WriteLine(mat2.ToString());
            Console.WriteLine(mat3.ToString());
            Console.WriteLine(mat4.ToString());
        }

        public static void TestMatrixEquality()
        {
            Console.WriteLine("\nTest Matrix Equality");
            Mat2 mat2 = new Mat2(1, 2, 3, 4);
            Mat2 mat2a = new Mat2(0.99999, 2, 3, 4);
            Mat2 mat2b = new Mat2(1.0001, 2, 3, 4);
            Mat3 mat3 = new Mat3(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Mat3 mat3a = new Mat3(1, 2, 3, 4.00002, 5, 6, 7, 8, 9);
            Mat3 mat3b = new Mat3(1, 2, 3, 4.001, 5, 6, 7, 8, 9);
            Mat4 mat4 = new Mat4(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Mat4 mat4a = new Mat4(1, 2, 3, 4.00002, 5, 6, 7, 8, 9);
            Mat4 mat4b = new Mat4(1, 2, 3, 4.001, 5, 6, 7, 8, 9);

            Console.WriteLine("Should be True");
            Console.WriteLine(mat2 == mat2a);
            Console.WriteLine(mat3 == mat3a);
            Console.WriteLine(mat4 == mat4a);

            Console.WriteLine("\nShould be False");
            Console.WriteLine(mat2 == mat2b);
            Console.WriteLine(mat2a == mat2b);
            Console.WriteLine(mat3 == mat3b);
            Console.WriteLine(mat3a == mat3b);
            Console.WriteLine(mat4 == mat4b);
            Console.WriteLine(mat4a == mat4b);
        }

        public static void TestMatrixMultiplication()
        {
            Console.WriteLine("\nTest Matrix Multiplication");
            Mat2 mat2 = new Mat2();
            Mat2 mat2a = new Mat2();
            Mat3 mat3 = new Mat3();
            Mat3 mat3a = new Mat3();
            Mat4 mat4 = new Mat4();
            Mat4 mat4a = new Mat4();

            Console.WriteLine("Should be Identity");
            Console.WriteLine(mat2 * mat2a);
            Console.WriteLine(mat3 * mat3a);
            Console.WriteLine(mat4 * mat4a);

            mat2[0, 0] = -2.0;
            mat2[0, 1] = -2.0;
            mat3[1, 0] = -2.0;
            mat3[1, 1] = -2.0;
            mat3[1, 2] = -2.0;
            mat4[0, 1] = -2.0;
            mat4[1, 1] = -2.0;
            mat4[2, 1] = -2.0;
            mat4[3, 1] = -2.0;
            Console.WriteLine("\nShould not be Identity");
            Console.WriteLine(mat2 * mat2);
            Console.WriteLine(mat3 * mat3);
            Console.WriteLine(mat4 * mat4);
        }

        public static void TestMatrixTranspose()
        {
            Console.WriteLine("\nTest Matrix Transpose");
            Mat2 mat2 = new Mat2();
            Mat3 mat3 = new Mat3();
            Mat4 mat4 = new Mat4();

            mat2[0, 1] = -2.0;
            mat2[0, 1] = -2.0;
            mat3[0, 1] = -2.0;
            mat3[1, 2] = -2.0;
            mat3[2, 0] = -2.0;
            mat4[0, 0] = -2.0;
            mat4[0, 1] = -2.0;
            mat4[0, 2] = -2.0;
            mat4[0, 3] = -2.0;

            Console.WriteLine(mat2.ToString()+"\n");
            mat2.Transpose();
            Console.WriteLine(mat2.ToString());
            Console.WriteLine(mat3.ToString() + "\n");
            mat3.Transpose();
            Console.WriteLine(mat3.ToString());
            Console.WriteLine(mat4.ToString() + "\n");
            mat4.Transpose();
            Console.WriteLine(mat4.ToString() + "\n");
            Mat4 t = mat4.TransposeMatrix();
            Console.WriteLine(t.ToString());
        }

        public static void TestSubMatrix()
        {
            Mat3 mat3 = new Mat3(1, 2, 6, -5, 8, -4, 2, 6, 4);
            Console.WriteLine("\nTest Sub Matrix");
            Console.WriteLine("Matrix 3x3");
            Console.WriteLine(mat3);

            Console.WriteLine("Sub Matrix (0,0)");
            Mat2 temp = mat3.Sub(0, 0);
            Console.WriteLine(temp);

            Console.WriteLine("Sub Matrix (0,1)");
            temp = mat3.Sub(0, 1);
            Console.WriteLine(temp);

            Console.WriteLine("Sub Matrix (0,2)");
            temp = mat3.Sub(0, 2);
            Console.WriteLine(temp);

            Console.WriteLine("Sub Matrix (1,1)");
            temp = mat3.Sub(1, 1);
            Console.WriteLine(temp + "\n");


            Mat4 mat4 = new Mat4(-2, -8, 3, 5, -3, 1, 7, 3, 1, 2, -9, 6, -6, 7, 7, -9);
            Console.WriteLine("Matrix 4x4");
            Console.WriteLine(mat4);

            Console.WriteLine("Sub Matrix (0,0)");
            Mat3 temp2 = mat4.Sub(0, 0);
            Console.WriteLine(temp2);

            Console.WriteLine("Sub Matrix (0,1)");
            temp2 = mat4.Sub(0, 1);
            Console.WriteLine(temp2);

            Console.WriteLine("Sub Matrix (0,2)");
            temp2 = mat4.Sub(0, 2);
            Console.WriteLine(temp2);

            Console.WriteLine("Sub Matrix (0,3)");
            temp2 = mat4.Sub(0, 3);
            Console.WriteLine(temp2);

            Console.WriteLine("Sub Matrix (1,1)");
            temp2 = mat4.Sub(1, 1);
            Console.WriteLine(temp2);
        }

        public static void TestMinorMatrix()
        {
            Mat3 mat3 = new Mat3(1, 2, 6, -5, 8, -4, 2, 6, 4);
            Console.WriteLine("\nTest Minor Matrix");
            Console.WriteLine("Matrix");
            Console.WriteLine(mat3);

            Console.WriteLine("Minor Matrix (0,0)");
            Console.WriteLine(mat3.Minor(0, 0));

            Console.WriteLine("Minor Matrix (0,1)");
            Console.WriteLine(mat3.Minor(0, 1));

            Console.WriteLine("Minor Matrix (0,2)");
            Console.WriteLine(mat3.Minor(0, 2));

            Console.WriteLine("Minor Matrix (1,1)");
            Console.WriteLine(mat3.Minor(1, 1));

            Console.WriteLine("Minor Matrix (1,2)");
            Console.WriteLine(mat3.Minor(1, 2));


            Mat4 mat4 = new Mat4(-2, -8, 3, 5, -3, 1, 7, 3, 1, 2, -9, 6, -6, 7, 7, -9);
            Console.WriteLine("Matrix 4x4");
            Console.WriteLine(mat4);

            Console.WriteLine("Sub Matrix (0,0)");
            Console.WriteLine(mat4.Minor(0, 0));

            Console.WriteLine("Sub Matrix (0,1)");
            Console.WriteLine(mat4.Minor(0, 1));

            Console.WriteLine("Sub Matrix (0,2)");
            Console.WriteLine(mat4.Minor(0, 2));

            Console.WriteLine("Sub Matrix (0,3)");
            Console.WriteLine(mat4.Minor(0, 3));

            Console.WriteLine("Sub Matrix (1,1)");
            Console.WriteLine(mat4.Minor(1, 1));
        }

        public static void TestCofactor()
        {
            Mat3 mat3 = new Mat3(1, 2, 6, -5, 8, -4, 2, 6, 4);
            Console.WriteLine("\nTest Cofactor");
            Console.WriteLine("Matrix 3x3");
            Console.WriteLine(mat3);

            Console.WriteLine("Cofactor (0,0)");
            Console.WriteLine(mat3.Cofactor(0, 0));

            Console.WriteLine("Cofactor (0,1)");
            Console.WriteLine(mat3.Cofactor(0, 1));

            Console.WriteLine("Cofactor (0,2)");
            Console.WriteLine(mat3.Cofactor(0, 2));

            Console.WriteLine("Cofactor (1,1)");
            Console.WriteLine(mat3.Cofactor(1, 1));

            Console.WriteLine("Cofactor (1,2)");
            Console.WriteLine(mat3.Cofactor(1, 2));


            Mat4 mat4 = new Mat4(-2, -8, 3, 5, -3, 1, 7, 3, 1, 2, -9, 6, -6, 7, 7, -9);
            Console.WriteLine("Matrix 4x4");
            Console.WriteLine(mat4);

            Console.WriteLine("Cofactor (0,0)");
            Console.WriteLine(mat4.Cofactor(0, 0));

            Console.WriteLine("Cofactor (0,1)");
            Console.WriteLine(mat4.Cofactor(0, 1));

            Console.WriteLine("Cofactor (0,2)");
            Console.WriteLine(mat4.Cofactor(0, 2));

            Console.WriteLine("Cofactor (0,3)");
            Console.WriteLine(mat4.Cofactor(0, 3));

            Console.WriteLine("Cofactor (1,1)");
            Console.WriteLine(mat4.Cofactor(1, 1));
        }

        public static void TestDet()
        {
            Mat2 mat2 = new Mat2(1, 2, 3, 4);
            Mat3 mat3 = new Mat3(1, 2, 6, -5, 8, -4, 2, 6, 4);
            Mat4 mat4 = new Mat4(-2, -8, 3, 5, -3, 1, 7, 3, 1, 2, -9, 6, -6, 7, 7, -9);
            Console.WriteLine("\nTest Determinant");
            Console.WriteLine("Matrix 2x2");
            Console.WriteLine(mat2);
            Console.WriteLine("Determinant 2x2");
            Console.WriteLine(mat2.Det()+"\n");

            Console.WriteLine("Matrix 3x3");
            Console.WriteLine(mat3);
            Console.WriteLine("Cofactor (0,0)");
            Console.WriteLine(mat3.Cofactor(0, 0));
            Console.WriteLine("Cofactor (0,1)");
            Console.WriteLine(mat3.Cofactor(0, 1));
            Console.WriteLine("Cofactor (0,2)");
            Console.WriteLine(mat3.Cofactor(0, 2));
            Console.WriteLine("Determinant 3x3");
            Console.WriteLine(mat3.Det() + "\n");

            Console.WriteLine("Matrix 4x4");
            Console.WriteLine(mat4);
            Console.WriteLine("Cofactor (0,0)");
            Console.WriteLine(mat4.Cofactor(0, 0));
            Console.WriteLine("Cofactor (0,1)");
            Console.WriteLine(mat4.Cofactor(0, 1));
            Console.WriteLine("Cofactor (0,2)");
            Console.WriteLine(mat4.Cofactor(0, 2));
            Console.WriteLine("Cofactor (0,3)");
            Console.WriteLine(mat4.Cofactor(0, 3));
            Console.WriteLine("Determinant 3x3");
            Console.WriteLine(mat4.Det() + "\n");
        }

        public static void TestInverseMatrix()
        {
            Mat2 mat2 = new Mat2(1, 2, 3, 4);
            Mat2 inv2 = new Mat2();
            Mat2 inv22 = new Mat2();
            Mat3 mat3 = new Mat3(1, 2, 6, -5, 8, -4, 2, 6, 4);
            Mat3 inv3 = new Mat3();
            Mat3 inv33 = new Mat3();
            Mat4 mat4 = new Mat4(-2, -8, 3, 5, -3, 1, 7, 3, 1, 2, -9, 6, -6, 7, 7, -9);
            Mat4 inv4 = new Mat4();
            Mat4 inv44 = new Mat4();

            Console.WriteLine("\nTest Inverse matrix");
            Console.WriteLine("Matrix 2x2");
            Console.WriteLine(mat2);
            Console.WriteLine("\nInverse matrix 2x2");
            inv2 = mat2.Inverse();
            inv22 = mat2.Inverse_10_percent_faster();
            Console.WriteLine(inv2 + "\n");
            Console.WriteLine(inv2 * mat2 + "\n");
            Console.WriteLine(inv22 + "\n");
            Console.WriteLine(inv22 * mat2 + "\n");

            Console.WriteLine("\nMatrix 3x3");
            Console.WriteLine(mat3);
            Console.WriteLine("\nInverse matrix 3x3");
            inv3 = mat3.Inverse();
            inv33 = mat3.Inverse_10_percent_faster();
            Console.WriteLine(inv3 + "\n");
            Console.WriteLine(inv3 * mat3 + "\n");
            Console.WriteLine(inv33 + "\n");
            Console.WriteLine(inv33 * mat3 + "\n");

            Console.WriteLine("\nMatrix 4x4");
            Console.WriteLine(mat4);
            Console.WriteLine("\nInverse matrix 4x4");
            inv4 = mat4.Inverse();
            inv44 = mat4.Inverse_10_percent_faster();
            Console.WriteLine(inv4 + "\n");
            Console.WriteLine(inv4 * mat4 + "\n");
            Console.WriteLine(inv44 + "\n");
            Console.WriteLine(inv44 * mat4 + "\n");
        }

        public static void TestTransformMatrix()
        {
            Point p = new Point(1.0, 2.0, 3.0);
            Vector v = new Vector(1.0, 2.0, 3.0);

            Console.WriteLine("\nTest Transform Matrix");
            Console.WriteLine("Translate Matrix");
            Mat4 translateMatrix = Mat4.TranslateMatrix(1, 2, 3);
            Console.WriteLine(translateMatrix);
            Console.WriteLine(translateMatrix * p);
            Console.WriteLine(translateMatrix * v);
            Console.WriteLine("\nScale Matrix");
            Mat4 scaleMatrix = Mat4.ScaleMatrix(4, 5, 6);
            Console.WriteLine(scaleMatrix);
            Console.WriteLine(scaleMatrix * p);
            Console.WriteLine(scaleMatrix * v);
            Console.WriteLine("\nRx Matrix");
            Mat4 rxMatrix = Mat4.RotateXMatrix(Math.PI / 2.0);
            Console.WriteLine(rxMatrix);
            Console.WriteLine(rxMatrix * p);
            Console.WriteLine(rxMatrix * v);
            Console.WriteLine("\nRy Matrix");
            Mat4 ryMatrix = Mat4.RotateYMatrix(Math.PI / 2.0);
            Console.WriteLine(ryMatrix);
            Console.WriteLine(ryMatrix * p);
            Console.WriteLine(ryMatrix * v);
            Console.WriteLine("\nRz Matrix");
            Mat4 rzMatrix = Mat4.RotateZMatrix(Math.PI / 2.0);
            Console.WriteLine(rzMatrix);
            Console.WriteLine(rzMatrix * p);
            Console.WriteLine(rzMatrix * v);
            Console.WriteLine("\nR Matrix");
            Mat4 rMatrix = Mat4.RotateMatrix(Math.PI / 2.0, Math.PI / 2.0, Math.PI / 2.0);
            Console.WriteLine(rMatrix);
            Console.WriteLine(rMatrix * p);
            Console.WriteLine(rMatrix * v);
            Console.WriteLine("\nShear Matrix");
            Mat4 shearMatrix = Mat4.ShearMatrix(1, 0, 0, 0, 0, 0);
            Console.WriteLine(shearMatrix);
            Console.WriteLine(shearMatrix * p);
            Console.WriteLine(shearMatrix * v);
        }

        public static void TestTransformMatrixFluency()
        {
            Console.WriteLine("\nTest Transform Matrix Fluency");

            Mat4 mat4 = new Mat4();
            Console.WriteLine(mat4 + "\n");
            mat4.Scale(2, 2, 2).Translate(5, 5, 5);
            Console.WriteLine(mat4 + "\n");

            mat4 = new Mat4();
            Console.WriteLine(mat4 + "\n");
            mat4.Translate(5, 5, 5).Scale(2, 2, 2);
            Console.WriteLine(mat4 + "\n");
        }

        public static void Chapter4TransformChallenge()
        {
            Console.WriteLine("\nChapter 4 Challeng");
            // Create canvas of set size and width.
            // Creat Transform that first offsets a point by 1/2 canvas size then
            // rotates the objects by 1/12th of 2*pi through 12 iterations.
            // At each location draw on the canvas a circle.
            int circleRadius = 5;
            Canvas canvas = new Canvas(100, 100);
            Point currentLocation = new Point();

            // Offset 1/3 distance of canvas size.
            currentLocation = Mat4.TranslateMatrix(canvas.GetWidth() * 0.3, 0.0, 0.0) * currentLocation;   // Rayon du cadrent de l'horloge par rapport à la taille du canvas.

            // Rotate loop.
            int maxIterations = 12;
            for (int r = 0; r < maxIterations; r++)
            {
                currentLocation = Mat4.RotateZMatrix(2.0 * Math.PI * (1.0 / maxIterations)) * currentLocation;

                // Offset current location so that it is centered in the image by 1/2 width and height through translation.
                Point screenSpaceLocation = Mat4.TranslateMatrix(canvas.GetWidth() * 0.5, canvas.GetHeight() * 0.5, 0.0) * currentLocation;

                Console.WriteLine("Point : " + r.ToString());
                Console.WriteLine(screenSpaceLocation);

                // Draw circle at current location.
                Color color = new Color(((double)r /(double)12), 0, 0);
                canvas.DrawCircle((int)screenSpaceLocation.x, (int)screenSpaceLocation.y, circleRadius, color);
            }
            Save.SaveCanvas(canvas, "Chapter_4_TransformChallenge");
        }

        public static void TestRay()
        {
            Ray ray = new Ray(new Point(2, 3, 4),
                              new Vector(1, 0, 0));
            Console.WriteLine("\nTest Ray");
            Console.WriteLine("Ray Constructor");
            Console.WriteLine(ray);

            Console.WriteLine("\nRay Position");
            Console.WriteLine(ray.Position(0.0));
            Console.WriteLine(ray.Position(1.0));
            Console.WriteLine(ray.Position(-1.0));
            Console.WriteLine(ray.Position(2.5));
        }

        public static void TestRayTransform()
        {
            Console.WriteLine("\nTest Ray Transform");
            Console.WriteLine("Ray Translation");
            Ray ray = new Ray(new Point(1, 2, 3),
                              new Vector(0, 1, 0));
            Mat4 matTrans = Mat4.TranslateMatrix(3, 4, 5);

            Ray transRay = Ray.Transform(ray, matTrans);
            Console.WriteLine(transRay);

            Console.WriteLine("Ray Scaling");
            Mat4 matScale = Mat4.ScaleMatrix(2, 3, 4);
            transRay = Ray.Transform(ray, matScale);
            Console.WriteLine(transRay);
        }

        public static void TestSphereIntersect()
        {
            Console.WriteLine("\nTest Sphere Intersection");
            Console.WriteLine("Sphere Intersection Through Center");
            Ray ray = new Ray(new Point(0.0, 0.0, -5.0),
                              new Vector(0.0, 0.0, 1.0));
            Sphere sphere = new Sphere();

            //List<double> intersections = sphere.Intersect(ray);
            List<Intersection> intersections = sphere.Intersect(ray);

            foreach (Intersection value in intersections)
            {
                Console.WriteLine(value.t);
            }

            Console.WriteLine("\nSphere Intersection Through Tangent");
            Ray ray2 = new Ray(new Point(0.0, 1.0, -5.0),
                              new Vector(0.0, 0.0, 1.0));
            Sphere sphere2 = new Sphere();

            //List<double> intersections2 = sphere2.Intersect(ray2);
            List<Intersection> intersections2 = sphere2.Intersect(ray2);

            foreach (Intersection value in intersections2)
            {
                Console.WriteLine(value);
            }

            Console.WriteLine("\nSphere Miss");
            Ray ray3 = new Ray(new Point(0.0, 2.0, -5.0),
                              new Vector(0.0, 0.0, 1.0));
            Sphere sphere3 = new Sphere();

            //List<double> intersections3 = sphere3.Intersect(ray3);
            List<Intersection> intersections3 = sphere3.Intersect(ray3);

            if (intersections3 != null)
            {
                foreach (Intersection value in intersections3)
                {
                    Console.WriteLine(ray3.Position(value.t));
                }
            }
            Console.WriteLine("\nSphere Ray Inside");
            Ray ray4 = new Ray(new Point(0.0, 0.0, 0.0),
                              new Vector(0.0, 0.0, 1.0));
            Sphere sphere4 = new Sphere();

            //List<double> intersections4 = sphere4.Intersect(ray4);
            List<Intersection> intersections4 = sphere4.Intersect(ray4);

            foreach (Intersection value in intersections4)
            {
                Console.WriteLine(value);
            }

            Console.WriteLine("\nSphere Behind Ray");
            Ray ray5 = new Ray(new Point(0.0, 0.0, 5.0),
                              new Vector(0.0, 0.0, 1.0));
            Sphere sphere5 = new Sphere();

            //List<double> intersections5 = sphere5.Intersect(ray5);
            List<Intersection> intersections5 = sphere5.Intersect(ray5);

            foreach (Intersection value in intersections5)
            {
                Console.WriteLine(value);
            }

            Console.WriteLine("Sphere Intersection Through Center but on a transformed sphere");
            Ray ray6 = new Ray(new Point(0.0, 0.0, -5.0),
                              new Vector(0.0, 0.0, 1.0));
            Sphere sphere6 = new Sphere();
            sphere6.SetMatrix(Mat4.TranslateMatrix(0, 0, 2) * sphere6.GetMatrix());
            Console.WriteLine(sphere6.GetPosition());
            Console.WriteLine(sphere6.GetMatrix());
            Console.WriteLine(sphere6.ToString());
            //List<double> intersections6 = sphere6.Intersect(ray6);
            List<Intersection> intersections6 = sphere6.Intersect(ray6);

            foreach (Intersection value in intersections6)
            {
                Console.WriteLine(value);
            }
        }

        public static void TestIntersection()
        {
            Console.WriteLine("\nTest Intersections");
            Scene scene = new Scene();
            Ray ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            RayObject sphere = new Sphere();
            List<Intersection>  temp = scene.Intersections(ray);
            Console.WriteLine("Number Intersections Found : " + temp.Count.ToString());
            foreach (Intersection i in temp)
            {
                Console.WriteLine(i);
            }
        }

        public static void TestHit()
        {
            Console.WriteLine("\nTest Hit");
            Scene scene = new Scene();
            Sphere sphere =new Sphere();
            Ray ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            Intersection i = Scene.current.Hit(scene.Intersections(ray));
            Console.WriteLine("Hit : " + i.ToString());
        }

        public static void Chapter5Challenge()
        {
            Console.WriteLine("\nChapter 5 Challeng");
            Scene scene = new Scene();
            Mat4 transmatrix = new Mat4();
            //transmatrix = Mat4.ScaleMatrix(1, 0.5, 1);
            //transmatrix = Mat4.ScaleMatrix(1.0, 1, 1);
            //transmatrix = Mat4.RotateMatrix(0.0, 0.0, Math.PI * 0.25) * Mat4.ScaleMatrix(1, 0.5, 1);
            transmatrix = Mat4.ShearMatrix(1, 0, 0, 0, 0, 0) * Mat4.ScaleMatrix(0.5, 1, 1);

            int resolution = 100;
            Canvas canvas = new Canvas(resolution, resolution);
            canvas.FillCanvas(Color.black);
            
            Sphere sphere = new Sphere();
            sphere.SetMatrix(transmatrix);

            Point camera = new Point(0, 0, -5);
            
            Point wall = new Point(0, 0, 10);
            double wallSize = 10.0;
            double increment = wallSize / resolution;
            double half = wallSize / 2;
            for (int y = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    Point currentWallPixel = new Point(wall - new Point(half - x * increment,
                                                                        half - y * increment,
                                                                        wall.z));
                    Vector direction = (currentWallPixel - camera).Normalize();
                    Ray ray = new Ray(camera, direction);

                    Intersection hit = Scene.current.Hit(scene.Intersections(ray));

                    if (hit != null)
                    {
                        canvas.SetPixel(x, y, Color.red);
                    }
                }
            }

            Save.SaveCanvas(canvas, "Chapter_5_Challenge");
            Console.WriteLine("Image created.");


            /*Scene scene = new Scene();
            // Create an image of a sphere by only testing for hits or misses.

            // Scaling matrix
            Mat4 transMatrix = new Mat4();
            transMatrix = Mat4.ShearMatrix(1, 0, 0, 0, 0, 0) * Mat4.ScaleMatrix(0.5, 1, 1);

            int resolution = 320;
            Canvas canvas = new Canvas(resolution, resolution);
            //canvas.FillCanvas(Color.black);

            Sphere sphere = new Sphere();
            sphere.SetMatrix(transMatrix);

            Point camera = new Point(0, 0, -5);

            // Use the wall x and y as the width and height and the position of the wall as the z value.
            Point wall = new Point(0, 0, 7);
            double wallSize = 7;

            // Camera is the start point, rays are created by taking iterating over the wall in resultion steps
            // vertically and horizontally, calc wall - camera to get direction of camera to wall location.
            // Chech if the ray hits the sphere, if it does draw red if it does not draw (black).

            for (int y = 0; y < canvas.GetHeight(); y++)
            {
                for (int x = 0; x < canvas.GetWidth(); x++)
                {
                    // Need to start at half the width over form the walls origin and increment from there.
                    double increment = wallSize / resolution;
                    Vector currentWallPixel = wall - new Point(wallSize * 0.5 - x * increment,
                                                               wallSize * 0.5 - y * increment,
                                                               wall.z);
                    Point point = currentWallPixel - camera;
                    Vector direction = new Vector(point).Normalize();

                    Ray ray = new Ray(camera, direction);

                    List<Intersection> intersections = Scene.current.Intersections(ray);
                    Intersection hit2 = Scene.current.Hit2(intersections);

                    if (hit2 != null)
                    {
                        canvas.SetPixel(x, y, Color.red);
                    }
                }
            }
            Save.SaveCanvas(canvas, "Chapter5_Challenge");
            Scene.current.Clear();
            Console.WriteLine("Image created.");
            */
        }

        public static void TestSphereNormals()
        {
            Console.WriteLine("\nTest Sphere Normals");
            Console.WriteLine("Non-transformed Sphere Normals");
            Sphere sphere = new Sphere();
            Vector normal = sphere.GetNormal(new Point(1, 0, 0));
            Console.WriteLine(normal);
            normal = sphere.GetNormal(new Point(0, 1, 0));
            Console.WriteLine(normal);
            normal = sphere.GetNormal(new Point(0, 0, 1));
            Console.WriteLine(normal);
            normal = sphere.GetNormal(new Point(Math.Sqrt(3) / 3.0,
                                                Math.Sqrt(3) / 3.0,
                                                Math.Sqrt(3) / 3.0));
            Console.WriteLine(normal);

            Console.WriteLine("\nTransformed Sphere Normals");
            Mat4 temp = Mat4.TranslateMatrix(0, 1, 0);
            Sphere sphere2 = new Sphere();
            sphere2.SetMatrix(temp);
            Vector normal2 = sphere2.GetNormal(new Point(0, 1 + Math.Sqrt(2) * 0.5, -Math.Sqrt(2) * 0.5));
            Console.WriteLine(normal2);

            //Console.WriteLine("\n\n\nTransformed Sphere Normals");
            Sphere sphere3 = new Sphere();
            Mat4 mat3 = Mat4.ScaleMatrix(1, 0.5, 1) * Mat4.RotateZMatrix(Math.PI / 5.0);
            sphere3.SetMatrix(mat3);
            Vector normal3 = sphere3.GetNormal(new Point(0, Math.Sqrt(2) * 0.5, -Math.Sqrt(2) * 0.5));
            Console.WriteLine(normal3);
        }

        public static void TestReflect()
        {
            Console.WriteLine("\nTest Reflection");
            Vector incoming = new Vector(1, -1, 0);
            Vector normal = new Vector(0, 1, 0);
            Console.WriteLine("Should be : " + new Vector(1, 1, 0) + ", I get : " + Vector.Reflect(incoming, normal));

            incoming = new Vector(0, -1, 0);
            normal = new Vector(Math.Sqrt(2) * 0.5, Math.Sqrt(2) * 0.5, 0);
            Console.WriteLine("Should be : " + new Vector(1, 0, 0) + ", I get : " + Vector.Reflect(incoming, normal));
        }

        public static void TestLight()
        {
            Console.WriteLine("\nTest Light");

            Light pointLight = new Light();
            Console.WriteLine(pointLight);

            pointLight = new Light(new Point(1, 5, -5), Color.green);
            Console.WriteLine(pointLight);
        }

        public static void TestMaterial()
        {
            Console.WriteLine("\nTest Material");
            Material material = new Material();
            material.color = Color.blue;
            material.Ambient = 0.1;
            material.Diffuse = 0.9;
            material.Specular = 0.9;
            material.Shininess = 200;
            Console.WriteLine(material);
        }

        public static void TestRayObjectMaterial()
        {
            Console.WriteLine("\nTest Ray Object Material");
            Sphere sphere = new Sphere();
            sphere.material.Ambient = 1;
            sphere.material.Shininess = 50;
            Console.WriteLine("Sphere Material : " + sphere.material.ToString());
        }

        public static void TestLightCalc()
        {
            Console.WriteLine("\nTest Ray Object Light - Phong");
            Sphere sphere = new Sphere();
            sphere.material.Ambient = 0.1;
            sphere.material.Diffuse = 0.9;
            sphere.material.Specular = 0.9;
            sphere.material.Shininess = 200;
            Point point = new Point(0, 0, 0);

            //Direct lighting
            Vector eye = new Vector(0, 0, -1);
            Vector normal = new Vector(0, 0, -1);
            Light light = new Light(new Point(0, 0, -10), Color.white);
            Color output = sphere.Lighting(point, light, eye, normal);
            Console.WriteLine("Face on : " + output.ToString() + "\n");

            //Off angle lighting
            eye = new Vector(0, Math.Sqrt(2) / 2.0, - Math.Sqrt(2) / 2.0);
            output = sphere.Lighting(point, light, eye, normal);
            Console.WriteLine("Glancing : " + output.ToString()+"\n");

            //Eye opposite surface, light offset 45
            eye = new Vector(0, 0, -1.0);
            light.position = new Point(0, 10, -10);
            output = sphere.Lighting(point, light, eye, normal);
            Console.WriteLine("Opposite 45 : " + output.ToString()+"\n");

            //Eye opposite reflection vector
            eye = new Vector(0, -Math.Sqrt(2) / 2.0, -Math.Sqrt(2) / 2.0);
            output = sphere.Lighting(point, light, eye, normal);
            Console.WriteLine("Path of Reflection vec : " + output.ToString()+"\n");

            //Lighting Behind Surface
            eye = new Vector(0, 0, -1.0);
            light.position = new Point(0, 0, 10);
            output = sphere.Lighting(point, light, eye, normal);
            Console.WriteLine("Lighting Behind Surface : " + output.ToString()+"\n");

        }

        public static void Chapter6Challenge()
        {
            Console.WriteLine("\nChapter 6 Challeng");
            Scene scene = new Scene();
            Mat4 transmatrix = new Mat4();
            transmatrix = Mat4.ScaleMatrix(2, 2, 2);
            //transmatrix = Mat4.ScaleMatrix(1.0, 1, 1);
            //transmatrix = Mat4.RotateMatrix(0.0, 0.0, Math.PI * 0.25) * Mat4.ScaleMatrix(1, 0.5, 1);
            //transmatrix = Mat4.ShearMatrix(1, 0, 0, 0, 0, 0) * Mat4.ScaleMatrix(0.5, 1, 1);

            int resolution = 512;
            Canvas canvas = new Canvas(resolution, resolution);
            canvas.FillCanvas(Color.black);

            Light light = new Light(new Point(-10, 10, -10), Color.white);

            Sphere sphere = new Sphere();
            sphere.material.color = new Color(1, 0.2, 1);
            sphere.material.Ambient = 0.1;
            sphere.material.Diffuse = 0.9;
            sphere.material.Specular = 0.9;
            sphere.SetMatrix(transmatrix);

            Point camera = new Point(0, 0, -5);

            Point wall = new Point(0, 0, 10);
            double wallSize = 10.0;

            for (int y = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    double increment = wallSize / resolution;
                    Point currentWallPixel = new Point(wall - new Point((wallSize * 0.5) - x * increment,
                                                                        (wallSize * 0.5) - y * increment,
                                                                        wall.z));
                    
                    Vector direction = (currentWallPixel - camera).Normalize();
                    Ray ray = new Ray(camera, direction);

                    Intersection hit = Scene.current.Hit(scene.Intersections(ray));

                    if (hit != null)
                    {
                        Point hitPosition = ray.Position(hit.t);
                        Color lighting = sphere.Lighting(hitPosition,
                                                         light,
                                                         -ray.direction,
                                                         sphere.GetNormal(hitPosition));
                        canvas.SetPixel(x, y, lighting);
                    }
                }
            }

            Save.SaveCanvas(canvas, "Chapter_6_Challenge");
            Console.WriteLine("Image created.");
        }

        public static void TestIntersectWorld()
        {
            Console.WriteLine("\nTest Interscet World");
            
            Scene scene = new Scene();
            scene.Default();
            Ray ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));

            List<Intersection> temp = scene.Intersections(ray);

            foreach (Intersection i in temp)
            {
                Console.WriteLine(i);
            }
        }

        public static void TestPerso()
        {
            Mat4 mat = new Mat4(2, 2, 0, 5,
                                7, 1.5, 0, 1,
                                3, -0.5, 2, -2,
                                0, 0, 0, 1);

            Console.WriteLine("mat : \n" + mat);
            Mat4 invert = new Mat4(-0.13636, 0.181818, 0, 0.5,
                                   0.636363, -0.181818, 0, -3,
                                   0.363636, -0.318181, 0.5, -0.5,
                                   0, 0, 0, 1);
            Console.WriteLine("invert : \n" + invert);

            Mat4 res1 = mat.Inverse().Transpose();
            Mat4 res2 = mat.Inverse();
            Console.WriteLine("mat.Inverse() : \n" + res2);
            res2 = res2.Transpose();
            Console.WriteLine("(mat.Inverse()).Transpose() : \n" + res2);
            Console.WriteLine("mat.Inverse().Transpose() : \n" + res1);
            Mat4 res3 = mat.Transpose();
            Console.WriteLine("mat.Transpose() : \n" + res3);
            res3 = res3.Inverse();
            Console.WriteLine("(mat.Transpose()).Inverse() : \n" + res3);
        }


        public static void Main(string[] args)
        {
            //TestInverse_vs_Inverse_10_percent_faster_3x3();
            //TestInverse_vs_Inverse_10_percent_faster_4x4();

            // Chapter 1
            //TestPointConstructor();
            //TestVectorConstructor();
            //TestAddition();
            //TestSubstraction();
            //TestNegation();
            //TestScalarMultiplication();
            //TestMagnitude();
            //TestNormalize();
            //TestDot();
            //TestCross();
            //TestTupleEquality();

            // Chapter 2
            //TestColorConstructor();
            //TestColorOperators();
            //TestCanvas();
            //TestWritePPM();

            // Chapter 3
            //TestMatrixConstructor();
            //TestMatrixEquality();
            //TestMatrixMultiplication();
            //TestMatrixTranspose();
            //TestSubMatrix();
            //TestMinorMatrix();
            //TestCofactor();
            //TestDet();
            //TestInverseMatrix();

            // Chapter 4
            //TestTransformMatrix();
            //TestTransformMatrixFluency();
            //Chapter4TransformChallenge();

            // Chapter 5
            //TestRay();
            //TestRayTransform();
            //TestSphereIntersect();
            //TestIntersection();
            //TestHit();
            //Chapter5Challenge();

            // Chapter 6
            //TestSphereNormals();
            //TestReflect();
            //TestLight();
            //TestMaterial();
            //TestRayObjectMaterial();
            //TestLightCalc();
            //Chapter6Challenge();

            //TestPerso();

            // Chapter 7
            //TestIntersectWorld();
            //UnitTesting.Chapter07Test chapter7Test = new UnitTesting.Chapter07Test();
            //chapter7Test.T20_RenderWorldWithCamera();
            //chapter7Test.T21_PuttingItAllTogether();

            // Chapter 8
            //UnitTesting.Chapter08Test chapter8Test = new UnitTesting.Chapter08Test();
            //chapter8Test.T08_PuttingItAllTogether();

            // Chapter 9
            //UnitTesting.Chapter09Test chapter9Test  = new UnitTesting.Chapter09Test();
            //chapter9Test.T07_PuttingItTogether();

            //chapter9Test.T04_NormalOfPlane();

            // Chapter 14
            //UnitTesting.Chapter14Test chapter14Test = new UnitTesting.Chapter14Test();
            //chapter14Test.T12_RenderingLotsOfObjects();
            //chapter14Test.T13_PuttingItAllTogether();

            // Chapter 15
            UnitTesting.Chapter15Test chapter15Test = new UnitTesting.Chapter15Test();
            chapter15Test.T11_MayaOBJFile();
            

            Console.ReadKey();
        }
    }
}
