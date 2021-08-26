using System;
using System.Collections.Generic;

namespace Eol.Cig.Etl.Salesforce.Extract
{
    public static class Helpers
    {
        /// <summary>
        /// Salesforce requires specific format for datetime ""yyyy-MM-ddThh:mm:ssZ"
        /// </summary>
        /// <param name="value">Value to format</param>
        /// <returns></returns>
        public static string ToSalesforceDateTimeFormat(this DateTime value)
        {
            //TOOD: Check if this makes sense
            return value.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        /// <summary>
        /// Use bounds like x -gt Tuple.First and x -lq Tuple.Second
        /// </summary>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static IList<Tuple<DateTime, DateTime>> GetBounds(DateTime starTime, DateTime endTime, int boundSizeInDays)
        {
            var result = new List<Tuple<DateTime, DateTime>>();
            var currentLowerBound = starTime;
            var currentUpperBound = currentLowerBound.AddDays(boundSizeInDays).Date;

            while (currentUpperBound < endTime)
            {
                result.Add(new Tuple<DateTime, DateTime>(currentLowerBound, currentUpperBound));
                currentLowerBound = currentUpperBound;
                currentUpperBound = currentLowerBound.AddDays(boundSizeInDays).Date;
            }

            result.Add(new Tuple<DateTime, DateTime>(currentLowerBound, endTime));

            return result;
        }
    }
}
