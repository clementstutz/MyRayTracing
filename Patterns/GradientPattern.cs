using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Patterns
{
    public class GradientPattern : Pattern
    {
        public Pattern a;
        public Pattern b;

        public GradientPattern() : base()
        {
            this.a = new SolidColorPattern(Color.white);
            this.b = new SolidColorPattern(Color.black);
        }

        public GradientPattern(Pattern a, Pattern b) : base()
        {
            this.a = a;
            this.b = b;
        }

        public override Color PatternAt(Point point)
        {
            Point transPoint = this.matrix.Inverse() * point;
            return a.PatternAt(transPoint) - (a.PatternAt(transPoint) - b.PatternAt(transPoint)) * (transPoint.x - Math.Floor(transPoint.x));

            // pour un grandiant en miroir
            //if (Math.Abs(transPoint.x - Math.Floor(transPoint.x)) < 0.5)
            //{
            //    return a.PatternAt(transPoint) + (b.PatternAt(transPoint) - a.PatternAt(transPoint)) * (2 * transPoint.x - Math.Floor(2 * transPoint.x));
            //}
            //else
            //{
            //    return b.PatternAt(transPoint) + (a.PatternAt(transPoint) - b.PatternAt(transPoint)) * (2 * transPoint.x - Math.Floor(2 * transPoint.x));
            //}
        }
    }
}
