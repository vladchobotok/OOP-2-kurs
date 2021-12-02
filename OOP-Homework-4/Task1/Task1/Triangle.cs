using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Task1
{
    class Triangle
    {
        private double a;
        private double b;
        private double c;
        private double angleBetweenAandB;
        private double angleBetweenBandC;
        private double angleBetweenCandA;
        private double perimeter;
        public void createTriangle(double _a, double _b, double _c)
        {
            a = _a;
            b = _b;
            c = _c;
        }
        public void calculateAngleBetweenAandB()
        {
            angleBetweenAandB = Math.Acos((b * b + a * a - c * c)/(2 * b * a)) / Math.PI * 180;
        }
        public void calculateAngleBetweenBandC()
        {
            angleBetweenBandC = Math.Acos((b * b + c * c - a * a) / (2 * b * c)) / Math.PI * 180;
        }
        public void calculateAngleBetweenCandA()
        {
            angleBetweenCandA = Math.Acos((a * a + c * c - b * b) / (2 * a * c)) / Math.PI * 180;
        }
        public void calculatePerimeter()
        {
            perimeter = a + b + c;
        }
        public double getA()
        {
            return a;
        }
        public double getB()
        {
            return b;
        }
        public double getC()
        {
            return c;
        }
        public double getAngleBetweenAandB()
        {
            return angleBetweenAandB;
        }
        public double getAngleBetweenBandC()
        {
            return angleBetweenBandC;
        }
        public double getAngleBetweenCandA()
        {
            return angleBetweenCandA;
        }
        public double getPerimeter()
        {
            return perimeter;
        }
    }
}
