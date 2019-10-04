using System;
using System.Collections.Generic;
using ZenStore.Models;

namespace ZenStore.Interfaces
{
  public interface IOrder
  {
    string Id { get; set; }
    string Name { get; set; }
    List<Product> Products { get; set; }
    bool Canceled { get; set; }
    bool Shipped { get; set; }
    DateTime OrderPlaced { get; set; }
    DateTime? ShippedDate { get; set; }
    decimal Total { get; }
  }
}