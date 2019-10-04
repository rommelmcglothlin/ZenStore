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
            (id, name, canceled, shipped, orderplaced, shippeddate)
            VALUES
            (@Id, @Name, @Canceled, @Shipped, @OrderPlaced, @ShippedDate);";
      var x = _db.Execute(sql, orderData);
      return orderData;
    }

    public IEnumerable<Order> GetAll()
    {
      return _db.Query<Order>("SELECT * FROM orders");
    }
    public Order GetOrderById(string id)
    {
      return _db.QueryFirstOrDefault<Order>(
                "SELECT * FROM orders WHERE id = @id",
                new { id });

    }
    internal bool UpdateOrder(Order Order)
    {
      var nRows = _db.Execute(@"
                UPDATE orders SET
                name = @Name,
                canceled = @Canceled,
                shipped = @Shipped,
                orderplaced = @OrderPlaced,
                shippeddate = @ShippedDate
                WHERE id = @Id
                ", Order);
      return nRows == 1;
    }

    internal bool ProductOrder(string orderId, string productId)
    {
      var id = Guid.NewGuid().ToString();
      var sql = @"INSERT INTO productorders 
                (id, itemid, orderid)
                VALUES 
                (@Id, @itemId, @orderId,)";
      var success = _db.Execute(sql, new { orderId, productId });
      return success == 1;

    }
    public OrdersRepository(IDbConnection db)
    {
      _db = db;
    }





  }
}