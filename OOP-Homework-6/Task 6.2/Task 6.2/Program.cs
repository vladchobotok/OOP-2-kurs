using System;

namespace AdapterExample
{
    //створити два інтерфейси: "пташка" та "гумова качечка".
    //Створити два класи, які імплементують кожен з інтерфейсів.
    //Кожен з інтерфейсів має свій особливий звук, тобто пташка цвірінькає, а качечка крякає
    //Адаптувати гумову качечку під пташку, щоб качечка могла відтворювати звук пташки
    interface Bird
    {
        public void makeSound();
    }

    class Sparrow : Bird
    {
        public void makeSound()
        {
            Console.WriteLine("Chirp Chirp");
        }
    }

    interface ToyDuck
    {
        public void squeak();
    }

    class PlasticToyDuck : ToyDuck
    {
        public void squeak()
        {
            Console.WriteLine("Squeak");
        }
    }

    class BirdAdapter : ToyDuck
    {
        Bird bird;
        public BirdAdapter(Bird bird)
        {
            this.bird = bird;
        }

        public void squeak()
        {
            bird.makeSound();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Sparrow sparrow = new Sparrow();
            ToyDuck toyDuck = new PlasticToyDuck();
            ToyDuck birdAdapter = new BirdAdapter(sparrow);

            Console.WriteLine("Sparrow");
            sparrow.makeSound();
            Console.WriteLine();

            Console.WriteLine("ToyDuck");
            toyDuck.squeak();
            Console.WriteLine();

            Console.WriteLine("BirdAdapter");
            birdAdapter.squeak();
            Console.WriteLine();
        }
    }
}
