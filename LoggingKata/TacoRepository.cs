using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LoggingKata
{
    class TacoRepository : ITacoRepository
    {
        private IDbConnection _connection;

        public TacoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<ITrackable> GetTacoBells()
        {
            //I'm sure there is an easier way to do this.
            //The commented out code below should work, cannot figure out exception

            //return _connection.Query<TacoBell, Point, TacoBell>("SELECT * FROM tacobells", (bell, p) => { bell.Location = p; return bell; }).ToList();

            //This will work and get correct answer
            var bells = _connection.Query<TacoBell>("SELECT name FROM tacobells").ToList();
            var points = _connection.Query<Point>("Select latitude, longitude FROM tacobells").ToList();

            for (int i = 0; i < bells.Count; i++)
            {
                bells[i].Location = points[i];
            }

            return bells;
        }
    }
}
