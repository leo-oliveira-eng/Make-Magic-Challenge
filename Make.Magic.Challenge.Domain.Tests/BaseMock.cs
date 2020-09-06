using FizzWare.NBuilder;
using Model = Make.Magic.Challenge.Domain.Character.Models;
using HouseModel = Make.Magic.Challenge.Domain.House.Models.House;

namespace Make.Magic.Challenge.Domain.Tests
{
    public class BaseMock
    {
        public Model.Character CharacterFake(string name = null, string role = null, string school = null, Model.CharacterHouse house = null, string patronus = null)
        {
            var houseFake = Builder<Model.CharacterHouse>.CreateNew().Build();

            return Builder<Model.Character>
                .CreateNew()
                .With(x => x.Name, name ?? "Any Name")
                .With(x => x.Role, role ?? "Any Role")
                .With(x => x.School, school ?? "Any School")
                .With(x => x.House, house ?? houseFake)
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
    }
}
