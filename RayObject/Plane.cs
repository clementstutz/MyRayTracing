using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Plane : RayObject
    {
        public Vector normal;

        public Plane() : base()
        {
            normal = new Vector(0, 1, 0);
        }

        public override Vector CalculateLocalNormal(Point localPoint, Intersection i = null) // WARNING : C'est le bordel, je ne comprend rien !!!
        {
            //Vector normal = this.GetMatrix() * (-this.normal);
            //return this.GetMatrix() * normal;

            // là ça a l'air de marché pusque nous somme deja dans le local space,
            // il n'y a donc pas besoin de faire GetMatrix() * normal
            // de plus le signe de la normal ne semble pas impacter le resultat
            // (c-a-d si on voit le plan par dessus ou par dessous).
            return normal;

            /*if (this.GetMatrix() * new Vector(0, 1, 0) <0)
            {
                async = 1;
            }
            if (localPoint.y > 0)   //le rayon arrive par dessus
            {
                Console.WriteLine("localNormal >0 = " + this.GetMatrix() * new Vector(0, 1, 0));
                return this.GetMatrix() * new Vector(0, 1, 0);
            }
            else if (localPoint.y < 0)  //le rayon arrive par dessous
            {
                Console.WriteLine("localNormal <0 = " + this.GetMatrix() * new Vector(0, -1, 0));
                return this.GetMatrix() * new Vector(0, -1, 0);
            }
            else    // WARNING : cas litigieux, il faut regardr l'origine du rayon! A regler plus tard
            {
                Console.WriteLine("localNormal =0 = " + this.GetMatrix() * new Vector(12, 12, 12));
                return this.GetMatrix() * new Vector(12, 12, 12);
            }*/
        }

        public override Vector GetNormal(Point worldPoint, Intersection i = null)
        {
            //Console.WriteLine("\nworldPoint = " + worldPoint);
            Point localPoint = this.WorldToObject(worldPoint);
            //Console.WriteLine("localPoint = " + localPoint);
            Vector localNormal = CalculateLocalNormal(localPoint, i);
            //Console.WriteLine("localNormal = " + localNormal);
            Vector worldNormal = NormalToWorld(localNormal);
            Console.WriteLine("worldNormal = " + worldNormal);
            return worldNormal;
        }

        public override List<Intersection> Intersect(Ray ray)
        {
            List<Intersection> intersections = new List<Intersection>();

            Ray localRay = RayToObjectSpace(ray);

            if (Math.Abs(localRay.direction.y) < Utility.epsilon)
            {
                return intersections;
            }

            intersections.Add(new Intersection(this, -localRay.origin.y / localRay.direction.y));

            return intersections;
        }

        public override Bounds GetLocalBounds()
        {
            //Bounds for a cone are the bottom, top and sides
            Bounds b = new Bounds();

            //I believe the max size is 1 unit from the center, will have to 
            //re-check the books chapters on this

            b.min.y = 0;
            b.max.y = 0;

            b.min.x = double.NegativeInfinity;
            b.max.x = double.PositiveInfinity;

            b.min.z = double.NegativeInfinity;
            b.max.z = double.PositiveInfinity;

            return b;
        }
    }
}

/*namespace RT
{
    public class Plane : RayObject
    {
        //public Vector normal;                 // a remettre !!

        public Plane() : base()
        {
            //normal = new Vector(0, 1, 0);     // a remettre !!
        }

        public override Vector CalculateLocalNormal(Point localPoint, Intersection i = null)
        {
            return this.GetMatrix() * new Vector(0, 1, 0);
            //return this.GetMatrix() * normal;
            //return normal;
        }

        public override Vector GetNormal(Point worldPoint, Intersection i = null)
        {
            Point localPoint = this.WorldToObject(worldPoint);
            Vector localNormal = CalculateLocalNormal(localPoint, i);
            Vector worldNormal = NormalToWorld(localNormal);
            return worldNormal;
            //localNormal.Normalize();    // The book sais the normal must be normalized !
            //return localNormal;
        }

        public override List<Intersection> Intersect(Ray ray)
        {
            List<Intersection> intersections = new List<Intersection>();

            Ray localRay = RayToObjectSpace(ray);

            if (Math.Abs(localRay.direction.y) < Utility.epsilon)
            {
                return intersections;
            }

            intersections.Add(new Intersection(this, -localRay.origin.y / localRay.direction.y));

            return intersections;
        }

        public override string ToString()
        {
            return "Plane (" + id.ToString() + ") -> position: " + GetPosition() + ", normal: " + CalculateLocalNormal(GetPosition());
        }
    }
}*/
