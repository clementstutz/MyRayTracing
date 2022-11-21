using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RT
{
    public abstract class RayObject // a RayObject is an object that can intersect with a ray !
    {
        public enum Axis
        {
            X,
            Y,
            Z
        }

        protected static int currentId = 0;
        protected int id;
        protected Mat4 matrix = new Mat4();
        protected Mat4 invertMat = new Mat4();
        public Material material = new Material();
        public bool canReceiveShadows = true;   // WARNING : A déplacer dans materiaux, car sinon je ne peux pas définir un mtx Glassy() qui ne peux caster des ombres !
        public bool canCastShadows = true;      // WARNING : A déplacer dans materiaux

        protected RayObject parent = null;
        protected List<RayObject> children = new List<RayObject>();

        public Bounds precalculatedBounds = null;

        public RayObject GetParent()
        {
            return parent;
        }

        /*public bool Includes(RayObject o)
        {
            // Are we the object?
            if (this == o)
            {
                return true;
            }

            // What about our children?
            foreach (RayObject i in GetChildren())
            {
                if (i.Includes(o))
                    return true;
            }

            // Object never found
            return false;
        }*/

        public List<RayObject> GetChildren()
        {
            return children;
        }

        // Method used outside of class to control setting the parent of an object.
        // We do not directly remove objects, instead we would set an object's parent to null.
        public void SetParent(RayObject newParent)
        {
            //Remove us from any objects that currently have us as a child
            if (parent != null)
            {
                parent.RemoveChild(this);
            }

            // Set ourselves as a child of the parent.
            if (newParent != null)
            {
                newParent.AddChild(this);
            }

            // Can't let objects just sit around, make ourselves
            // part of the root.
            //else if (Scene.current.root != null)
            //{
            //Scene.current.root.AddChild(this);
            //}
            // Only thing that should get here is the global root of the scene...
            //else
            //{
            //    Console.WriteLine("Root group of scene successfully created.");
            //}
        }

        protected void AddChild(RayObject newChild)
        {
            //Is this already a child of this object?
            if (newChild.parent != this)
            {
                children.Add(newChild);
            }
            newChild.parent = this;
        }

        protected void RemoveChild(RayObject child)
        {
            if (child.parent == this)
            {
                children.Remove(child);
            }
            child.parent = null;
        }

        public int Id
        {
            get { return id; }
            private set { id = value; }
        }
        
        public void SetMatrix(Mat4 mat4)
        {
            this.matrix = mat4;
            this.invertMat = mat4.Inverse();
        }

        public Mat4 GetMatrix()
        {
            return this.matrix;
        }

        public Mat4 GetInvertMatrix()
        {
            return this.invertMat;
        }

        public void SetPosition(Point point)
        {
            this.matrix[0, 3] = point.x;
            this.matrix[1, 3] = point.y;
            this.matrix[2, 3] = point.z;
        }

        public Point GetPosition()
        {
            return new Point(this.matrix[0, 3],
                             this.matrix[1, 3],
                             this.matrix[2, 3]);
        }

        public void SetMaterial(Material material)
        {
            this.material = material;
        }

        public Material GetMaterial()
        {
            return material;
        }

        virtual protected Ray RayToObjectSpace(Ray ray) // transform a ray to an onbect space
        {
            return GetMatrix().Inverse() * ray;
        }

        public RayObject()
        {
            if (Scene.current != null)
            {
                this.SetParent(Scene.current.root);
                Scene.current.AddRayObject(this);
            }
            id = currentId++;
            material = new Material();
        }

        public Point WorldToObject(Point worldPoint)
        {
            // Travels through all parent objects till we hit the root object
            // Then begins returning the point multiplied by the top node matrix
            // down to the bottom
            if (this.GetParent() != null)
            {
                worldPoint = this.GetParent().WorldToObject(worldPoint);
            }
            return (this.GetMatrix().Inverse()) * worldPoint;
        }

        public abstract Vector CalculateLocalNormal(Point localPoint, Intersection i = null);

        public Vector NormalToWorld(Vector localNormal)
        {
            // WARNING : il se peut que je ne fasse pas les operations dans le bon ordre..!!
            // finalement ça semble bon, mais ça ferait pas de mal de vérifier..!!!
            Vector worldNormal = (this.GetMatrix().Inverse().Transpose()) * localNormal;
            /*Mat4 untransform = this.GetMatrix().Inverse();
            untransform = untransform.Transpose();
            Vector worldNormal1 = untransform * localNormal; // celle-ci devrait être bonne
            Mat4 untransform2 = this.GetMatrix().Transpose();
            untransform2 = untransform2.Inverse();
            Vector worldNormal2 = untransform2 * localNormal;// celle-ci devrait être fausse
            if (worldNormal1 == worldNormal)
            {
                //Console.WriteLine("RayObject.cs : NormalToWorld() : worldNormal2 == worldNormal !!! NOOOO");
            }
            else
            {
                Console.WriteLine("RayObject.cs : NormalToWorld() : worldNormal2 != worldNormal !!!");
            }*/

            worldNormal.w = 0;
            worldNormal.Normalize();

            if (this.GetParent() != null)
            {
                worldNormal = this.GetParent().NormalToWorld(worldNormal);
            }
            return worldNormal;
        }

        public abstract Vector GetNormal(Point worldPoint, Intersection i = null);
        //public Vector GetNormal(Point worldPoint, Intersection i = null)
        //{
              // WARNING : Idée pour aller plus vite avec les groupes : créer deux Id, un pour l'objet comme c'est dejà le cas
              // et un pour le groupe qui serait égale à l'ID du premier objet parent, pour aller directement à cette objet plutot que de fair eune boucle...
        //    //Plane tempPlane = new Plane();
        //    //if (this.GetType().Equals(tempPlane.GetType()))
        //    //{
        //    //    Vector normal = CalculateLocalNormal(worldPoint, i);
        //    //    return normal;
        //    //}
        //    Point localPoint = this.WorldToObject(worldPoint);
        //    Vector localNormal = CalculateLocalNormal(localPoint, i);
        //    Vector worldNormal = NormalToWorld(localNormal);
        //    return worldNormal;
        //}

        public abstract List<Intersection> Intersect(Ray ray);  //returns a list of all intersections in the scene against all scene object.

        //public abstract Color Lighting(Point position, Light light, Vector eye, Vector normal, bool inShadow);
        public Color Lighting(Point position, Light light, Vector eye, Vector normal, bool inShadow = false)
        {
            Color temp = material.color;
            if (material.pattern != null)
            {
                temp = material.pattern.PatternAtObject(this, position);
            }

            Color effectiveColor = temp * light.intensity;
            Vector lightVec = (light.position - position).Normalize();
            Color ambientColor = temp * material.Ambient;
            Color diffuseColor;
            Color specularColor;

            if (inShadow)
                return ambientColor;

            double lDotN = Vector.Dot(lightVec, normal);
            if (lDotN <= 0)
            {
                diffuseColor = Color.black;
                specularColor = Color.black;
            }

            else
            {
                diffuseColor = effectiveColor * material.Diffuse * lDotN;
                Vector reflect = Vector.Reflect(-lightVec, normal);
                double rDotE = Vector.Dot(reflect, eye);

                if (rDotE <= 0)
                    specularColor = Color.black;
                else
                {
                    double factor = Math.Pow(rDotE, material.Shininess);
                    specularColor = light.intensity * material.Specular * factor;
                }
            }
            return ambientColor + diffuseColor + specularColor;
        }

        public abstract Bounds GetLocalBounds();

        public Bounds GetBounds()
        {
            if (precalculatedBounds == null)
            {
                return CalcBounds();
            }
            //Console.WriteLine("precalculatedBounds = (" + precalculatedBounds.min + ", " + precalculatedBounds.max + ")");
            return precalculatedBounds;
        }

        public Bounds CalcBounds()
        {
            Bounds finalBound = new Bounds();

            //return a bounding box for all elements within this object in its space

            List<Bounds> bounds = new List<Bounds>();

            //Get our bounds first.
            finalBound = GetLocalBounds();      //Get the local bounds of the object
            finalBound = finalBound.GetAABB(GetMatrix()); //Convert transformed to axis aligned

            //Look at our children and see if they exceed our geometric boundaries
            //and if so, then extend our current bounds to include them.
            for (int i = 0; i < children.Count; i++)
            {
                //Get the bounds of the child with its transforms taken into consideration
                Bounds bound = children[i].GetBounds();
                //Bounds bound = children[i].CalcBounds();

                //get child bounds
                bound = bound.GetAABB(GetMatrix());
                //bound.min = GetMatrix() * bound.min;
                //bound.max = GetMatrix() * bound.max;

                //Find min and max across all bounds
                if (finalBound.min.x > bound.min.x)
                {
                    finalBound.min.x = bound.min.x;
                }
                if (finalBound.min.y > bound.min.y)
                {
                    finalBound.min.y = bound.min.y;
                }
                if (finalBound.min.z > bound.min.z)
                {
                    finalBound.min.z = bound.min.z;
                }

                if (finalBound.max.x < bound.max.x)
                {
                    finalBound.max.x = bound.max.x;
                }
                if (finalBound.max.y < bound.max.y)
                {
                    finalBound.max.y = bound.max.y;
                }
                if (finalBound.max.z < bound.max.z)
                {
                    finalBound.max.z = bound.max.z;
                }
            }
            //Store bounds so we only calculate this one.
            precalculatedBounds = finalBound;

            return finalBound;
        }

        public override string ToString()
        {
            return "RayObject: " + id.ToString();
        }
    }
}
