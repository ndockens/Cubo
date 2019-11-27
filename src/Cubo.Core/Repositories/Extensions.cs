using System;
using System.Threading.Tasks;
using Cubo.Core.Domain;

namespace Cubo.Core.Repositories
{
    public static class Extensions
    {
        public static async Task<Bucket> GetAsyncOrFail(this IBucketRepository repository, string bucketName)
        {
            var bucket = await repository.GetAsync(bucketName);

            if (bucket == null)
                throw new CuboException("bucket_not_found");

            return bucket;
        }
    }
}