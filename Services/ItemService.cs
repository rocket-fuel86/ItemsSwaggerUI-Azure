using Azure.Data.Tables;
using HW1.Models;

namespace HW1.Services
{
    public class ItemService
    {
        private readonly TableClient _table;

        public ItemService(IConfiguration config)
        {
            var service = new TableServiceClient(config.GetConnectionString("AzureStorage"));

            _table = service.GetTableClient("items");

            _table.CreateIfNotExists();
        }

        public async Task<List<Item>> GetAll()
        {
            var list = new List<Item>();

            await foreach (var item in _table.QueryAsync<Item>())
            {
                list.Add(item);
            }

            return list;
        }

        public async Task<Item?> GetById(int id)
        {
            try
            {
                var result = await _table.GetEntityAsync<Item>("Items", id.ToString());

                return result.Value;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Item> Add(ItemDto dto)
        {
            var entity = new Item
            {
                PartitionKey = "Items",
                RowKey = $"{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid()}",
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CreatedAt = DateTime.UtcNow
            };

            await _table.AddEntityAsync(entity);

            return entity;
        }

        public async Task<Item?> Update(int id, ItemDto dto)
        {
            var existing = await GetById(id);

            if (existing == null)
                return null;

            existing.Name = dto.Name;

            existing.Description = dto.Description;

            existing.Price = dto.Price;

            await _table.UpdateEntityAsync(
                existing,
                existing.ETag,
                TableUpdateMode.Replace
            );

            return existing;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                await _table.DeleteEntityAsync("Items", id.ToString());

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}