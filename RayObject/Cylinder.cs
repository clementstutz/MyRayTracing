using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Cylinder : RayObject
    {
        public double minimum = double.NegativeInfinity;
        public double maximum = double.PositiveInfinity;
        public bool isClosed = false;   // WARNING : dissocier isClose en isTopClose et isBottomClose

        public Cylinder(double min = double.NegativeInfinity, double max = double.PositiveInfinity, bool isClosed = false) : base()
        {
            minimum = min;
            maximum = max;
            this.isClosed = isClosed;
        }

        public override Vector CalculateLocalNormal(Point localPoint, Intersection i = null)
        {
            double distance = localPoint.x * localPoint.x + localPoint.z * localPoint.z;

            if (distance < 1 && localPoint.y >= this.maximum - Utility.epsilon)
            {
                return new Vector(0, 1, 0);
            }

            else if (distance < 1 && localPoint.y <= this.minimum + Utility.epsilon)
            {
                return new Vector(0, -1, 0);
            }
            else
            {
                return new Vector(localPoint.x, 0, localPoint.z);
            }
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
            Ray transRay = RayToObjectSpace(ray);

            List<Intersection> xs = new List<Intersection>();

            double a = transRay.direction.x * transRay.direction.x +
                       transRay.direction.z * transRay.direction.z;

            // ray is parallel to the y axis
            if (Utility.FE(0, a))   // WARNING : vrai pour les cylindre ouvert mais pas pour les fermés, si !?
            {
                return xs;
            }

            double b = 2.0 * transRay.origin.x * transRay.direction.x +
                       2.0 * transRay.origin.z * transRay.direction.z;

            double c = transRay.origin.x * transRay.origin.x +
                       transRay.origin.z * transRay.origin.z - 1;

            double discriminant = b * b - 4 * a * c;

            //ray does not intersect the cylinder
            if (discriminant < 0)
                return xs;

            double t0 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            double t1 = (-b + Math.Sqrt(discriminant)) / (2 * a);

            if (t0 > t1)
            {
                double temp = t0;
                t0 = t1;
                t1 = temp;
            }

            double y0 = transRay.origin.y + transRay.direction.y * t0;

            if (this.minimum < y0 && y0 < this.maximum)
            {
                xs.Add(new Intersection(this, t0));
            }

            double y1 = transRay.origin.y + transRay.direction.y * t1;

            if (this.minimum < y1 && y1 < this.maximum)
            {
                xs.Add(new Intersection(this, t1));
            }

            IntersectCaps(transRay, ref xs); // ref xs

            return xs;
        }

        protected void IntersectCaps(Ray transRay, ref List<Intersection> xs) // ref List<double> xs
        {
            if (!isClosed || Utility.FE(transRay.direction.y, 0))
            {
                return;
            }

            double t = (this.minimum - transRay.origin.y) / transRay.direction.y;
            if (CheckCap(transRay, t))
            {
                xs.Add(new Intersection(this, t));
            }

            t = (this.maximum - transRay.origin.y) / transRay.direction.y;
            if (CheckCap(transRay, t))
            {
                xs.Add(new Intersection(this, t));
            }
        }

        protected bool CheckCap(Ray transRay, double t)
        {
            double x = transRay.origin.x + transRay.direction.x * t;
            double z = transRay.origin.z + transRay.direction.z * t;
            return (x * x + z * z) <= 1;
        }

        public override Bounds GetLocalBounds()
        {
            //Bounds for a cone are the bottom, top and sides
            Bounds b = new Bounds();

            //I believe the max size is 1 unit from the center, will have to 
            //re-check the books chapters on this
            b.min.x = -1;
            b.max.x = 1;

            b.min.y = minimum;
            b.max.y = maximum;

            b.min.z = -1;
            b.max.z = 1;

            return b;
        }

        public override string ToString()
        {
            return "Cylinder (" + id.ToString() + ") -> position: " + GetPosition() + ", min: " + minimum + ", max: " + maximum + ", isClosed: " + isClosed;
        }
    }
}
