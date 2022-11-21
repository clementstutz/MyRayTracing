using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Patterns
{
    public abstract class Pattern
    {
        public Mat4 matrix;

        public Pattern()
        {
            matrix = new Mat4();
        }

        public abstract Color PatternAt(Point point);

        public Color PatternAtObject(RayObject obj, Point point)
        {
            Point transPoint = obj.WorldToObject(point);
            //Move this into each pattern?
            //transPoint = this.matrix.Inverse() * transPoint;
            return this.PatternAt(transPoint);
        }
    }
}
