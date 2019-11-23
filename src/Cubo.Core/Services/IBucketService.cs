using System.Threading.Tasks;
using System.Collections.Generic;

namespace Cubo.Core.Services
{
    public interface IBucketService
    {
        Task<BucketDTO> GetAsync(string bucketName);
        Task<IEnumerable<string>> GetNamesAsync();
        Task AddAsync(string bucketName);
        Task RemoveAsync(string bucketName);
    }
}