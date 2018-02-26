using System.Threading.Tasks;
using Devpro.VstsClient.VstsApiLib;
using Moq;
using Xunit;

namespace Devpro.VstsClient.VstsApiLibUnitTests
{
    public class BuildServiceTest
    {
        [Fact]
        public async Task ReturnFalseGivenValueOf1()
        {
            var service = new Mock<BuildService>();
            service
                .Setup(x => x.GetAsync("toto1", "toto2", "toto3", "build/builds"))
                .ReturnsAsync(new VstsApiLib.Dto.BuildFindResultDto());
            var titi = await service.Object.GetBuilds("toto1", "toto2", "toto3");
            Assert.True(titi is VstsApiLib.Dto.BuildFindResultDto);
            //TODO: do more tests
        }
    }
}
