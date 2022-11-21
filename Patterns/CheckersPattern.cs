using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Patterns
{
    public class CheckersPattern : Pattern
    {
        public Pattern a;
        public Pattern b;

        public CheckersPattern() : base()
        {
            a = new SolidColorPattern(Color.white);
            b = new SolidColorPattern(Color.black);
        }

        public CheckersPattern(Pattern a, Pattern b) : base()
        {
            this.a = a;
            this.b = b;
        }

        public override Color PatternAt(Point point)
        {
            Point transPoint = this.matrix.Inverse() * point;
            if ((Math.Floor(transPoint.x + Utility.epsilon) + Math.Floor(transPoint.y + Utility.epsilon) + Math.Floor(transPoint.z + Utility.epsilon)) % 2 == 0.0)
            //if ((Math.Floor(transPoint.x) + Math.Floor(transPoint.y + Utility.epsilon) + Math.Floor(transPoint.z)) % 2 == 0.0)
            {
                return this.a.PatternAt(transPoint);
            }
            return this.b.PatternAt(transPoint);
        }
    }
}
