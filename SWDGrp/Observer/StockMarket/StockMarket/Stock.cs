using System.Collections.Generic;
using System.Linq;

namespace StockMarket
{
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
                StockMarket.ValueChanged(this);
            }
        }

        public Stock(string name ,decimal value)
        {
            Name = name;
            Value = value;
            StockMarket.Stocks.Add(this);
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
}