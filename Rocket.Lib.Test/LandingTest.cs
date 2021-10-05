using Xunit;

namespace Rocket.Lib.Test
{
    public class LandingTest
    {
        [Theory]
        [InlineData(10, 5, 5, 6, 6)]
        [InlineData(10, 60, 5, 65, 14)]
        [InlineData(10, 60, 5, 69, 14)]
        [InlineData(20, 40, 5, 48, 24)]
        [InlineData(10, 0, 0, 0, 0)]
        public void RocketLib_WhenOkForLanding(int platformSize, int platformStartX, int platformStartY, int landX, int landY)
        {
            // Arrange
            var landingArea = new LandingArea();
            landingArea.CreateAreaAndPlatform(platformSize, platformStartX, platformStartY);
            
            var rocket = new Rocket { 
                Name = "Falcon-9", 
                LandingPositionX= landX, 
                LandingPositionY= landY
            };
            
            //Action
            var result = landingArea.CheckLandingArea(rocket);

            //Assert
            Assert.Equal("Ok for landing", result);
        }

        [Theory]
        [InlineData(10, 5, 5, 16, 16)]
        [InlineData(10, 60, 5, 20, 14)]
        [InlineData(20, 40, 5, 12, 24)]
        [InlineData(10, 0, 0, 11, 0)]
        [InlineData(10, 60, 5, 60, 15)]
        [InlineData(10, 60, 5, 69, 15)]
        [InlineData(10, 60, 5, 70, 15)]
        public void RocketLib_WhenOutOfPlatform(int platformSize, int platformStartX, int platformStartY, int landX, int landY)
        {
            // Arrange
            var landingArea = new LandingArea();
            landingArea.CreateAreaAndPlatform(platformSize, platformStartX, platformStartY);

            var rocket = new Rocket
            {
                Name = "Lisa-1",
                LandingPositionX = landX,
                LandingPositionY = landY
            };

            //Action
            var result = landingArea.CheckLandingArea(rocket);

            //Assert
            Assert.Equal("Out of platform", result);
        }

        [Fact]
        public void RocketLib_WhenClashAndOkForMultiRockets()
        {
            // Arrange
            var landingArea = new LandingArea();
            int platformSize = 20;
            int platformStartX = 5;
            int platformStartY = 5;

            landingArea.CreateAreaAndPlatform(platformSize, platformStartX, platformStartY);

            var rocket1 = new Rocket
            {
                Name = "Magnus",
                LandingPositionX = 12,
                LandingPositionY = 7
            };

            var rocket2 = new Rocket
            {
                Name = "Nepo",
                LandingPositionX = 11,
                LandingPositionY = 6
            };

            var rocket3 = new Rocket
            {
                Name = "Fabi",
                LandingPositionX = 12,
                LandingPositionY = 5
            };

            var rocket4 = new Rocket
            {
                Name = "Levo",
                LandingPositionX = 24,
                LandingPositionY = 8
            };

            //Action
            var result1 = landingArea.CheckLandingArea(rocket1);
            var result2 = landingArea.CheckLandingArea(rocket2);
            var result3 = landingArea.CheckLandingArea(rocket3);
            var result4 = landingArea.CheckLandingArea(rocket4);

            //Assert
            Assert.Equal("Ok for landing", result1);
            Assert.Equal("Clash", result2);
            Assert.Equal("Clash", result3);
            Assert.Equal("Ok for landing", result4);
        }
    }
}
