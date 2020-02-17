using System.Collections.Generic;

namespace LoggingKata
{
    interface ITacoRepository
    {
        IEnumerable<ITrackable> GetTacoBells();
    }
}
