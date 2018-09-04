using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire;
using Hangfire.Server;

namespace HangfireSample.App_Start
{
    public class JobConfig
    {
        public static void Register()
        {
            // fire and got:站台啟動後只會執行一次
            BackgroundJob.Enqueue(() => Console.WriteLine("Fire and forgot"));

            // delay: 設定時間間隔，每隔時間間隔執行一次
            BackgroundJob.Schedule(() => Console.WriteLine("Delayed"), TimeSpan.FromDays(1));

            // recurring: 設定cron敘述，重複執行多次
            RecurringJob.AddOrUpdate(() => Console.WriteLine("Daily Job"), Cron.Daily);

            // continue: 在某個job執行完後接續執行
            var id = BackgroundJob.Enqueue(() => Console.WriteLine("Hello, "));
            BackgroundJob.ContinueWith(id, () => Console.WriteLine("world!"));

            //BackgroundJob.Enqueue(() => TestFail());
        }

        //public static void TestFail()
        //{
        //    var i = 0;
        //    var result = 1 / i;
        //}
    }
}