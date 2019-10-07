using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenStore.Models;
using ZenStore.Services;
using Microsoft.AspNetCore.Mvc;
using ZenStore.Data;

namespace ZenStore.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrdersController : ControllerBase
  {
    private readonly OrdersService _os;


    [HttpGet]
    public ActionResult<IEnumerable<Order>> Get() //not sure if needed, but it's in here just in case
    {
      return _os.GetOrders();
    }

    [HttpGet("{id}")]
    public ActionResult<Order> Get(string id)
    {
      try
      {
        Order order = _os.GetOrderById(id);
        return Ok(order);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    public ActionResult Post([FromBody] Order orderData)
    {
      try
      {
        Order newOrder = _os.CreateOrder(orderData);
        return Ok(orderData);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    public ActionResult Put(string id, [FromBody] Order orderData)
    {
      try
      {
        orderData.Id = id;
        var order = _os.EditOrder(orderData);
        return Ok(order);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}/Cancel")]
    public ActionResult<Order> Cancel(string id)
    {
      try
      {
        Order order = _os.CancelOrder(id);
        return Ok(order);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}/Ship")]
    public ActionResult<Order> Ship(string id)
    {
      try
      {
        Order order = _os.ShippedOrder(id);
        return Ok(order);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    public OrdersController(OrdersService os)
    {
      _os = os;
    }
  }
}



