using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.UnitTesting
{
    [TestFixture]
    class Chapter02Test
    {
        [Test, Order(1)]
        public void T01_ColorConstructor()
        {
            //Create
            Color color0 = new Color();
            Assert.AreEqual(0, color0.r);
            Assert.AreEqual(0, color0.g);
            Assert.AreEqual(0, color0.b);

            Color color1 = new Color(-0.5, 0.4, 1.7);
            Assert.AreEqual(-0.5, color1.r);
            Assert.AreEqual(0.4, color1.g);
            Assert.AreEqual(1.7, color1.b);

            Color color2 = Color.yellow;
            Assert.AreEqual(1, color2.r);
            Assert.AreEqual(1, color2.g);
            Assert.AreEqual(0, color2.b);
        }

        [Test, Order(2)]
        public void T02_ColorOperators()
        {
            //Add
            Color c1 = new Color(0.9, 0.6, 0.75);
            Color c2 = new Color(0.7, 0.1, 0.25);
            Assert.AreEqual(new Color(1.6, 0.7, 1.0), c1 + c2);

            //Substract
            Assert.AreEqual(new Color(0.1, 0.4, 0.25), Color.white - c1);
            Assert.AreEqual(new Color(0.2, 0.5, 0.5), c1 - c2);

            //Multiply Scalar
            c1 = new Color(0.2, 0.3, 0.4);
            Assert.AreEqual(new Color(0.4, 0.6, 0.8), c1 * 2);

            //Hadamard Product
            c1 = new Color(1.0, 0.2, 0.4);
            c2 = new Color(0.9, 1.0, 0.1);
            Assert.AreEqual(new Color(0.9, 0.2, 0.04), c1 * c2);
            Assert.AreEqual(new Color(1.0, 0.2, 0.4), Color.white * c1);
            Assert.AreEqual(new Color(0, 0, 0), Color.black * c2);
        }

        [Test, Order(3)]
        public void T03_Canvas()
        {
            Canvas canvas = new Canvas(10, 20);
            Assert.AreEqual(10, canvas.GetWidth());
            Assert.AreEqual(20, canvas.GetHeight());

            for(int y = 0; y < canvas.GetHeight(); y++)
            {
                for (int x = 0; x < canvas.GetWidth(); x++)
                {
                    Assert.AreEqual(Color.black, canvas.GetPixel(x, y));
                }
            }

            canvas.FillCanvas(Color.green);
            for (int y = 0; y < canvas.GetHeight(); y++)
            {
                for (int x = 0; x < canvas.GetWidth(); x++)
                {
                    Assert.AreEqual(Color.green, canvas.GetPixel(x, y));
                }
            }

            canvas.SetPixel(2, 3, Color.red);
            Assert.AreEqual(Color.red, canvas.GetPixel(2, 3));
        }

        /*[Test, Order(4)]
        public void T4_RandomizeColor()
        {
            Color p = new Color().Randomize();
        }*/
    }
}
