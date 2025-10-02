using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thoughtful.Dal
{
    public class RandomNumberGenerator
    {
        // Instantiate random number generator.  
        private static readonly Random _random = new Random();

        // Generates a random number within a range.      
        public static int Generate(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
