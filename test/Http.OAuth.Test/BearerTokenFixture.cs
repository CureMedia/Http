using System;
using System.Net.Http;
using System.Web;
using Xunit;
using static Cure.Http.OAuth.BearerToken;

namespace Http.OAuth.Test
{
    public class BearerTokenFixture
    {
        [Fact]
        public void Should_GetAccessToken_AuthorizationHeader()
        {
            // Arrange
            const string accessToken = "access_token";
            var accessMethod = new AuthorizationHeaderAccessMethod();
            var request = new HttpRequestMessage();

            // Act
            accessMethod.Set(request, accessToken);

            // Assert
            Assert.Equal(accessToken, accessMethod.Get(request));
        }

        [Fact]
        public void Should_GetAccessToken_QueryString()
        {
            // Arrange
            const string accessToken = "access_token";
            var accessMethod = new QueryStringAccessMethod();
            var request = new HttpRequestMessage { RequestUri = new Uri("https://host.com/") };

            // Act
            accessMethod.Set(request, accessToken);

            // Assert
            Assert.Equal(accessToken, accessMethod.Get(request));
        }

        [Fact]
        public void Should_SetAccessToken_AuthorizationHeader()
        {
            // Arrange
            const string accessToken = "access_token";
            var accessMethod = new AuthorizationHeaderAccessMethod();
            var request = new HttpRequestMessage();

            // Act
            accessMethod.Set(request, accessToken);

            // Assert
            Assert.Equal(AuthorizationHeaderAccessMethod.Scheme, request.Headers.Authorization.Scheme);
            Assert.Equal(accessToken, request.Headers.Authorization.Parameter);
        }

        [Fact]
        public void Should_SetAccessToken_QueryString_Empty()
        {
            // Arrange
            const string accessToken = "access_token";
            var accessMethod = new QueryStringAccessMethod();
            var request = new HttpRequestMessage { RequestUri = new Uri("https://host.com/") };

            // Act
            accessMethod.Set(request, accessToken);

            // Assert
            var query = request.RequestUri.Query;
            Assert.Contains(QueryStringAccessMethod.ParameterKey, query);
            Assert.Contains(accessToken, query);
        }

        [Fact]
        public void Should_SetAccessToken_QueryString_NotEmpty()
        {
            // Arrange
            const string accessToken = "access_token";
            var accessMethod = new QueryStringAccessMethod();
            var request = new HttpRequestMessage { RequestUri = new Uri("https://host.com/?param=value") };

            // Act
            accessMethod.Set(request, accessToken);

            // Assert
            var query = request.RequestUri.Query;
            // Is there a better way checking that the query string is valid?
            Assert.Equal(2, HttpUtility.ParseQueryString(query).Count);
            Assert.Contains(QueryStringAccessMethod.ParameterKey, query);
            Assert.Contains(accessToken, query);
        }
    }
}