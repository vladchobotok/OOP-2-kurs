using System;
using System.Collections.Generic;
using System.Text;

namespace Task2
{
    class Circle : Figure
    {
        private double r;
        public override double calculatePerimeter()
        {
            return 2 * Math.PI * r;
        }
        public override double calculateSquare()
        {
            return Math.PI * r * r;
        }

        public Circle(double _r)
        {
            r = _r;
        }
    }
}
