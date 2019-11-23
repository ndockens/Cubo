using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Cubo.Core.Repositories;
using Cubo.Core.Domain;
using Cubo.Core.DTOs;

namespace Cubo.Core.Services
{
    public class BucketService : IBucketService
    {
        private readonly IBucketRepository _repository;
        public BucketService(IBucketRepository repository)
        {
            _repository = repository;
        }

        public async Task<BucketDTO> GetAsync(string bucketName)
        {
            var bucket = await _repository.GetAsyncOrFail(bucketName);

            return new BucketDTO
            {
                Name = bucket.Name,
                CreatedAt = bucket.CreatedAt,
                Items = bucket.Items.Select(x => x.Key).ToList()
            };
        }

        public async Task<IEnumerable<string>> GetNamesAsync()
        {
            return await _repository.GetNamesAsync();
        }

        public async Task AddAsync(string bucketName)
        {
            var bucket = new Bucket(Guid.NewGuid(), bucketName);
            await _repository.AddAsync(bucket);
        }

        public async Task RemoveAsync(string bucketName)
        {
            await _repository.RemoveAsync(bucketName);
        }

    }
}