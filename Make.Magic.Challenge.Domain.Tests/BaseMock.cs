using FizzWare.NBuilder;
using Model = Make.Magic.Challenge.Domain.Character.Models;

namespace Make.Magic.Challenge.Domain.Tests
{
    public class BaseMock
    {
        public Model.Character CharacterFake(string name = null, string role = null, string school = null, string house = null, string patronus = null)
            => Builder<Model.Character>
                .CreateNew()
                .With(x => x.Name, name ?? "Any Name")
                .With(x => x.Role, role ?? "Any Role")
                .With(x => x.School, school ?? "Any School")
                .With(x => x.House, house ?? "Any House")
                .With(x => x.Patronus, patronus ?? "Any Animal")
                .Build();
    }
}
