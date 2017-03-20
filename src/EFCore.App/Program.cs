using EFCore.App.Config;
using EFCore.Lib.Data;
using EFCore.Lib.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace EFCore.App
{
    public class Program
    {
        private static Currency[] _currencyData = new[]
        {
            new Currency { IsoCode = "USD", Name = "US Dolar", Symbol = "US$" },
            new Currency { IsoCode = "EUR", Name = "Euro", Symbol = "€" },
            new Currency { IsoCode = "CHF", Name = "Swiss Franc", Symbol = "Fr." },
        };

        public static AppOptions AppOptions { get; set; }

        public static IConfigurationRoot Configuration { get; set; }

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("EF Core Lib\n");

            ReadConfiguration();

            InitDb();

            PrintDb();
        }

        private static void InitDb()
        {
            using (var db = new CommonDbContext(AppOptions.ConnectionStrings))
            {
                Console.WriteLine("Creating database...\n");

                db.Database.EnsureCreated();

                Console.WriteLine("Seeding database...\n");

                LoadInitalData(db);
            }
        }

        private static void LoadInitalData(CommonDbContext db)
        {
            foreach (var item in _currencyData)
            {
                Currency currency = db.Currencies.FirstOrDefault(c => c.Symbol == item.Symbol);

                if (currency == null)
                {
                    db.Currencies.Add(item);
                }
                else
                {
                    currency.Name = item.Name;
                    currency.Symbol = item.Symbol;
                }
            }

            db.SaveChanges();
        }

        private static void PrintDb()
        {
            using (var db = new CommonDbContext(AppOptions.ConnectionStrings))
            {
                Console.WriteLine("Reading database...\n");

                Console.WriteLine("Currencies");
                Console.WriteLine("----------");

                int symbolLength = _currencyData.Select(c => c.Symbol.Length).Max();
                int nameLength = _currencyData.Select(c => c.Name.Length).Max();

                foreach (var item in db.Currencies)
                {
                    Console.WriteLine($"| {item.IsoCode} | {item.Symbol.PadRight(symbolLength)} | {item.Name.PadRight(nameLength)} |");
                }

                Console.WriteLine();
            }
        }

        private static void ReadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            // Reads appsettings.json into a (strongly typed) class
            AppOptions = Configuration.Get<AppOptions>();

            Console.WriteLine("Configuration\n");
            Console.WriteLine($@"connectionString (defaultConnection) = ""{AppOptions.ConnectionStrings.DefaultConnection}""");
            Console.WriteLine();
        }
    }
}