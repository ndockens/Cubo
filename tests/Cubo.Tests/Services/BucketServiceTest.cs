using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;
using Cubo.Core.Services;
using Cubo.Core.Repositories;
using Cubo.Core.Domain;
using Cubo.Core.Mappers;
using Cubo.Core.DTOs;

namespace Cubo.Tests.Services
{
    public class BucketServiceTest
    {
        [Fact]
        public async Task add_async_should_create_new_bucket()
        {
            var repository = new InMemoryBucketRepository();
            var service = new BucketService(repository, AutoMapperConfig.GetMapper());
            var name = "test-bucket";

            await service.AddAsync(name);

            var bucketDto = await service.GetAsync(name);
            bucketDto.Name.Should().BeEquivalentTo(name);
        }

        [Fact]
        public async Task add_async_should_invoke_repository_methods()
        {
            var name = "test-bucket";
            var repositoryMock = new Mock<IBucketRepository>();
            var mapperMock = new Mock<IMapper>();
            var service = new BucketService(repositoryMock.Object, mapperMock.Object);

            await service.AddAsync(name);

            repositoryMock.Verify(x => x.AddAsync(It.IsAny<Bucket>()), Times.Once);
        }

        [Fact]
        public async Task get_async_should_return_bucket_dto()
        {
            var name = "test-bucket";
            var bucket = new Bucket(Guid.NewGuid(), name);
            var bucketDto = new BucketDTO { Name = name };
            var repositoryMock = new Mock<IBucketRepository>();
            var mapperMock = new Mock<IMapper>();
            var service = new BucketService(repositoryMock.Object, mapperMock.Object);
            repositoryMock.Setup(x => x.GetAsync(name))
                .ReturnsAsync(bucket);
            mapperMock.Setup(x => x.Map<BucketDTO>(bucket))
                .Returns(bucketDto);

            var result = await service.GetAsync(name);

            result.Should().BeEquivalentTo(bucketDto);
        }
    }
}