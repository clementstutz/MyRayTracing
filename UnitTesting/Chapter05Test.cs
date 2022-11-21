using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.UnitTesting
{
    [TestFixture]
    class Chapter05Test
    {
        [Test, Order(1)]
        public void T01_CreatRay()
        {
            Point p = new Point(1, 2, 3);
            Vector v = new Vector(4, 5, 6);
            Ray ray = new Ray(p, v);
            Assert.AreEqual(ray.origin, p);
            Assert.AreEqual(ray.direction, v);
        }

        [Test, Order(2)]
        public void T02_PointAtDistance()
        {
            Ray ray = new Ray(new Point(2, 3, 4), new Vector(1, 0, 0));
            Assert.AreEqual(new Point(2, 3, 4), ray.Position(0));
            Assert.AreEqual(new Point(3, 3, 4), ray.Position(1));
            Assert.AreEqual(new Point(1, 3, 4), ray.Position(-1));
            Assert.AreEqual(new Point(4.5, 3, 4), ray.Position(2.5));

            ray = new Ray(new Point(2, 3, 4), new Vector(0, 2, 0));
            Assert.AreEqual(new Point(2, 3, 4), ray.Position(0));
            Assert.AreEqual(new Point(2, 5, 4), ray.Position(1));
            Assert.AreEqual(new Point(2, 1, 4), ray.Position(-1));
            Assert.AreEqual(new Point(2, 8, 4), ray.Position(2.5));

            ray = new Ray(new Point(2, 3, 4), new Vector(0, 0, -1));
            Assert.AreEqual(new Point(2, 3, 4), ray.Position(0));
            Assert.AreEqual(new Point(2, 3, 3), ray.Position(1));
            Assert.AreEqual(new Point(2, 3, 5), ray.Position(-1));
            Assert.AreEqual(new Point(2, 3, 1.5), ray.Position(2.5));

            ray = new Ray(new Point(2, 3, 4), new Vector(-1, 1.5, 2));
            Assert.AreEqual(new Point(2, 3, 4), ray.Position(0));
            Assert.AreEqual(new Point(1, 4.5, 6), ray.Position(1));
            Assert.AreEqual(new Point(3, 1.5, 2), ray.Position(-1));
            Assert.AreEqual(new Point(-0.5, 6.75, 9), ray.Position(2.5));
        }

        [Test, Order(3)]
        public void T03_IntersectSphere()
        {
            // Straight On
            Ray ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            Sphere sphere = new Sphere();
            sphere.SetMatrix(Mat4.ScaleMatrix(2, 2, 2));
            //List<double> i = sphere.Intersect(ray);
            List<Intersection> i = sphere.Intersect(ray);
            Assert.AreEqual(2, i.Count);
            Assert.AreEqual(3.0, i[0].t);
            Assert.AreEqual(7.0, i[1].t);
            //Assert.AreEqual(3.0, i[0]);
            //Assert.AreEqual(7.0, i[1]);

            // Tangent
            ray = new Ray(new Point(0, 2, -5), new Vector(0, 0, 1));
            i = sphere.Intersect(ray);
            Assert.AreEqual(2, i.Count);
            Assert.AreEqual(5.0, i[0].t);
            Assert.AreEqual(5.0, i[1].t);
            //Assert.AreEqual(5.0, i[0]);
            //Assert.AreEqual(5.0, i[1]);

            // Miss
            ray = new Ray(new Point(0, 3, -5), new Vector(0, 0, 1));
            i = sphere.Intersect(ray);
            Assert.AreEqual(0, i.Count);

            // Inside
            ray = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            i = sphere.Intersect(ray);
            Assert.AreEqual(2, i.Count);
            Assert.AreEqual(-2.0, i[0].t);
            Assert.AreEqual(2.0, i[1].t);
            //Assert.AreEqual(-2.0, i[0]);
            //Assert.AreEqual(2.0, i[1]);

            // Behind
            ray = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));
            i = sphere.Intersect(ray);
            Assert.AreEqual(2, i.Count);
            Assert.AreEqual(-7.0, i[0].t);
            Assert.AreEqual(-3.0, i[1].t);
            //Assert.AreEqual(-7.0, i[0]);
            //Assert.AreEqual(-3.0, i[1]);
        }

        [Test, Order(4)]
        public void T04_Intersection()
        {
            if (Scene.current == null)
                new Scene();
            Scene.current.Clear();
            Sphere s = new Sphere();
            s.SetMatrix(Mat4.ScaleMatrix(2, 2, 2));
            Intersection i = new Intersection(s, 3.1415);
            Assert.AreEqual(3.1415, i.t);
            Assert.AreEqual(s, i.rayObject);

            Sphere s2 = new Sphere();
            s2.SetMatrix(Mat4.TranslateMatrix(0, 0, 2));
            Ray ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            List<Intersection> intersections = Scene.current.Intersections(ray);
            Assert.AreEqual(4, intersections.Count);
            Console.WriteLine(intersections.ToString());
            Assert.AreEqual(s, intersections[0].rayObject);
            Assert.AreEqual(s2, intersections[1].rayObject);
            Assert.AreEqual(s, intersections[2].rayObject);
            Assert.AreEqual(s2, intersections[3].rayObject);
            Assert.AreEqual(3, intersections[0].t);
            Assert.AreEqual(6, intersections[1].t);
            Assert.AreEqual(7, intersections[2].t);
            Assert.AreEqual(8, intersections[3].t);
        }

        [Test, Order(5)]
        public void T05_Hits()
        {
            if (Scene.current == null)
                new Scene();
            Scene.current.Clear();
            Sphere s = new Sphere();
            List<Intersection> intersections = new List<Intersection>();
            Intersection i1 = new Intersection(s, 1);
            Intersection i2 = new Intersection(s, 2);
            intersections.Add(i1);
            intersections.Add(i2);

            Intersection i = Scene.current.Hit(intersections);
            Assert.AreEqual(i1, i);

            // Negative t
            intersections.Clear();
            i1 = new Intersection(s, -1);
            i2 = new Intersection(s, 1);
            intersections.Add(i1);
            intersections.Add(i2);
            i = Scene.current.Hit(intersections);
            Assert.AreEqual(i2, i);

            // All negative t
            intersections.Clear();
            i1 = new Intersection(s, -2);
            i2 = new Intersection(s, -1);
            intersections.Add(i1);
            intersections.Add(i2);
            i = Scene.current.Hit(intersections);
            Assert.AreEqual(null, i);

            // Big List, always lowest non-negative
            intersections.Clear();
            i1 = new Intersection(s, 5);
            i2 = new Intersection(s, 6);
            Intersection i3 = new Intersection(s, -3);
            Intersection i4 = new Intersection(s, 2);
            intersections.Add(i1);
            intersections.Add(i2);
            intersections.Add(i3);
            intersections.Add(i4);
            i = Scene.current.Hit(intersections);
            Assert.AreEqual(i4, i);
        }

        [Test, Order(6)]
        public void T06_TransformingRay()
        {
            if (Scene.current == null)
                new Scene();
            Scene.current.Clear();

            //Translating a ray
            Ray r = new Ray(new Point(1, 2, 3), new Vector(0, 1, 0));
            Mat4 trans = Mat4.TranslateMatrix(3, 4, 5);
            Ray r2 = trans * r;
            Assert.AreEqual(new Point(4, 6, 8), r2.origin);
            Assert.AreEqual(new Vector(0, 1, 0), r2.direction);

            //Scaling a ray
            Mat4 scale = Mat4.ScaleMatrix(2, 3, 4);
            r2 = scale * r;
            Assert.AreEqual(new Point(2, 6, 12), r2.origin);
            Assert.AreEqual(new Vector(0, 3, 0), r2.direction);

            //A sphere's default transformation
            Sphere s = new Sphere();
            Assert.AreEqual(new Mat4(), s.GetMatrix());

            //Changing a sphere's transformation
            trans = Mat4.TranslateMatrix(2, 3, 4);
            s.SetMatrix(trans);
            Assert.AreEqual(trans, s.GetMatrix());
            Assert.AreEqual(trans * new Mat4(), s.GetMatrix());

            //Intersecting a scaled sphere with a ray
            r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            s.SetMatrix(Mat4.ScaleMatrix(2, 2, 2));
            List<Intersection> intersections = Scene.current.Intersections(r);
            Assert.AreEqual(2, intersections.Count);
            Assert.AreEqual(3, intersections[0].t);
            Assert.AreEqual(7, intersections[1].t);

            //Intersecting a translated sphere with a ray
            s.SetMatrix(Mat4.TranslateMatrix(5, 0, 0));
            intersections = Scene.current.Intersections(r);
            Assert.AreEqual(0, intersections.Count);
        }
    }
}
