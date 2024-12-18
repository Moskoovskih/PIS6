using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS3
{
    public class FuelDiscount : FuelInfo
    {
        public string FuelType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }       
        public decimal DiscountPercent { get; set; }
        public override bool IsEqual(FuelInfo other)
        {
            if (!base.IsEqual(other)) 
            {
                return false; 
            }           
            var otherDiscount = other as FuelDiscount;
            if (otherDiscount == null)
            {
                return false; 
            }            
            return this.StartDate == otherDiscount.StartDate &&
                   this.EndDate == otherDiscount.EndDate &&
                   this.DiscountPercent == otherDiscount.DiscountPercent;
        }

        public override string ToString()
        {
            return $"FuelDiscount: {FuelType}, Start: {StartDate.ToShortDateString()}, End: {EndDate.ToShortDateString()}, Discount: {DiscountPercent}%";
        }
        public bool isEqual(FuelInfo other)
        {
            if (this == other)
            {
                return true;
            }

            if (other == null)
            {
                return false; 
            }

           
            return this.FuelType == other.FuelType;
        }
        public static bool CompareDescriptionWithResult(string description, FuelInfo result)
        {
            
            var parts = description.Split(',');

            if (parts.Length != 4)
            {
                throw new ArgumentException("Неверный формат описания.");
            }

            
            string fuelType = parts[0];
            DateTime startDate = DateTime.Parse(parts[1]);
            DateTime endDate = DateTime.Parse(parts[2]);

            decimal discountPercent;
            bool isDecimalParsed = Decimal.TryParse(parts[3], NumberStyles.Any, CultureInfo.InvariantCulture, out discountPercent);

            if (!isDecimalParsed)
            {
                throw new FormatException("Не удалось распарсить процент скидки.");
            }

          
            if (result is FuelDiscount fuelDiscount)
            {
                return fuelDiscount.FuelType == fuelType &&
                       fuelDiscount.StartDate == startDate &&
                       fuelDiscount.EndDate == endDate &&
                       fuelDiscount.DiscountPercent == discountPercent;
            }

            return false;
        }
    }
}
