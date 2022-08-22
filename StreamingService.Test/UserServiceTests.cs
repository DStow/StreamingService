using StreamingService.Services;
using System;
using Xunit;

namespace StreamingService.Test
{
    /// <summary>
    /// Test suite for <see cref="UserServiceOld"/>.
    /// </summary>
    public class UserServiceTests
    {
        public readonly UserServiceOld SUT = new();

        [Fact]
        public void SubscribeFreemium()
        {
            var freemiumId = Guid.Parse("e0cb3b2f-1297-4cc1-8a87-a659b1698fc2");

            var result = SUT.Subscribe("email", freemiumId);

            Assert.True(result);
        }
    }
}
