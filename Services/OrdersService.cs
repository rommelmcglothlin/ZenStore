using System;
using System.Collections.Generic;
using System.Linq;
using ZenStore.Models;
using ZenStore.Data;

namespace ZenStore.Services
{
  public class OrdersService
  {
    private readonly OrdersRepository _repo;
    private readonly ProductsRepository _pr;

    public List<Order> GetOrders()
    {
      return _repo.GetAll().ToList();
    }

    public Order EditOrder(Order orderData)
    {
      var order = _repo.GetOrderById(orderData.Id);
      if (orderData.Shipped == true || orderData.Canceled == true)
      {
        throw new Exception("You can't edit orders that have been canceled or shipped");
      }
      order.Name = orderData.Name;
      order.Products = orderData.Products;
      order.ShippedDate = null;

      bool success = _repo.UpdateOrder(order);
      if (!success)
      {
        throw new Exception("Order couldn't be edited. Please try again later");
      }
      return order;
    }

    public Order GetOrderById(string id)
    {
      var order = _repo.GetOrderById(id);
      if (order == null)
      {
        throw new Exception("Invalid Order Id");
      }
      return order;
    }

    public Order CreateOrder(Order orderData)
    {
      orderData.Id = Guid.NewGuid().ToString();
      _repo.Create(orderData);
      orderData.OrderPlaced = DateTime.Now;
      orderData.Products.ForEach(item =>
      {
        _repo.ProductOrder(orderData.Id, item.Id);
      });
      return orderData;
    }

    public Order CancelOrder(string id)
    {
      var order = _repo.GetOrderById(id);
      if (order.Shipped == true)
      {
        throw new Exception("Order cannot be canceled after it was fullfilled");
      }
      order.Canceled = true;
      _repo.UpdateOrder(order);
      return order;
    }

    public Order ShippedOrder(string id)
    {
      var order = _repo.GetOrderById(id);


      if (order.Canceled == true)
      {
        throw new Exception("Order cannot be fulfilled after it was canceled");
      }

      if (order.Shipped != false)
      {
        throw new Exception("Order already fulfilled");
      }

      order.ShippedDate = DateTime.Today;
      _repo.UpdateOrder(order);
      return order;
    }

    public OrdersService(OrdersRepository repo, ProductsRepository pr)
    {
      _repo = repo;
      _pr = pr;
    }
  }
}