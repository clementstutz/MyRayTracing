using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.UnitTesting
{
    [TestFixture]
    class Chapter06Test
    {
        [Test, Order(1)] // Ok.
        public void T01_SphereNormals()
        {
            //The normal on a sphere at a point on the x axis
            Sphere s = new Sphere();
            Vector n = s.GetNormal(new Point(1, 0, 0));
            Assert.AreEqual(new Vector(1, 0, 0), n);

            //The normal on a sphere at a point on the y axis
            n = s.GetNormal(new Point(0, 1, 0));
            Assert.AreEqual(new Vector(0, 1, 0), n);

            //The normal on a sphere at a point on the z axis
            n = s.GetNormal(new Point(0, 0, 1));
            Assert.AreEqual(new Vector(0, 0, 1), n);

            //The normal on a sphere at a nonaxial point
            n = s.GetNormal(new Point(Math.Sqrt(3) / 3.0,
                                      Math.Sqrt(3) / 3.0,
                                      Math.Sqrt(3) / 3.0));
            Assert.AreEqual(new Vector(Math.Sqrt(3) / 3.0,
                                       Math.Sqrt(3) / 3.0,
                                       Math.Sqrt(3) / 3.0), n);

            n = s.GetNormal(new Point(-1, -1, -1));
            Assert.AreEqual(new Vector(-Math.Sqrt(3) / 3.0,
                                       -Math.Sqrt(3) / 3.0,
                                       -Math.Sqrt(3) / 3.0), n);

            n = s.GetNormal(new Point(3, 3, 3));
            Assert.AreEqual(new Vector(Math.Sqrt(3) / 3.0,
                                       Math.Sqrt(3) / 3.0,
                                       Math.Sqrt(3) / 3.0), n);
        }

        [Test, Order(2)] // Ok.
        public void T02_Normalized()
        {
            Sphere s = new Sphere();
            Vector n = s.GetNormal(new Point(Math.Sqrt(3) / 3.0,
                                             Math.Sqrt(3) / 3.0,
                                             Math.Sqrt(3) / 3.0));
            Assert.AreEqual(n, n.Normalize());
        }

        [Test, Order(3)]
        public void T03_TranslatedSphere()
        {
            //Computing the normal on a translated sphere
            Sphere s = new Sphere();
            s.SetMatrix(Mat4.TranslateMatrix(0, 1, 0));
            Vector n = s.GetNormal(new Point(0, 1.70711, -0.70711));
            Assert.AreEqual(new Vector(0, 0.70711, -0.70711), n);

            //Computing the normal on a transformed sphere
            // This example seems to confuse things...
            // Are these points on the sphere's surface or arbitrary points in world space ?
            s.SetMatrix(Mat4.ScaleMatrix(1, 0.5, 1) * Mat4.RotateZMatrix(Math.PI / 5));
            n = s.GetNormal(new Point(0, Math.Sqrt(2) / 2, - Math.Sqrt(2) / 2));
            Assert.AreEqual(new Vector(0, 0.97014, -0.24254), n);
        }

        [Test, Order(4)] // Ok.
        public void T04_ReflectingAVector()
        {
            //Reflecting a vector approaching at 45
            Vector incoming = new Vector(1, -1, 0);
            Vector normal = new Vector(0, 1, 0);
            Vector r = Vector.Reflect(incoming, normal);
            Assert.AreEqual(new Vector(1, 1, 0), r);

            //Reflecting a vector off a slanted surface
            incoming = new Vector(0, -1, 0);
            normal = new Vector(Math.Sqrt(2) / 2, Math.Sqrt(2) / 2, 0);
            r = Vector.Reflect(incoming, normal);
            Assert.AreEqual(new Vector(1, 0, 0), r);
        }

        [Test, Order(5)] // Ok.
        public void T05_Lighting()
        {
            //A point light has a position and intensity
            Color c = new Color(1, 1, 1);
            Point p = new Point(0, 0, 0);
            Light light = new Light(p, c);
            Assert.AreEqual(p, light.position);
            Assert.AreEqual(c, light.intensity);
        }

        [Test, Order(6)] // Ok.
        public void T06_Material()
        {
            //The default material
            Material m = new Material();
            Assert.AreEqual(Color.white, m.color);
            Assert.AreEqual(0.1, m.Ambient);
            Assert.AreEqual(0.9, m.Diffuse);
            Assert.AreEqual(0.9, m.Specular);
            Assert.AreEqual(200, m.Shininess);
        }

        [Test, Order(7)] // Ok.
        public void T07_SphereMaterial()
        {
            //A sphere has a default material
            Sphere s = new Sphere();
            Assert.AreEqual(new Material(), s.material);

            //A sphere may be assigned a material
            Material m = new Material();
            m.Ambient = 1.0;
            s.material = m;
            Assert.AreEqual(m, s.material);
            Assert.AreEqual(m.Ambient, s.material.Ambient);

            s.material.Ambient = 0.5;
            Assert.AreEqual(0.5, s.material.Ambient);
        }

        [Test, Order(8)] // Ok.
        public void T08_LightingResults()
        {
            Material m = new Material();
            Point p = new Point(0, 0, 0);

            // Direct lighting
            Vector eye = new Vector(0, 0, -1);
            Vector normal = new Vector(0, 0, -1);
            Light light = new Light(new Point(0, 0, -10), Color.white);
            RayObject test = new TestRayObject();
            Color result = test.Lighting(p, light, eye, normal, false);
            Assert.AreEqual(new Color(1.9, 1.9, 1.9), result);

            // Off angle lighting
            eye = new Vector(0, Math.Sqrt(2) / 2, - Math.Sqrt(2) / 2);
            result = test.Lighting(p, light, eye, normal, false);
            Assert.AreEqual(new Color(1, 1, 1), result);

            // Eye opposite surface, light offset 45
            eye = new Vector(0, 0, -1);
            light.position = new Point(0, 10, -10);
            result = test.Lighting(p, light, eye, normal, false);
            Assert.AreEqual(new Color(0.73639, 0.73639, 0.73639), result);

            // Eye opposite refletion vector
            eye = new Vector(0, - Math.Sqrt(2) / 2, - Math.Sqrt(2) / 2);
            result = test.Lighting(p, light, eye, normal, false);
            Assert.AreEqual(new Color(1.6364, 1.6364, 1.6364), result);

            // Lighting behind surface
            eye = new Vector(0, 0, -1);
            light.position = new Point(0, 0, 10);
            result = test.Lighting(p, light, eye, normal, false);
            Assert.AreEqual(new Color(0.1, 0.1, 0.1), result);
        }
    }
}
