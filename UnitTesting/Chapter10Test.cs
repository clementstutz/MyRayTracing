using NUnit.Framework;
using RT.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.UnitTesting
{
    [TestFixture]
    class Chapter10Test
    {
        [Test, Order(1)]
        public void T01_StripePattern()
        {
            //StripePattern pattern_0 = new StripePattern();
            //Assert.AreEqual(Color.white, pattern_0.a);
            //Assert.AreEqual(Color.black, pattern_0.b);


            StripePattern pattern = new StripePattern(new SolidColorPattern(Color.white),
                                                      new SolidColorPattern(Color.black));
            //StripePattern pattern = new StripePattern(Color.white, Color.black);
            Assert.AreEqual(Color.white, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(Color.black, pattern.PatternAt(new Point(1, 0, 0)));
            //Assert.AreEqual(Color.white, pattern.a);
            //Assert.AreEqual(Color.black, pattern.b);
        }

        [Test, Order(2)]
        public void T02_StripePatternResults()
        {
            StripePattern pattern = new StripePattern(new SolidColorPattern(Color.white),
                                                      new SolidColorPattern(Color.black));

            Color c = pattern.PatternAt(new Point(0, 0, 0));
            Assert.AreEqual(Color.white, c);

            c = pattern.PatternAt(new Point(0, 1, 0));
            Assert.AreEqual(Color.white, c);

            c = pattern.PatternAt(new Point(0, 2, 0));
            Assert.AreEqual(Color.white, c);

            c = pattern.PatternAt(new Point(0, 0, 1));
            Assert.AreEqual(Color.white, c);

            c = pattern.PatternAt(new Point(0, 0, 2));
            Assert.AreEqual(Color.white, c);

            c = pattern.PatternAt(new Point(0.9, 0, 0));
            Assert.AreEqual(Color.white, c);
            c = pattern.PatternAt(new Point(1.0, 0, 0));
            Assert.AreEqual(Color.black, c);
            c = pattern.PatternAt(new Point(-0.1, 0, 0));
            Assert.AreEqual(Color.black, c);
            c = pattern.PatternAt(new Point(-1.0, 0, 0));
            Assert.AreEqual(Color.black, c);
            c = pattern.PatternAt(new Point(-1.1, 0, 0));
            Assert.AreEqual(Color.white, c);
        }

        [Test, Order(3)]
        public void T03_LightingPattern()
        {
            if (Scene.current != null)
                Scene.current.Clear();

            Sphere sphere = new Sphere();
            StripePattern pattern = new StripePattern(new SolidColorPattern(Color.white),
                                                      new SolidColorPattern(Color.black));

            Material material = new Material();
            material.pattern = pattern;
            material.Ambient = 1.0;
            material.Diffuse = 0.0;
            material.Specular = 0.0;
            sphere.material = material;
            Vector eye = new Vector(0, 0, -1);
            Vector normal = new Vector(0, 0, -1);
            Light light = new Light(new Point(0, 0, -10), new Color(1, 1, 1));
            Color c1 = sphere.Lighting(new Point(0.9, 0, 0), light, eye, normal, false);
            Color c2 = sphere.Lighting(new Point(1.1, 0, 0), light, eye, normal, false);
            Assert.AreEqual(Color.white, c1);
            Assert.AreEqual(Color.black, c2);
        }

        [Test, Order(4)]
        public void T04_StripeTransformation()
        {
            if (Scene.current != null)
            {
                Scene.current.Clear();
            }

            Sphere sphere = new Sphere();
            sphere.SetMatrix(Mat4.ScaleMatrix(2, 2, 2));
            StripePattern pattern = new StripePattern(new SolidColorPattern(Color.white),
                                                      new SolidColorPattern(Color.black));

            Color color = pattern.PatternAtObject(sphere, new Point(1.5, 0, 0));
            Assert.AreEqual(Color.white, color);

            sphere.SetMatrix(new Mat4());
            pattern.matrix = Mat4.ScaleMatrix(2, 2, 2);
            color = pattern.PatternAtObject(sphere, new Point(1.5, 0, 0));
            Assert.AreEqual(Color.white, color);

            sphere.SetMatrix(Mat4.ScaleMatrix(2, 2, 2));
            pattern.matrix = Mat4.TranslateMatrix(0.5, 0, 0);
            color = pattern.PatternAtObject(sphere, new Point(2.5, 0, 0));
            Assert.AreEqual(Color.white, color);
        }

        [Test, Order(5)]    // Il y a une couille dans le pottage à cause de "transPoint = this.matrix.Inverse() * transPoint;" dans Pattern.cs !!!
        public void T05_TestPattern()
        {
            if (Scene.current != null)
            {
                Scene.current.Clear();
            }
            Scene scene = new Scene();
            Light light = new Light(new Point(-5, 3, -5), Color.white);
            Light light2 = new Light(new Point(5, 3, -5), new Color(1, 1, 1));

            Plane floor = new Plane();
            floor.material.pattern = new TestPattern();

            Plane ceiling = new Plane();
            ceiling.SetMatrix(Mat4.TranslateMatrix(0, 4, 0));
            ceiling.material.pattern = new TestPattern();

            Plane wall = new Plane();
            wall.SetMatrix(Mat4.RotateXMatrix(Math.PI / 2.0) *
                           Mat4.TranslateMatrix(0, 0, 4));
            wall.material.pattern = new TestPattern();

            Pattern pattern = new TestPattern();

            Assert.AreEqual(new Mat4(), pattern.matrix);

            pattern.matrix = Mat4.TranslateMatrix(1, 2, 3);
            Assert.AreEqual(Mat4.TranslateMatrix(1, 2, 3), pattern.matrix);

            Sphere sphere1 = new Sphere();
            //sphere1.SetMatrix(Mat4.ScaleMatrix(2, 2, 2));
            sphere1.SetMatrix(Mat4.TranslateMatrix(0, 1, -1));
            sphere1.material.pattern = new TestPattern();
            Color c = sphere1.material.pattern.PatternAtObject(sphere1, new Point(2, 3, 4));
            //Assert.AreEqual(new Color(1, 1.5, 2), c);

            Sphere sphere2 = new Sphere();
            //sphere2.SetMatrix(Mat4.TranslateMatrix(-2, 2, -2));
            sphere2.SetMatrix(Mat4.TranslateMatrix(-2, 0.5, -3.5)* Mat4.ScaleMatrix(0.5, 0.5, 0.5));
            sphere2.material.pattern = new TestPattern();
            sphere2.material.pattern.matrix = Mat4.TranslateMatrix(-2, 0.5, -3.5);
            c = sphere2.material.pattern.PatternAtObject(sphere2, new Point(2, 3, 4));
            //Assert.AreEqual(new Color(1, 1.5, 2), c);

            Sphere sphere3 = new Sphere();
            sphere3.SetMatrix(Mat4.TranslateMatrix(2, 1, -2));
            //sphere3.SetMatrix(Mat4.ScaleMatrix(2, 2, 2));
            sphere3.material.pattern = new TestPattern();
            sphere3.material.pattern.matrix = Mat4.TranslateMatrix(0.5, 1, -1.5);
            c = sphere3.material.pattern.PatternAtObject(sphere3, new Point(2.5, 3, 3.5));
            //Assert.AreEqual(new Color(0.75, 0.5, 0.25), c);

            Camera camera = new Camera(100, 66, Math.PI / 3.0);
            camera.ViewTransform(new Point(0, 2, -10),
                                 new Point(0, 2, 4),
                                 new Vector(0, 1, 0));

            Canvas canvas = Scene.current.Render(camera);

            Save.SaveCanvas(canvas, "Chapter_10_TestPattern");
            Scene.current.Clear();
            Console.WriteLine("Image created.");
        }

        [Test, Order(6)]
        public void T06_Gradient()
        {
            Pattern pattern = new GradientPattern(new SolidColorPattern(Color.white),
                                                  new SolidColorPattern(Color.black));

            Assert.AreEqual(Color.white, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(new Color(0.75, 0.75, 0.75), pattern.PatternAt(new Point(0.25, 0, 0)));
            Assert.AreEqual(new Color(0.5, 0.5, 0.5), pattern.PatternAt(new Point(0.5, 0, 0)));
            Assert.AreEqual(new Color(0.25, 0.25, 0.25), pattern.PatternAt(new Point(0.75, 0, 0)));
            Assert.AreEqual(new Color(1, 1, 1), pattern.PatternAt(new Point(1, 0, 0)));
        }


        [Test, Order(7)]
        public void T07_Ring()
        {
            Pattern pattern = new RingPattern(new SolidColorPattern(Color.white),
                                              new SolidColorPattern(Color.black));

            Assert.AreEqual(Color.white, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(Color.black, pattern.PatternAt(new Point(1, 0, 0)));
            Assert.AreEqual(Color.black, pattern.PatternAt(new Point(0, 0, 1)));
            Assert.AreEqual(Color.black, pattern.PatternAt(new Point(0.708, 0, 0.708)));
        }

        [Test, Order(8)]
        public void T08_3DChecker()
        {
            Pattern pattern = new CheckersPattern(new SolidColorPattern(Color.white),
                                                  new SolidColorPattern(Color.black));

            Assert.AreEqual(Color.white, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(Color.white, pattern.PatternAt(new Point(0.99, 0, 0)));
            Assert.AreEqual(Color.black, pattern.PatternAt(new Point(1.01, 0, 0)));

            Assert.AreEqual(Color.white, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(Color.white, pattern.PatternAt(new Point(0, 0.99, 0)));
            Assert.AreEqual(Color.black, pattern.PatternAt(new Point(0, 1.01, 0)));

            Assert.AreEqual(Color.white, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(Color.white, pattern.PatternAt(new Point(0, 0, 0.99)));
            Assert.AreEqual(Color.black, pattern.PatternAt(new Point(0, 0, 1.01)));
        }

        [Test, Order(9)]
        public void T09_BasicScene()
        {
            if (Scene.current != null)
            {
                Scene.current.Clear();
            }
            Scene scene = new Scene();
            Light light = new Light(new Point(-5, 3, -5), Color.white);
            Light light2 = new Light(new Point(5, 1, -5), new Color(0.6, 0.6, 0.15));

            Plane floor = new Plane();
            floor.material.pattern = new RingPattern(new SolidColorPattern(Color.red),
                                                      new SolidColorPattern(Color.green));

            Plane ceiling = new Plane();
            ceiling.material.pattern = new GradientPattern(new SolidColorPattern(Color.white),
                                                           new SolidColorPattern(Color.magenta));
            ceiling.SetMatrix(Mat4.TranslateMatrix(0, 4, 0)); 
            
            Plane wall = new Plane();
            wall.material.pattern = new StripePattern(new SolidColorPattern(Color.white),
                                                      new SolidColorPattern(Color.black));
            wall.SetMatrix(Mat4.TranslateMatrix(0, 0, 4) *
                           Mat4.RotateXMatrix(Math.PI / 2.0));

            Sphere sphere1 = new Sphere();
            sphere1.SetMatrix(Mat4.TranslateMatrix(0, 0.5, -3) *
                              Mat4.ScaleMatrix(0.5, 0.5, 0.5));
            sphere1.material.pattern = new CheckersPattern(new SolidColorPattern(Color.yellow),
                                                           new SolidColorPattern(Color.blue));

            Sphere sphere2 = new Sphere();
            sphere2.SetMatrix(Mat4.TranslateMatrix(2, 1, -1));
            sphere2.material.pattern = new GradientPattern(new SolidColorPattern(Color.white),
                                                           new SolidColorPattern(Color.green));

            Camera camera = new Camera(100, 66, Math.PI / 3.0);
            camera.ViewTransform(new Point(0, 2, -10),
                                 new Point(0, 2, 4),
                                 new Vector(0, 1, 0));

            Canvas canvas = Scene.current.Render(camera);

            Save.SaveCanvas(canvas, "Chapter_10_BasicScene");
            Scene.current.Clear();
            Console.WriteLine("Image created.");
        }

        [Test, Order(10)]
        public void T10_RadialGradientPattern()
        {
            //if (Scene.current != null)
            //{
            //    Scene.current.Clear();
            //}

            Pattern pattern = new RadialGradientPattern();
            Scene scene = new Scene();
            Light light = new Light(new Point(-5, 3, -5), Color.white);
            Light light2 = new Light(new Point(5, 1, -5), new Color(0.6, 0.6, 0.15));

            Plane floor = new Plane();
            floor.material.pattern = new RingPattern(new SolidColorPattern(Color.red),
                                                     new SolidColorPattern(Color.green));

            Plane wall = new Plane();
            wall.SetMatrix(Mat4.TranslateMatrix(0, 0, 4) *
                           Mat4.RotateXMatrix(Math.PI / 2.0));
            wall.material.pattern = pattern;

            Sphere sphere1 = new Sphere();
            sphere1.SetMatrix(Mat4.TranslateMatrix(0, 0.5, -2) *
                              Mat4.ScaleMatrix(0.5, 0.5, 0.5));
            sphere1.material.pattern = pattern;

            Sphere sphere2 = new Sphere();
            sphere2.SetMatrix(Mat4.TranslateMatrix(2, 1, 0));
            sphere2.material.pattern = pattern;

            Camera camera = new Camera(100, 66, Math.PI / 3.0);
            camera.ViewTransform(new Point(0, 2, -10),
                                 new Point(0, 2, 4),
                                 new Vector(0, 1, 0));

            Canvas canvas = Scene.current.Render(camera);

            Save.SaveCanvas(canvas, "Chapter_10_RadialGradientPattern");
            Scene.current.Clear();
            Console.WriteLine("Image created.");
        }

        [Test, Order(11)]
        public void T11_NestedPatterns()
        {
            RadialGradientPattern radialPattern = new RadialGradientPattern();
            CheckersPattern checkerBoard = new CheckersPattern();
            checkerBoard.a = radialPattern;
            checkerBoard.b = new SolidColorPattern(Color.green);

            Scene scene = new Scene();
            Light light = new Light(new Point(-5, 5, -5), Color.white);
            //Light light2 = new Light(new Point(5, 0.4, -5), new Color(0.6, 0.6, 0.15));

            Plane floor = new Plane();
            floor.material.pattern = new RingPattern(new SolidColorPattern(Color.white),
                                                     new SolidColorPattern(Color.black));

            Plane wall = new Plane();
            wall.SetMatrix(Mat4.TranslateMatrix(0, 0, 4) *
                           Mat4.RotateXMatrix(Math.PI / 2.0));
            wall.material.pattern = checkerBoard;

            Sphere sphere1 = new Sphere();
            sphere1.SetMatrix(Mat4.TranslateMatrix(0, 0.5, -3) *
                              Mat4.ScaleMatrix(0.5, 0.5, 0.5));
            sphere1.material.pattern = checkerBoard;

            Sphere sphere2 = new Sphere();
            sphere2.SetMatrix(Mat4.TranslateMatrix(2, 1, -1));
            sphere2.material.pattern = checkerBoard;

            Camera camera = new Camera(100, 66, Math.PI / 3.0);
            camera.ViewTransform(new Point(0, 2, -10),
                                 new Point(0, 2, 4),
                                 new Vector(0, 1, 0));

            Canvas canvas = Scene.current.Render(camera);

            Save.SaveCanvas(canvas, "Chapter_10_NestedPatterns_1");
            Scene.current.Clear();
            Console.WriteLine("Image created.");


            //RadialGradientPattern radialPattern = new RadialGradientPattern();
            RingPattern checkerBoard2 = new RingPattern();
            checkerBoard2.a = new CheckersPattern(new SolidColorPattern(Color.white), new SolidColorPattern(Color.black));
            checkerBoard2.b = new RadialGradientPattern(new SolidColorPattern(Color.red), new SolidColorPattern(Color.green));


            //scene.Clear();
            //Scene.current.Clear();
            Scene.current = null;
            scene = new Scene();
            Light light2 = new Light(new Point(-5, 5, -5), Color.white);
            //Light light2 = new Light(new Point(5, 0.4, -5), new Color(0.6, 0.6, 0.15));

            Plane floor2 = new Plane();
            floor2.material.pattern = checkerBoard2;

            Plane wall2 = new Plane();
            wall2.SetMatrix(Mat4.TranslateMatrix(0, 0, 4) *
                           Mat4.RotateXMatrix(Math.PI / 2.0));
            wall2.material.pattern = checkerBoard2;

            Sphere sphere12 = new Sphere();
            sphere12.SetMatrix(Mat4.TranslateMatrix(0, 0.5, -3) *
                              Mat4.ScaleMatrix(0.5, 0.5, 0.5));
            sphere12.material.pattern = checkerBoard2;

            Sphere sphere22 = new Sphere();
            sphere22.SetMatrix(Mat4.TranslateMatrix(2, 1, -1));
            sphere22.material.pattern = checkerBoard2;

            Camera camera2 = new Camera(100, 66, Math.PI / 3.0);
            camera2.ViewTransform(new Point(0, 2, -10),
                                 new Point(0, 2, 4),
                                 new Vector(0, 1, 0));

            Canvas canvas2 = Scene.current.Render(camera2);

            Save.SaveCanvas(canvas2, "Chapter_10_NestedPatterns_2");
            Scene.current.Clear();
            Console.WriteLine("Image created.");
        }

        [Test, Order(12)]
        public void T12_BlendTPatterns()
        {
            if (Scene.current == null)
            {
                new Scene();
            }
            Scene.current.Clear();

            StripePattern s1 = new StripePattern(new SolidColorPattern(Color.white),
                                                 new SolidColorPattern(Color.green));
            StripePattern s2 = new StripePattern(new SolidColorPattern(Color.yellow),
                                                 new SolidColorPattern(Color.magenta));

            s1.matrix = Mat4.RotateYMatrix(Math.PI / 2.0);

            BlendPattern blend = new BlendPattern(s1, s2);

            Light light = new Light(new Point(-5, 5, -5), Color.white);

            Plane floor = new Plane();
            floor.material.pattern = blend;

            Plane wall = new Plane();
            wall.SetMatrix(Mat4.TranslateMatrix(0, 0, 4) *
                           Mat4.RotateXMatrix(Math.PI / 2.0));
            wall.material.pattern = blend;

            Sphere sphere1 = new Sphere();
            sphere1.SetMatrix(Mat4.TranslateMatrix(0, 1.5, -3) *
                              Mat4.ScaleMatrix(1.5, 1.5, 1.5));
            BlendPattern blendS1 = new BlendPattern(s1, s2, 0.2);
            sphere1.material.pattern = blendS1;
            sphere1.material.pattern.matrix = Mat4.ScaleMatrix(0.2, 0.2, 0.2);

            Sphere sphere2 = new Sphere();
            sphere2.SetMatrix(Mat4.TranslateMatrix(2, 1, -1));
            sphere2.material.pattern = blend;

            Camera camera = new Camera(100, 66, Math.PI / 3.0);
            camera.ViewTransform(new Point(0, 2, -10),
                                 new Point(0, 2, 4),
                                 new Vector(0, 1, 0));

            Canvas canvas = Scene.current.Render(camera);

            Save.SaveCanvas(canvas, "Chapter_10_BlendTPatterns");
            Scene.current.Clear();
            Console.WriteLine("Image created.");
        }
    }
}
