using FizzWare.NBuilder;
using Make.Magic.Challenge.Domain.Character.Models;
using Make.Magic.Challenge.Domain.House.Models;
using Make.Magic.Challenge.Messages.RequestMessages;

namespace Make.Magic.Challenge.ApplicationService.Tests
{
    public class BaseMock
    {
        public Character CharacterFake(string name = null, string role = null, string school = null, CharacterHouse house = null, string patronus = null)
        {
            var houseFake = Builder<House>
                .CreateNew()
                .Build();

            var characterHouseFake = Builder<CharacterHouse>
                    .CreateNew()
                    .With(x => x.House, houseFake)
                    .Build();

            return Builder<Character>
                .CreateNew()
                .With(x => x.Name, name ?? "Any Name")
                .With(x => x.Role, role ?? "Any Role")
                .With(x => x.School, school ?? "Any School")
                .With(x => x.House, house ?? characterHouseFake)
                .With(x => x.Patronus, patronus ?? "Any Animal")
                .Build();
        }

        public CharacterRequestMessage CharacterRequestMessageFake(string name = null, string role = null, string school = null, string house = null, string patronus = null)
            => Builder<CharacterRequestMessage>
                .CreateNew()
                .With(x => x.Name, name ?? "Any Name")
                .With(x => x.Role, role ?? "Any Role")
                .With(x => x.School, school ?? "Any School")
                .With(x => x.House, house ?? "abc123")
                .With(x => x.Patronus, patronus ?? "Any Animal")
                .Build();

        public GetCharactersRequestMessage GetCharactersRequestMessageFake(string name = null, string role = null, string school = null, string house = null, string patronus = null)
            => Builder<GetCharactersRequestMessage>
                .CreateNew()
                .With(x => x.Name, name ?? "Any Name")
                .With(x => x.Role, role ?? "Any Role")
                .With(x => x.School, school ?? "Any School")
                .With(x => x.House, house ?? "abc123")
                .With(x => x.Patronus, patronus ?? "Any Animal")
                .Build();
    }
}
