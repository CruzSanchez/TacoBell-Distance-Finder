using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

			logger.LogInfo($"Lines:");

			var parser = new TacoParser();

            var locations = lines.Select(parser.Parse);

			double distance = 0;
			double maxDistance = 0;
			string store1 = "";
			string store2 = "";
			
			foreach(var line in locations)
			{
				GeoCoordinate corA = new GeoCoordinate();

				corA.Latitude = line.Location.Latitude;
				corA.Longitude = line.Location.Longitude;

				foreach (var line2 in locations)
				{
					GeoCoordinate corB = new GeoCoordinate();

					corB.Latitude = line2.Location.Latitude;
					corB.Longitude = line2.Location.Longitude;
					distance = corA.GetDistanceTo(corB);

					if (maxDistance < distance)
					{
						store1 = line.Name;
						store2 = line2.Name;
						
						maxDistance = distance;
					}

				}

			}

			Console.WriteLine($"{store1}, {store2}, {maxDistance}");


			Console.ReadLine();



		}
	}
}