using FluentAssertions;
using Make.Magic.Challenge.ApplicationService.Services;
using Make.Magic.Challenge.Domain.Character.Dtos;
using Make.Magic.Challenge.Domain.Character.Models;
using Make.Magic.Challenge.Messages.ResponseMessages;
using Messages.Core;
using Messages.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Make.Magic.Challenge.ApplicationService.Tests.AppicationServiceTests
{
    [TestClass, TestCategory(nameof(CharacterApplicationService))]
    public class UpdateAsyncUnitTests : CharacterAppicationServiceUnitTests
    {
        [TestMethod]
        public async Task UpdateAsync_ShouldReturnError_RequestMessageIsNull()
        {
            var response = await ApplicationService.UpdateAsync(Guid.NewGuid(), null);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnError_AnyCharacterServiceError()
        {
            _characterService.Setup(x => x.UpdateAsync(It.IsAny<UpdateCharacterDto>())).ReturnsAsync(Response<Character>.Create().WithBusinessError("another error"));

            var response = await ApplicationService.UpdateAsync(Guid.NewGuid(), CharacterRequestMessageFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnSuccess_WithValidArguments()
        {
            _characterService.Setup(x => x.UpdateAsync(It.IsAny<UpdateCharacterDto>())).ReturnsAsync(Response<Character>.Create(CharacterFake()));

            var response = await ApplicationService.UpdateAsync(Guid.NewGuid(), CharacterRequestMessageFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(CharacterResponseMessage));
        }
    }
}
