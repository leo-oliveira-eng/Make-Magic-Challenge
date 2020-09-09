using FizzWare.NBuilder;
using Model = Make.Magic.Challenge.Domain.Character.Models;
using HouseModel = Make.Magic.Challenge.Domain.House.Models.House;
using Make.Magic.Challenge.SharedKernel.ExternalServices.Dtos;
using Make.Magic.Challenge.Domain.Character.Dtos;
using System;

namespace Make.Magic.Challenge.Domain.Tests
{
    public class BaseMock
    {
        public Model.Character CharacterFake(string name = null, string role = null, string school = null, Model.CharacterHouse house = null, string patronus = null)
        {
            var houseFake = Builder<HouseModel>
                .CreateNew()
                .Build();
            
            var characterHouseFake = Builder<Model.CharacterHouse>
                    .CreateNew()
                    .With(x => x.House, houseFake)
                    .Build();

            return Builder<Model.Character>
                .CreateNew()
                .With(x => x.Name, name ?? "Any Name")
                .With(x => x.Role, role ?? "Any Role")
                .With(x => x.School, school ?? "Any School")
                .With(x => x.House, house ?? characterHouseFake)
                .With(x => x.Patronus, patronus ?? "Any Animal")
                .Build();
        }

        public Model.CharacterHouse CharacterHouseFake(long? houseId = null)
        {
            var houseFake = Builder<HouseModel>
                .CreateNew()
                .With(x => x.Id, houseId ?? 1)
                .Build();

            return Builder<Model.CharacterHouse>
                .CreateNew()
                .With(x => x.HouseId, houseFake.Id)
                .With(x => x.House, houseFake)
                .Build();
        }

        public HouseModel HouseFake(string name = null, string externalId = null)
            => Builder<HouseModel>
                .CreateNew()
                .With(x => x.Name, name ?? "Any House")
                .With(x => x.ExternalId, externalId ?? "abc123")
                .Build();

        public HouseResponseDto HouseResponseDtoFake(string name = null, string externalId = null)
            => Builder<HouseResponseDto>
                .CreateNew()
                .With(x => x.Name, name ?? "Any House")
                .With(x => x.Id, externalId ?? "abc123")
                .Build();

        public CreateCharacterDto CreateCharacterDtoFake(string name = null, string role = null, string school = null, string house = null, string patronus = null)
            => Builder<CreateCharacterDto>
                .CreateNew()
                .With(x => x.Name, name ?? "Any Name")
                .With(x => x.Role, role ?? "Any Role")
                .With(x => x.School, school ?? "Any School")
                .With(x => x.House, house ?? "abc123")
                .With(x => x.Patronus, patronus ?? "Any Animal")
                .Build();

        public UpdateCharacterDto UpdateCharacterDtoFake(Guid? code = null, string name = null, string role = null, string school = null, string house = null, 
            string patronus = null)
            => Builder<UpdateCharacterDto>
                .CreateNew()
                .With(x => x.Code, code ?? Guid.NewGuid())
                .With(x => x.Name, name ?? "Any Name")
                .With(x => x.Role, role ?? "Any Role")
                .With(x => x.School, school ?? "Any School")
                .With(x => x.House, house ?? "abc123")
                .With(x => x.Patronus, patronus ?? "Any Animal")
                .Build();
    }
}
