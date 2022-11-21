using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Ray
    {
        public Point origin;
        public Vector direction;

        public Ray()
        {
            this.origin = new Point();
            this.direction = new Vector();
        }

        public Ray(Point origin, Vector direction)
        {
            this.origin = origin;
            this.direction = direction;
        }

        public static Ray operator *(Mat4 mat, Ray ray)
        {
            Ray temp = new Ray();

            temp.origin = mat * ray.origin;
            temp.direction = mat * ray.direction;
            return temp;
        }

        public static Ray Transform(Ray ray, Mat4 mat)
        {
            Ray temp;
            temp = mat * ray;
            return temp;
        }

        public Point Position(double t) // Renvoie les coordonées de l'intersection par rapport au centre du repère.
        {
            return this.origin + this.direction * t;
        }

        public override string ToString()
        {
            return origin.ToString() + " -> " + direction.ToString();
        }
    }
}
