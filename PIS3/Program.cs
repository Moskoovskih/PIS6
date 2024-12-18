using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS3
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var objects = FuelInfoLoader.ReadObjectsFromFile("C:\\Users\\User\\Desktop\\1.txt");
                PrintObjects(objects);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
            Console.ReadKey();
        }

        /// <summary>Prints the objects.</summary>
        /// <param name="objects">The objects.</param>
        private static void PrintObjects(IEnumerable<FuelInfo> objects)
        {
            foreach (var obj in objects)
            {
                Console.WriteLine(obj.ToString());
            }
        }
    }
}
