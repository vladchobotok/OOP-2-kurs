using System;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Converter converter = new Converter(27.32, 30.82);
            Console.WriteLine(converter.HrnToUsd(3000));
            Console.WriteLine(converter.UsdToHrn(30));
            Console.WriteLine(converter.HrnToEur(3000));
            Console.WriteLine(converter.EurToHrn(30));
        }
    }

    public class Converter
    {
        public double eur { get; set; }
        public double usd { get; set; }

        public Converter(double _usd, double _eur)
        {
            usd = _usd;
            eur = _eur;
        }

        public double HrnToUsd(double hrnAmount)
        {
            return hrnAmount / usd;
        }

        public double UsdToHrn(double usdAmount)
        {
            return usdAmount * usd;
        }

        public double HrnToEur(double hrnAmount)
        {
            return hrnAmount / eur;
        }

        public double EurToHrn(double eurAmount)
        {
            return eurAmount * eur;
        }
    }
}
