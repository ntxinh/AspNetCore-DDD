using System;
using System.Configuration;
using System.Linq;
using System.Reflection;

using DbUp;

namespace DDD.CLI.Migration
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString =
                args.FirstOrDefault()
                ?? ConfigurationManager.ConnectionStrings["PublishToTargetDB"].ConnectionString;

            if (string.IsNullOrEmpty(connectionString)) return;

            // If you want your application to create the database for you
            // EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly
                    (
                        Assembly.GetExecutingAssembly(),
                        (string Script) => Script.StartsWith("DDD.CLI.Migration.Scripts.")
                    )
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
        #if DEBUG
                Console.ReadLine();
        #endif
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return;
        }
    }
}
