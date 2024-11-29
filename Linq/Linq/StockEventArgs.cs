namespace Linq;

public class StockEventArgs : EventArgs
{
    public StockEventArgs(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public Product Product { get; }

    public int Quantity { get; }
}