using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS3
{
    public class FuelPrice : FuelInfo
    {
        public string FuelType { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public override string ToString()
        {
            return $"Цена на топливо: {FuelType}, Дата: {Date:yyyy.MM.dd}, Цена: {Price:C}";
        }
    }
}
