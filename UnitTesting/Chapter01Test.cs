using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.UnitTesting
{
    [TestFixture]
    class Chapter01Test
    {
        [Test, Order(1)]
        public void T01_Points()
        {
            Point a = new Point(4.3, -4.2, 3.1);
            Assert.AreEqual(4.3, a.x);
            Assert.AreEqual(-4.2, a.y);
            Assert.AreEqual(3.1, a.z);
            Assert.AreEqual(1, a.w);
            Assert.IsInstanceOf(typeof(Point), a);
            Assert.IsNotInstanceOf(typeof(Vector), a);
        }

        [Test, Order(2)]
        public void T02_Vectors()
        {
            Vector a = new Vector(4.3, -4.2, 3.1);
            Assert.AreEqual(4.3, a.x);
            Assert.AreEqual(-4.2, a.y);
            Assert.AreEqual(3.1, a.z);
            Assert.AreEqual(0, a.w);
            Assert.IsNotInstanceOf(typeof(Point), a);
            Assert.IsInstanceOf(typeof(Vector), a);
        }

        [Test, Order(3)]
        public void T03_Adding()
        {
            //Two Points
            Point p1 = new Point(3, 2, 1);
            Point p2 = new Point(5, 6, 7);
            Assert.AreEqual(new Vector(8, 8, 8), p1 + p2);
            Assert.IsInstanceOf(typeof(Vector), p1 + p2);
            Assert.IsNotInstanceOf(typeof(Point), p1 + p2);

            //Point plus Vector
            Vector v1 = new Vector(-2, 3, 1);
            Assert.AreEqual(new Point(1, 5, 2), p1 + v1);
            Assert.IsInstanceOf(typeof(Point), p1 + v1);
            Assert.IsNotInstanceOf(typeof(Vector), p1 + v1);

            //Vector plus Point
            Assert.AreEqual(new Point(1, 5, 2), v1 + p1);
            Assert.IsInstanceOf(typeof(Point), v1 + p1);
            Assert.IsNotInstanceOf(typeof(Vector), v1 + p1);

            //Two Vectors
            Vector v2 = new Vector(5, 6, 7);
            Assert.AreEqual(new Vector(3, 9, 8), v1 + v2);
            Assert.IsInstanceOf(typeof(Vector), v1 + v2);
            Assert.IsNotInstanceOf(typeof(Point), v1 + v2);
        }

        [Test, Order(4)]
        public void T04_Subtraction()
        {
            //Two Points
            Point p1 = new Point(3, 2, 1);
            Point p2 = new Point(5, 6, 7);
            Assert.AreEqual(new Vector(-2, -4, -6), p1 - p2);
            Assert.IsInstanceOf(typeof(Vector), p1 - p2);
            Assert.IsNotInstanceOf(typeof(Point), p1 - p2);

            //Vector from Point
            Vector v1 = new Vector(5, 6, 7);
            Assert.AreEqual(new Point(-2, -4, -6), p1 - v1);
            Assert.IsInstanceOf(typeof(Point), p1 - v1);
            Assert.IsNotInstanceOf(typeof(Vector), p1 - v1);

            //Point from Vector
            Assert.AreEqual(new Point(2, 4, 6), v1 - p1);
            Assert.IsInstanceOf(typeof(Point), v1 - p1);
            Assert.IsNotInstanceOf(typeof(Vector), v1 - p1);

            //Two Vectors
            Vector v2 = new Vector(-2, 3, 1);
            Assert.AreEqual(new Vector(7, 3, 6), v1 - v2);
            Assert.IsInstanceOf(typeof(Vector), v1 - v2);
            Assert.IsNotInstanceOf(typeof(Point), v1 - v2);
        }

        [Test, Order(5)]
        public void T05_Negation()
        {
            // Negation vector
            Vector v0 = new Vector(0, 0, 0);
            Vector v1 = new Vector(1, -2, 3);

            Assert.AreEqual(new Vector(-1, 2, -3), v0 - v1);
            Assert.IsInstanceOf(typeof(Vector), v0 - v1);
            Assert.IsNotInstanceOf(typeof(Point), v0 - v1);

            Assert.AreEqual(new Vector(-1, 2, -3), -v1);
            Assert.IsInstanceOf(typeof(Vector), -v1);
            Assert.IsNotInstanceOf(typeof(Point), -v1);

            Assert.AreEqual(new Vector(-1, 2, -3), v1.Negate());
            Assert.IsInstanceOf(typeof(Vector), v1.Negate());
            Assert.IsNotInstanceOf(typeof(Point), v1.Negate());

            // Negation point
            Point p0 = new Point(0, 0, 0);
            Point p1 = new Point(1, -2, 3);

            Assert.AreEqual(new Vector(-1, 2, -3), p0 - p1);
            Assert.IsInstanceOf(typeof(Vector), p0 - p1);
            Assert.IsNotInstanceOf(typeof(Point), p0 - p1);

            Assert.AreEqual(new Point(-1, 2, -3), -p1);
            Assert.IsInstanceOf(typeof(Point), -p1);
            Assert.IsNotInstanceOf(typeof(Vector), -p1);

            Assert.AreEqual(new Point(-1, 2, -3), p1.Negate());
            Assert.IsInstanceOf(typeof(Point), p1.Negate());
            Assert.IsNotInstanceOf(typeof(Vector), p1.Negate());
        }

        [Test, Order(6)]
        public void T06_ScalarMultiplication()
        {
            Point p = new Point(1, -2, 3);
            Assert.AreEqual(new Point(3.5, -7, 10.5), p * 3.5);
            Assert.AreEqual(1, (p * 3.5).w);
            Assert.AreEqual(new Point(3.5, -7, 10.5), 3.5 * p);
            Assert.AreEqual(1, (3.5 * p).w);
            Assert.AreEqual(new Point(3.5, -7, 10.5), p.Scale(3.5));
            Assert.AreEqual(1, p.Scale(3.5).w);

            Vector v = new Vector(1, -2, 3);
            Assert.AreEqual(new Vector(0.5, -1, 1.5), v * 0.5);
            Assert.AreEqual(0, (v * 0.5).w);
            Assert.AreEqual(new Vector(0.5, -1, 1.5), 0.5 * v);
            Assert.AreEqual(0, (0.5 * v).w);
            Assert.AreEqual(new Vector(0.5, -1, 1.5), v.Scale(0.5));
            Assert.AreEqual(0, v.Scale(0.5).w);
        }

        [Test, Order(7)]
        public void T07_Magnitude()
        {
            Vector v = new Vector(1, 0, 0);
            Assert.AreEqual(1, v.Magnitude());

            v = new Vector(0, 1, 0);
            Assert.AreEqual(1, v.Magnitude());

            v = new Vector(0, 0, 1);
            Assert.AreEqual(1, v.Magnitude());

            v = new Vector(0, 0, 0);
            Assert.AreEqual(0, v.Magnitude());

            v = new Vector(-1, 2, -3);
            Assert.AreEqual(Math.Sqrt(14), v.Magnitude());
            Assert.IsTrue(Math.Sqrt(14) == v.Magnitude());

            Assert.AreEqual(14, v.SqrtMagnitude());
            Assert.IsTrue(14 == v.SqrtMagnitude());

            Point p = new Point(1, 0, 0);
            Assert.AreEqual(1, p.Magnitude());

            p = new Point(0, 1, 0);
            Assert.AreEqual(1, p.Magnitude());

            p = new Point(0, 0, 1);
            Assert.AreEqual(1, p.Magnitude());

            p = new Point(0, 0, 0);
            Assert.AreEqual(0, p.Magnitude());

            p = new Point(-1, 2, -3);
            Assert.AreEqual(Math.Sqrt(14), p.Magnitude());
            Assert.IsTrue(Math.Sqrt(14) == p.Magnitude());

            Assert.AreEqual(14, p.SqrtMagnitude());
            Assert.IsTrue(14 == p.SqrtMagnitude());
        }

        [Test, Order(8)]
        public void T08_Normalization()
        {
            // Normalize.
            Vector v1 = new Vector(4, 0, 0);
            Assert.AreEqual(new Vector(1, 0, 0), v1.Normalize());

            v1 = new Vector(1, 2, 3);
            Assert.AreEqual(new Vector(0.26726, 0.53452, 0.80178), v1.Normalize());
            Assert.IsTrue(new Vector(0.26726, 0.53452, 0.80178) == v1.Normalize());

            v1 = new Vector(1, 2, 3);
            Vector normV1 = v1.Normalize();
            Assert.IsTrue(Utility.FE(1, normV1.Magnitude()));
            Assert.IsTrue(1 == normV1.Magnitude());

            /*Point p1 = new Point(4, 0, 0);
            Assert.AreEqual(new Point(1, 0, 0), p1.Normalize());

            p1 = new Point(1, 2, 3);
            Assert.AreEqual(new Point(0.26726, 0.53452, 0.80178), p1.Normalize());
            Assert.IsTrue(new Point(0.26726, 0.53452, 0.80178) == p1.Normalize());

            p1 = new Point(1, 2, 3);
            Point normP1 = p1.Normalize();
            Assert.IsTrue(Utility.FE(1, normP1.Magnitude()));*/

            // Normalized.
            Vector v = new Vector(4, 0, 0);
            Assert.AreEqual(new Vector(1, 0, 0), v.Normalized());

            v = new Vector(1, 2, 3);
            Assert.AreEqual(new Vector(0.26726, 0.53452, 0.80178), v.Normalized());
            Assert.IsTrue(new Vector(0.26726, 0.53452, 0.80178) == v.Normalized());

            //v = new Vector(1, 2, 3);
            Vector normV = v.Normalized();
            Assert.IsTrue(Utility.FE(1, normV.Magnitude()));
            Assert.IsTrue(1 == normV.Magnitude());

            /*Point p = new Point(4, 0, 0);
            Assert.AreEqual(new Point(1, 0, 0), p.Normalized());

            p = new Point(1, 2, 3);
            Assert.AreEqual(new Point(0.26726, 0.53452, 0.80178), p.Normalized());
            Assert.IsTrue(new Point(0.26726, 0.53452, 0.80178) == p.Normalized());

            p = new Point(1, 2, 3);
            Point normP = p.Normalized();
            Assert.IsTrue(Utility.FE(1, normP.Magnitude()));*/
        }

            [Test, Order(9)]
        public void T09_DotProduct()
        {
            Vector a = new Vector(1, 2, 3);
            Vector b = new Vector(2, 3, 4);
            Assert.AreEqual(20, a.Dot(b));
            Assert.AreEqual(20, b.Dot(a));
            Assert.AreEqual(14, a.Dot(a));
            Assert.AreEqual(20, Tuple.Dot(a, b));
            Assert.AreEqual(20, Tuple.Dot(b, a));
            Assert.AreEqual(14, Tuple.Dot(a, a));

            // A supprimer, car fair un produit sacalaire entre deux points
            // ou entre un point et un vecteur n'a pas de sens !
            Point p1 = new Point(1, 2, 3);
            Point p2 = new Point(2, 3, 4);
            Assert.AreEqual(20, p1.Dot(p2));
            Assert.AreEqual(20, p2.Dot(p1));
            Assert.AreEqual(14, p1.Dot(p1));
            Assert.AreEqual(20, Tuple.Dot(p1, p2));
            Assert.AreEqual(20, Tuple.Dot(p2, p1));
            Assert.AreEqual(14, Tuple.Dot(p1, p1));
        }

        [Test, Order(10)]
        public void T10_CrossProduct()
        {
            Vector a = new Vector(1, 2, 3);
            Vector b = new Vector(2, 3, 4);
            Assert.AreEqual(new Vector(-1, 2, -1), a.Cross(b));
            Assert.AreEqual(new Vector(1, -2, 1), b.Cross(a));
            Assert.AreEqual(new Vector(0, 0, 0), a.Cross(a));
            Assert.AreEqual(new Vector(-1, 2, -1), Vector.Cross(a, b));
            Assert.AreEqual(new Vector(1, -2, 1), Vector.Cross(b, a));
            Assert.AreEqual(new Vector(0, 0, 0), Vector.Cross(a, a));
        }

        /*[Test, Order(11)]
        public void T11_RandomizePoint()
        {
            Point start = new Point(-10, -5, -10);
            Point end = new Point(10, -4, 10);

            Point p = new Point().Randomize(start, end);
        }


        [Test, Order(12)]
        public void T12_RandomizeVector()
        {
            Vector start = new Vector(-10, -5, -10);
            Vector end = new Vector(10, -4, 10);

            Vector v = new Vector().Randomize(start, end);
        }*/
    }
}
