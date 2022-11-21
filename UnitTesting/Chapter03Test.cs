using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.UnitTesting
{
    [TestFixture]
    class Chapter03Test
    {
        [Test, Order(1)]
        public void T01_MatrixConstructorIndexer()
        {
            Mat2 mat2 = new Mat2(-3, 5,
                                 1, -1);

            Assert.AreEqual(-3, mat2[0, 0]);
            Assert.AreEqual(5, mat2[0, 1]);
            Assert.AreEqual(1, mat2[1, 0]);
            Assert.AreEqual(-1, mat2[1, 1]);
            mat2[0, 0] = -2;
            Assert.AreEqual(-2, mat2[0, 0]);

            Mat3 mat3 = new Mat3(-3, 5, 0,
                                 1, -2, -7,
                                 0, 1, 1);

            Assert.AreEqual(-3, mat3[0, 0]);
            Assert.AreEqual(-7, mat3[1, 2]);
            Assert.AreEqual(1, mat3[2, 2]);
            mat3[2, 1] = -15.0;
            Assert.AreEqual(-15, mat3[2, 1]);

            Mat4 mat4 = new Mat4(1, 2, 3, 4,
                                 5.5, 6.5, 7.5, 8.5,
                                 9, 10, 11, 12,
                                 13.5, 14.5, 15.5, 16.5);

            Assert.AreEqual(1, mat4[0, 0]);
            Assert.AreEqual(4, mat4[0, 3]);
            Assert.AreEqual(5.5, mat4[1, 0]);
            Assert.AreEqual(7.5, mat4[1, 2]);
            Assert.AreEqual(11, mat4[2, 2]);
            Assert.AreEqual(13.5, mat4[3, 0]);
            Assert.AreEqual(15.5, mat4[3, 2]);
            mat4[1, 3] = -4565.0998;
            Assert.AreEqual(-4565.0998, mat4[1, 3]);
        }

        [Test, Order(2)]
        public void T02_MatrixEquality()
        {
            Mat2 mat2a = new Mat2(1, 2, 3, 4);
            Mat2 mat2b = new Mat2(1, 2, 3, 4);
            Assert.AreEqual(mat2a, mat2b);

            mat2b = new Mat2(2, 3,
                             4, 5);
            Assert.AreNotEqual(mat2a, mat2b);

            Mat3 mat3a = new Mat3(1, 2, 3,
                                  4, 5, 6,
                                  7, 8, 9);
            Mat3 mat3b = new Mat3(1, 2, 3,
                                  4, 5, 6,
                                  7, 8, 9);
            Assert.AreEqual(mat3a, mat3b);

            mat3b = new Mat3(2, 3, 4,
                             5, 6, 7,
                             8, 9, 10);
            Assert.AreNotEqual(mat3a, mat3b);

            Mat4 mat4a = new Mat4(1, 2, 3, 4, 5, 6, 7, 8, 9, 8, 7, 6, 5, 4, 3, 2);
            Mat4 mat4b = new Mat4(1, 2, 3, 4, 5, 6, 7, 8, 9, 8, 7, 6, 5, 4, 3, 2);
            Assert.AreEqual(mat4a, mat4b);

            mat4b = new Mat4(2, 3, 4, 5, 6, 7, 8, 9, 8, 7, 6, 5, 4, 3, 2, 1);
            Assert.AreNotEqual(mat4a, mat4b);
        }

        [Test, Order(3)]
        public void T03_MatrixMultiplication()
        {
            Mat2 mat2a = new Mat2(4, 5,9, 10);
            Mat2 mat2b = new Mat2(2, 3, 4, 5);
            Assert.AreEqual(new Mat2(28, 37, 58, 77), mat2a * mat2b);

            Mat3 mat3a = new Mat3(5, 7, 0,
                                  0, 4, 7,
                                  0.5, 1.8, 9);
            Mat3 mat3b = new Mat3(1, 2, 6,
                                  -5, 8, -4,
                                  2, 6, 4);
            Assert.AreEqual(new Mat3(-30, 66, 2,
                                     -6, 74, 12,
                                     9.5, 69.4, 31.8), mat3a * mat3b);

            Mat4 mat4a = new Mat4(1, 2, 3, 4, 5, 6, 7, 8, 9, 8, 7, 6, 5, 4, 3, 2);
            Mat4 mat4b = new Mat4(-2, 1, 2, 3, 3, 2, 1, -1, 4, 3, 6, 5, 1, 2, 7, 8);
            Assert.AreEqual(new Mat4(20, 22, 50, 48, 44, 54, 114, 108, 40, 58, 110, 102, 16, 26, 46, 42), mat4a * mat4b);

            Assert.AreEqual(new Mat2(16, 20, 36, 40), mat2a * 4);
            Assert.AreEqual(new Mat3(25, 35, 0, 0, 20, 35, 2.5, 9, 45), mat3a * 5);
            Assert.AreEqual(new Mat4(2, 4, 6, 8, 10, 12, 14 ,16, 18, 16, 14, 12, 10, 8, 6, 4), mat4a * 2);
        }

        [Test, Order(4)]
        public void T04_MatrixTimesPointAndVector()
        {
            Mat4 mat = new Mat4(1, 2, 3, 4,
                                2, 4, 4, 2,
                                8, 6, 4, 1,
                                0, 0, 0, 1);
            Vector v = new Vector(1, 2, 3);
            Point p = new Point(1, 2, 3);

            Assert.AreEqual(new Vector(14, 22, 32), mat * v);
            Assert.AreEqual(new Point(18, 24, 33), mat * p);
        }

        [Test, Order(5)]
        public void T05_IdentityMatrix()
        {
            Mat4 a = new Mat4(1, 2, 3, 4,
                              2, 4, 4, 2,
                              8, 6, 4, 1,
                              0, 0, 0, 1);
            Assert.AreEqual(a, a * new Mat4());
            //Assert.AreEqual(a, a * new Mat4().Identity());

            Mat3 b = new Mat3(1, 2, 3,
                              2, 4, 4,
                              8, 6, 4);
            Assert.AreEqual(b, b * new Mat3());

            Mat2 c = new Mat2(1, 2,
                              2, 4);
            Assert.AreEqual(c, c * new Mat2());
        }

        [Test, Order(6)]
        public void T06_Transpose()
        {
            Mat4 mat4 = new Mat4(1, 2, 3, 4,
                                 2, 4, 4, 2,
                                 8, 6, 4, 1,
                                 0, 0, 0, 1);
            Mat4 Tmat4 = new Mat4(1, 2, 8, 0,
                                  2, 4, 6, 0,
                                  3, 4, 4, 0,
                                  4, 2, 1, 1);
            Assert.AreEqual(Tmat4, mat4.Transpose());

            Mat3 mat3 = new Mat3(1, 2, 3,
                                 2, 4, 4,
                                 8, 6, 4);
            Mat3 Tmat3 = new Mat3(1, 2, 8,
                                  2, 4, 6,
                                  3, 4, 4);
            Assert.AreEqual(Tmat3, mat3.Transpose());

            Mat2 mat2 = new Mat2(1, 2,
                                 3, 4);
            Mat2 Tmat2 = new Mat2(1, 3,
                                  2, 4);
            Assert.AreEqual(Tmat2, mat2.Transpose());
        }

        [Test, Order(7)]
        public void T07_SubMatrix()
        {
            Mat3 mat3 = new Mat3(1, 5, 0,
                                 -3, 2, 7,
                                 0, 6, -3);
            Assert.AreEqual(new Mat2(-3, 2, 0, 6), mat3.Sub(0, 2));

            Mat4 mat4 = new Mat4(-6, 1, 1, 6,
                                 -8, 5, 8, 6,
                                 -1, 0, 8, 2,
                                 -7, 1, -1, 1);
            Assert.AreEqual(new Mat3(-6, 1, 6, -8, 8, 6, -7, -1, 1), mat4.Sub(2, 1));
            Assert.AreEqual(new Mat2(8, 6, -1, 1), mat4.Sub(2, 1).Sub(0,0));
        }

        [Test, Order(8)]
        public void T08_Minors()
        {
            Mat3 mat3 = new Mat3(3, 5, 0,
                                 2, -1, -7,
                                 6, -1, 5);

            Mat2 mat2 = mat3.Sub(1, 0);
            Assert.AreEqual(mat2.Det(), mat3.Minor(1, 0));
            Assert.AreEqual(25, mat2.Det());
            Assert.AreEqual(25, mat3.Minor(1, 0));

            mat2 = mat3.Sub(0, 1);
            Assert.AreEqual(mat2.Det(), mat3.Minor(0, 1));

            mat2 = mat3.Sub(1, 2);
            Assert.AreEqual(mat2.Det(), mat3.Minor(1, 2));

            Mat4 mat4 = new Mat4(-2, -8, 3, 5,
                                 -3, 1, 7, 3,
                                 1, 2, -9, 6,
                                 -6, 7, 7, -9);

            Mat3 mat3b = mat4.Sub(0, 0);
            Assert.AreEqual(mat3b.Det(), mat4.Minor(0, 0));

            mat3b = mat4.Sub(1, 0);
            Assert.AreEqual(mat3b.Det(), mat4.Minor(1, 0));

            mat3b = mat4.Sub(0, 1);
            Assert.AreEqual(mat3b.Det(), mat4.Minor(0, 1));

            mat3b = mat4.Sub(1, 2);
            Assert.AreEqual(mat3b.Det(), mat4.Minor(1, 2));
        }

        [Test, Order(9)]
        public void T09_Cofactors()
        {
            Mat3 mat3 = new Mat3(3, 5, 0,
                                 2, -1, -7,
                                 6, -1, 5);
            Assert.AreEqual(-12, mat3.Minor(0, 0));
            Assert.AreEqual(-12, mat3.Cofactor(0, 0));
            Assert.AreEqual(25, mat3.Minor(1, 0));
            Assert.AreEqual(-25, mat3.Cofactor(1, 0));

            Mat4 mat4 = new Mat4(-2, -8, 3, 5,
                                 -3, 1, 7, 3,
                                 1, 2, -9, 6,
                                 -6, 7, 7, -9);
            Assert.AreEqual(mat3.Minor(0, 0), mat3.Cofactor(0, 0));
            Assert.AreEqual(mat3.Minor(1, 0), -mat3.Cofactor(1, 0));
        }

        [Test, Order(10)]
        public void T10_Det()
        {
            Mat2 mat2 = new Mat2(1, 5,
                                 -3, 2);
            Assert.AreEqual(17, mat2.Det());

            Mat3 mat3 = new Mat3(1, 2, 6,
                                 -5, 8, -4,
                                 2, 6, 4);
            Assert.AreEqual(56, mat3.Cofactor(0, 0));
            Assert.AreEqual(12, mat3.Cofactor(0, 1));
            Assert.AreEqual(-46, mat3.Cofactor(0, 2));
            Assert.AreEqual(mat3[0, 0] * mat3.Cofactor(0, 0) +
                            mat3[1, 0] * mat3.Cofactor(1, 0) +
                            mat3[2, 0] * mat3.Cofactor(2, 0), mat3.Det());
            Assert.AreEqual(mat3[0, 1] * mat3.Cofactor(0, 1) +
                            mat3[1, 1] * mat3.Cofactor(1, 1) +
                            mat3[2, 1] * mat3.Cofactor(2, 1), mat3.Det());
            Assert.AreEqual(-196, mat3.Det());

            Mat4 mat4 = new Mat4(-2, -8, 3, 5,
                                 -3, 1, 7, 3,
                                 1, 2, -9, 6,
                                 -6, 7, 7, -9);
            Assert.AreEqual(mat4[0, 0] * mat4.Cofactor(0, 0) +
                            mat4[1, 0] * mat4.Cofactor(1, 0) +
                            mat4[2, 0] * mat4.Cofactor(2, 0) +
                            mat4[3, 0] * mat4.Cofactor(3, 0), mat4.Det());
            Assert.AreEqual(mat4[2, 0] * mat4.Cofactor(2, 0) +
                            mat4[2, 1] * mat4.Cofactor(2, 1) +
                            mat4[2, 2] * mat4.Cofactor(2, 2) +
                            mat4[2, 3] * mat4.Cofactor(2, 3), mat4.Det());
            Assert.AreEqual(-4071, mat4.Det());
        }

        [Test, Order(11)]
        public void T11_Invertability()
        {
            Mat4 a = new Mat4(6, 4, 4, 4,
                              5, 5, 7, 6,
                              4, -9, 3, -7,
                              9, 1, 7, -6);
            Assert.AreEqual(-2120, a.Det());

            Mat4 b = new Mat4(-4, 2, -2, -3,
                              9, 6, 2, 6,
                              0, -5, 1, -5,
                              0, 0, 0, 0);
            Assert.AreEqual(0, b.Det());
        }

        [Test, Order(12)]
        public void T12_Inversion()
        {
            Mat2 mat2 = new Mat2(2, 3,
                                 4, 5);
            Assert.IsTrue(0 != mat2.Det());
            Mat2 inverse2 = mat2.Inverse();
            Assert.AreEqual(new Mat2(), mat2 * inverse2);

            Mat3 mat3 = new Mat3(1, 2, 6,
                                 -5, 8, -4,
                                 2, 6, 4);
            Assert.IsTrue(0 != mat3.Det());
            Mat3 inverse3 = mat3.Inverse();
            Assert.AreEqual(new Mat3(), mat3 * inverse3);

            Mat4 mat4 = new Mat4(-5, 2, 6, -8,
                                 1, -5, 1, 8,
                                 7, 7, -6, -7,
                                 1, -3, 7, 4);
            Assert.IsTrue(0 != mat4.Det());
            Assert.AreEqual(532, mat4.Det());
            Mat4 inverse4 = mat4.Inverse();
            Assert.IsTrue(Utility.FE((double)-160 / (double)532, inverse4[3, 2]));
            Assert.IsTrue((double)-160 / (double)532 == inverse4[3, 2]);
            Assert.IsTrue(Utility.FE((double)105 / (double)532, inverse4[2, 3]));
            Assert.AreEqual(new Mat4(), mat4 * inverse4);
            Assert.AreEqual(new Mat4(0.21805, 0.45113, 0.24060, -0.04511,
                                    -0.80827, -1.45677, -0.44361, 0.52068,
                                    -0.07895, -0.22368, -0.05263, 0.19737,
                                    -0.52256, -0.81391, -0.30075, 0.30639), inverse4);

            Mat4 a = new Mat4(3, -9, 7, 3,
                              3, -8, 2, -9,
                              -4, 4, 4, 1,
                              -6, 5, -1, 1);
            Mat4 b = new Mat4(8, 2, 2, 2,
                              3, -1, 7, 0,
                              7, 0, 5, 4,
                              6, -2, 0, 5);
            Mat4 c = a * b;
            Assert.AreEqual(a, c * b.Inverse());
            Assert.AreEqual(a, c * (b.Inverse()));
            Assert.AreNotEqual(a, (c * b).Inverse());
        }
    }
}
