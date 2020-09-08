using BaseEntity.Domain.UnitOfWork;
using Make.Magic.Challenge.Domain.House.Repositories.Contracts;
using Make.Magic.Challenge.SharedKernel.ExternalServices.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service = Make.Magic.Challenge.Domain.House.Services.HouseService;

namespace Make.Magic.Challenge.Domain.Tests.Services.HouseService
{
    public class HouseServiceUnitTests : BaseMock
    {
        #region Fields

        protected readonly Mock<IUnitOfWork> _uowMock = new Mock<IUnitOfWork>();

        protected readonly Mock<IHouseRepository> _houseRepository = new Mock<IHouseRepository>();

        protected readonly Mock<IHarryPotterExternalService> _harryPotterExternalService = new Mock<IHarryPotterExternalService>();

        #endregion

        #region Properties

        protected Service HouseService { get; set; }

        #endregion

        #region Methods

        [TestInitialize]
        public void TestInitialize()
        {
            BeforeCreateService();

            HouseService = new Service(
                _houseRepository.Object,
                _uowMock.Object,
                _harryPotterExternalService.Object);
        }

        protected virtual void BeforeCreateService() { }

        #endregion
    }
}
