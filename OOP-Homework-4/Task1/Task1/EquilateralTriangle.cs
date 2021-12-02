using System;
using System.Collections.Generic;
using System.Text;

namespace Task1
{
    class EquilateralTriangle : Triangle
    {
        private double square;

        public void calculateSquare()
        {
            calculatePerimeter();
            double halfPerimetr = getPerimeter() / 2;
            square = Math.Sqrt(halfPerimetr * (halfPerimetr - getA()) * (halfPerimetr - getB()) * (halfPerimetr - getC()));
        }

        public double getSquare()
        {
            return square;
        }
    }
}
