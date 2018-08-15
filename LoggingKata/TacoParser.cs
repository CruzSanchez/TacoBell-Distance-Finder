namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
			var cells = line.Split(',');

			if(cells.Length < 3)
			{
				return null;
			}

			string lat = cells[0];
			string lon = cells[1];
			string city = cells[2];

			double _lat = double.Parse(lat);
			double _lon = double.Parse(lon);

			TacoBell bell1 = new TacoBell();
			bell1.Name = city;
			bell1.Location = new Point { Latitude = _lat, Longitude = _lon };



			return bell1;


			
			// TODO Implement
        }
    }
}