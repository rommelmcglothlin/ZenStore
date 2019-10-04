using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using ZenStore.Models;

namespace ZenStore.Data
{
  public class OrdersRepository
  {
    private readonly IDbConnection _db;

    public Order Create(Order orderData)
    {
      var sql = @"INSERT INTO orders
            (id, name, canceled, orderplaced)
            VALUES
            (@Id, @Name, @Canceled, @OrderPlaced);";
      var x = _db.Execute(sql, orderData);
      return orderData;
    }

    public IEnumerable<Order> GetAll()
    {
      return _db.Query<Order>("SELECT * FROM orders");
    }
    public Order GetOrderById(string id)
    {
      var order = _db.QueryFirstOrDefault<Order>(
                "SELECT * FROM orders WHERE id = @id",
                new { id });
      return order;

    }
    internal bool UpdateOrder(Order order)
    {
      var update = _db.Execute(@"
                UPDATE orders SET
                name = @Name,
                canceled = @Canceled,
                shipped = @Shipped,
                orderplaced = @OrderPlaced,
                shippeddate = @ShippedDate
                WHERE id = @Id
                ", order);
      return update == 1;
    }

    internal bool ProductOrder(string orderId, string productId)
    {
      var id = Guid.NewGuid().ToString();
      var sql = @"INSERT INTO productorders 
                (id, orderid, productid)
                VALUES 
                (@Id, @orderId, @productId);";
      var success = _db.Execute(sql, new { id, orderId, productId });
      return success == 1;

    }

    public IEnumerable<Order> GetProductOrder(string id)
    {
      return _db.Query<Order>(@"SELECT * FROM productorders 
                                WHERE id = @id",
                                new { id });
    }
    public OrdersRepository(IDbConnection db)
    {
      _db = db;
    }





  }
}