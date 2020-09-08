using BaseEntity.Domain.UnitOfWork;
using Make.Magic.Challenge.Domain.Character.Factories.Contracts;
using Make.Magic.Challenge.Domain.Character.Repositories.Contracts;
using Make.Magic.Challenge.Domain.House.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service = Make.Magic.Challenge.Domain.Character.Services.CharacterService;

namespace Make.Magic.Challenge.Domain.Tests.Services.CharacterService
{
    public class CharacterServiceUnitTests : BaseMock
    {
        #region Fields

        protected readonly Mock<IUnitOfWork> _uow = new Mock<IUnitOfWork>();

        protected readonly Mock<ICharacterRepository> _characterRepository = new Mock<ICharacterRepository>();

        protected readonly Mock<IHouseService> _houseService = new Mock<IHouseService>();

        protected readonly Mock<ICharacterFactory> _characterFactory = new Mock<ICharacterFactory>();

        #endregion

        #region Properties

        protected Service CharacterService { get; set; }

        #endregion

        #region Methods

        [TestInitialize]
        public void TestInitialize()
        {
            BeforeCreateService();

            CharacterService = new Service(
                _characterRepository.Object,
                _uow.Object,
                _houseService.Object,
                _characterFactory.Object);
        }

        protected virtual void BeforeCreateService() { }

        #endregion
    }
}
