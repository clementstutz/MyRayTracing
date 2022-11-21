using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.UnitTesting
{
    [TestFixture]
    class Chapter04Test
    {
        [Test, Order(1)]
        public void T01_TranslationMatrix()
        {
            Mat4 trans = Mat4.TranslateMatrix(5, -3, 2);
            Point pOrigine = new Point(-3, 4, 5);
            Point pOrigineTrans = trans * pOrigine;
            Assert.AreEqual(new Point(2, 1, 7), pOrigineTrans);

            Mat4 inverseTrans = trans.Inverse();
            Assert.AreEqual(pOrigine, inverseTrans * pOrigineTrans);


            Vector v = new Vector(-3, 4, 5);
            Assert.AreEqual(v, trans * v);
        }

        [Test, Order(2)]
        public void T02_ScalingMatrix()
        {
            Mat4 scaling = Mat4.ScaleMatrix(-1, 1, 2);
            Point pOrigine = new Point(2, 3, 4);
            Point origineScale = scaling * pOrigine;
            Assert.AreEqual(new Point(-2, 3, 8), origineScale);

            Mat4 scalingInverse = scaling.Inverse();
            Assert.AreEqual(pOrigine, scalingInverse * origineScale);


            scaling = Mat4.ScaleMatrix(2, 3, 4);
            Vector vOrigine = new Vector(-4, 6, 8);
            Vector origineVScale = scaling * vOrigine;
            Assert.AreEqual(new Vector(-8, 18, 32), origineVScale);

            scalingInverse = scaling.Inverse();
            Assert.AreEqual(vOrigine, scalingInverse * origineVScale);
        }

        [Test, Order(3)]
        public void T03_Rotation()
        {
            Point pOrigine = new Point(0, 1, 0);
            Vector vOrigine = new Vector(0, 1, 0);
            Mat4 halfQuarter = Mat4.RotateXMatrix(Math.PI / 4.0);
            Mat4 fullQuarter = Mat4.RotateXMatrix(Math.PI / 2.0);
            Point pRotateX_half = halfQuarter * pOrigine;
            Vector vRotateX_half = halfQuarter * vOrigine;
            Assert.AreEqual(new Point(0, Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0), pRotateX_half);
            Assert.AreEqual(new Vector(0, Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0), vRotateX_half);
            Point pRotateX_full = fullQuarter * pOrigine;
            Vector vRotateX_full = fullQuarter * vOrigine;
            Assert.AreEqual(new Point(0, 0, 1), pRotateX_full);
            Assert.AreEqual(new Vector(0, 0, 1), vRotateX_full);

            Mat4 inverse = halfQuarter.Inverse();
            Assert.AreEqual(pOrigine, inverse * pRotateX_half);
            Assert.AreEqual(vOrigine, inverse * vRotateX_half);


            pOrigine = new Point(0, 0, 1);
            vOrigine = new Vector(0, 0, 1);
            halfQuarter = Mat4.RotateYMatrix(Math.PI / 4.0);
            fullQuarter = Mat4.RotateYMatrix(Math.PI / 2.0);
            Point pRotateY_half = halfQuarter * pOrigine;
            Vector vRotateY_half = halfQuarter * vOrigine;
            Assert.AreEqual(new Point(Math.Sqrt(2) / 2.0, 0, Math.Sqrt(2) / 2.0), pRotateY_half);
            Assert.AreEqual(new Vector(Math.Sqrt(2) / 2.0, 0, Math.Sqrt(2) / 2.0), vRotateY_half);
            Point pRotateY_full = fullQuarter * pOrigine;
            Vector vRotateY_full = fullQuarter * vOrigine;
            Assert.AreEqual(new Point(1, 0, 0), pRotateY_full);
            Assert.AreEqual(new Vector(1, 0, 0), vRotateY_full);

            inverse = halfQuarter.Inverse();
            Assert.AreEqual(pOrigine, inverse * pRotateY_half);
            Assert.AreEqual(vOrigine, inverse * vRotateY_half);


            pOrigine = new Point(1, 0, 0);
            vOrigine = new Vector(1, 0, 0);
            halfQuarter = Mat4.RotateZMatrix(Math.PI / 4.0);
            fullQuarter = Mat4.RotateZMatrix(Math.PI / 2.0);
            Point pRotateZ_half = halfQuarter * pOrigine;
            Vector vRotateZ_half = halfQuarter * vOrigine;
            Assert.AreEqual(new Point(Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0, 0), pRotateZ_half);
            Assert.AreEqual(new Vector(Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0, 0), vRotateZ_half);
            Point pRotateZ_full = fullQuarter * pOrigine;
            Vector vRotateZ_full = fullQuarter * vOrigine;
            Assert.AreEqual(new Point(0, 1, 0), pRotateZ_full);
            Assert.AreEqual(new Vector(0, 1, 0), vRotateZ_full);

            inverse = halfQuarter.Inverse();
            Assert.AreEqual(pOrigine, inverse * pRotateZ_half);
            Assert.AreEqual(vOrigine, inverse * vRotateZ_half);
        }

        [Test, Order(4)]
        public void T04_ShearingMatrix()
        {
            Mat4 shearing = Mat4.ShearMatrix(1, 0, 0, 0, 0, 0);
            Point p = new Point(2, 3, 4);
            Assert.AreEqual(new Point(5, 3, 4), shearing * p);
            Mat4 inverse = shearing.Inverse();
            p = new Point(5, 3, 4);
            Assert.AreEqual(new Point(2, 3, 4), inverse * p);

            shearing = Mat4.ShearMatrix(0, 1, 0, 0, 0, 0);
            p = new Point(2, 3, 4);
            Assert.AreEqual(new Point(6, 3, 4), shearing * p);
            inverse = shearing.Inverse();
            p = new Point(6, 3, 4);
            Assert.AreEqual(new Point(2, 3, 4), inverse * p);

            shearing = Mat4.ShearMatrix(0, 0, 1, 0, 0, 0);
            p = new Point(2, 3, 4);
            Assert.AreEqual(new Point(2, 5, 4), shearing * p);
            inverse = shearing.Inverse();
            p = new Point(2, 5, 4);
            Assert.AreEqual(new Point(2, 3, 4), inverse * p);

            shearing = Mat4.ShearMatrix(0, 0, 0, 1, 0, 0);
            p = new Point(2, 3, 4);
            Assert.AreEqual(new Point(2, 7, 4), shearing * p);
            inverse = shearing.Inverse();
            p = new Point(2, 7, 4);
            Assert.AreEqual(new Point(2, 3, 4), inverse * p);

            shearing = Mat4.ShearMatrix(0, 0, 0, 0, 1, 0);
            p = new Point(2, 3, 4);
            Assert.AreEqual(new Point(2, 3, 6), shearing * p);
            inverse = shearing.Inverse();
            p = new Point(2, 3, 6);
            Assert.AreEqual(new Point(2, 3, 4), inverse * p);

            shearing = Mat4.ShearMatrix(0, 0, 0, 0, 0, 1);
            p = new Point(2, 3, 4);
            Assert.AreEqual(new Point(2, 3, 7), shearing * p);
            inverse = shearing.Inverse();
            p = new Point(2, 3, 7);
            Assert.AreEqual(new Point(2, 3, 4), inverse * p);
        }

        [Test, Order(5)]
        public void T05_ChainingTransforms()
        {
            // Order of transformation is as folow:
            // (thirs_transform * (second_transform * (first_transform * obj)))
            // which is the same as :
            // (thirs_transform * second_transform * first_transform) * obj
            // you must concatenate the transformations in reverse order
            // to have them applied in the order you want!
            Point p = new Point(1, 0, 1);
            Mat4 Rot = Mat4.RotateXMatrix(Math.PI / 2.0);
            Mat4 Sca = Mat4.ScaleMatrix(5, 5, 5);
            Mat4 Tra = Mat4.TranslateMatrix(10, 5, 7);

            Point p2 = Rot * p;
            Assert.AreEqual(new Point(1, -1, 0), p2);

            Point p3 = Sca * p2;
            Assert.AreEqual(new Point(5, -5, 0), p3);

            Point p4 = Tra * p3;
            Assert.AreEqual(new Point(15, 0, 7), p4);

            Mat4 trans = Tra * Sca * Rot;
            Assert.AreEqual(new Point(15, 0, 7), trans * p);

            trans = new Mat4() * Sca.Translate(5, 5, 5);
            Assert.AreEqual(new Point(30, 25, 30), trans * p);


            // ce test ne fait pas parti du livre
            Mat4 mat = new Mat4(2, 2, 0, 5,
                                7, 1.5, 0, 1,
                                3, -0.5, 2, -2,
                                0, 0, 0, 1);
            Mat4 invert = new Mat4(-0.13636, 0.181818, 0, 0.5,
                                   0.636363, -0.181818, 0, -3,
                                   0.363636, -0.318181, 0.5, -0.5,
                                   0, 0, 0, 1);
            Mat4 res1 = mat.Inverse().Transpose();
            Mat4 res2 = mat.Inverse();
            Assert.AreEqual(invert, res2);
            res2 = res2.Transpose();
            Mat4 res3 = mat.Transpose();
            res3 = res3.Inverse();
            Assert.AreEqual(res1, res2);
            Assert.AreEqual(res1, res3);
            Assert.AreEqual(res2, res2);
        }
    }
}
