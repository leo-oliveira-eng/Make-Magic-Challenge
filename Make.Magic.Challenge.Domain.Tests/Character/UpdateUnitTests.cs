using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model = Make.Magic.Challenge.Domain.Character.Models;

namespace Make.Magic.Challenge.Domain.Tests.Character
{
    [TestClass, TestCategory(nameof(Model.Character))]
    public class UpdateUnitTests : BaseMock
    {
        readonly string _name = "Another Name";
        readonly string _role = "Another Role";
        readonly string _school = "Another School";
        Model.CharacterHouse _house;
        readonly string _patronus = "Another Animal";

        [TestInitialize]
        public void TestInitialize()
        {
            _house = CharacterHouseFake();
        }

        [TestMethod]
        public void UpdateCharacter_ShouldReturnAnEmptyResponse_WithValidParameters()
        {
            var character = CharacterFake();

            var response = character.Update(_name, _role, _school, _house, _patronus);

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
        }

        [TestMethod]
        public void UpdateCharacter_ShouldReturnAnEmptyResponse_WithValidParametersButPatronusIsEmpty()
        {
            var character = CharacterFake();

            var response = character.Update(_name, _role, _school, _house, string.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
        }

        [TestMethod]
        public void UpdateCharacter_ShouldReturnBusinessError_NameIsEmpty()
        {
            var response = Model.Character.Create(string.Empty, _role, _school, _house, string.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("name"));
        }

        [TestMethod]
        public void UpdateCharacter_ShouldReturnBusinessError_RoleIsEmpty()
        {
            var response = Model.Character.Create(_name, string.Empty, _school, _house, string.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("role"));
        }

        [TestMethod]
        public void UpdateCharacter_ShouldReturnBusinessError_SchoolIsEmpty()
        {
            var response = Model.Character.Create(_name, _role, string.Empty, _house, string.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("school"));
        }

        [TestMethod]
        public void UpdateCharacter_ShouldReturnBusinessError_HouseIsEmpty()
        {
            var response = Model.Character.Create(_name, _role, _school, null, string.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("house"));
        }

        [TestMethod]
        public void UpdateCharacter_ShouldReturnBusinessError_AllParametersAreNull()
        {
            var response = Model.Character.Create(null, null, null, null, null);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(4);
            response.Messages.Should().Contain(message => message.Property.Equals("name"));
        }
    }
}
