using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Point : Tuple
    {
        public Point() : base(0.0, 0.0, 0.0, 1.0)
        { }

        public Point(double x = 0.0, double y = 0.0, double z = 0.0, double w = 1.0) : base(x, y, z, 1.0)
        { }

        public Point(Point p) : base(p.x, p.y, p.z, 1.0)
        { }

        public Point(Vector v) : base(v.x, v.y, v.z, 1.0)
        { }

        public Point Randomize(Point start, Point end)
        {
            new Random();
            this.x = Random.Instance.NextDouble(start.x, end.x);
            this.y = Random.Instance.NextDouble(start.y, end.y);
            this.z = Random.Instance.NextDouble(start.z, end.z);
            return this;
        }

        public static Point operator +(Point a, Vector b)
        {
            Point temp = new Point();
            temp.x = a.x + b.x;
            temp.y = a.y + b.y;
            temp.z = a.z + b.z;
            return temp;
        }

        public static Point operator +(Vector a, Point b)
        {
            Point temp = new Point();
            temp.x = a.x + b.x;
            temp.y = a.y + b.y;
            temp.z = a.z + b.z;
            return temp;
        }

        public static Point operator -(Point a, Vector b)
        {
            Point temp = new Point();
            temp.x = a.x - b.x;
            temp.y = a.y - b.y;
            temp.z = a.z - b.z;
            return temp;
        }

        public static Point operator -(Vector a, Point b)
        {
            Point temp = new Point();
            temp.x = a.x - b.x;
            temp.y = a.y - b.y;
            temp.z = a.z - b.z;
            return temp;
        }

        public static Vector operator -(Point a, Point b)
        {
            Vector temp = new Vector();
            temp.x = a.x - b.x;
            temp.y = a.y - b.y;
            temp.z = a.z - b.z;
            return temp;
        }

        public static Vector operator +(Point a, Point b)   // ajouter 2 point = non-sens mathématique
        {
            Vector temp = new Vector();
            temp.x = a.x + b.x;
            temp.y = a.y + b.y;
            temp.z = a.z + b.z;
            return temp;
        }

        public static Point operator -(Point a)
        {
            Point temp = new Point();
            temp.x = -a.x;
            temp.y = -a.y;
            temp.z = -a.z;
            return temp;
        }

        public Point Negate()    //modifie le point actuel
        {
            this.x = -x;
            this.y = -y;
            this.z = -z;
            return this;
        }

        public static Point operator *(Point a, double b)
        {
            Point temp = new Point();
            temp.x = a.x * b;
            temp.y = a.y * b;
            temp.z = a.z * b;
            return temp;
        }

        public static Point operator *(double a, Point b)
        {
            Point temp = new Point();
            temp.x = b.x * a;
            temp.y = b.y * a;
            temp.z = b.z * a;
            return temp;
        }

        public static Point operator *(Mat4 a, Point b)
        {
            Point temp = new Point();

            temp.x = a[0, 0] * b.x + a[0, 1] * b.y + a[0, 2] * b.z + a[0, 3] * b.w;
            temp.y = a[1, 0] * b.x + a[1, 1] * b.y + a[1, 2] * b.z + a[1, 3] * b.w;
            temp.z = a[2, 0] * b.x + a[2, 1] * b.y + a[2, 2] * b.z + a[2, 3] * b.w;
            temp.w = a[3, 0] * b.x + a[3, 1] * b.y + a[3, 2] * b.z + a[3, 3] * b.w;
            return temp;
        }

        public Point Scale(double a)
        {
            this.x *= a;
            this.y *= a;
            this.z *= a;
            return this;
        }

        /*public Point Normalize()    // Normaliser un point a-il du sens ?
        {
            double mag = this.Magnitude();

            if (Utility.FE(0.0, mag))
            {
                this.x = 0.0;
                this.y = 0.0;
                this.z = 0.0;
                Console.WriteLine("This point is zero and can't be normalized.");
                return this;
            }

            this.x = this.x / mag;
            this.y = this.y / mag;
            this.z = this.z / mag;
            return this;
        }*/

        /*public Point Normalized()   // Normaliser un point a-il du sens ?
        {
            Point temp = new Point();
            double mag = this.Magnitude();

            if (Utility.FE(0.0, mag))
            {
                Console.WriteLine("This point is zero and can't be normalized.");
                return new Point(0, 0, 0);
            }

            temp.x = this.x / mag;
            temp.y = this.y / mag;
            temp.z = this.z / mag;
            return temp;
        }*/

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Point point = obj as Point;

            if (Utility.FE(this.x, point.x) &&
                Utility.FE(this.y, point.y) &&
                Utility.FE(this.z, point.z) &&
                Utility.FE(this.w, point.w))
                return true;

            return false;
        }
    }
}
