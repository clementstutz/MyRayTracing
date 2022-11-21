using System;
using System.IO;

namespace RT
{
    public class Save
    {
        public static void SaveCanvas(Canvas canvas, string filename = "temp")
        {
            CreatePPM(canvas, filename);
        }

        static void CreatePPM(Canvas canvas, string filename)
        {
            // Creat Header
            // (Type) P3
            // Width Height
            // (Max Value) 255

            using (StreamWriter sw = File.CreateText(filename + ".ppm"))
            {
                int maxValue = 255;

                sw.WriteLine("P3\n" +
                             canvas.GetWidth().ToString() + " " + canvas.GetHeight().ToString() + "\n" +
                             maxValue.ToString());  // Write header.

                WritePPMBody(canvas, maxValue, sw); // Write body.

                sw.WriteLine("\n"); // Write footer (For ImageMagick loading, width requires a new line at the end.).

                sw.Close();
            }
        }

        static void WritePPMBody(Canvas canvas, int maxValue, StreamWriter sw)
        {
            string currentLine = "";
            int canvasHeight = canvas.GetHeight();
            int canvasWidth = canvas.GetWidth();

            for (int y = 0; y < canvasHeight; y++)
            //for (int y = canvas.GetHeight() - 1; y >= 0; y--)
            {
                for (int x = 0; x < canvasWidth; x++)
                {
                    Color color = canvas.GetPixel(x, y);

                    string r = Clamp(color.r * maxValue, maxValue).ToString();
                    string g = Clamp(color.g * maxValue, maxValue).ToString();
                    string b = Clamp(color.b * maxValue, maxValue).ToString();

                    currentLine = r + " " + g + " " + b;
                    sw.WriteLine(currentLine);
                }
            }
        }

        static int Clamp(double channelColor, int maxValue, int minValue = 0)
        {
            int temp = (int)(channelColor);
            if (temp > maxValue)
            {
                temp = maxValue;
                return temp;
            }
            if (temp < minValue)
            {
                temp = minValue;
            }
            return temp;
        }
    }
}
