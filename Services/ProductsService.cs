using System;
using System.Collections.Generic;
using System.Linq;
using ZenStore.Data;
using ZenStore.Models;

namespace ZenStore.Services
{
  public class ProductsService
  {
    private readonly ProductsRepository _repo;
    private readonly ReviewsRepository _rs;

    public Product Create(Product productData)
    {
      productData.Id = Guid.NewGuid().ToString();
      _repo.Create(productData);
      return productData;
    }

    public Product GetProductById(string id)
    {
      var product = _repo.GetProductById(id);
      if (product == null)
      {
        throw new Exception("Not a valid ID");
      }
      return product;
    }

    public Product EditProduct(Product productData)
    {
      var product = GetProductById(productData.Id);
      product.Name = productData.Name;
      product.Description = productData.Description;
      product.Price = productData.Price;

      bool success = _repo.EditProductById(product);
      if (!success)
      {
        throw new Exception("Unable to edit this product");
      }
      return product;

    }
    public List<Product> GetProducts()
    {
      return _repo.GetAll().ToList();
    }

    public List<Review> GetReviews(string id)
    {
      Product product = _repo.GetProductById(id);
      if (product == null)
      {
        throw new Exception("Not a valid ID");
      }
      return _rs.GetReviewsForProduct(product.Id).ToList();
    }

    public Product DeleteProduct(string id)
    {
      var product = GetProductById(id);
      var deleted = _repo.DeleteProduct(id);
      if (!deleted)
      {
        throw new Exception($"Unable to remove product at Id {id} ");
      }
      return product;
    }

    public ProductsService(ProductsRepository repo, ReviewsRepository rs)
    {
      _repo = repo;
      _rs = rs;
    }

  }
}