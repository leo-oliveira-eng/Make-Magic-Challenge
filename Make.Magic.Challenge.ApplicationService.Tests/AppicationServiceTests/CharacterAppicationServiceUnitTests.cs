using BaseEntity.Domain.UnitOfWork;
using Make.Magic.Challenge.ApplicationService.Services;
using Make.Magic.Challenge.Domain.Character.Repositories.Contracts;
using Make.Magic.Challenge.Domain.Character.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Make.Magic.Challenge.ApplicationService.Tests.AppicationServiceTests
{
    public class CharacterAppicationServiceUnitTests : BaseMock
    {
        #region Fields

        protected readonly Mock<IUnitOfWork> _uow = new Mock<IUnitOfWork>();

        protected readonly Mock<ICharacterRepository> _characterRepository = new Mock<ICharacterRepository>();

        protected readonly Mock<ICharacterService> _characterService = new Mock<ICharacterService>();

        #endregion

        #region Properties

        protected CharacterApplicationService ApplicationService { get; set; }

        #endregion

        #region Methods

        [TestInitialize]
        public void TestInitialize()
        {
            BeforeCreateService();

            ApplicationService = new CharacterApplicationService(_characterRepository.Object, _characterService.Object, _uow.Object);
        }

        protected virtual void BeforeCreateService() { }

        #endregion
    }
}
