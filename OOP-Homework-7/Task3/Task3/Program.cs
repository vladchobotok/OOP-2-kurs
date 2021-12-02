using System;

namespace Task3
{
    class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public int GetRectangleArea()
        {
            return Width * Height;
        }
    }
    //квадрат наслідується від прямокутника!!!
    //(LSP) Функції, які використовують посилання на базові класи,
    //повинні мати можливість використовувати об'єкти похідних класів, не знаючи про це.
    class Square : Rectangle
    {
        /*public override int Width
        {
            get { return base.Width; }
            set
            {
                base.Height = value;
                base.Width = value;
            }
        }
        public override int Height
        {
            get { return base.Height; }
            set
            {
                base.Height = value;
                base.Width = value;
            }
        }*/

        class Program
        {
            static void Main(string[] args)
            {
                Rectangle rect = new Square();
                rect.Width = 5;
                rect.Height = 10;

                Console.WriteLine(rect.GetRectangleArea());
                Console.ReadKey();
            }
        }
    }
}