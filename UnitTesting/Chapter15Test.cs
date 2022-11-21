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
    class Chapter15Test
    {
        [Test, Order(1)]
        public void T01_CreateATriangle()
        {
            Point p1 = new Point(0, 1, 0);
            Point p2 = new Point(-1, 0, 0);
            Point p3 = new Point(1, 0, 0);

            Triangle triangle = new Triangle(p1, p2, p3);

            Assert.AreEqual(p1, triangle.GetP1());
            Assert.AreEqual(p2, triangle.GetP2());
            Assert.AreEqual(p3, triangle.GetP3());

            Assert.AreEqual(new Vector(-1, -1, 0), triangle.GetE1());
            Assert.AreEqual(new Vector(1, -1, 0), triangle.GetE2());
            Assert.AreEqual(new Vector(0, 0, -1), triangle.CalculateLocalNormal(new Point()));
        }

        [Test, Order(2)]
        public void T02_NormalOnATriangle()
        {
            Vector normal = new Vector(0, 0, -1);
            Triangle t = new Triangle(new Point(0, 1, 0),
                                      new Point(-1, 0, 0),
                                      new Point(1, 0, 0));

            Vector normal1 = t.CalculateLocalNormal(new Point(0, 0.5, 0));
            Vector normal2 = t.CalculateLocalNormal(new Point(-0.5, 0.75, 0));
            Vector normal3 = t.CalculateLocalNormal(new Point(0.5, 0.25, 0));

            Assert.AreEqual(normal, normal1);
            Assert.AreEqual(normal, normal2);
            Assert.AreEqual(normal, normal3);

            Assert.AreEqual(t.n1, normal1);
            Assert.AreEqual(t.n2, normal2);
            Assert.AreEqual(t.n3, normal3);
        }

        [Test, Order(3)]
        public void T03_RayParallelToTriangle()
        {
            Triangle t = new Triangle(new Point(0, 1, 0),
                                      new Point(-1, 0, 0),
                                      new Point(1, 0, 0));
            Ray ray = new Ray(new Point(0, -1, -2), new Vector(0, 1, 0));
            List<Intersection> xs = t.Intersect(ray);
            Assert.IsEmpty(xs);
        }

        [Test, Order(4)]
        public void T04_RayMissesEdges()
        {
            Triangle t = new Triangle(new Point(0, 1, 0),
                                      new Point(-1, 0, 0),
                                      new Point(1, 0, 0));
            Ray r = new Ray(new Point(1, 1, -2), new Vector(0, 0, 1));
            List<Intersection> xs = t.Intersect(r);
            Assert.IsEmpty(xs);

            r = new Ray(new Point(-1, 1, -2), new Vector(0, 0, 1));
            xs = t.Intersect(r);
            Assert.IsEmpty(xs);

            r = new Ray(new Point(0, -1, -2), new Vector(0, 0, 1));
            xs = t.Intersect(r);
            Assert.IsEmpty(xs);
        }

        [Test, Order(5)]

        public void T05_RayStrikesTriangle()
        {
            Triangle t = new Triangle(new Point(0, 1, 0),
                                      new Point(-1, 0, 0),
                                      new Point(1, 0, 0));
            Ray ray = new Ray(new Point(0, 0.5, -2), new Vector(0, 0, 1));
            List<Intersection> xs = t.Intersect(ray);
            Assert.AreEqual(1, xs.Count);
            Assert.AreEqual(2, xs[0].t);
        }

        [Test, Order(6)]
        public void T06_OBJParserIgnoring()
        {
            ObjLoader loader = new ObjLoader();
            loader.Load(@"ObjFiles\Test.obj");
            Assert.AreEqual(5, loader.GetLinesIgnored());
        }

        [Test, Order(7)]
        public void T07_OBJParserVertices()
        {
            ObjLoader loader = new ObjLoader();
            loader.Load(@"ObjFiles\Vertices.obj");
            Assert.AreEqual(new Point(-1, 1, 0), loader.v[0]);
            Assert.AreEqual(new Point(-1, 0.5, 0), loader.v[1]);
            Assert.AreEqual(new Point(1, 0, 0), loader.v[2]);
            Assert.AreEqual(new Point(1, 1, 0), loader.v[3]);
        }

        [Test, Order(8)]
        public void T08_OBJParserFaces()
        {
            ObjLoader loader = new ObjLoader();
            loader.Load(@"ObjFiles\TriangleFaces.obj");

            Assert.AreEqual(loader.v[0], loader.t[0].GetP1());
            Assert.AreEqual(loader.v[1], loader.t[0].GetP2());
            Assert.AreEqual(loader.v[2], loader.t[0].GetP3());

            Assert.AreEqual(loader.v[0], loader.t[1].GetP1());
            Assert.AreEqual(loader.v[2], loader.t[1].GetP2());
            Assert.AreEqual(loader.v[3], loader.t[1].GetP3());
        }

        [Test, Order(9)]
        public void T09_TriangulatingPolygons()

        {
            ObjLoader loader = new ObjLoader();
            loader.Load(@"ObjFiles\PolygonData.obj");

            Assert.AreEqual(loader.v[0], loader.t[0].GetP1());
            Assert.AreEqual(loader.v[1], loader.t[0].GetP2());
            Assert.AreEqual(loader.v[2], loader.t[0].GetP3());

            Assert.AreEqual(loader.v[0], loader.t[1].GetP1());
            Assert.AreEqual(loader.v[2], loader.t[1].GetP2());
            Assert.AreEqual(loader.v[3], loader.t[1].GetP3());

            Assert.AreEqual(loader.v[0], loader.t[2].GetP1());
            Assert.AreEqual(loader.v[3], loader.t[2].GetP2());
            Assert.AreEqual(loader.v[4], loader.t[2].GetP3());
        }

        [Test, Order(10)]
        public void T10_OBJToGroup()
        {
            ObjLoader loader = new ObjLoader();
            loader.Load(@"ObjFiles\Groups.obj");
            Assert.AreEqual(2, loader.root.GetChildren().Count);
        }

        [Test, Order(11)]
        public void T11_MayaOBJFile()
        {
            if (Scene.current == null)
                new Scene();

            Light light = new Light(new Point(5, 7, -5), new Color(1, 1, 1));

            ObjLoader loader = new ObjLoader();
            loader.Load(@"ObjFiles\teapot_low.obj");
            loader.root.SetMatrix(Mat4.TranslateMatrix(0, 0, 0) * Mat4.ScaleMatrix(0.05, 0.05, 0.05) * Mat4.RotateYMatrix(Math.PI / 5) * Mat4.RotateXMatrix(-Math.PI/2));

            StripePattern s1 = new StripePattern(new SolidColorPattern(Color.white),
                                                 new SolidColorPattern(Color.red));
            StripePattern s2 = new StripePattern(new SolidColorPattern(Color.white),
                                                 new SolidColorPattern(Color.red));
            s1.matrix = Mat4.RotateYMatrix(Math.PI / 2.0);
            BlendPattern blend = new BlendPattern(s1, s2);

            Cube c = new Cube();
            c.material = new Material(Color.white);
            c.SetMatrix(Mat4.TranslateMatrix(0, -2, 0) * Mat4.ScaleMatrix(2, 2, 2));
            c.material.pattern = blend;
            c.material.pattern.matrix = Mat4.ScaleMatrix(0.2, 0.2, 0.2);

            Camera camera = new Camera(100, 66, Math.PI / 3.0);
            camera.ViewTransform(new Point(0, 1.5, -3),
                                 new Point(0, 0, 0),
                                 new Vector(0, 1, 0));

            Canvas canvas = Scene.current.Render(camera);

            Save.SaveCanvas(canvas, "Chapter15_teapot_low");
            Scene.current.Clear();
            Console.WriteLine("Image created.");
        }

        [Test, Order(12)]
        public void T12_SmoothTriangles()
        {
            Point p1 = new Point(0, 1, 0);
            Point p2 = new Point(-1, 0, 0);
            Point p3 = new Point(1, 0, 0);

            Vector n1 = new Vector(0, 1, 0);
            Vector n2 = new Vector(-1, 0, 0);
            Vector n3 = new Vector(1, 0, 0);

            Triangle st = new Triangle(p1, p2, p3, n1, n2, n3);

            Assert.AreEqual(p1, st.GetP1());
            Assert.AreEqual(p2, st.GetP2());
            Assert.AreEqual(p3, st.GetP3());

            Assert.AreEqual(n1, st.n1);
            Assert.AreEqual(n2, st.n2);
            Assert.AreEqual(n3, st.n3);
        }

        [Test, Order(13)]
        public void T13_Intersection()
        {
            Triangle triangle = new Triangle(new Point(0, 1, 0),
                                             new Point(-1, 0, 0),
                                             new Point(1, 0, 0));

            Intersection i = new Intersection(triangle, 3.5, 0.2, 0.4);
        }

        [Test, Order(14)]
        public void T14_SmoothTrianglesIntersection()
        {
            Point p1 = new Point(0, 1, 0);
            Point p2 = new Point(-1, 0, 0);
            Point p3 = new Point(1, 0, 0);

            Vector n1 = new Vector(0, 1, 0);
            Vector n2 = new Vector(-1, 0, 0);
            Vector n3 = new Vector(1, 0, 0);

            Triangle st = new Triangle(p1, p2, p3, n1, n2, n3);

            Ray ray = new Ray(new Point(-0.2, 0.3, -2),
                              new Vector(0, 0, 1));

            List<Intersection> xs = st.Intersect(ray);

            Assert.IsTrue(Utility.FE(0.45, xs[0].u));
            Assert.IsTrue(Utility.FE(0.25, xs[0].v));
        }


        [Test, Order(15)]
        public void T15_NormalInterpolation()
        {
            Point p1 = new Point(0, 1, 0);
            Point p2 = new Point(-1, 0, 0);
            Point p3 = new Point(1, 0, 0);

            Vector n1 = new Vector(0, 1, 0);
            Vector n2 = new Vector(-1, 0, 0);
            Vector n3 = new Vector(1, 0, 0);

            Triangle st = new Triangle(p1, p2, p3, n1, n2, n3);

            Intersection i = new Intersection(st, 1, 0.45, 0.25);

            Vector n = st.GetNormal(new Point(0, 0, 0), i);

            Assert.AreEqual(new Vector(-0.5547, 0.83205, 0), n);
        }

        [Test, Order(16)]
        public void T16_SmoothTriangleNormalPreparation()
        {
            Point p1 = new Point(0, 1, 0);
            Point p2 = new Point(-1, 0, 0);
            Point p3 = new Point(1, 0, 0);

            Vector n1 = new Vector(0, 1, 0);
            Vector n2 = new Vector(-1, 0, 0);
            Vector n3 = new Vector(1, 0, 0);

            Triangle st = new Triangle(p1, p2, p3, n1, n2, n3);

            Intersection i = new Intersection(st, 1, 0.45, 0.25);

            Ray r = new Ray(new Point(-0.2, 0.3, -2), new Vector(0, 0, 1));

            List<Intersection> xs = st.Intersect(r);

            Computations c = Computations.Prepare(i, r, xs);

            Assert.AreEqual(new Vector(-0.5547, 0.83205, 0), c.normal);
        }

        [Test, Order(17)]
        public void T17_VertexNormalOBJ()
        {
            ObjLoader loader = new ObjLoader();
            loader.Load(@"ObjFiles\VertexNormal.obj");
            Assert.AreEqual(new Vector(0, 0, 1), loader.n[0]);
            Assert.AreEqual(new Vector(0.707, 0, -0.707), loader.n[1]);
            Assert.AreEqual(new Vector(1, 2, 3), loader.n[2]);
        }

        [Test, Order(18)]   // 24:00 in video
        public void T18_FacesWithNormalVectors()
        {
            ObjLoader loader = new ObjLoader();
            loader.Load(@"ObjFiles\FacesVertexNormal.obj");

            List<RayObject> g = loader.root.GetChildren();

            Triangle t1 = (Triangle)g[0];
            Triangle t2 = (Triangle)g[1];

            Assert.AreEqual(new Vector(0, 1, 0), t1.n1);
            Assert.AreEqual(new Vector(-1, 0, 0), t1.n2);
            Assert.AreEqual(new Vector(1, 0, 0), t1.n3);

            Assert.AreEqual(new Vector(0, 1, 0), t2.n1);
            Assert.AreEqual(new Vector(-1, 0, 0), t2.n2);
            Assert.AreEqual(new Vector(1, 0, 0), t2.n3);
        }
    }
}
