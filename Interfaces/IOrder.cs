using System.Collections.Generic;
using ZenStore.Models;

namespace ZenStore.Interfaces
{
    public interface IOrder
    {
        string Name { get; set; }
        List<Product> Products { get; set; }
        bool Canceled { get; set; }
        bool Shipped { get; set; }
        decimal Total { get; }
    }
}