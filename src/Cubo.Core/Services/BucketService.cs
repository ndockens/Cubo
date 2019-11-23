using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Cubo.Core.Repositories;
using Cubo.Core.Domain;

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
            var bucket = _repository.GetAsyncOrFail(bucketName);
            return new BucketDTO(bucket);
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