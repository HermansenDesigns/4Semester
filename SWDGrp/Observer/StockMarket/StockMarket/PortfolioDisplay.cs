using System;

namespace StockMarket
{
    public class PortfolioDisplay
    {
        public void PrintInformation(Portfolio portfolio)
        {
            Console.WriteLine("Displaying current portfolio...");

            Console.WriteLine($"| {nameof(portfolio)} has {portfolio.Total} in total stock value");
            Console.WriteLine($"| List of Stock");
            Console.WriteLine("|-------------------------------------");
            Console.WriteLine("|  Name  | Value | Amount |  Total   |");
            Console.WriteLine("|-------------------------------------");
            foreach (var stock in portfolio.Stocks)
            {
                Console.WriteLine($"| {stock.Key.Name} |  {stock.Key.Value:0.##}   |   {stock.Value}   |   {stock.Value * stock.Key.Value:0.##}   |");
                Console.WriteLine("|-------------------------------------");
            }

            Console.WriteLine();
        }
    }
}