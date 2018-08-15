using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldDoSomething()
        {
            // Arrange


			//Act

			//Assert
        }

        [Theory]
        [InlineData("56.22222, -19.455, TacoBell Name")]
		[InlineData("0, 180.000, name")]
		[InlineData("0, -180.0000, TacoBell Name")]
		[InlineData("90.00000, 180.0000, TacoBell Name")]
		[InlineData("-90.0000, -180.0000, TacoBell Name")]
		[InlineData("0, 0, \"123\"")]
		[InlineData("0,0, Name/..." )]
		[InlineData("45, 45, Name")]
		[InlineData("45, 45, Name/...")]
		[InlineData("45, 45,")]
		[InlineData("45, 45")]
		[InlineData("45,45,   ")]
		[InlineData("50,50,Name, ect.")]
		public void ShouldParse(string str, double lat, double lon)
        {
			// Arrange
			TacoParser parser = new TacoParser();

			//Act
			var actual = parser.Parse(str);

			//Assert
			
			Assert.

		}

		[Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldFailParse(string str)
        {
			// Arrange
			TacoParser parser = new TacoParser();

			//Act
			var actual = parser.Parse(str);

			//Assert



		}
	}
}
