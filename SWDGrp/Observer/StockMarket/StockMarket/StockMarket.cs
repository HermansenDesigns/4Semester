using System;
using System.Collections.Generic;

namespace StockMarket
{
    public static class StockMarket
    {
        public static HashSet<Stock> Stocks { get; set; } = new HashSet<Stock>();
        public static void ValueChanged(ISubject stock)
        {
            Console.WriteLine("Value");
            Console.WriteLine($"- {((Stock)stock).Name} changed to {((Stock)stock).Value:0.##}\n");
        }

        public static void RefreshStocks()
        {
            Random rnd = new Random();
            foreach (var stock in Stocks)
            {
                stock.Value = stock.Value + stock.Value * rnd.Next(-2, 2)/100;
            }
        }
    }
}