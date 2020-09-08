using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model = Make.Magic.Challenge.Domain.Character.Models;

namespace Make.Magic.Challenge.Domain.Tests.Character
{
    [TestClass, TestCategory(nameof(Model.Character))]
    public class CreateUnitTests : BaseMock
    {
        readonly string _name = "Any Name";
        readonly string _role = "Any Role";
        readonly string _school = "Any School";
        Model.CharacterHouse _house;
        readonly string _patronus = "eagle";

        [TestInitialize]
        public void TestInitialize()
        {
            _house = CharacterHouseFake();
        }

        [TestMethod]
        public void CreateCharacter_ShouldCreateWithValidParameters()
        {          
            var response = Model.Character.Create(_name, _role, _school, _house, _patronus);

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(Model.Character));
            response.Data.Value.Name.Should().Be(_name);
        }

        [TestMethod]
        public void CreateCharacter_ShouldCreateWithValidParametersButWithEmptyPatronus()
        {
            var response = Model.Character.Create(_name, _role, _school, _house, string.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(Model.Character));
            response.Data.Value.Name.Should().Be(_name);
            response.Data.Value.Patronus.Should().Be(string.Empty);
        }

        [TestMethod]
        public void CreateCharacter_ShouldReturnBusinessError_NameIsEmpty()
        {
            var response = Model.Character.Create(string.Empty, _role, _school, _house, string.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("name"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public void CreateCharacter_ShouldReturnBusinessError_RoleIsEmpty()
        {
            var response = Model.Character.Create(_name, string.Empty, _school, _house, string.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("role"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public void CreateCharacter_ShouldReturnBusinessError_SchoolIsEmpty()
        {
            var response = Model.Character.Create(_name, _role, string.Empty, _house, string.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("school"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public void CreateCharacter_ShouldReturnBusinessError_HouseIsEmpty()
        {
            var response = Model.Character.Create(_name, _role, _school, null, string.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("house"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public void CreateCharacter_ShouldReturnBusinessError_AllParametersAreNull()
        {
            var response = Model.Character.Create(null, null, null, null, null);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(4);
            response.Messages.Should().Contain(message => message.Property.Equals("name"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }
    }
}
