using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Light
    {
        protected static int currentId = 0;
        protected int id;
        public Color intensity;
        public Point position;

        public int Id
        {
            get { return id; }
            private set { id = value; }
        }

        public Light()
        {
            if (Scene.current != null)
            {
                Scene.current.AddLight(this);
            }
            id = currentId++;
            intensity = new Color(1, 1, 1);
            position = new Point(0, 0, 0);
        }

        public Light(Point position, Color intensity)
        {
            if (Scene.current != null)
            {
                Scene.current.AddLight(this);
            }
            id = currentId++;
            this.intensity = intensity;
            this.position = position;
        }

        public override string ToString()
        {
            return "Light (" + id.ToString() + ") -> position: " + position.ToString() + ", intensity: " + intensity.ToString();
        }
    }
}
