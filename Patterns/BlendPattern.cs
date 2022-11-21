using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Patterns
{
    public class BlendPattern : Pattern
    {
        public Pattern a;
        public Pattern b;
        public double t;

        public BlendPattern(Pattern a, Pattern b, double t = 0.5) : base()
        {
            this.a = a;
            this.b = b;
            this.t = t;
        }

        public override Color PatternAt(Point point)
        {
            Point transPoint = this.matrix.Inverse() * point;
            return a.PatternAt(transPoint) * (1.0 - t) + b.PatternAt(transPoint) * t;
        }
    }
}
