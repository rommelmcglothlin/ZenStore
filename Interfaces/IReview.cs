namespace ZenStore.Interfaces
{
    public interface IReview
    {
        string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        double Rating { get; set; }
        string ProductId { get; set; }
    }
}