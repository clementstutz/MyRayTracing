using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Patterns
{
    public class RingPattern : Pattern
    {
        public Pattern a;
        public Pattern b;

        public RingPattern() : base()
        {
            this.a = new SolidColorPattern(Color.white);
            this.b = new SolidColorPattern(Color.black);
        }

        public RingPattern(Pattern a, Pattern b) : base()
        {
            this.a = a;
            this.b = b;
        }

        public override Color PatternAt(Point point)
        {
            // WARNING : On pourait faire pareil mais avec y aussi
            Point transPoint = this.matrix.Inverse() * point;
            if (Math.Floor(Math.Sqrt(transPoint.x * transPoint.x + transPoint.z * transPoint.z)) % 2 == 0.0)
            {
                return this.a.PatternAt(transPoint);
            }
            return this.b.PatternAt(transPoint);
        }
    }
}
