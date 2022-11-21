﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.UnitTesting
{
    [TestFixture]
    class Chapter08Test
    {
        [Test, Order(1)]
        public void T01_LightingWithTheSurfaceInShadow()
        {
            Scene scene = new Scene();
            Vector eye = new Vector(0, 0, -1);
            Vector normal = new Vector(0, 0, -1);
            Light light = new Light(new Point(0, 0, -10), new Color(1, 1, 1));
            bool inShadow = true;

            Sphere sphere = new Sphere();

            Color result = sphere.Lighting(sphere.GetPosition(),
                                           light,
                                           eye,
                                           normal,
                                           inShadow);
            Assert.AreEqual(new Color(0.1, 0.1, 0.1), result);
        }

        [Test, Order(2)]
        public void T02_NoShadow()
        {
            if (Scene.current == null)
                new Scene();
            Scene.current.Default();
            List<Light> lights = Scene.current.GetLights();
            Point point = new Point(0, 10, 0);
            Assert.IsFalse(Scene.current.IsShadowed(point, lights[0]));
            //Assert.IsFalse(Scene.current.IsShadowed(point));
        }

        [Test, Order(3)]
        public void T03_BetweenPointAndLight()
        {
            if (Scene.current == null)
                new Scene();
            Scene.current.Default();
            List<Light> lights = Scene.current.GetLights();
            Point point = new Point(10, -10, 10);
            Assert.IsTrue(Scene.current.IsShadowed(point, lights[0]));
            //Assert.IsTrue(Scene.current.IsShadowed(point));
        }

        [Test, Order(4)]
        public void T04_ObjectBehindLight()
        {
            if (Scene.current == null)
                new Scene();
            Scene.current.Default();
            List<Light> lights = Scene.current.GetLights();
            Point point = new Point(-20, 20, -20);
            Assert.IsFalse(Scene.current.IsShadowed(point, lights[0]));
            //Assert.IsFalse(Scene.current.IsShadowed(point));
        }

        [Test, Order(5)]
        public void T05_ObjectBehindPoint()
        {
            if (Scene.current == null)
                new Scene();
            Scene.current.Default();
            List<Light> lights = Scene.current.GetLights();
            Point point = new Point(-2, 2, -2);
            Assert.IsFalse(Scene.current.IsShadowed(point, lights[0]));
            //Assert.IsFalse(Scene.current.IsShadowed(point));
        }

        [Test, Order(6)]
        public void T06_ShadeHitIntersectionInShadow()
        {
            if (Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Clear();

            Light light = new Light(new Point(0, 0, -10), new Color(1, 1, 1));
            Sphere s1 = new Sphere();
            Sphere s2 = new Sphere();
            s2.SetMatrix(Mat4.TranslateMatrix(0, 0, 10));

            Ray ray = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));

            Intersection i = new Intersection(s2, 4);

            Computations c = Computations.Prepare(i, ray);

            Color color = Scene.current.ShadeHit(c);

            Assert.AreEqual(new Color(0.1, 0.1, 0.1), color);
        }

        [Test, Order(7)]
        public void T07_Offsethit()
        {
            Ray ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            if (Scene.current == null)
                new Scene();
            Scene.current.Clear();

            Sphere sphere = new Sphere();
            sphere.SetMatrix(Mat4.TranslateMatrix(0, 0, 1));
            Intersection i = new Intersection(sphere, 5);
            Computations c = Computations.Prepare(i, ray);
            Assert.IsTrue(c.overPoint.z < Utility.epsilon / -2.0);
            Assert.IsTrue(c.point.z > c.overPoint.z);
        }

        [Test, Order(8)]
        public void T08_PuttingItAllTogether()
        {
            Scene.current = null;
            Scene scene = new Scene();
            scene.ClearRayObjects();

            RayObject floor = new Sphere();
            floor.SetMatrix(Mat4.ScaleMatrix(10, 0.01, 10));
            floor.material = new Material();
            floor.material.color = new Color(1, 0.9, 0.9);
            floor.material.Specular = 0;

            RayObject leftWall = new Sphere();
            leftWall.SetMatrix(Mat4.TranslateMatrix(0, 0, 5) *
                                 Mat4.RotateYMatrix(Math.PI / -4.0) *
                                 Mat4.RotateXMatrix(Math.PI / 2.0) *
                                 Mat4.ScaleMatrix(10, 0.01, 10));
            leftWall.material = floor.material;

            RayObject rightWall = new Sphere();
            rightWall.SetMatrix(Mat4.TranslateMatrix(0, 0, 5) *
                                Mat4.RotateYMatrix(Math.PI / 4.0) *
                                Mat4.RotateXMatrix(Math.PI / 2.0) *
                                Mat4.ScaleMatrix(10, 0.01, 10));
            rightWall.material = floor.material;

            RayObject middle = new Sphere();
            middle.SetMatrix(Mat4.TranslateMatrix(-0.5, 1.0, 0.5));
            middle.material.color = new Color(0.1, 1.0, 0.5);
            middle.material.Diffuse = 0.7;
            middle.material.Specular = 0.3;

            RayObject right = new Sphere();
            right.SetMatrix(Mat4.TranslateMatrix(1.5, 0.5, -0.5) *
                            Mat4.ScaleMatrix(0.5, 0.5, 0.5));
            right.material.color = new Color(0.5, 1.0, 0.1);
            right.material.Diffuse = 0.7;
            right.material.Specular = 0.3;

            RayObject left = new Sphere();
            left.SetMatrix(Mat4.TranslateMatrix(-1.5, 0.33, -0.75) *
                           Mat4.ScaleMatrix(0.33, 0.33, 0.33));
            left.material.color = new Color(1, 0.8, 0.1);
            left.material.Diffuse = 0.7;
            left.material.Specular = 0.3;

            Light light = new Light(new Point(-10, 10, -10), Color.white);

            Camera camera = new Camera(100, 66, Math.PI / 3.0);

            camera.ViewTransform(new Point(0, 1.5, -5.0),
                                 new Point(0, 1, 0),
                                 new Vector(0, 1, 0));

            Canvas canvas = Scene.current.Render(camera);
            Save.SaveCanvas(canvas, "Chapter_8_Chalenge");
            Scene.current.Clear();
            Console.WriteLine("Image created.");
        }
    }
}
