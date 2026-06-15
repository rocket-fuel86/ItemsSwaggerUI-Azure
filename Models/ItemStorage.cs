namespace HW1.Models
{
    public class ItemStore
    {
        private readonly Dictionary<int, Item> _items = new();
        private int _nextId = 1;

        public ItemStore()
        {
            Add(new Item { Name = "Laptop", Description = "High-performance laptop", Price = 999.99m });
            Add(new Item { Name = "Mouse", Description = "Wireless ergonomic mouse", Price = 29.99m });
            Add(new Item { Name = "Keyboard", Description = "Mechanical keyboard", Price = 79.99m });
        }

        public IEnumerable<Item> GetAll() => _items.Values.OrderBy(i => i.Id);

        public Item? GetById(int id) =>
            _items.TryGetValue(id, out var item) ? item : null;

        public Item Add(Item dto)
        {
            var item = new Item
            {
                Id = _nextId++,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CreatedAt = DateTime.UtcNow
            };
            _items[item.Id] = item;
            return item;
        }

        public Item? Update(int id, Item dto)
        {
            if (!_items.TryGetValue(id, out var item)) return null;

            item.Name = dto.Name;
            item.Description = dto.Description;
            item.Price = dto.Price;
            return item;
        }

        public bool Delete(int id) => _items.Remove(id);
    }
}
