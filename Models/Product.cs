using ZenStore.Interfaces;

namespace ZenStore.Models
{
    public class Product : IProduct
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}