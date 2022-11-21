using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RT.Patterns;


namespace RT
{
    public class RefractiveIndex
    {
        public const double Vacuum = 1.0;
        public const double Air = 1.000293;
        public const double Water = 1.333;
        public const double Glass = 1.52;
        public const double Diamond = 2.417;
    }
    public class Material
    {
        public Color color;
        double ambient;
        double diffuse;
        double specular;
        double shininess;
        double reflectivity;
        double refractiveIndex;
        double transparency;
        public Pattern pattern = null;

        public double Ambient
        {
            get { return ambient; }

            set
            {
                if (value < 0.0)
                { value = 0.0; }
                ambient = value;
            }
        }

        public double Diffuse
        {
            get { return diffuse; }

            set
            {
                if (value < 0.0)
                { value = 0.0; }
                diffuse = value;
            }
        }

        public double Specular
        {
            get { return specular; }

            set
            {
                if (value < 0.0)
                { value = 0.0; }
                specular = value;
            }
        }

        public double Shininess
        {
            get { return shininess; }

            set
            {
                if (value <= 10.0)
                { value = 10.0; }
                if (value > 200.0)
                { value = 200.0; }
                shininess = value;
            }
        }

        public double Reflectivity
        {
            get { return reflectivity; }

            set
            {
                if (value < 0.0)
                { value = 0.0; }
                if (value > 1.0)
                { value = 1.0; }
                reflectivity = value;
            }
        }

        public double RefractIndex
        {
            get { return refractiveIndex; }

            set
            {
                refractiveIndex = value;
            }
        }

         public double Transparency
        {
            get { return transparency; }

            set
            {
                if (value < 0.0)
                { value = 0.0; }
                if (value > 1.0)
                { value = 1.0; }
                transparency = value;
            }
        }

        public Material()
        {
            color = Color.white;
            ambient = 0.1;      // [0; 1] normalement
            diffuse = 0.9;      // [0; 1] normalement
            specular = 0.9;     // [0; 1] normalement
            shininess = 200;   // [10; 200] normalement
            reflectivity = 0.0;   // [0; 1] normalement
            refractiveIndex = RefractiveIndex.Air;
            transparency = 0.0; // [0; 1] normalement
            pattern = null;
        }

        public Material(Color color,
                        double ambient = 0.1,
                        double diffuse = 0.9,
                        double specular = 0.9,
                        double shininess = 200.0,
                        double reflectivity = 0.0,
                        double refractiveIndex = RefractiveIndex.Air,
                        double transparency = 0.0,
                        Pattern pattern = null )
        {
            this.color = color;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininess;
            Reflectivity = reflectivity;
            RefractIndex = refractiveIndex;
            Transparency = transparency;
            this.pattern = pattern;
        }

        public void Glassy(double transparency = 1.0, double index = RefractiveIndex.Glass)
        {
            this.refractiveIndex = index;
            this.transparency = transparency;
            this.Diffuse = 0;
        }

        public override string ToString()
        {
            return "Material -> Color : " + color.ToString() +
                             ", Ambient : " + ambient.ToString() +
                             ", Diffuse : " + diffuse.ToString() +
                             ", Specular : " + specular.ToString() +
                             ", Shinniness : " + shininess.ToString() +
                             ", Reflectivity : " + reflectivity.ToString() +
                             ", Refractive Index : " + refractiveIndex.ToString() +
                             ", Transparency : " + transparency.ToString() ;
        }

        public override bool Equals(object obj)
        {
            var material = obj as Material;
            return material != null &&
                   EqualityComparer<Color>.Default.Equals(color, material.color) &&
                   ambient == material.ambient &&
                   diffuse == material.diffuse &&
                   specular == material.specular &&
                   shininess == material.shininess &&
                   Ambient == material.Ambient &&
                   Diffuse == material.Diffuse &&
                   Specular == material.Specular &&
                   Shininess == material.Shininess &&
                   Reflectivity == material.reflectivity &&
                   RefractIndex == material.RefractIndex &&
                   Transparency == material.Transparency &&
                   this.pattern == material.pattern ;
        }

        //public override int GetHashCode()
        //{
        //    var hashCode = 412502415;
        //    hashCode = hashCode * -1521134295 + EqualityComparer<Pattern>.Default.GetHashCode(pattern);
        //    hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(color);
        //    hashCode = hashCode * -1521134295 + ambient.GetHashCode();
        //    hashCode = hashCode * -1521134295 + diffuse.GetHashCode();
        //    hashCode = hashCode * -1521134295 + specular.GetHashCode();
        //    hashCode = hashCode * -1521134295 + shininess.GetHashCode();
        //    hashCode = hashCode * -1521134295 + reflectivity.GetHashCode();
        //    hashCode = hashCode * -1521134295 + refractiveIndex.GetHashCode();
        //    hashCode = hashCode * -1521134295 + transparency.GetHashCode();
        //    hashCode = hashCode * -1521134295 + Ambient.GetHashCode();
        //    hashCode = hashCode * -1521134295 + Diffuse.GetHashCode();
        //    hashCode = hashCode * -1521134295 + Specular.GetHashCode();
        //    hashCode = hashCode * -1521134295 + Shininess.GetHashCode();
        //    hashCode = hashCode * -1521134295 + Reflectivity.GetHashCode();
        //    hashCode = hashCode * -1521134295 + RefractIndex.GetHashCode();
        //    hashCode = hashCode * -1521134295 + Transparency.GetHashCode();
        //    return hashCode;
        //}
    }
}
