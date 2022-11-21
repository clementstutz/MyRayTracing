using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Cone : RayObject
    {
        public double minimum = double.NegativeInfinity;
        public double maximum = double.PositiveInfinity;
        public bool isClosed = false;   // WARNING : dissocier isClose en isTopClose et isBottomClose

        public Cone(double min = double.NegativeInfinity, double max = double.PositiveInfinity, bool isClosed = false) : base()
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
                double y = Math.Sqrt(localPoint.x * localPoint.x + localPoint.z * localPoint.z);
                if (localPoint.y > 0)
                {
                    y = -y;
                }
                return new Vector(localPoint.x, y, localPoint.z);
            }
        }

        public override Vector GetNormal(Point worldPoint, Intersection i = null)
        {
            Point localPoint = this.WorldToObject(worldPoint);
            Vector localNormal = CalculateLocalNormal(localPoint, i);
            Vector worldNormal = NormalToWorld(localNormal);
            return worldNormal;
        }
        /*public override Vector GetNormal(Point point)
        {
            //End caps are the same as the cylinder except for:
            Point worldToLocal = GetMatrix().Inverse() * point;

            Vector localNormal = CalculateLocalNormal(worldToLocal);
            return localNormal;
        }*/

        public override List<Intersection> Intersect(Ray ray)
        {
            Ray transRay = RayToObjectSpace(ray);

            List<Intersection> xs = new List<Intersection>();

            double a = transRay.direction.x * transRay.direction.x -
                       transRay.direction.y * transRay.direction.y +
                       transRay.direction.z * transRay.direction.z;

            double b = 2.0 * transRay.origin.x * transRay.direction.x -
                       2.0 * transRay.origin.y * transRay.direction.y +
                       2.0 * transRay.origin.z * transRay.direction.z;

            double c = transRay.origin.x * transRay.origin.x -
                       transRay.origin.y * transRay.origin.y +
                       transRay.origin.z * transRay.origin.z;

            if (Utility.FE(0, a))
            {
                if (Utility.FE(0, b))
                {
                    // All misses
                    return xs;
                }

                // b is not zero, have a single point of intersection.
                xs.Add(new Intersection(this, -c / (2 * b)));

            }

            // Both A and B are not zero at this point.
            // Use the cylinder algorithm but with the new A B and C...

            double discriminant = b * b - 4 * a * c;

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

            double y0 = transRay.origin.y + t0 * transRay.direction.y;

            if (this.minimum < y0 && y0 < this.maximum)
            {
                xs.Add(new Intersection(this, t0));
            }

            double y1 = transRay.origin.y + t1 * transRay.direction.y;

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
            double x = transRay.origin.x + t * transRay.direction.x;
            double z = transRay.origin.z + t * transRay.direction.z;
            return (x * x + z * z) <= Math.Abs(transRay.origin.y + t * transRay.direction.y); // Radius of 1 was for a cylinder, this needs to be changed
            // to be the absolute value of the y coordinate, because a cone fans out with distance.
        }

        public override Bounds GetLocalBounds()
        {
            //Bounds for a cone are the bottom, top and sides
            Bounds b = new Bounds();

            //These can potentially be infinite.
            b.min.y = this.minimum;
            b.max.y = this.maximum;

            //I believe the max size is 1 unit from the center, will have to 
            //re-check the books chapters on this
            b.min.x = -1;
            b.max.x = 1;

            b.min.z = -1;
            b.max.z = 1;

            return b;
        }

        public override string ToString()
        {
            return "Cone (" + id.ToString() + ") -> position: " + GetPosition() + ", min: " + minimum + ", max: " + maximum + ", isClosed: " + isClosed;
        }
    }
}
