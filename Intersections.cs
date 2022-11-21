using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Intersection   // This class store the object intersected ant the t value of intersection
    {
        public RayObject rayObject; // Object avec lequel le rayon entre en colision.
        public double t;    // Distance entre la source du rayon et le point de colision.

        //Used with triangles, disregard with anything else.
        public double u;
        public double v;

        public Intersection(RayObject obj, double t, double u = 0.0, double v = 0.0)
        //public Intersection(RayObject obj, double t)
        {
            rayObject = obj;
            this.t = t;
            this.u = u;
            this.v = v;
        }

        public static List<Intersection> SortIntersections(List<Intersection> Intersections) // Trie les intersections par distances croissantes.
        {
            if (Intersections.Count == 0)
            {
                return Intersections;
            }

            List<Intersection> sortedIntersections = new List<Intersection>();

            sortedIntersections.Add(Intersections[0]);

            int currentIntersection = 1;
            bool valueInserted = false;

            while (currentIntersection < Intersections.Count)
            {
                valueInserted = false;
                for (int i = 0; i < sortedIntersections.Count; i++)
                {
                    if (Intersections[currentIntersection].t < sortedIntersections[i].t)
                    {
                        sortedIntersections.Insert(i, Intersections[currentIntersection]);
                        currentIntersection++;
                        valueInserted = true;
                        break;
                    }
                }

                if (!valueInserted)
                {
                    sortedIntersections.Add(Intersections[currentIntersection]);
                    currentIntersection++;
                }
            }
            return sortedIntersections;
        }

        public override bool Equals(object obj)
        {
            return obj is Intersection intersection &&
                   EqualityComparer<RayObject>.Default.Equals(rayObject, intersection.rayObject) &&
                   t == intersection.t &&
                   u == intersection.u &&
                   v == intersection.v;
        }

        public override string ToString()
        {
            return "Intersection: " + rayObject.Id + " " + this.t.ToString();
        }
    }
}
