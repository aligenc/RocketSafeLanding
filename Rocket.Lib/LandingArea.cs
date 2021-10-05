using System;

namespace Rocket.Lib
{
    public class LandingArea
    {
        private const int AreaSize = 100;
        private const int Seperation = 1;

        private int[,] _landingArea;
        private int _lastCheckedAreaX = -1, _lastCheckedAreaY = -1;

        public void CreateAreaAndPlatform(int platformSize, int platformStartX, int platformStartY)
        {
            if (platformSize < 0)
                throw new InvalidOperationException("The platform size can not be under 0.");

            if (platformSize > AreaSize || platformSize > AreaSize)
                throw new InvalidOperationException("The safe area size can not be greater than platform size.");

            if (platformStartX < 0 || platformStartY < 0)
                throw new InvalidOperationException("The safe area start position of X and Y can not be below 0.");

            if (platformStartX + platformSize > AreaSize || platformStartY + platformSize > AreaSize)
                throw new InvalidOperationException("The safe area is out of the platform. Check X and Y coordinates of the safe area starting positions.");

            _landingArea = new int[AreaSize, AreaSize];

            // For the platform coordinates set 1, out of the platform set 0
            for (int i = platformStartX; i < platformStartX + platformSize; i++)
            {
                for (int j = platformStartY; j < platformStartY + platformSize; j++)
                {
                    _landingArea[i, j] = 1;
                }
            }
        }

        public string CheckLandingArea(Rocket rocket)
        {
            if (rocket.LandingPositionX < 0 || rocket.LandingPositionY < 0
                || rocket.LandingPositionX > AreaSize || rocket.LandingPositionY > AreaSize)
                throw new Exception($"Rocket coordinates are not valid! Values must be in 0x0 to {AreaSize}x{AreaSize}");

            var result = "Ok for landing";

            if (!IsInPlatformArea(rocket))
                result = "Out of platform";
            else if (IsInLastCheckedArea(rocket))
                result = "Clash";

            _lastCheckedAreaX = rocket.LandingPositionX;
            _lastCheckedAreaY = rocket.LandingPositionY;

            Console.WriteLine($"{rocket.Name} > {rocket.LandingPositionX}x{rocket.LandingPositionY} : {result}");
            return result;

        }
        private bool IsInPlatformArea(Rocket rocket)
        {
            return Convert.ToBoolean(_landingArea[rocket.LandingPositionX, rocket.LandingPositionY]);
        }

        private bool IsInLastCheckedArea(Rocket rocket)
        {
            //If not checked yet and this is the first checked, no need to run codes, return.
            if (_lastCheckedAreaX == -1)
                return false;

            int minX = _lastCheckedAreaX - Seperation;
            int maxX = _lastCheckedAreaX + Seperation;
            int minY = _lastCheckedAreaY - Seperation;
            int maxY = _lastCheckedAreaY + Seperation;

            if (rocket.LandingPositionX >= minX && rocket.LandingPositionX <= maxX
                && rocket.LandingPositionY >= minY && rocket.LandingPositionY <= maxY)
                return true;

            return false;
        }
    }
}
