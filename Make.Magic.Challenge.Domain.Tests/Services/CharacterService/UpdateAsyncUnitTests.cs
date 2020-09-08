using FluentAssertions;
using Messages.Core;
using Messages.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using CharacterModel = Make.Magic.Challenge.Domain.Character.Models.Character;
using HouseModel = Make.Magic.Challenge.Domain.House.Models.House;

namespace Make.Magic.Challenge.Domain.Tests.Services.CharacterService
{
    [TestClass, TestCategory(nameof(Domain.Character.Services.CharacterService))]
    public class UpdateAsyncUnitTests : CharacterServiceUnitTests
    {
        protected override void BeforeCreateService()
        {
            _uow.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            _characterRepository.Setup(x => x.FindAsync(It.IsAny<Guid>())).ReturnsAsync(Maybe<CharacterModel>.Create(CharacterFake()));
            _houseService.Setup(x => x.GetHouseAsync(It.IsAny<string>())).ReturnsAsync(Response<HouseModel>.Create(HouseFake()));
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnResponseNewValidCharacter()
        {
            var response = await CharacterService.UpdateAsync(UpdateCharacterDtoFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(CharacterModel));
            response.Data.Value.Name.Should().Be(CharacterFake().Name);
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnError_DtoIsNull()
        {
            var response = await CharacterService.UpdateAsync(null);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnError_CharacterNotFound()
        {
            _characterRepository.Setup(x => x.FindAsync(It.IsAny<Guid>())).ReturnsAsync(Maybe<CharacterModel>.Create());

            var response = await CharacterService.UpdateAsync(UpdateCharacterDtoFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnError_HouseNotFound()
        {
            _houseService.Setup(x => x.GetHouseAsync(It.IsAny<string>())).ReturnsAsync(Response<HouseModel>.Create().WithBusinessError("Not Found"));

            var response = await CharacterService.UpdateAsync(UpdateCharacterDtoFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnError_CharacterNameIsEmpty()
        {
            var response = await CharacterService.UpdateAsync(UpdateCharacterDtoFake(name: string.Empty));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("name"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnError_CommitAsyncFailed()
        {
            _uow.Setup(x => x.CommitAsync()).ReturnsAsync(false);

            var response = await CharacterService.UpdateAsync(UpdateCharacterDtoFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }
    }
}
