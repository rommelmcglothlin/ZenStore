using ZenStore.Interfaces;

namespace ZenStore.Models
{
  public class Review : IReview
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Rating { get; set; }
    public string ProductId { get; set; }
  }
}