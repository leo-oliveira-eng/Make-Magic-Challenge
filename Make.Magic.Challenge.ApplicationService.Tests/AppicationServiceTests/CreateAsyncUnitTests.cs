using FluentAssertions;
using Make.Magic.Challenge.ApplicationService.Services;
using Make.Magic.Challenge.Domain.Character.Dtos;
using Make.Magic.Challenge.Domain.Character.Models;
using Make.Magic.Challenge.Messages.ResponseMessages;
using Messages.Core;
using Messages.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Make.Magic.Challenge.ApplicationService.Tests.AppicationServiceTests
{
    [TestClass, TestCategory(nameof(CharacterApplicationService))]
    public class CreateAsyncUnitTests : CharacterAppicationServiceUnitTests
    {
        [TestMethod]
        public async Task CreateAsync_ShouldReturnError_RequestMessageIsNull()
        {
            var response = await ApplicationService.CreateAsync(null);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnError_AnyCharacterServiceError()
        {
            _characterService.Setup(x => x.CreateAsync(It.IsAny<CreateCharacterDto>())).ReturnsAsync(Response<Character>.Create().WithBusinessError("Some error"));

            var response = await ApplicationService.CreateAsync(CharacterRequestMessageFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnSuccess_ValidArguments()
        {
            _characterService.Setup(x => x.CreateAsync(It.IsAny<CreateCharacterDto>())).ReturnsAsync(Response<Character>.Create(CharacterFake()));

            var response = await ApplicationService.CreateAsync(CharacterRequestMessageFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(CharacterResponseMessage));
            response.Data.Value.Name.Should().Be(CharacterFake().Name);
        }
    }
}
