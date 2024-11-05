using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class OrderItem
    {
        public string ItemID { get; set; }
        public string OrderID { get; set; }
        public string BookID { get; set; }
        public int Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal TotalPrice { get { return Quantity * Price; } set { } }
		//public decimal TotalPrice
		//{
		//	get
		//	{
		//		if (Quantity == 0)
		//		{
		//			return 0;
		//		}

		//		return Quantity * Price;
		//	}
		//	set
		//	{
		//		//Crucial:  Error handling!
		//		if (value < 0)
		//		{
		//			throw new ArgumentException("TotalPrice cannot be negative.");
		//		}


		//		//Avoid modifying Quantity or Price directly.  Use this to enforce relationship.
		//		if (value == 0)
		//		{
		//			Quantity = 0; // If TotalPrice is 0, set Quantity to 0
		//			Price = 0; // Reset Price if TotalPrice is 0
		//		}
		//		else if (value > 0)
		//		{
		//			//Important:  Proper Calculation.
		//			if (Quantity != 0 && Price != 0)
		//			{
		//				Price = value / Quantity;
		//			}
		//			else if (Quantity == 0)
		//			{
		//				Quantity = 1; //if Quantity is zero set to 1
		//				Price = value; // Set Price
		//			}

		//		}
		//	}
		//}
		public int Discount { get; set; } = 0;

    }
}
