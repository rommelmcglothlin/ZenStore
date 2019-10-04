using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenStore.Models;
using ZenStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace ZenStore.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    private readonly ProductsService _ps;

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
      return _ps.GetProducts();
    }

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<Product>> Get(string id)
    {

      try
      {
        Product product = _ps.GetProductById(id);
        return Ok(product);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    public ActionResult<Product> Post([FromBody] Product productData)
    {
      try
      {
        Product newProduct = _ps.Create(productData);
        return Created("api/products/" + newProduct.Id, newProduct);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }

    }

    [HttpPut("{id}")]
    public ActionResult<Product> Put(string id, [FromBody] Product productData)
    {
      try
      {
        productData.Id = id;
        var product = _ps.EditProduct(productData);
        return Ok(product);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/reviews")]
    public ActionResult<IEnumerable<Review>> GetReviews(string id)
    {
      try
      {
        var allReviews = _ps.GetReviews(id);
        return Ok(allReviews);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }

    }

    [HttpDelete("{id}")]
    public ActionResult<Product> Delete(string id)
    {
      try
      {
        var product = _ps.DeleteProduct(id);
        return Ok(product);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    public ProductsController(ProductsService ps)
    {
      _ps = ps;

    }
  }
}