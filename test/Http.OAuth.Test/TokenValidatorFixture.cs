using System;
using Cure.Http.OAuth;
using Cure.Http.OAuth.Abstractions;
using Moq;
using Xunit;

namespace Http.OAuth.Test
{
    public class TokenValidatorFixture
    {
        private static ISystemClock Clock()
        {
            var moq = new Mock<ISystemClock>();
            moq.Setup(_ => _.Now).Returns(DateTime.UtcNow);
            moq.Setup(_ => _.NowOffset).Returns(DateTimeOffset.Now);
            return moq.Object;
        }


        [Fact]
        public void Should_ValidateMissingAccessToken()
        {
            // Arrange
            var validator = new TokenValidator();
            var token = new Token();

            // Act
            var result = validator.Validate(token, Clock());

            // Assert
            Assert.True(result.Failed);
            Assert.False(result.HasTokenExpired());
            Assert.True(result.IsMissingAccessToken());
        }

        [Fact]
        public void Should_ValidateTokenExpired()
        {
            // Arrange
            var validator = new TokenValidator();
            var token = new Token
            {
                AccessToken = "token",
                ExpiresAt = DateTime.Now.AddSeconds(-86000)
            };

            // Act
            var result = validator.Validate(token, Clock());

            // Assert
            Assert.True(result.Failed);
            Assert.False(result.IsMissingAccessToken());
            Assert.True(result.HasTokenExpired());
        }

        [Fact]
        public void Should_ValidateValidToken()
        {
            // Arrange
            var validator = new TokenValidator();
            var token = new Token
            {
                AccessToken = "token",
                ExpiresAt = DateTime.Now.AddSeconds(86000)
            };

            // Act
            var result = validator.Validate(token, Clock());

            // Assert
            Assert.True(result.Succeeded);
            Assert.False(result.IsMissingAccessToken());
            Assert.False(result.HasTokenExpired());
        }
    }
}