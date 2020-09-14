using Server_API.DB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server_API.Utility
{
    public class DBTimeWatch : LazySingleton<DBTimeWatch>
    {
        private DBConfig _dbConfig;
        private bool processingSync;
        private Stopwatch stopwatch;
        private DateTime lastSyncDBTime;
        private int lastSyncDBUnixTimestamp;
        private static double timeGapToleranceInMilliseconds = 500;

        public long DBTimestampTicks => lastSyncDBTime.Ticks + stopwatch.Elapsed.Ticks;
        public int DBUnixTimestamp => lastSyncDBUnixTimestamp + (int)stopwatch.ElapsedMilliseconds / 1000;
        public DateTime DBTime
        {
            get
            {
                if (processingSync || stopwatch?.Elapsed.Hours == 0) { return lastSyncDBTime + stopwatch?.Elapsed ?? lastSyncDBTime; }

                Console.WriteLine($"{DateTime.Now.Ticks} : stopwatch runs {stopwatch.Elapsed.TotalMilliseconds} ms. try to sync time");
                Task.Run(() => SyncDBTime());

                return lastSyncDBTime + stopwatch?.Elapsed ?? lastSyncDBTime;
            }
        }

        public void Initialize(DBConfig dbConfig)
        {
            _dbConfig = dbConfig;

            processingSync = false;
            lastSyncDBTime = DateTime.MinValue;
            lastSyncDBUnixTimestamp = 0;
            stopwatch = null;

            SyncDBTime();
        }

        public void SyncDBTime()
        {
            if (processingSync) { return; }

            processingSync = true;

            var currentDBTime = DBProcedure.GetServerTimestamp(_dbConfig);
            var timeGapToFollow = currentDBTime - DBTime;
            long absoluteTimeGapInMilliseconds = Convert.ToInt64(Math.Abs(timeGapToFollow.TotalMilliseconds));

            if (absoluteTimeGapInMilliseconds > timeGapToleranceInMilliseconds)
            {
                if (timeGapToFollow.TotalMilliseconds > 0)
                {
                    Console.WriteLine($"{DateTime.Now.Ticks} : local is {DBTime.Ticks} db is {currentDBTime.Ticks}. just re sync");
                    ResetFromDBTime(currentDBTime);
                }
                else
                {
                    Console.WriteLine($"{DateTime.Now.Ticks} : old syncedDBTime is {DBTime.Ticks} / currentDBTime is {currentDBTime.Ticks}. stasis needed {absoluteTimeGapInMilliseconds} ms.");
                    stopwatch.Stop();
                    Thread.Sleep(Convert.ToInt32(absoluteTimeGapInMilliseconds));
                    ResetFromDBTime(currentDBTime);
                    Console.WriteLine($"{DateTime.Now.Ticks} : sync after stasis is done");
                }
            }

            processingSync = false;
        }

        private void ResetFromDBTime(DateTime dbTime)
        {
            lastSyncDBTime = dbTime;
            lastSyncDBUnixTimestamp = (int)lastSyncDBTime.Subtract(DateTime.UnixEpoch).TotalSeconds;
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }
    }
}
