using System;
using System.Threading.Tasks;
using Xunit;
using Cubo.Core.Repositories;
using Cubo.Core.Domain;

namespace Cubo.Tests.Repositories
{
    public class BucketRepositoryTests
    {
        [Fact]
        public async Task add_async_should_create_new_bucket()
        {
            var repository = new InMemoryBucketRepository();
            var name = "test-bucket";
            var bucket = new Bucket(Guid.NewGuid(), name);

            await repository.AddAsync(bucket);

            var createdBucket = await repository.GetAsync(name);
            Assert.Same(bucket, createdBucket);
        }
    }
}