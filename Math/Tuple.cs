using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public abstract class Tuple
    {
        public double x;
        public double y;
        public double z;
        public double w;

        public Tuple(double x = 0.0, double y = 0.0, double z = 0.0, double w = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public override string ToString()
        {
            return "(" + x.ToString() + "," +
                         y.ToString() + "," +
                         z.ToString() + "," + 
                         w.ToString() + ")";
        }

        public double Magnitude()
        {
            double temp = Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
            return temp;
        }

        public double SqrtMagnitude() // Faster than Magnitude
        {   // changer son nom pour MagnitudeSqrt (plus compréhensible)
            return this.x * this.x + this.y * this.y + this.z * this.z;
        }

        public double Dot(Tuple a)
        {
            return this.x * a.x + this.y * a.y + this.z * a.z;
        }

        public static double Dot(Tuple a, Tuple b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public override bool Equals(object obj)
        {
            Tuple tuple = obj as Tuple;
            return tuple != null &&
                   Utility.FE(x, tuple.x) &&
                   Utility.FE(y, tuple.y) &&
                   Utility.FE(z, tuple.z) &&
                   Utility.FE(w, tuple.w);
        }

        public static bool operator ==(Tuple tuple1, Tuple tuple2)
        {
            //If null values passed in, are they both null?
            if (object.ReferenceEquals(tuple1, null))
            {
                return object.ReferenceEquals(tuple2, null);
            }

            return tuple1.Equals(tuple2);

        }

        public static bool operator !=(Tuple tuple1, Tuple tuple2)
        {
            return !(tuple1 == tuple2);
        }
    }
}
