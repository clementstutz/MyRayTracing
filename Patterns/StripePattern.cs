using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Patterns
{
    public class StripePattern : Pattern
    {
        public Pattern a;
        public Pattern b;

        public StripePattern() : base()
        {
            a = new SolidColorPattern(Color.white);
            b = new SolidColorPattern(Color.black);
        }

        public StripePattern(Pattern a, Pattern b) : base()
        {
            this.a = a;
            this.b = b;
        }

        public override Color PatternAt(Point point)
        {
            Point transPoint = this.matrix.Inverse() * point;

            if (Utility.FE(Math.Floor(transPoint.x) % 2, 0.0))
            {
                return this.a.PatternAt(transPoint);
            }
            return this.b.PatternAt(transPoint);
        }
    }
}
