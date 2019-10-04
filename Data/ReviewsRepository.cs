using System.Collections.Generic;
using System.Data;
using Dapper;
using ZenStore.Models;
using System.Linq;

namespace ZenStore.Data
{
  public class ReviewsRepository
  {
    private readonly IDbConnection _db;

    public IEnumerable<Review> GetAll()
    {
      return _db.Query<Review>("SELECT * FROM reviews");
    }

    public Review GetReviewsById(string id)
    {
      return _db.QueryFirstOrDefault<Review>(
          "SELECT * FROM reviews WHERE id = @id",
          new { id }
      );
    }


    public Review Create(Review reviewData)
    {
      var sql = @"INSERT INTO reviews
                (productid, rating, id, name, description)
                VALUES
                (@productId, @Rating, @Id, @Name, @Description);";
      var x = _db.Execute(sql, reviewData);
      return reviewData;

    }

    internal bool EditReviewById(Review reviewData)
    {
      var edit = _db.Execute(@"
                UPDATE reviews SET
                rating = @Rating,
                name = @Name,
                description = @Description
                WHERE id = @Id
                ;", reviewData);
      return edit == 1;

    }

    public IEnumerable<Review> GetReviewsForProduct(string id)
    {
      return _db.Query<Review>(@"
                SELECT * FROM reviews 
                WHERE productid = @id;",
                 new { id });

    }

    public ReviewsRepository(IDbConnection db)
    {
      _db = db;
    }


  }
}