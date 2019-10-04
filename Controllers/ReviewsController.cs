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
  public class ReviewsController : ControllerBase
  {
    private readonly ReviewsService _rs;

    [HttpPost]
    public ActionResult Post([FromBody] Review reviewData)
    {
      try
      {
        Review newReview = _rs.AddReview(reviewData);
        return Ok(reviewData);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    public ActionResult Put(string id, [FromBody] Review reviewData)
    {
      try
      {
        reviewData.Id = id;
        var review = _rs.EditReview(reviewData);
        return Ok(review);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    public ReviewsController(ReviewsService rs)
    {
      _rs = rs;
    }
  }
}



