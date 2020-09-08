using FluentAssertions;
using Make.Magic.Challenge.ApplicationService.Services;
using Make.Magic.Challenge.Domain.Character.Dtos;
using Make.Magic.Challenge.Domain.Character.Models;
using Make.Magic.Challenge.Messages.ResponseMessages;
using Messages.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Make.Magic.Challenge.ApplicationService.Tests.AppicationServiceTests
{
    [TestClass, TestCategory(nameof(CharacterApplicationService))]
    public class GetCharacterAsyncUnitTests : CharacterAppicationServiceUnitTests
    {
        [TestMethod]
        public async Task GetCharacterAsync_ShouldReturnError_CodeIsInvalid()
        {
            var response = await ApplicationService.GetCharacterAsync(Guid.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task GetCharacterAsync_ShouldReturnError_CharacterNotFound()
        {
            _characterRepository.Setup(x => x.FindAsync(It.IsAny<Guid>())).ReturnsAsync(Maybe<Character>.Create());

            var response = await ApplicationService.GetCharacterAsync(Guid.NewGuid());

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task GetCharacterAsync_ShouldReturnSuccess_CharacterFound()
        {
            _characterRepository.Setup(x => x.FindAsync(It.IsAny<Guid>())).ReturnsAsync(CharacterFake());

            var response = await ApplicationService.GetCharacterAsync(Guid.NewGuid());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(CharacterResponseMessage));
            response.Data.Value.Name.Should().Be(CharacterFake().Name);
        }

        [TestMethod]
        public async Task GetCharacterAsync_ShouldReturnError_RequestMessageIsInvalid()
        {
            var response = await ApplicationService.GetCharacterAsync(requestMessage: null);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task GetCharacterAsync_ShouldReturnError_CharactersNotFound()
        {
            _characterRepository.Setup(x => x.FindAsync(It.IsAny<GetCharactersDto>())).ReturnsAsync(new List<Character>());

            var response = await ApplicationService.GetCharacterAsync(GetCharactersRequestMessageFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task GetCharacterAsync_ShouldReturnSuccess_CharactersFound()
        {
            _characterRepository.Setup(x => x.FindAsync(It.IsAny<GetCharactersDto>())).ReturnsAsync(new List<Character> { CharacterFake() });

            var response = await ApplicationService.GetCharacterAsync(GetCharactersRequestMessageFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(List<CharacterResponseMessage>));
            response.Data.Value[0].Name.Should().Be(CharacterFake().Name);
        }
    }
}
