using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Patterns
{
    public class RadialGradientPattern : Pattern
    {
        public Pattern a;
        public Pattern b;

        public RadialGradientPattern() : base()
        {
            a = new SolidColorPattern(Color.white);
            b = new SolidColorPattern(Color.black);
        }

        public RadialGradientPattern(Pattern a, Pattern b) : base()
        {
            this.a = a;
            this.b = b;
        }

        public override Color PatternAt(Point point)
        {
            Point transPoint = this.matrix.Inverse() * point;

            // Ring pattern.
            double distance = Math.Sqrt(transPoint.x * transPoint.x + transPoint.z * transPoint.z);
            double fraction = distance - Math.Floor(distance);

            // Gradient pattern.
            return this.a.PatternAt(transPoint) - (this.a.PatternAt(transPoint) - this.b.PatternAt(transPoint)) * fraction;
        }
    }
}
