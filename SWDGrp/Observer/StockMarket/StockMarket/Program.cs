using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockMarket
{
    class Program
    {
        static void Main(string[] args)
        {
            var pd = new PortfolioDisplay();
            var myPortfolio = new Portfolio(pd);
            var hisPortfolio = new Portfolio(pd);

            var googleStock = new Stock("Google" , 200M);
            var vestasStock = new Stock("Vestas", 45M);

            myPortfolio.AddStock(googleStock, 50);
            myPortfolio.AddStock(vestasStock, 95);
            hisPortfolio.AddStock(googleStock, 20);

            googleStock.Value = 55M;
            vestasStock.Value = 20M;

            Console.WriteLine($"{nameof(myPortfolio)} has {myPortfolio.Total} value");
            Console.WriteLine($"{nameof(hisPortfolio)} has {hisPortfolio.Total} value");

            while (true)
            {
                StockMarket.RefreshStocks();
                Thread.Sleep(10000);
            }
        }
    }
}
