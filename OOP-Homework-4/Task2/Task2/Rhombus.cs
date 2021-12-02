using System;
using System.Collections.Generic;
using System.Text;

namespace Task2
{
    class Rhombus : Figure
    {
        private double a;
        private double angle;
        public override double calculatePerimeter()
        {
            return 4 * a;
        }
        public override double calculateSquare()
        {
            return a * a * Math.Sin(angle * Math.PI / 180);
        }
        public Rhombus(double _a, double _angle)
        {
            a = _a;
            angle = _angle;
        }
    }
}
