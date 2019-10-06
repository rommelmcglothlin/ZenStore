using System.Collections.Generic;
using System.Data;
using Dapper;
using ZenStore.Models;

namespace ZenStore.Data
{
  public class ProductsRepository
  {
    private readonly IDbConnection _db;

    public Product Create(Product productData)
    {
      var sql = @"INSERT INTO products
                (id, name, description, price)
                VALUES
                (@Id, @Name, @Description, @Price);";
      var x = _db.Execute(sql, productData);
      return productData;
    }

    public IEnumerable<Product> GetAll()
    {
      return _db.Query<Product>("SELECT * FROM products");
    }

    public Product GetProductByName(string name)
    {
      return _db.QueryFirstOrDefault<Product>
      (
        "SELECT * FROM products WHERE name = @Name",
        new { name }
      );
    }

    public Product GetProductById(string id)
    {
      return _db.QueryFirstOrDefault<Product>
      (
        "SELECT * FROM products WHERE id = @Id",
        new { id }
      );
    }

    internal bool EditProductById(Product product)
    {
      var edit = _db.Execute(@"
                  UPDATE products SET
                  name = @Name,
                  description = @Description,
                  price = @Price
                  WHERE id = @Id", product);
      return edit == 1;
    }

    internal bool DeleteProduct(string id)
    {
      var success = _db.Execute("DELETE FROM products WHERE id = @id", new { id });
      return success == 1;
    }

    public ProductsRepository(IDbConnection db)
    {
      _db = db;
    }
  }
}

