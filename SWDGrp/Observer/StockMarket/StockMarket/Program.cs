using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket
{
    public interface ISubject
    {
        void Register(IObserver observer);
        void Unregister(IObserver observer);

        void Notify();
    }

    public interface IObserver
    {
        void ValueChanged(ISubject stock);
    }

    public class Stock : ISubject
    {
        public string Name { get; private set; }
        private decimal _value;
        public decimal Value
        {
            get { return _value; }
            set
            {
                _value = value;
                Notify();
            }
        }

        public Stock(string name ,decimal value)
        {
            Name = name;
            Value = value;
        }

        #region ISubject Members

        private HashSet<IObserver> _observers = new HashSet<IObserver>();

        public void Register(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unregister(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            _observers.ToList().ForEach(o => o.ValueChanged(this));
        }

        #endregion
    }

    public class Portfolio : IObserver
    {
        public Portfolio(PortfolioDisplay portfolioDisplay)
        {
            PortfolioDisplay = portfolioDisplay ?? throw new ArgumentNullException(nameof(portfolioDisplay));
        }

        public PortfolioDisplay PortfolioDisplay { get; set; }
        public Dictionary<Stock, int> Stocks { get; private set; } = new Dictionary<Stock, int>();

        public void AddStock(Stock stock, int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("amount has to be greater than 0");

            if (!Stocks.ContainsKey(stock))
            {
                Stocks.Add(stock, amount);
                stock.Register(this);
                Console.WriteLine($"{stock.Name} has added to the portfolio");
            }
            else
            {
                Stocks[stock] += amount;
                Console.WriteLine($"{amount} has been added to {stock.Name}");

            }
        }

        public void RemoveStock(Stock stock, int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("amount has to be greater than 0");

            if (Stocks.ContainsKey(stock))
            {
                if (amount >= Stocks[stock])
                {
                    Stocks.Remove(stock);
                    stock.Unregister(this);
                    Console.WriteLine($"{stock.Name} has been removed from portfolio");
                }
                else
                {
                    Stocks[stock] -= amount;
                    Console.WriteLine($"{amount} has been removed from {stock.Name}");

                }
            }
            else
            {
                Console.WriteLine($"{stock.Name} is not a part of portfolio");
            }
        }

        public decimal Total
        {
            get { return Stocks.Sum(o => o.Value * o.Key.Value); }
        }

        #region IObserver Members

        public void ValueChanged(ISubject value)
        {
            Console.WriteLine("Value");
            Console.WriteLine($"- {((Stock)value).Name} changed to {((Stock)value).Value}\n");

            PortfolioDisplay.PrintInformation(this);
        }

        #endregion

    }

    public class PortfolioDisplay
    {
        public void PrintInformation(Portfolio portfolio)
        {
            Console.WriteLine("Displaying current portfolio...");

            Console.WriteLine($"{nameof(portfolio)} has {portfolio.Total} in total stock value");
            Console.WriteLine($"List of Stock");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("|  Name  | Value | Amount |  Total   |");
            Console.WriteLine("--------------------------------------");
            foreach (var stock in portfolio.Stocks)
            {
                Console.WriteLine($"| {stock.Key.Name} |  {stock.Key.Value}   |   {stock.Value}   |   {stock.Value * stock.Key.Value}   |");
                Console.WriteLine("--------------------------------------");

            }
        }
    }

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
        }
    }
}
