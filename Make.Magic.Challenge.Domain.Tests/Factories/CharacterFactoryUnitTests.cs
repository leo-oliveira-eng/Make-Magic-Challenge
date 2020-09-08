using BaseEntity.Domain.UnitOfWork;
using FluentAssertions;
using Make.Magic.Challenge.Domain.Character.Factories;
using Make.Magic.Challenge.Domain.Character.Repositories.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using CharacterModel = Make.Magic.Challenge.Domain.Character.Models.Character;

namespace Make.Magic.Challenge.Domain.Tests.Factories
{
    [TestClass, TestCategory(nameof(CharacterFactory))]
    public class CharacterFactoryUnitTests : BaseMock
    {
        #region Fields

        readonly Mock<IUnitOfWork> _uow = new Mock<IUnitOfWork>();

        readonly Mock<ICharacterRepository> _characterRepository = new Mock<ICharacterRepository>();

        #endregion

        #region Properties

        CharacterFactory CharacterFactory { get; set; }

        #endregion

        #region Unit Tests

        [TestInitialize]
        public void TestInitialize()
        {
            CharacterFactory = new CharacterFactory(_uow.Object, _characterRepository.Object);
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnResponseNewValidCharacter()
        {
            _uow.Setup(x => x.CommitAsync()).ReturnsAsync(true);

            var response = await CharacterFactory.CreateAsync(CreateCharacterDtoFake(), HouseFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(CharacterModel));
            response.Data.Value.Name.Should().Be(CharacterFake().Name);
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnError_SchoolIsEmpty()
        {
            var response = await CharacterFactory.CreateAsync(CreateCharacterDtoFake(school: string.Empty), HouseFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("school"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnError_CommitAsyncFailed()
        {
            _uow.Setup(x => x.CommitAsync()).ReturnsAsync(false);

            var response = await CharacterFactory.CreateAsync(CreateCharacterDtoFake(), HouseFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        #endregion
    }
}
