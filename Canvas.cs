using RT;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Canvas
    {
        int width;
        int height;
        Color[,] canvas;

        public Canvas(int width, int height, double color1 = 0, double color2 = 0, double color3 = 0)
        {
            this.width = width;
            this.height = height;
            CreateCanvas(color1, color2, color3);
        }

        void CreateCanvas(double color1 = 0, double color2 = 0, double color3 = 0)
        {
            canvas = new Color[width, height];
            Color color = new Color(color1, color2, color3);
            FillCanvas(color);
        }

        public void FillCanvas(Color color)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    canvas[x, y] = new Color(color);
                }
            }
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                canvas[x, y] = color;
            }
        }

        public Color GetPixel(int x, int y)
        {
            Color temp = new Color();
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                temp = canvas[x, y];
            }
            return temp;
        }

        public void DrawCircle(int cx, int cy, int radius, Color color)
        {
            // Creat a block to iterate over.
            int xStart = cx - radius;
            int xEnd = cx + radius;
            int yStart = cy - radius;
            int yEnd = cy + radius;

            // Iterate over every element of the block and test if it is within the radius.
            for (int y = yStart; y <= yEnd; y++) // (int y = yStart; y <= yEnd; y++)
            {
                for (int x = xStart; x <= xEnd; x++) // (int x = xStart; x <= xEnd; x++)
                {
                    // Calculate distance to center, use square as it is faster that root.
                    int squareRadius = radius * radius;
                    double distance = (x - cx) * (x - cx) + (y - cy) * (y - cy);
                    if (Math.Floor(distance) < squareRadius)
                    {
                        // Draw to location, which tests to see if it is even possible.
                        SetPixel(x, y, color);
                    }
                }
            }
        }

        public void DrawRing(int cx, int cy, int radius, int thickness, Color color)
        {
            // Creat a block to iterate over.
            int xStart = cx - radius;
            int xEnd = cx + radius;
            int yStart = cy - radius;
            int yEnd = cy + radius;

            // Iterate over every element of the block and test if it is within the radius.
            for (int y = yStart; y <= yEnd; y++) // (int y = yStart; y <= yEnd; y++)
            {
                for (int x = xStart; x <= xEnd; x++) // (int x = xStart; x <= xEnd; x++)
                {
                    // Calculate distance to center, use square as it is faster that root.
                    int supSquareRadius = radius * radius;
                    int distance = (x - cx) * (x - cx) + (y - cy) * (y - cy);
                    if (thickness > 0)
                    {
                        int infSquareRadius = (radius - thickness) * (radius - thickness);
                        //for (int t = 0; t < thickness; t++)
                        {
                            if (infSquareRadius <= distance && distance < supSquareRadius)
                            {
                                // Draw to location, which tests to see if it is even possible.
                                SetPixel(x, y, color);
                            }
                        }
                    }
                    else
                    {
                        if (distance == supSquareRadius)
                        {
                            // Draw to location, which tests to see if it is even possible.
                            SetPixel(x, y, color);
                        }
                    }
                }
            }
        }

        public void DrawSquare(int cx, int cy, int length, Color color)
        {
            // Creat a block to iterate over.
            int xStart = cx - length / 2;
            int xEnd = cx + length / 2;
            int yStart = cy - length / 2;
            int yEnd = cy + length / 2;

            for (int y = yStart; y <= yEnd; y++)
            {
                for (int x = xStart; x <= xEnd; x++)
                {
                    //Draw to location, which tests to see if it is even possible
                    SetPixel(x, y, color);
                }
            }
        }

        public void DrawRectangle(int cx, int cy, int width, int height, Color color)
        {
            //Creat a block to iterate over
            int xStart = cx - width / 2;
            int xEnd = cx + width / 2;
            int yStart = cy - height / 2;
            int yEnd = cy + height / 2;

            for (int y = yStart; y <= yEnd; y++)
            {
                for (int x = xStart; x <= xEnd; x++)
                {
                    //Draw to location, which tests to see if it is even possible
                    SetPixel(x, y, color);
                }
            }
        }

        public void DrawRectangleFromCorner(int cx, int cy, int width, int height, Color color)
        {
            //Creat a block to iterate over
            int xStart = cx;
            int xEnd = cx + width - 1;
            int yStart = cy;
            int yEnd = cy + height - 1;

            for (int y = yStart; y <= yEnd; y++)
            {
                for (int x = xStart; x <= xEnd; x++)
                {
                    //Draw to location, which tests to see if it is even possible
                    SetPixel(x, y, color);
                }
            }
        }

        public void DrawBox(int hLength, int vLength, int thickness, Color color, bool centered = true, int hMargin = 0, int vMargin = 0)
        {
            if (thickness < 1)
                thickness = 1;

            if (centered == true)
            {
                int Cx = 0;
                int Lcx = 0;
                int Rcx = 0;
                int Cy = 0;
                int Tcy = 0;
                int Bcy = 0;

                if (Utility.IsEven(width))
                {
                    Cx = (width / 2) - 1;
                    if (Utility.IsEven(hLength))
                    {
                        Lcx = Cx - (hLength / 2) + 1; // Ok
                        Rcx = Cx + (hLength / 2) + 1 - thickness; // Ok
                    }
                    else
                    {
                        int temp = hLength - 1; // Ok
                        Lcx = Cx - temp / 2; // Ok
                        Rcx = Cx + temp / 2 + 1 - thickness; // Ok
                    }
                }
                else
                {
                    Cx = ((width + 1) / 2) - 1;
                    if (Utility.IsEven(hLength))
                    {
                        Lcx = Cx - (hLength / 2); // Ok
                        Rcx = Cx + (hLength / 2) - thickness; // Ok
                    }
                    else
                    {
                        int temp = hLength - 1; // Ok
                        Lcx = Cx - temp / 2; // Ok
                        Rcx = Cx + temp / 2 + 1 - thickness; // Ok
                    }
                }

                if (Utility.IsEven(height))
                {
                    Cy = (height / 2) - 1;
                    if (Utility.IsEven(vLength))
                    {
                        Bcy = Cy - vLength / 2 + 1; // Ok
                        Tcy = Cy + vLength / 2 + 1 - thickness; // Ok
                    }
                    else
                    {
                        int temp = vLength - 1; // Ok
                        Bcy = Cy - temp / 2; // Ok
                        Tcy = Cy + temp / 2 + 1 - thickness; // Ok
                    }
                }
                else
                {
                    Cy = ((height + 1) / 2) - 1;
                    if (Utility.IsEven(vLength))
                    {
                        Bcy = Cy - vLength / 2; // Ok
                        Tcy = Cy + vLength / 2 - thickness; // Ok
                    }
                    else
                    {
                        int temp = vLength - 1; // Ok
                        Bcy = Cy - temp / 2; // Ok
                        Tcy = Cy + temp / 2 + 1 - thickness; // Ok
                    }
                }

                // Draw left wall of the box
                DrawRectangleFromCorner(Lcx, Bcy, thickness, vLength, color);

                // Draw top wall of the box
                DrawRectangleFromCorner(Lcx, Tcy, hLength, thickness, color);

                // Draw right wall of the box
                DrawRectangleFromCorner(Rcx, Bcy, thickness, vLength, color);

                // Draw botom wall of the box
                DrawRectangleFromCorner(Lcx, Bcy, hLength, thickness, color);
            }
            else
            {
                int Lcx = hMargin;
                int Bcy = vMargin;
                int Rcx = hMargin + hLength - thickness;
                int Tcy = vMargin + vLength - thickness;

                // Draw left wall of the box
                DrawRectangleFromCorner(Lcx, Bcy, thickness, vLength, color);

                // Draw top wall of the box
                DrawRectangleFromCorner(Lcx, Tcy, hLength, thickness, color);

                // Draw right wall of the box
                DrawRectangleFromCorner(Rcx, Bcy, thickness, vLength, color);

                // Draw botom wall of the box
                DrawRectangleFromCorner(Lcx, Bcy, hLength, thickness, color);
            }
        }

        public void DrawVGrid(int spacing, int thickness, Color color)
        {
            if (thickness > 0)
            {
                for (int t = 0; t < thickness; t++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x = x + spacing)
                        {
                            canvas[x + t, y] = color;
                        }
                    }
                }
            }
        }

        public void DrawHGrid(int spacing, int thickness, Color color)
        {
            if (thickness > 0)
            {
                for (int t = 0; t < thickness; t++)
                {
                    for (int y = 0; y < height; y = y + spacing)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            canvas[x, y + t] = color;
                        }
                    }
                }
            }
        }

        public void DrawGradiantColor(int cx, int cy, int width, int height, Color color1, Color color2)
        {
            //Color color;

            //return this.a.PatternAt(transPoint) -
            //       (this.a.PatternAt(transPoint) - this.b.PatternAt(transPoint)) *
            //       (transPoint.x - Math.Floor(transPoint.x));

            //DrawRectangle(cx, cy, width, height, color);

        }

        public override string ToString()
        {
            return "Width: " + this.width.ToString() + ", Height: " + this.height.ToString();
        }
    }
}
