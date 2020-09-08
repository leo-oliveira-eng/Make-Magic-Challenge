using FluentAssertions;
using Make.Magic.Challenge.ApplicationService.Services;
using Make.Magic.Challenge.Domain.Character.Models;
using Messages.Core;
using Messages.Core.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Make.Magic.Challenge.ApplicationService.Tests.AppicationServiceTests
{
    [TestClass, TestCategory(nameof(CharacterApplicationService))]
    public class RemoveAsyncUnitTests : CharacterAppicationServiceUnitTests
    {
        [TestMethod]
        public async Task RemoveAsync_ShouldReturnError_CodeIsInvalid()
        {
            var response = await ApplicationService.RemoveAsync(Guid.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            response.Messages.All(message => message.Type.Equals(MessageType.BusinessError));
        }

        [TestMethod]
        public async Task RemoveAsync_ShouldReturnError_CharacterNotFound()
        {
            _characterRepository.Setup(x => x.FindAsync(It.IsAny<Guid>())).ReturnsAsync(Maybe<Character>.Create());

            var response = await ApplicationService.RemoveAsync(Guid.NewGuid());

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            response.Messages.All(message => message.Type.Equals(MessageType.BusinessError));
        }

        [TestMethod]
        public async Task RemoveAsync_ShouldReturnError_CommitAsyncFailed()
        {
            _characterRepository.Setup(x => x.FindAsync(It.IsAny<Guid>())).ReturnsAsync(Maybe<Character>.Create(CharacterFake()));
            _uow.Setup(x => x.CommitAsync()).ReturnsAsync(false);

            var response = await ApplicationService.RemoveAsync(Guid.NewGuid());

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            response.Messages.All(message => message.Type.Equals(MessageType.CriticalError));
        }
    }
}
