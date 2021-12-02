using System;
namespace FactoryMethodExample
{
    //написати реалізацію класу "Автомобільна фабрика", який створює транспорт від кількості обраної коліс
    public static class VehicleFactory
    {
        public interface IVehicle
        {

        }
        public class Unicycle : IVehicle
        {

        }
        public class Trike : IVehicle
        {

        }
        public class Car : IVehicle
        {

        }
        public class Motorbike : IVehicle
        {

        }
        public class Truck : IVehicle
        {

        }
        public static IVehicle Build(int numberOfWheels)
        {
            switch (numberOfWheels)
            {
                case 1:
                    return new Unicycle();
                case 2:
                    return new Motorbike();
                case 3:
                    return new Trike();
                case 4:
                    return new Car();
                default:
                    return new Truck();

            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number of wheels to build a vehicle");
            var wheels = Console.ReadLine();
            var vehicle = VehicleFactory.Build(Convert.ToInt32(wheels));
            Console.WriteLine($"You built a {vehicle.GetType().Name}");
        }
    }
}
