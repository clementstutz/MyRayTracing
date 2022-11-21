using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Sphere : RayObject
    {
        //public Point position;
        double radius = 1.0;

        public Sphere(double radius = 1.0) : base()
        {
            //position = new Point();
            this.radius = radius;   // L'afectation du radius ne change rien à la taille réelle de la sphere...
        }

        public override Vector CalculateLocalNormal(Point localPoint, Intersection i = null)
        {
            return new Vector(localPoint).Normalize(); // il faut normaliser ou pas ???
        }

        public override Vector GetNormal(Point worldPoint, Intersection i = null)
        {
            Point localPoint = this.WorldToObject(worldPoint);
            Vector localNormal = CalculateLocalNormal(localPoint, i);
            Vector worldNormal = NormalToWorld(localNormal);
            return worldNormal;
        }

        //public override List<Intersection> Intersect(Ray ray)
        //public override List<double> Intersect(Ray ray) // Renvoit la distance entre la source de lumere et la surface intersectée.
        //{
        //    /* Idée à tester pour améliorer les performence en évitent de renvoyer une liste vide lorsqu'il n'y a pas d'intersection avec un objet:
        //     * faire la methode Intersect renvoie un void, mais en fonction du déterminant, elle appel une methode_1 qui, elle, renvoie une liste d'intersection si il y a intersection,
        //     * ou qui appelle une methode_2 qui ne renvoit rien (void) si pas d'intersection.
        //     */

        //    //List<Intersection> intersectionPoints = new List<Intersection>();
        //    List<double> intersectionPoints = new List<double>();

        //    Ray transRay = RayToObjectSpace(ray);

        //    Vector sphereToRay = (transRay.origin - new Point(0, 0, 0)); // pourquoi - new Point(0,0,0) ?
        //    double a = transRay.direction.Dot(transRay.direction);   //Should always be 1.0
        //    double b = 2.0 * transRay.direction.Dot(sphereToRay);
        //    double c = sphereToRay.Dot(sphereToRay) - 1.0;
        //    double discriminant = b * b - 4.0 * a * c;
        //    if (discriminant < 0)   // Miss.
        //        return intersectionPoints; // pour optimiser on peut peut-être essayer de ne pas renvoyer de liste vide comme c'est le cas ici...

        //    double t1 = (-b - (double)Math.Sqrt(discriminant)) / (2.0 * a);
        //    double t2 = (-b + (double)Math.Sqrt(discriminant)) / (2.0 * a);

        //    // pour optimiser encore on pourrait regarder le signe de t1 et t2 et ne renvoyer que les valeur positiver
        //    // les valeurs négatives sont des intersection qui on lieu derrière la camera (normalement...)
        //    //intersectionPoints.Add(new Intersection(this, t1)); // voir pour ne pas renvoyer les ti < 0...
        //    //intersectionPoints.Add(new Intersection(this, t2));
        //    intersectionPoints.Add(t1);
        //    intersectionPoints.Add(t2);

        //    //if (t1 < 0)
        //    //Console.WriteLine("intersection t1 derriere la camera.");
        //    //if (t2 < 0)
        //    //Console.WriteLine("intersection t2 derriere la camera.");

        //    return intersectionPoints;
        //}
        public override List<Intersection> Intersect(Ray ray) // Renvoit la distance entre la source de lumere et la surface intersectée.
        {
            List<Intersection> intersectionPoints = new List<Intersection>();

            Ray transRay = RayToObjectSpace(ray);

            Vector sphereToRay = transRay.origin - new Point(0, 0, 0);
            double a = transRay.direction.Dot(transRay.direction);   //Same as transRay.direction.SqrtMagnitude();
            double b = 2.0 * transRay.direction.Dot(sphereToRay);
            double c = sphereToRay.Dot(sphereToRay) - 1.0;  //Same as sphereToRay.SqrtMagnitude() -1;
            double discriminant = b * b - 4.0 * a * c;

            if (discriminant < 0) // Miss.
                return intersectionPoints;

            double t1 = (-b - Math.Sqrt(discriminant)) / (2.0 * a);
            double t2 = (-b + Math.Sqrt(discriminant)) / (2.0 * a);

            intersectionPoints.Add(new Intersection(this, t1)); // voir pour ne pas renvoyer les ti < 0...
            intersectionPoints.Add(new Intersection(this, t2));

            return intersectionPoints;
        }

        public override Bounds GetLocalBounds()
        {
            //Bounds for a cone are the bottom, top and sides
            Bounds b = new Bounds();

            //I believe the max size is 1 unit from the center, will have to 
            //re-check the books chapters on this
            b.min.x = -1;
            b.max.x = 1;

            b.min.y = -1;
            b.max.y = 1;

            b.min.z = -1;
            b.max.z = 1;

            return b;
        }

        public override string ToString()
        {
            return "Sphere (" + id.ToString() + ") -> position: " + GetPosition() + ", rayonsXYZ: (" + GetMatrix()[0, 0] + "," + GetMatrix()[1, 1] + "," + GetMatrix()[2, 2] + ")";
        }
    }
}
