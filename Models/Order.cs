using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ZenStore.Interfaces;

namespace ZenStore.Models
{
  public class Order : IOrder
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public List<Product> Products { get; set; }
    public bool Canceled { get; set; }
    public bool Shipped { get; set; }

    public DateTime OrderPlaced { get; set; }

    public DateTime? ShippedDate { get; set; }

    public decimal Total
    {
      get
      {
        return Products.Sum(i => i.Price);
      }
    }
  }
}