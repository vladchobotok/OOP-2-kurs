using System;
using System.Collections.Generic;
using System.Text;

namespace Task2
{
    class Rectangle : Figure
    {
        private double a;
        private double b;
        public override double calculatePerimeter()
        {
            return 2 * a + 2 * b;
        }
        public override double calculateSquare()
        {
            return a * b;
        }

        public Rectangle(double _a, double _b)
        {
            a = _a;
            b = _b;
        }
    }
}
