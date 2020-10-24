using System;
using System.Threading.Tasks;
using System.Linq;
using Cubo.Core.Repositories;
using Cubo.Core.Domain;

namespace Cubo.Core.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IBucketRepository _bucketRepository;

        public DataInitializer(IBucketRepository bucketRepository)
        {
            _bucketRepository = bucketRepository;
        }

        public async Task SeedAsync()
        {
            var names = await _bucketRepository.GetNamesAsync();

            if (names.Any()) return;

            for (int i = 0; i < 3; i++)
                await _bucketRepository.AddAsync(CreateBucket(i));
        }

        public static Bucket CreateBucket(int number)
        {
            var bucketId = Guid.NewGuid();
            var bucket = new Bucket(bucketId, $"bucket-{number}");

            for (int i = 0; i < 3; i++)
                bucket.AddItem($"item-{Guid.NewGuid()}", $"Value {Guid.NewGuid()}");

            return bucket;
        }
    }
}