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

    public List<Order> GetOrders()
    {
      return _repo.GetAll().ToList();
    }

    public Order CanceledorShipped(Order orderData)//to not type this all the time
    {
      if (orderData.Shipped == true || orderData.Canceled == true)
      {
        throw new Exception("You can't edit orders that have been canceled or shipped");
      }
      return orderData;
    }


    public Order EditOrder(Order orderData)
    {
      var order = _repo.GetOrderById(orderData.Id);
      if (order == null)
      {
        throw new Exception("You need an id to find an order to edit");
      }
      var checkOrder = CanceledorShipped(order);
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
      orderData.OrderPlaced = DateTime.Now;
      orderData.ShippedDate = null;
      _repo.Create(orderData);
      orderData.Products.ForEach(product =>
      {
        _repo.ProductOrder(orderData.Id, product.Id);
      });
      return orderData;
    }

    public Order CancelOrder(string id)
    {
      var order = _repo.GetOrderById(id);
      var checkOrder = CanceledorShipped(order);
      var getOrders = _repo.GetOrders(id).ToList();
      order.Products = getOrders;
      order.ShippedDate = null;
      order.Canceled = true;
      _repo.UpdateOrder(order);
      return order;
    }

    public Order ShippedOrder(string id)
    {
      var order = _repo.GetOrderById(id);
      var checkOrder = CanceledorShipped(order);
      var getOrders = _repo.GetOrders(id).ToList();
      order.Products = getOrders;
      order.ShippedDate = DateTime.Today;
      order.Shipped = true;
      _repo.UpdateOrder(order);
      return order;
    }


    public OrdersService(OrdersRepository repo)
    {
      _repo = repo;
    }
  }
}
