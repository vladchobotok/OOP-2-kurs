using System;

namespace Task4
{
    class Program
    {
        //ISP (Багато клієнтських специфічних інтерфейсів краще, ніж один загальний інтерфейс.)
        interface LowPricer
        {
            void ApplyDiscount(String discount);
            void ApplyPromocode(String promocode);
        }
        interface Parametrs
        {
            void SetColor(byte color);
            void SetSize(byte size);
        }
        interface Price
        {
            void SetPrice(double price);
        }
        class Book : Price
        {
            public void SetPrice(double price)
            {

            }
            public void ApplyDiscount(String discount)
            {

            }
            public void ApplyPromocode(String promocode)
            {

            }
        }
        class Outerwear : Parametrs, LowPricer, Price
        {
            public void SetPrice(double price)
            {

            }
            public void ApplyDiscount(String discount)
            {

            }
            public void ApplyPromocode(String promocode)
            {

            }
            public void SetColor(byte color)
            {

            }
            public void SetSize(byte size)
            {

            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
