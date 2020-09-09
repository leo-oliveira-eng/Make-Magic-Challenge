using BaseEntity.Domain.UnitOfWork;
using Make.Magic.Challenge.Domain.House.Repositories.Contracts;
using Make.Magic.Challenge.Domain.House.Services.Contracts;
using Make.Magic.Challenge.SharedKernel.ExternalServices.Contracts;
using Messages.Core;
using Messages.Core.Extensions;
using System;
using System.Threading.Tasks;
using HouseModel = Make.Magic.Challenge.Domain.House.Models.House;

namespace Make.Magic.Challenge.Domain.House.Services
{
    public class HouseService : IHouseService
    {
        IHouseRepository HouseRepository { get; }

        IUnitOfWork Uow { get; }

        IHarryPotterExternalService HarryPotterExternalService { get; }

        public HouseService(IHouseRepository houseRepository, IUnitOfWork uow, IHarryPotterExternalService harryPotterExternalService)
        {
            HouseRepository = houseRepository ?? throw new ArgumentNullException(nameof(houseRepository));
            Uow = uow ?? throw new ArgumentNullException(nameof(uow));
            HarryPotterExternalService = harryPotterExternalService ?? throw new ArgumentNullException(nameof(harryPotterExternalService));
        }

        public async Task<Response<HouseModel>> GetHouseAsync(string houseId)
        {
            var response = Response<HouseModel>.Create();

            if (string.IsNullOrEmpty(houseId))
                return response.WithBusinessError($"{nameof(houseId)} is invalid");

            var house = await HouseRepository.FindAsync(houseId);

            if (house.HasValue)
                return response.SetValue(house);

            var newHouseResponse = await HarryPotterExternalService.GetHouseAsync(houseId);

            if (newHouseResponse.HasError)
                return response.WithMessages(newHouseResponse.Messages);

            var createNewHouseResponse = HouseModel.Create(newHouseResponse.Data.Value.Name, newHouseResponse.Data.Value.Id);

            if (createNewHouseResponse.HasError)
                return response.WithMessages(createNewHouseResponse.Messages);

            await HouseRepository.AddAsync(createNewHouseResponse);

            if (!await Uow.CommitAsync())
                return response.WithCriticalError("Failed to try to save the House");

            return response.SetValue(createNewHouseResponse);
        }
    }
}
