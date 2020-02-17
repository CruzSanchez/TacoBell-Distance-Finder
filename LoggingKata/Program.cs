using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using GeoCoordinatePortable;
using MySql.Data.MySqlClient;
using Dapper;

namespace LoggingKata
{
    public class Program
    {
        #region Setup
        static readonly ILog logger = new TacoLogger();
        public const string csvPath = "TacoBell-US-AL.csv";
        public const string connString = "Server=localhost;Database=tacobell;uid=root;Pwd=password";
        #endregion

        static void Main(string[] args)
		{
			logger.LogInfo("Log initialized");

			Console.Write("Read from where? CSV or Database: ");
			var typeOfFile = Console.ReadLine();

			typeOfFile = CheckUserInput(typeOfFile);

			var locations = SelectFileType(typeOfFile);

			double maxDistance = 0;
			ITrackable store1 = null;
			ITrackable store2 = null;

			foreach (var line1 in locations)
			{
				GeoCoordinate corA = new GeoCoordinate(line1.Location.Latitude, line1.Location.Longitude);

				foreach (var line2 in locations)
				{
					GeoCoordinate corB = new GeoCoordinate(line2.Location.Latitude, line2.Location.Longitude);

					double distance = corA.GetDistanceTo(corB);

					if (maxDistance < distance)
					{
						store1 = line1;
						store2 = line2;

						maxDistance = distance;
					}
				}
			}

			Console.WriteLine($"{store1.Name}, {store2.Name}, {maxDistance}");
			Console.ReadLine();
		}

		private static string CheckUserInput(string typeOfFile)
		{
			while (typeOfFile.ToLower() != "csv" && typeOfFile.ToLower() != "database")
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Input was: {typeOfFile} which was invalid!");
				Console.WriteLine();
				Console.ResetColor();
				Console.WriteLine("Please input csv or database");
				typeOfFile = Console.ReadLine();
			}

			return typeOfFile;
		}

		private static IEnumerable<ITrackable> SelectFileType(string typeOfFile)
		{
			switch (typeOfFile.ToLower())
			{
				case "csv":
					return ReadFromCSV();
				case "database":
					return ReadFromDatabase();
				default:
					return ReadFromCSV();
			}
		}

		private static IEnumerable<ITrackable> ReadFromCSV()
		{
			var lines = File.ReadAllLines(csvPath);

			logger.LogInfo($"Lines:");

			var parser = new TacoParser();

			var locations = lines.Select(parser.Parse);
			return locations;
		}

		private static IEnumerable<ITrackable> ReadFromDatabase()
		{
			IDbConnection connection = new MySqlConnection(connString);
			var repo = new TacoRepository(connection);

			return repo.GetTacoBells();
		}
	}
}