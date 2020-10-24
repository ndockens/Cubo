using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using Cubo.Core.Repositories;
using Cubo.Core.Domain;
using Cubo.Core.DTOs;

namespace Cubo.Core.Services
{
    public class BucketService : IBucketService
    {
        private readonly IBucketRepository _repository;
        private readonly IMapper _mapper;

        public BucketService(IBucketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BucketDTO> GetAsync(string bucketName)
        {
            var bucket = await _repository.GetAsyncOrFail(bucketName);

            return _mapper.Map<BucketDTO>(bucket);
        }

        public async Task<IEnumerable<string>> GetNamesAsync()
        {
            return await _repository.GetNamesAsync();
        }

        public async Task AddAsync(string bucketName)
        {
            var bucket = await _repository.GetAsync(bucketName);

            if (bucket != null)
                throw new CuboException("bucket_already_exists");

            bucket = new Bucket(Guid.NewGuid(), bucketName);
            await _repository.AddAsync(bucket);
        }

        public async Task RemoveAsync(string bucketName)
        {
            await _repository.RemoveAsync(bucketName);
        }

    }
}