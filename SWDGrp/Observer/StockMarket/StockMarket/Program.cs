using System;
using System.Collections.Generic;
using System.Linq;
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

        #region IObserver Members

        public void ValueChanged(ISubject value)
        {
            Console.WriteLine($"Value has changed: {((Stock)value).Name} | {((Stock)value).Value}");
        }

        #endregion

    }

    class Program
    {
        static void Main(string[] args)
        {
            var myPortfolio = new Portfolio();
            var googleStock = new Stock("Google" ,200M);
            var vestasStock = new Stock("Vestas", 45M);

            googleStock.Register(myPortfolio);
            vestasStock.Register(myPortfolio);

            googleStock.Value = 55M;
            vestasStock.Value = 20M;
        }
    }
}
