using System;
using System.Collections.Generic;
using System.Linq;

namespace StockMarket
{
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
            PortfolioDisplay.PrintInformation(this);
        }

        #endregion

    }
}