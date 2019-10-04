using System;
using System.Collections.Generic;
using ZenStore.Data;
using ZenStore.Models;

namespace ZenStore.Services
{
  public class ReviewsService
  {
    private readonly ReviewsRepository _repo;
    private readonly ProductsRepository _pr;


    public Review AddReview(Review reviewData)
    {
      var findProduct = _pr.GetProductById(reviewData.ProductId);
      if (findProduct == null)
      {
        throw new Exception("Can't find that product");
      }
      reviewData.Id = Guid.NewGuid().ToString();
      _repo.Create(reviewData);
      return reviewData;
    }
    public Review GetReviewById(string id)
    {
      var getReview = _repo.GetReviewsById(id);
      if (getReview == null)
      {
        throw new Exception("That's not a valid Id");
      }
      return getReview;
    }

    public Review EditReview(Review reviewData)
    {
      var review = GetReviewById(reviewData.Id);
      review.Name = reviewData.Name;
      review.Description = reviewData.Description;
      review.Rating = reviewData.Rating;

      bool success = _repo.EditReviewById(review);
      if (!success)
      {
        throw new Exception("Couldn't update this review");
      }
      return reviewData;
    }

    public ReviewsService(ReviewsRepository repo, ProductsRepository pr)
    {
      _repo = repo;
      _pr = pr;
    }
  }
}

