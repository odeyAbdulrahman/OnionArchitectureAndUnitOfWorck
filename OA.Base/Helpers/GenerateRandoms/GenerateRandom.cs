using System;
using System.Linq;

namespace OA.Base.Helpers.GenerateRandoms
{
    class GenerateRandom : IGenerateRandom
    {
        readonly Random Random = new Random();

        public string RandomNumDigit(int num)
        {
            const string ch = "0123456789";
            return new string(Enumerable.Repeat(ch, num).Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public string Random4Digit()
        {
            int _min = 1000;
            int _max = 9999;
            return Random.Next(_min, _max).ToString();
        }
    }
}
