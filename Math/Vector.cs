using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Vector : Tuple
    {

        public Vector() : base(0.0, 0.0, 0.0, 0.0)
        { }

        public Vector(double x = 0.0, double y = 0.0, double z = 0.0, double w = 0.0) : base(x, y, z, 0.0)
        { }

        public Vector(Point p) : base(p.x, p.y, p.z, 0.0)
        { }

        public Vector(Vector v) : base(v.x, v.y, v.z, 0.0)
        {}

        public Vector Randomize(Vector start, Vector end)
        {
            new Random();
            this.x = Random.Instance.NextDouble(start.x, end.x);
            this.y = Random.Instance.NextDouble(start.y, end.y);
            this.z = Random.Instance.NextDouble(start.z, end.z);
            return this;
        }

        public static Vector operator +(Vector a, Vector b) // return a new vector
        {
            Vector temp = new Vector();
            temp.x = a.x + b.x;
            temp.y = a.y + b.y;
            temp.z = a.z + b.z;
            temp.w = a.w + b.w;
            return temp;
        }

        public static Vector operator -(Vector a, Vector b) // return a new vector
        {
            Vector temp = new Vector();
            temp.x = a.x - b.x;
            temp.y = a.y - b.y;
            temp.z = a.z - b.z;
            temp.w = a.w - b.w;
            return temp;
        }

        public static Vector operator -(Vector a)   // return a new vector
        {
            Vector temp = new Vector();
            temp.x = -a.x;
            temp.y = -a.y;
            temp.z = -a.z;
            temp.w = -a.w;
            return temp;
        }

        public Vector Negate()
        {
            this.x = -x;
            this.y = -y;
            this.z = -z;
            this.w = -w;
            return this;
        }

        public static Vector operator *(Vector a, double b) // return a new vector
        {
            Vector temp = new Vector();
            temp.x = a.x * b;
            temp.y = a.y * b;
            temp.z = a.z * b;
            temp.w = a.w * b;
            return temp;
        }

        public static Vector operator *(double a, Vector b) // return a new vector
        {
            Vector temp = new Vector();
            temp.x = b.x * a;
            temp.y = b.y * a;
            temp.z = b.z * a;
            temp.w = b.w * a;
            return temp;
        }

        public static Vector operator *(Mat4 a, Vector b)   // return a new vector
        {
            Vector temp = new Vector();

            temp.x = a[0, 0] * b.x + a[0, 1] * b.y + a[0, 2] * b.z + a[0, 3] * b.w;
            temp.y = a[1, 0] * b.x + a[1, 1] * b.y + a[1, 2] * b.z + a[1, 3] * b.w;
            temp.z = a[2, 0] * b.x + a[2, 1] * b.y + a[2, 2] * b.z + a[2, 3] * b.w;
            temp.w = a[3, 0] * b.x + a[3, 1] * b.y + a[3, 2] * b.z + a[3, 3] * b.w;
            return temp;
        }

        public Vector Scale(double a)
        {
            this.x *= a;
            this.y *= a;
            this.z *= a;
            this.w *= a;
            return this;
        }

        public Vector Normalize()
        {
            double mag = this.Magnitude();

            if (Utility.FE(0.0, mag))
            {
                this.x = 0.0;
                this.y = 0.0;
                this.z = 0.0;
                this.w = 0.0;
                Console.WriteLine("This vector is zero and can't be normalized.");
                return this;
            }

            this.x = this.x / mag;
            this.y = this.y / mag;
            this.z = this.z / mag;
            this.w = this.w / mag;
            return this;
        }

        public Vector Normalized()  // return a new vector
        {
            Vector temp = new Vector();
            double mag = this.Magnitude();

            if (Utility.FE(0.0, mag))
            {
                Console.WriteLine("This vector is zero and can't be normalized.");
                return new Vector(0, 0, 0);
            }

            temp.x = this.x / mag;
            temp.y = this.y / mag;
            temp.z = this.z / mag;
            temp.w = this.w / mag;
            return temp;
        }

        public Vector Cross(Vector b)   // return a new vector
        {
            Vector temp = new Vector();
            temp.x = this.y * b.z - b.y * this.z;
            temp.y = this.z * b.x - b.z * this.x;
            temp.z = this.x * b.y - b.x * this.y;
            return temp;
        }

        public static Vector Cross(Vector a, Vector b)  // return a new vector
        {
            Vector temp = new Vector();
            temp.x = a.y * b.z - b.y * a.z;
            temp.y = a.z * b.x - b.z * a.x;
            temp.z = a.x * b.y - b.x * a.y;
            return temp;
        }

        public static Vector Reflect(Vector incoming, Vector normal)
        {
            return incoming - 2.0 * Dot(incoming, normal) * normal;
            //formule : r - i = - 2 * (i dot n) * n -> r = i - 2 * (i dot n) * n
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Vector vector = obj as Vector;

            if (Utility.FE(this.x, vector.x) &&
                Utility.FE(this.y, vector.y) &&
                Utility.FE(this.z, vector.z) &&
                Utility.FE(this.w, vector.w))
                return true;

            return false;
        }
    }
}
