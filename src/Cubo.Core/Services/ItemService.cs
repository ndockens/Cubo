using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using Cubo.Core.Repositories;
using Cubo.Core.Domain;
using Cubo.Core.DTOs;

namespace Cubo.Core.Services
{
    public class ItemService : IItemService
    {
        private readonly IBucketRepository _repository;

        public ItemService(IBucketRepository repository)
        {
            _repository = repository;
        }

        public async Task<ItemDTO> GetAsync(string bucketName, string key)
        {
            var bucket = await _repository.GetAsyncOrFail(bucketName);
            var item = bucket.GetItemOrFail(key);

            return new ItemDTO
            {
                Key = item.Key,
                Value = item.Value
            };
        }

        public async Task<IEnumerable<string>> GetKeysAsync(string bucketName)
        {
            var bucket = await _repository.GetAsyncOrFail(bucketName);
            var keys = bucket.GetKeys();
            return keys;
        }

        public async Task AddAsync(string bucketName, string key, object value)
        {
            var bucket = await _repository.GetAsyncOrFail(bucketName);
            bucket.AddItem(key, JsonSerializer.Serialize(value));
            await _repository.UpdateAsync(bucket);
        }

        public async Task RemoveAsync(string bucketName, string key)
        {
            var bucket = await _repository.GetAsyncOrFail(bucketName);
            bucket.RemoveItem(key);
            await _repository.UpdateAsync(bucket);
        }
    }
}