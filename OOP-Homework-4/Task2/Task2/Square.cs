using System;
using System.Collections.Generic;
using System.Text;

namespace Task2
{
    class Square : Figure
    {
        private double a;
        public override double calculatePerimeter()
        {
            return 4 * a;
        }
        public override double calculateSquare()
        {
            return a * a;
        }
        public Square(double _a)
        {
            a = _a;
        }
    }
}
