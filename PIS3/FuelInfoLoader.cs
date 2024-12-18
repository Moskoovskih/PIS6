using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS3
{
    public static class FuelInfoLoader
    {
        public static List<FuelInfo> ReadObjectsFromFile(string fileName)        
        {
            var objects = new List<FuelInfo>();

            using (var reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        try
                        {
                            objects.Add(CreateFromDescription(line));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при обработке строки: '{line}'. Подробности: {ex.Message}");
                        }
                    }
                }
            }
            return objects;
        }

        public static FuelInfo CreateFromDescription(string description)
        {
            string[] properties = description.Split(',');
            if (properties.Length < 3)
                throw new ArgumentException("Недостаточно данных для создания объекта.");

            string objectType = properties[0].Trim('"');

            switch (objectType)
            {
                case "Бензин":
                    //throw new FormatException("неверный форма даты");
                    return new FuelPrice
                    {
                        FuelType = properties[0].Trim('"'),
                        Date = DateTime.ParseExact(properties[1], "yyyy.MM.dd", CultureInfo.InvariantCulture),
                        Price = decimal.Parse(properties[2], CultureInfo.InvariantCulture)
                    };
                case "Дизельное":
                    return new FuelPrice
                    {
                        FuelType = properties[0].Trim('"'),
                        Date = DateTime.ParseExact(properties[1], "yyyy.MM.dd", CultureInfo.InvariantCulture),
                        Price = decimal.Parse(properties[2], CultureInfo.InvariantCulture)
                    };
                case "Заправка":
                    if (properties.Length < 4) throw new ArgumentException("Недостаточно данных для создания заправки.");
                    return new FuelStation
                    {
                        StationName = properties[1].Trim('"'),
                        Address = properties[2].Trim('"'),
                        ContactInfo = properties[3].Trim('"')
                    };
                case "Скидка на Бензин":
                    if (properties.Length < 4)
                        throw new ArgumentException("Недостаточно данных для создания скидки.");
                    return new FuelDiscount
                    {
                        FuelType = properties[0].Trim('"'),
                        StartDate = DateTime.ParseExact(properties[1], "yyyy.MM.dd", CultureInfo.InvariantCulture),
                        EndDate = DateTime.ParseExact(properties[2], "yyyy.MM.dd", CultureInfo.InvariantCulture),
                        DiscountPercent = decimal.Parse(properties[3], CultureInfo.InvariantCulture)
                    };



                case "Скидка на Дизельное":
                    if (properties.Length < 4)
                        throw new ArgumentException("Недостаточно данных для создания скидки.");
                    return new FuelDiscount
                    {
                        FuelType = properties[0].Trim('"'),
                        StartDate = DateTime.ParseExact(properties[1], "yyyy.MM.dd", CultureInfo.InvariantCulture),
                        EndDate = DateTime.ParseExact(properties[2], "yyyy.MM.dd", CultureInfo.InvariantCulture),
                        DiscountPercent = decimal.Parse(properties[3], CultureInfo.InvariantCulture)
                    };
                default:
                    throw new ArgumentException($"Неизвестный тип объекта: {objectType}");
            }
        }
        public static DateTime ParseDate(string dateStr)
        {
            return DateTime.ParseExact(dateStr, "yyyy.MM.dd", CultureInfo.InvariantCulture);
        }

        public static decimal ParseDecimal(string decimalStr)
        {
            return decimal.Parse(decimalStr, CultureInfo.InvariantCulture);
        }

    }
}

