namespace ZenStore.Interfaces
{
  public interface IProduct
  {
    string Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    decimal Price { get; set; }
  }
}