using Common.Library.Model;
using Common.Library.Utils;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace Common.Library.Tests
{
    /// <summary>
    /// Library Unit Test
    /// </summary>
    public class UnitTests
    {
        [Fact]
        public void JwtGenerator_Success()
        {
            // Arrange
            JwtTokenGeneratorModel jwtTokenGeneratorModel = new()
            {
                Issuer = "test",
                Audiance = "Test",
                Key = "HowStrongThisKeyShouldbe",
                ExpireAfter = 5,
                Claims = new List<Claim>()
            };

            jwtTokenGeneratorModel.Claims.Add(new Claim(ClaimTypes.Name, "Raj"));

            // Act
            string token = JwtTokenGenerator.GenerateJwtToken(jwtTokenGeneratorModel);

            // Assert
            Assert.IsType<string>(token);

        }
    }
}
