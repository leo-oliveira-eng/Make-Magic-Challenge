using FluentAssertions;
using Make.Magic.Challenge.Domain.Character.Dtos;
using Messages.Core;
using Messages.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using HouseModel = Make.Magic.Challenge.Domain.House.Models.House;
using CharacterModel = Make.Magic.Challenge.Domain.Character.Models.Character;

namespace Make.Magic.Challenge.Domain.Tests.Services.CharacterService
{
    [TestClass, TestCategory(nameof(Domain.Character.Services.CharacterService))]
    public class CreateAsyncUnitTests : CharacterServiceUnitTests
    {
        [TestMethod]
        public async Task CreateAsync_ShouldReturnResponseNewValidCharacter()
        {
            _houseService.Setup(x => x.GetHouseAsync(It.IsAny<string>())).ReturnsAsync(Response<HouseModel>.Create(HouseFake()));
            _characterFactory.Setup(x => x.CreateAsync(It.IsAny<CreateCharacterDto>(), It.IsAny<HouseModel>()))
                .ReturnsAsync(Response<CharacterModel>.Create(CharacterFake()));

            var response = await CharacterService.CreateAsync(CreateCharacterDtoFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(CharacterModel));
            response.Data.Value.Name.Should().Be(CharacterFake().Name);
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnError_DtoIsNull()
        {
            var response = await CharacterService.CreateAsync(null);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnError_HouseNotFound()
        {
            _houseService.Setup(x => x.GetHouseAsync(It.IsAny<string>())).ReturnsAsync(Response<HouseModel>.Create().WithBusinessError("Not Found"));
             
            var response = await CharacterService.CreateAsync(CreateCharacterDtoFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }
    }
}
