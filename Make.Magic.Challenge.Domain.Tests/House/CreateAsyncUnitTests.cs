using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model = Make.Magic.Challenge.Domain.House.Models;


namespace Make.Magic.Challenge.Domain.Tests.House
{
    [TestClass, TestCategory(nameof(Model.House))]
    public class CreateUnitTests
    {
        readonly string _externalId = "abc123";
        readonly string _name = "Any House";

        [TestMethod]
        public void CreateHouse_ShouldCreateWithValidParameters()
        {
            var response = Model.House.Create(_name, _externalId);

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(Model.House));
            response.Data.Value.Name.Should().Be(_name);
        }

        [TestMethod]
        public void CreateHouse_ShouldReturnBusinessError_NameIsEmpty()
        {
            var response = Model.House.Create(string.Empty, _externalId);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("name"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public void CreateHouse_ShouldReturnBusinessError_ExternalIdIsEmpty()
        {
            var response = Model.House.Create(_name, string.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("externalId"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }
    }
}
