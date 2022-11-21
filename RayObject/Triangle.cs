using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Triangle : RayObject
    {
        protected Point p1;
        protected Point p2;
        protected Point p3;

        protected Vector e1;
        protected Vector e2;

        public Vector n1;
        public Vector n2;
        public Vector n3;

        public Triangle(Point p1, Point p2, Point p3, Vector n1 = null, Vector n2 = null, Vector n3 = null) : base()
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;

            this.n1 = n1;
            this.n2 = n2;
            this.n3 = n3;

            CalcE1();
            CalcE2();

            //If we were not given enough information for the normals, then calculate them.
            if (n1 == null || n2 == null || n3 == null)
                CalcNormal();
        }

        public Point GetP1()
        {
            return this.p1;
        }

        public Point GetP2()
        {
            return this.p2;
        }

        public Point GetP3()
        {
            return this.p3;
        }

        public void SetP1(Point p)
        {
            this.p1 = p;
            CalcE1();
            CalcE2();
        }

        public void SetP2(Point p)
        {
            this.p2 = p;
            CalcE1();
        }

        public void SetP3(Point p)
        {
            this.p3 = p;
            CalcE2();
        }

        public Vector GetE1()
        {
            return this.e1;
        }

        public Vector GetE2()
        {
            return this.e2;
        }

        protected void CalcE1()
        {
            if (p1 != null && p2 != null)
            {
                e1 = p2 - p1;
            }
        }

        protected void CalcE2()
        {
            if (p1 != null && p3 != null)
            {
                e2 = p3 - p1;
            }
        }

        //protected void CalcNormal()
        public void CalcNormal()
        {
            if (e1 != null && e2 != null)
            {
                Vector normal = Vector.Cross(e2, e1).Normalize();
                n1 = new Vector(normal);
                n2 = new Vector(normal);
                n3 = new Vector(normal);
            }
        }

        public override Vector CalculateLocalNormal(Point localPoint, Intersection i = null)
        {
            if (i != null)
            {
                return this.n2 * i.u + this.n3 * i.v + this.n1 * (1 - i.u - i.v);
            }
            return n1;
        }

        public override Vector GetNormal(Point worldPoint, Intersection i = null)
        {
            Point localPoint = this.WorldToObject(worldPoint);
            Vector localNormal = CalculateLocalNormal(localPoint, i);
            Vector worldNormal = NormalToWorld(localNormal);
            return worldNormal;
        }

        public override List<Intersection> Intersect(Ray ray)
        {
            Ray transRay = matrix.Inverse() * ray;
            //Ray localRay = RayToObjectSpace(ray);

            List<Intersection> intersections = new List<Intersection>();
            Vector dirCrossE2 = Vector.Cross(transRay.direction, e2);
            double det = Vector.Dot(e1, dirCrossE2);
            if (Math.Abs(det) < Utility.epsilon)
            {
                return intersections;
            }

            //If the value is not between 0 and 1 the ray misses.
            double f = 1.0 / det;

            Vector p1ToOrigin = transRay.origin - this.p1;
            double u = f * Vector.Dot(p1ToOrigin, dirCrossE2);
            if (u < 0 || u > 1)
                return intersections;

            Vector originCrossE1 = Vector.Cross(p1ToOrigin, e1);
            double v = f * Vector.Dot(ray.direction, originCrossE1);
            //double v = f * Vector.Dot(transRay.direction, originCrossE1); // WARNING : je ne sais pas quelle ligne est bonne..!

            if (v < 0 || (u + v) > 1)
                return intersections;

            double t = f * Vector.Dot(e2, originCrossE1);
            intersections.Add(new Intersection(this, t, u, v));
            return intersections;
        }

        public override Bounds GetLocalBounds() // WARNING : pre-calculate it to optimize performance. especially when *.obj file involves thousands of triangles.
        {
            //Look at trignales and determine min and max from points
            Bounds bounds = new Bounds();
            bounds.min = new Point(p1);
            bounds.max = new Point(p1);

            if (p2.x < bounds.min.x)
            {
                bounds.min.x = p2.x;
            }
            if (p2.y < bounds.min.y)
            {
                bounds.min.y = p2.y;
            }
            if (p2.z < bounds.min.z)
            {
                bounds.min.z = p2.z;
            }

            if (p2.x > bounds.max.x)
            {
                bounds.max.x = p2.x;
            }
            if (p2.y > bounds.max.y)
            {
                bounds.max.y = p2.y;
            }
            if (p2.z > bounds.max.z)
            {
                bounds.max.z = p2.z;
            }

            if (p3.x < bounds.min.x)
            {
                bounds.min.x = p3.x;
            }
            if (p3.y < bounds.min.y)
            {
                bounds.min.y = p3.y;
            }
            if (p3.z < bounds.min.z)
            {
                bounds.min.z = p3.z;
            }

            if (p3.x > bounds.max.x)
            {
                bounds.max.x = p3.x;
            }
            if (p3.y > bounds.max.y)
            {
                bounds.max.y = p3.y;
            }
            if (p3.z > bounds.max.z)
            {
                bounds.max.z = p3.z;
            }

            return bounds;
        }
    }
}
