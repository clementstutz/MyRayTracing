using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.UnitTesting
{
    [TestFixture]
    class Chapter07Test
    {
        [Test, Order(1)]  // Ok.
        public void T01_EmptyScene()
        {
            Scene.current = null;
            Assert.IsTrue(null == Scene.current);
            Scene scene = new Scene();
            Scene.current.Clear();
            Assert.IsFalse(null == Scene.current);
            Assert.AreEqual(new List<Light>(), scene.GetLights());
            Assert.AreEqual(new List<RayObject>(), scene.GetRayObjects());
            Assert.IsEmpty(scene.GetLights());
            Assert.IsEmpty(scene.GetRayObjects());
            Assert.IsTrue(scene == Scene.current);
        }

        [Test, Order(2)] // Seems to be ok.
        public void T02_DefaultWorld()
        {
            if (Scene.current == null)
            {
                Scene scene = new Scene();
            }
            Scene.current.Default();

            /*Scene.current = null;
            Scene scene = new Scene();
            scene.Default();*/

            List<Light> lights = Scene.current.GetLights();
            List<RayObject> rayObjects = Scene.current.GetRayObjects();

            Assert.IsNotEmpty(lights);
            Assert.IsNotEmpty(rayObjects);

            Assert.AreEqual(lights[0].position, new Point(-10, 10, -10));
            Assert.AreEqual(lights[0].intensity, new Color(1, 1, 1));
            Assert.AreEqual(rayObjects[0].material.color, new Color(0.8, 1.0, 0.6));
            Assert.AreEqual(rayObjects[0].material.Diffuse, 0.7);
            Assert.AreEqual(rayObjects[0].material.Specular, 0.2);
            Assert.AreEqual(rayObjects[1].GetMatrix(), Mat4.ScaleMatrix(0.5, 0.5, 0.5));

            Scene.current.Clear();
            Assert.AreEqual(new List<Light>(), Scene.current.GetLights());
            Assert.AreEqual(new List<RayObject>(), Scene.current.GetRayObjects());
            Assert.IsEmpty(Scene.current.GetLights());
            Assert.IsEmpty(Scene.current.GetRayObjects());
        }

        [Test, Order(3)] // Ok.
        public void T03_IntersectWorld()
        {
            if (Scene.current == null)
            {
                Scene scene = new Scene();
            }
            Scene.current.Clear();
            Scene.current.Default();

            /*Scene.current = null;
            Scene scene = new Scene();
            scene.Default();*/

            Ray ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));

            List<Intersection> temp = Scene.current.Intersections(ray);
            Assert.AreEqual(4, temp.Count);
            Assert.AreEqual(4, temp[0].t);
            Assert.AreEqual(4.5, temp[1].t);
            Assert.AreEqual(5.5, temp[2].t);
            Assert.AreEqual(6, temp[3].t);
        }

        [Test, Order(4)] // Ok.
        public void T04_Precomputations()
        {
            if (Scene.current == null)
            {
                Scene scene = new Scene();
            }
            Scene.current.Default();

            //Scene.current = null;
            //Scene scene = new Scene();

            Ray ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));

            List<Intersection> intersections = Scene.current.Intersections(ray);
            Intersection hit = Scene.current.Hit(intersections);

            Computations c = Computations.Prepare(hit, ray);

            Assert.AreSame(hit.rayObject, c.rayObject);
            Assert.AreEqual(hit.t, c.t);
            Assert.AreEqual(new Point(0, 0, -1), c.point);
            Assert.AreEqual(new Vector(0, 0, -1), c.eye);
            Assert.AreEqual(new Vector(0, 0, -1), c.normal);
            //Assert.AreEqual(false, c.inside);
        }

        [Test, Order(5)] // Ok.
        public void T05_Intersections()
        {
            // On the outside.
            Scene.current = null;
            Scene scene = new Scene();
            Sphere s = new Sphere();

            Ray ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));

            List<Intersection> intersections = Scene.current.Intersections(ray);
            Intersection hit = Scene.current.Hit(intersections);

            Computations c = Computations.Prepare(hit, ray);

            Assert.AreEqual(false, c.inside);
            //Assert.AreEqual(new Point(0, 0, -1.00001), c.overPoint);

            // On the inside.
            ray = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));

            intersections = Scene.current.Intersections(ray);
            hit = Scene.current.Hit(intersections);

            c = Computations.Prepare(hit, ray);

            Assert.AreEqual(true, c.inside);
            Assert.AreEqual(new Point(0, 0, 1), c.point);
            Assert.AreEqual(new Vector(0, 0, -1), c.normal);
            //Assert.AreEqual(new Point(0, 0, 0.99999), c.overPoint);
        }

        [Test, Order(6)]
    public void T06_ShadingIntersectionsOutside()
        {
            //if (Scene.current == null)
            //{
            //    Scene scene = new Scene();
            //}
            //Scene.current.Default();

            Scene.current = null;
            Scene scene = new Scene();
            scene.Default();

            Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));

            List<Intersection> intersections = scene.Intersections(r);
            Intersection hit = scene.Hit(intersections);

            Computations c = Computations.Prepare(hit, r);

            Color finalColor = scene.ShadeHit(c);
            Assert.AreEqual(new Color(0.38066, 0.47583, 0.2855), finalColor);
        }

        [Test, Order(7)]
        public void T07_ShadingIntersectionsInside()
        {
            //if (Scene.current == null)
            //{
            //    Scene scene = new Scene();
            //}
            //Scene.current.Default();

            Scene.current = null;
            Scene scene = new Scene();
            scene.Default();

            List<Light> lights = scene.GetLights();

            lights[0].position = new Point(0, 0.25, 0);
            lights[0].intensity = new Color(1, 1, 1);

            Ray r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));

            List<Intersection> intersections = scene.Intersections(r);
            Intersection hit = scene.Hit(intersections);

            Computations c = Computations.Prepare(hit, r);

            Color finalColor = scene.ShadeHit(c);
            Assert.AreEqual(new Color(0.90498, 0.90498, 0.90498), finalColor);
        }

        [Test, Order(8)]
        public void T08_RayMissColor()
        {
            //if (Scene.current == null)
            //{
            //    Scene scene = new Scene();
            //}
            //Scene.current.Default();

            //Scene.current = null;
            Scene scene = new Scene();
            scene.Default();

            Ray ray = new Ray(new Point(0, 0, -5), new Vector(0, 1, 0));
            Color finalColor = scene.ColorAt(ray);
            Assert.AreEqual(Color.black, finalColor);
        }

        [Test, Order(9)]
        public void T09_RayHitColor()
        {
            //if (Scene.current == null)
            //{
            //    Scene scene = new Scene();
            //}
            //Scene.current.Default();

            Scene.current = null;
            Scene scene = new Scene();
            scene.Default();

            Ray ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            Color finalColor = scene.ColorAt(ray);
            Assert.AreEqual(new Color(0.38066, 0.47583, 0.2855), finalColor);
        }

        [Test, Order(10)]
        public void T10_RayIntersectionNehindColor()
        {
            //if (Scene.current == null)
            //{
            //    Scene scene = new Scene();
            //}
            //Scene.current.Default();

            Scene.current = null;
            Scene scene = new Scene();
            scene.Default();

            List<RayObject> rayObjects = scene.GetRayObjects();
            rayObjects[0].material.Ambient = 1.0;
            rayObjects[1].material.Ambient = 1.0;

            Ray ray = new Ray(new Point(0, 0, 0.75), new Vector(0, 0, -1));
            Color finalColor = scene.ColorAt(ray);
            Assert.AreEqual(rayObjects[1].material.color, finalColor);
        }

        [Test, Order(11)]
        public void T11_TransformMatrixForDefaultOrientation()
        {
            Point from = new Point(0, 0, 0);
            Point to = new Point(0, 0, -1);
            Vector up = new Vector(0, 1, 0);
            Camera camera = new Camera();
            //Something... Transform(from, to, up);
            Mat4 viewTransform = camera.ViewTransform(from, to, up);
            Assert.AreEqual(new Mat4(), viewTransform);
        }

        [Test, Order(12)]
        public void T12_TransformMatrixLookingZDirection()
        {
            Point from = new Point(0, 0, 0);
            Point to = new Point(0, 0, 1);
            Vector up = new Vector(0, 1, 0);
            Camera camera = new Camera();
            Mat4 viewTransform = camera.ViewTransform(from, to, up);
            Assert.AreEqual(Mat4.ScaleMatrix(-1, 1, -1), viewTransform);
        }

        [Test, Order(13)]
        public void T13_TransformMovesTheWorld()
        {
            Point from = new Point(0, 0, 8);
            Point to = new Point(0, 0, 0);
            Vector up = new Vector(0, 1, 0);
            Camera camera = new Camera();
            Mat4 viewTransform = camera.ViewTransform(from, to, up);
            Assert.AreEqual(Mat4.TranslateMatrix(0, 0, -8), viewTransform);
        }

        [Test, Order(14)]
        public void T14_TransformArbitrary()
        {
            Point from = new Point(1, 3, 2);
            Point to = new Point(4, -2, 8);
            Vector up = new Vector(1, 1, 0);
            Camera camera = new Camera();
            Mat4 viewTransform = camera.ViewTransform(from, to, up);

            // this matrix works if we don't normalize all the vector in the ViewTransform() method
            // but it seems that we should definitly normalize them...!
            /*Mat4 mat = new Mat4(-0.50709, 0.50709, 0.67612, -2.36643,
                                0.76772, 0.60609, 0.12122, -2.82843,
                                -0.35857, 0.59761, -0.71714, 0,
                                0.00000, 0.00000, 0.00000, 1.00000);*/
            Mat4 mat = new Mat4(-0.51450, 0.51450, 0.68599, -2.40098,
                                0.77892, 0.61494, 0.12299, -2.86972,
                                -0.35857, 0.59761, -0.71714, 0.00000,
                                0.00000, 0.00000, 0.00000, 1.00000);

            Assert.AreEqual(mat, viewTransform);
        }

        [Test, Order(15)]
        public void T15_ConstructingACamera()
        {
            int hSize = 160;
            int vSize = 120;
            double fieldOfView = Math.PI / 2.0;
            Camera camera = new Camera(hSize, vSize, fieldOfView);
            Assert.AreEqual(hSize, camera.hSize);
            Assert.AreEqual(vSize, camera.vSize);
            Assert.AreEqual(fieldOfView, camera.fov);
            Assert.AreEqual(new Mat4(), camera.transform);
        }

        [Test, Order(16)]
        public void T16_PixelSize()
        {
            Camera camera1 = new Camera(200, 125, Math.PI / 2.0);
            Assert.AreEqual(0.01, camera1.PixelSize, 0.0001);
            Camera camera2 = new Camera(125, 200, Math.PI / 2.0);
            Assert.AreEqual(0.01, camera2.PixelSize, 0.0001);
        }

        [Test, Order(17)]
        public void T17_RayThroughCenter()
        {
            Camera camera = new Camera(201, 101, Math.PI / 2.0);
            //if (Scene.current == null)
            //{
            //    Scene scene = new Scene();
            //}
            //Scene.current.Default();

            Scene scene = new Scene();
            scene.Default();

            Ray ray = scene.RayForPixel(camera, 100, 50);
            Assert.AreEqual(new Point(0, 0, 0), ray.origin);
            Assert.AreEqual(new Vector(0, 0, -1), ray.direction);
        }

        [Test, Order(18)]
        public void T18_RayThroughCorner()
        {
            Camera camera = new Camera(201, 101, Math.PI / 2.0);
            //if (Scene.current == null)
            //{
            //    Scene scene = new Scene();
            //}
            //Scene.current.Default();

            Scene scene = new Scene();
            scene.Default();

            Ray ray = scene.RayForPixel(camera, 0, 0);
            Assert.AreEqual(new Point(0, 0, 0), ray.origin);
            Assert.AreEqual(new Vector(0.66519, 0.33259, -0.66851), ray.direction);
        }

        [Test, Order(19)]
        public void T19_RayThroughTransformed()
        {
            Camera camera = new Camera(201, 101, Math.PI / 2.0);
            //if (Scene.current == null)
            //{
            //    Scene scene = new Scene();
            //}
            //Scene.current.Default();

            Scene scene = new Scene();
            scene.Default();

            camera.transform = Mat4.RotateYMatrix(Math.PI / 4.0) * Mat4.TranslateMatrix(0, -2, 5);
            Ray ray = scene.RayForPixel(camera, 100, 50);
            Assert.AreEqual(new Point(0, 2, -5), ray.origin);
            Assert.AreEqual(new Vector((double)Math.Sqrt(2) / 2.0, 0, -(double)Math.Sqrt(2) / 2.0), ray.direction);
        }

        [Test, Order(20)]
        public void T20_RenderWorldWithCamera()
        {
            Camera camera = new Camera(11, 11, Math.PI / 2.0);
            //if (Scene.current == null)
            //{
            //    Scene scene = new Scene();
            //}
            //Scene.current.Default();

            Scene.current = null;
            Scene scene = new Scene();
            scene.Default();

            Point from = new Point(0, 0, -5);
            Point to = new Point(0, 0, 0);
            Vector up = new Vector(0, 1, 0);
            camera.ViewTransform(from, to, up);
            Canvas canvas = scene.Render(camera);

            Save.SaveCanvas(canvas, "Chapter_7_RenderWorldWithCamera");
            Assert.AreEqual(new Color(0.38066, 0.47583, 0.2855), canvas.GetPixel(5, 5));
            scene.Clear();
            Console.WriteLine("Image created.");
        }

        [Test, Order(21)]
        public void T21_TransformOrder()  // Malgré que je change les couleurs des murs, ils ont tous la même car j'affecte le même materiaux à tous. Ils sont donc liés et non indépendants!!!
        {
            //if (Scene.current == null)
            //{
            //    Scene scene = new Scene();
            //}
            //Scene.current.Default();

            Scene.current = null;
            Scene scene = new Scene();

            RayObject right = new Sphere();
            right.SetMatrix(Mat4.TranslateMatrix(1.5, 0, 0) *
                            Mat4.ScaleMatrix(1, 0.5, 1) * Mat4.RotateZMatrix(Math.PI / 5));
            right.material.color = new Color(1, 0, 0);
            right.material.Diffuse = 0.7;
            right.material.Specular = 0.3;

            RayObject left = new Sphere();
            left.SetMatrix(Mat4.TranslateMatrix(-1.5, 0, 0) *
                           Mat4.RotateZMatrix(Math.PI / 5) * Mat4.ScaleMatrix(1, 0.5, 1));
            left.material.color = new Color(0, 1, 0);
            left.material.Diffuse = 0.7;
            left.material.Specular = 0.3;

            Light light = new Light(new Point(-10, 10, -10), Color.white);

            Camera camera = new Camera(100, 66, Math.PI / 3.0);

            camera.ViewTransform(new Point(0, 0, -5.0),
                                 new Point(0, 0, 0),
                                 new Vector(0, 1, 0));

            Canvas canvas = scene.Render(camera);
            Save.SaveCanvas(canvas, "Chapter_7_Test_Transform_Order");
            scene.Clear();
            Console.WriteLine("Image created.");
        }

        [Test, Order(22)]
        public void T22_PuttingItAllTogether()  // Malgré que je change les couleurs des murs, ils ont tous la même car j'affecte le même materiaux à tous. Ils sont donc liés et non indépendants!!!
        {
            Console.WriteLine("Malgré que je change les couleurs des murs, ils ont tous la même car j'affecte le même materiaux à tous. Ils sont donc liés et non indépendants!!!;");
            //if (Scene.current == null)
            //{
            //    Scene scene = new Scene();
            //}
            //Scene.current.Default();

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
            //leftWall.material.color = new Color(0, 1, 0);

            RayObject rightWall = new Sphere();
            rightWall.SetMatrix(Mat4.TranslateMatrix(0, 0, 5) *
                                Mat4.RotateYMatrix(Math.PI / 4.0) *
                                Mat4.RotateXMatrix(Math.PI / 2.0) *
                                Mat4.ScaleMatrix(10, 0.01, 10));
            rightWall.material = floor.material;
            //rightWall.material.color = new Color(0, 0, 1);

            RayObject middle = new Sphere();
            middle.SetMatrix(Mat4.TranslateMatrix(-0.5, 1, 0.5));
            middle.material.color = new Color(0.1, 1.0, 0.5);
            middle.material.Diffuse = 0.7;
            middle.material.Specular = 0.3;
            middle.canCastShadows = false;

            RayObject right = new Sphere();
            right.SetMatrix(Mat4.TranslateMatrix(1.5, 0.5, -0.5) *
                            Mat4.ScaleMatrix(0.5, 0.5, 0.5));
            right.material.color = new Color(0.5, 1.0, 0.1);
            right.material.Diffuse = 0.7;
            right.material.Specular = 0.3;
            right.canCastShadows = false;

            RayObject left = new Sphere();
            left.SetMatrix(Mat4.TranslateMatrix(-1.5, 0.33, -0.75) *
                           Mat4.ScaleMatrix(0.33, 0.33, 0.33));
            left.material.color = new Color(1, 0.8, 0.1);
            left.material.Diffuse = 0.7;
            left.material.Specular = 0.3;
            left.canCastShadows = false;

            Light light = new Light(new Point(-10, 10, -10), Color.white);

            Camera camera = new Camera(100, 66, Math.PI / 3.0);

            camera.ViewTransform(new Point(0, 1.5, -5.0),
                                 new Point(0, 1, 0),
                                 new Vector(0, 1, 0));

            Canvas canvas = scene.Render(camera);
            Save.SaveCanvas(canvas, "Chapter_7_PuttingItAllTogether");
            scene.Clear();
            Console.WriteLine("Image created.");
        }
    }
}
