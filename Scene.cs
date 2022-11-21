using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Scene
    {
        public static Scene current = null;

        List<Light> lights;
        //Should this be removed now?
        //Since I have a system that accurately holds the hierarchy
        //of all information in a scene using groups...
        List<RayObject> rayObjects;
        public Group root;

        public List<Light> GetLights()
        {
            return lights;
        }

        public List<RayObject> GetRayObjects()
        {
            return rayObjects;
        }

        public Scene()
        {
            if (current == null)
            {
                current = this;
                //new Random();
            }
            lights = new List<Light>();
            rayObjects = new List<RayObject>();
            root = new Group();
            //Clear();

            //Default();
        }

        public void ClearLights()
        {
            if (lights != null)
            {
                lights.Clear();
            }
        }

        public void ClearRayObjects()
        {
            if (rayObjects != null)
            {
                rayObjects.Clear();
            }
            // root = new Group();
            // //Do I need to manually go through to garbage collect the group?
            // //The hierarchy still has references to one another, the counter isn't zero is it?
        }

        public void Clear()
        {
            ClearLights();
            ClearRayObjects();
        }

        public void AddLight(Light light)
        {
            lights.Add(light);
        }

        public void AddRayObject(RayObject rayObject)
        {
            rayObjects.Add(rayObject);
        }

        public void Default()
        {
            Clear();

            lights = new List<Light>();
            rayObjects = new List<RayObject>();

            Light light = new Light(new Point(-10, 10, -10), new Color(1, 1, 1));

            Sphere s1 = new Sphere();
            s1.material = new Material(new Color(0.8, 1.0, 0.6));
            s1.material.Diffuse = 0.7;
            s1.material.Specular = 0.2;
            Sphere s2 = new Sphere();
            s2.SetMatrix(Mat4.ScaleMatrix(0.5, 0.5, 0.5));
        }

        public void PreCalculateBounds()
        {
            //Causes all objects to calculate their bounds so that this is not done during actual rendering.
            //Obviously, if we ever introduce animation this will have to be continually recalcualted frame by frame.
            root.CalcBounds();
        }

        /*protected List<Intersection> SortIntersections(List<Intersection> intersections)    // WARNING : a déplacer dans Intersection.cs
        {
            if (intersections.Count == 0)
            {
                return intersections;
            }

            List<Intersection> sortedIntersections = new List<Intersection>();
            
            sortedIntersections.Add(intersections[0]);

            int currentIntersection = 1;
            bool valueInserted = false;

            while (currentIntersection < intersections.Count)
            {
                valueInserted = false;
                for (int i = 0; i < sortedIntersections.Count; i++)
                {
                    if (intersections[currentIntersection].t < sortedIntersections[i].t)
                    {
                        sortedIntersections.Insert(i, intersections[currentIntersection]);
                        currentIntersection++;
                        valueInserted = true;
                        break;
                    }
                }

                if (!valueInserted)
                {
                    sortedIntersections.Add(intersections[currentIntersection]);
                    currentIntersection++;
                    //valueInserted = false;
                }
            }
            return sortedIntersections;
        }*/

        public List<Intersection> Intersections(Ray ray)    // WARNING : a déplacer dans Intersection.cs
        {
            // Renvoie l'ensemble des intersections d'une scene via la methode SortIntersections().
            List<Intersection> intersections = new List<Intersection>();

            //Why am I doing intersections over all the children?
            //Shouldn't I just perform intersections on the group and have them be returned?

            //intersections = root.Intersect(ray); // WARNING : cause un problème avec le simages du chaptire 9!

            foreach (RayObject rObj in root.GetChildren())
            {
                /*List<double> tempList = rObj.Intersect(ray);
                //Take intersections whit object and make them Intersection
                for (int i = 0; i < tempList.Count; i++)
                {
                    intersections.Add(new Intersection(rObj, tempList[i]));
                }*/

                // Renvoit la distance entre la source de lumere et la surface intersectée
                // pour tous les RayObjects de la scene.
                List<Intersection> tempList = rObj.Intersect(ray);
                intersections.AddRange(tempList);
            }
            return Intersection.SortIntersections(intersections);    // WARNING : Surement à supprimer car doublon avec Hit().
        }

        public Intersection Hit(List<Intersection> intersections)   // WARNING : a déplacer dans Intersection.cs
        {
            if (intersections.Count == 0)
            {
                return null;
            }
            // WARNING : This might be doubled up in actual code, watch for calling twice.
            intersections = Intersection.SortIntersections(intersections);

            Intersection first = null;
            for (int i = 0; i < intersections.Count; i++)
            {
                if (intersections[i].t >= 0.0)
                {
                    first = intersections[i];
                    break;
                }

                //if (intersections[i].t < 0.0) // WARNING : Tester plutôt si la valeur est positive pour plus de conpréhension.
                //{
                //    continue;
                //}
                //else
                //{
                //    first = intersections[i];
                //    break;
                //}
            }
            return first;
        }
        // Autre methode pour cacluler Hit() sans trie préalable des intersections par ordre croissant.
        // Methode potentiellement plus courte...
        public Intersection Hit2(List<Intersection> intersections)
        {
            if (intersections.Count == 0)
                return null;

            Intersection intersection = intersections[0];
            foreach (Intersection obj in intersections) // Parcours n fois la boucle.
            {
                if (obj.t >= 0.0)
                {
                    if (obj.t < intersection.t)
                        intersection = obj;
                }
            }
            return intersection;
        }

        public bool IsShadowed(Point point, Light light)
        {
            // On créé un vecteur de la source de lumiere vers le point qui nous interesse,
            // et si on intersect quelque chose entre les deux alors nous savons que la lumière est bloqué
            // et que le point est dans l'ombre. Cela marche pour les objet opaque.
            
            // Need this to work with all lights in the scene!
            // This does nothing for accumulating multiple shadows over the scene I believe.

            Vector temp = light.position - point;
            double distance = temp.Magnitude();
            Vector direction = temp.Normalize();

            Ray ray = new Ray(point, direction);

            List<Intersection> intersections = Intersections(ray);
            Intersection intersection = Hit(intersections);

            // Do we have a hit and is it within the distance to the light?
            if (intersection != null && intersection.rayObject.canCastShadows && intersection.t < distance)
            {
                return true; // true = shadows on  false = shadows off
            }
            return false;
        }


        public Color ShadeHit(Computations c, int remaining = 1)
        {
            if (c == null)
            {
                return Color.black;
            }

            Color surfaceColor = new Color();
            for (int i = 0; i < lights.Count; i++)
            {
                // Is this light in shadow?
                if (c.rayObject.canReceiveShadows)
                {
                    bool isShadow = IsShadowed(c.overPoint, lights[i]);
                    surfaceColor += c.rayObject.Lighting(c.point, lights[i], c.eye, c.normal, isShadow);
                }
            }

            Color reflected = this.ReflectedColor(c, remaining);
            Color refracted = this.RefractedColor(c, remaining);

            if (c.rayObject.material.Reflectivity > 0 && c.rayObject.material.Transparency > 0)
            {
                double reflectance = this.Schlick(c);
                return surfaceColor + reflected * reflectance + refracted * (1.0 - reflectance);
            }
            
            return surfaceColor + reflected + refracted;
        }

        public Color ColorAt(Ray ray, int remaining = 1)
        {
            List<Intersection> hits = this.Intersections(ray);
            if (hits.Count == 0)
                return Color.black;
            Intersection hit = this.Hit(hits);
            Computations c = Computations.Prepare(hit, ray, hits);
            Color finalColor = Scene.current.ShadeHit(c, remaining);
            return finalColor;
        }

        public Ray RayForPixel(Camera camera, int x, int y)
        {
            double xOffset = (x + 0.5) * camera.PixelSize;
            double yOffset = (y + 0.5) * camera.PixelSize;

            double worldX = camera.HalfWidth - xOffset;
            double worldY = camera.HalfHeight - yOffset;

            Point pixel = camera.transform.Inverse() * new Point(worldX, worldY, -1.0);
            Point origin = camera.transform.Inverse() * new Point(0, 0, 0);
            Vector direction = (pixel - origin).Normalize();
            return new Ray(origin, direction);
        }

        //public Canvas DrawAABBs(Camera camera)
        //{
        //    RT.Canvas canvas = new RT.Canvas(camera.hSize, camera.vSize);
        //    canvas.FillCanvas(RT.Color.black);

        //    if (this.lights.Count == 0)
        //    {
        //        Console.WriteLine("No lights in scene, this will always produce a black image.");
        //    }

        //    for (int y = 0; y < camera.vSize; y++)
        //    {
        //        for (int x = 0; x < camera.hSize; x++)
        //        {
        //            Console.WriteLine(x.ToString() + ',' + y.ToString());
        //            Ray temp = this.RayForPixel(camera, x, y);
        //            Color pixelColor = this.CheckAABB(temp);
        //            //Blend the colors to allow for transparency among the cubes and layering
        //            canvas.SetPixel(x, y, canvas.GetPixel(x, y) + pixelColor);
        //        }
        //    }
        //    return canvas;
        //}

        //public Color CheckAABB(Ray ray, int remaining = 1)
        //{
        //    if (this.AABBIntersections(ray))
        //        return new Color(0.1, 0.1, 0.1);

        //    return Color.black;

        //}

        //public bool AABBIntersections(RT.Ray ray)
        //{
        //    foreach (RT.RayObject r in root.GetChildren())
        //    {
        //        if (r.GetBounds().Intersect(ray))
        //            return true;
        //    }
        //    return false;
        //}

        public Canvas Render(Camera camera, int remaining = 1)
        {
            if (this.lights.Count == 0)
            {
                Console.WriteLine("No lights in scene, this will always produce a black image.");
            }
            Canvas canvas = new Canvas(camera.hSize, camera.vSize);
            canvas.FillCanvas(Color.black); // WARNING : pas sur qu'il y ait besoit de faire ça...

            //Precalculate the bounds
            //PreCalculateBounds();

            for (int y = 0; y < camera.vSize; y++)
            {
                for (int x = 0; x < camera.hSize; x++)
                {
                    Ray temp = this.RayForPixel(camera, x, y);
                    Color pixelColor = this.ColorAt(temp, remaining);
                    canvas.SetPixel(x, y, pixelColor);
                    // affiche le pourcentage de progression de la génération de l'image
                    //Console.WriteLine(((double)(x + y * camera.hSize) / (double)(camera.vSize * camera.hSize) * 100).ToString() + "%");
                }
                // affiche le pourcentage de progression de la génération de l'image
                Console.WriteLine("y progression = " + ((double)(y+1) / (double)(camera.vSize) * 100).ToString() + "%");
            }
            Console.WriteLine("Rendering done !");
            return canvas;
        }

        public Color ReflectedColor(Computations c, int remaining = 1)  //recursions = remaining
        {
            if (remaining == 0 || Utility.FE(c.rayObject.material.Reflectivity, 0.0))
            {
                return Color.black;
            }

            Ray reflectionRay = new Ray(c.overPoint, c.reflectVector);
            return ColorAt(reflectionRay, remaining - 1) * c.rayObject.material.Reflectivity;
        }

        public Color RefractedColor(Computations c, int remaining = 1)  //recursions = remaining
        {
            // Check the material of the hit object and if the transparency is 0, return black.
            // Return calculated refracted value if it is transparent.

            if (Utility.FE(c.rayObject.material.Transparency, 0.0) || remaining == 0)
            {
                return Color.black;
            }

            //Check for infinit internal reflection.
            double nRatio = c.n1 / c.n2;
            double cosI = Vector.Dot(c.eye, c.normal);
            double sin2T = nRatio * nRatio * (1.0 - (cosI * cosI));
            if (sin2T > 1.0)
            {
                return Color.black;
            }

            double cosT = Math.Sqrt(1.0 - sin2T);

            Vector direction = c.normal * (nRatio * cosI - cosT) - c.eye * nRatio;

            Ray refractRay = new Ray(c.underPoint, direction);

            Color refractedColor = ColorAt(refractRay, remaining - 1) * c.rayObject.material.Transparency;

            //Replace later with code for refraction calculations
            return refractedColor;
        }

        public double Schlick(Computations c)   // WARNING :Deal with fresnel effect but don't realy know what it does. (cf. Chapter11Test Test 16)
        {
            double cosI = Vector.Dot(c.eye, c.normal);
            if (c.n1 > c.n2)
            {
                double nRatio = c.n1 / c.n2;
                double sin2T = nRatio * nRatio * (1.0 - (cosI * cosI));
                if (sin2T > 1.0)
                    return 1.0;

                double cosT = Math.Sqrt(1.0 - sin2T);

                cosI = cosT;
            }
            double r0 = Math.Pow((c.n1 - c.n2) / (c.n1 + c.n2), 2);
            return r0 + (1.0 - r0) * Math.Pow((1.0 - cosI), 5);
        }

        public override string ToString()
        {
            string output = "Scene -> rayObjects : \n";
            foreach (var obj in rayObjects)
            {
                output += "    " + obj.ToString() +  "\n";
            }
            output += "Scene -> lights : \n";
            foreach (var light in lights)
            {
                output += "    " + light.ToString() + "\n";
            }
            return output;
        }
    }
}
