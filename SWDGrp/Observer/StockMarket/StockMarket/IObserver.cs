namespace StockMarket
{
    public interface IObserver
    {
        void ValueChanged(ISubject stock);
    }
}