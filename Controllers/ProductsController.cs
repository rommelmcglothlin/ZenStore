using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ZenStore.Models;

namespace ZenStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(new List<Product>()
            {
                new Product()
                {
                    Id = "123",
                    Name = "Fake Product",
                    Description = "A Fake Product",
                    Price = 100.99m
                }
            });
        }
    }
}