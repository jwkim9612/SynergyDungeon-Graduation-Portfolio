using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Service
{
    public class RandomService
    {
        //public const int NUM_Of_FRACTION = 10000;
        public static System.Random random = new System.Random();

        public static float GetRandom()
        {
            float randomNum = (float)random.NextDouble();

            return randomNum;
        }

        /// <summary>
        /// 0부터 100까지의 수 중 랜덤으로 하나 리턴
        /// </summary>
        /// <returns></returns>
        public static long GetRandomLong()
        {
            return (long)(GetRandom() * 100);
        }

        public static int RandRange(int min, int max)
        {
            return random.Next(min, max);
        }


    }
}
