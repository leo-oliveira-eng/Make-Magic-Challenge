using FluentAssertions;
using Make.Magic.Challenge.SharedKernel.ExternalServices.Dtos;
using Messages.Core;
using Messages.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using HouseModel = Make.Magic.Challenge.Domain.House.Models.House;

namespace Make.Magic.Challenge.Domain.Tests.Services.HouseService
{
    [TestClass, TestCategory(nameof(Domain.House.Services.HouseService))]
    public class CreateAsyncUnitTests : HouseServiceUnitTests
    {
        protected override void BeforeCreateService()
        {
            _uowMock.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            _houseRepository.Setup(x => x.FindAsync(It.IsAny<string>())).ReturnsAsync(Maybe<HouseModel>.Create());
            _harryPotterExternalService.Setup(x => x.GetHouseAsync(It.IsAny<string>())).ReturnsAsync(Response<HouseResponseDto>.Create(HouseResponseDtoFake()));
        }

        [TestMethod]
        public async Task GetHouseAsync_ShouldReturnResponseWithAnExistingHome()
        {
            _houseRepository.Setup(x => x.FindAsync(It.IsAny<string>())).ReturnsAsync(HouseFake());

            var response = await HouseService.GetHouseAsync("abc123");

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(HouseModel));
            response.Data.Value.Name.Should().Be(HouseFake().Name);
        }

        [TestMethod]
        public async Task GetHouseAsync_ShouldReturnError_HouseIdIsEmpty()
        {
            var response = await HouseService.GetHouseAsync(string.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task GetHouseAsync_ShouldReturnError_HouseIdIsNotFound()
        {
            _harryPotterExternalService.Setup(x => x.GetHouseAsync(It.IsAny<string>())).ReturnsAsync(Response<HouseResponseDto>.Create().WithBusinessError("Not Found"));
            
            var response = await HouseService.GetHouseAsync("abc123");

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task GetHouseAsync_ShouldReturnError_HouseNameIsEmpty()
        {
            _harryPotterExternalService.Setup(x => x.GetHouseAsync(It.IsAny<string>())).ReturnsAsync(Response<HouseResponseDto>.Create(HouseResponseDtoFake(name: string.Empty)));

            var response = await HouseService.GetHouseAsync("abc123");

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("name"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task GetHouseAsync_ShouldReturnError_CommitAsyncFailed()
        {
            _uowMock.Setup(x => x.CommitAsync()).ReturnsAsync(false);

            var response = await HouseService.GetHouseAsync("abc123");

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task GetHouseAsync_ShouldReturnResponseWithANewHome()
        {         
            var response = await HouseService.GetHouseAsync("abc123");

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(HouseModel));
            response.Data.Value.Name.Should().Be(HouseFake().Name);
        }
    }
}
