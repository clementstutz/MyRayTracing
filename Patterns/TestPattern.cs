using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Patterns
{
    public class TestPattern : Pattern
    {
        public TestPattern() : base()
        {}

        public override Color PatternAt(Point point)
        {
            Point transPoint = this.matrix.Inverse() * point;
            return new Color(transPoint.x, transPoint.y, transPoint.z);
        }
    }
}
