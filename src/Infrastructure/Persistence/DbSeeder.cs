using System.IO.Compression;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using CsvHelper;
using Infrastructure.Persistance;
using System.Globalization;
using System.Text.Json;


namespace Infrastructure.Persistence
{
    public class DbSeeder
    {
        public DbSeeder() { }


        public async Task SeedAsync(DatabaseContext context)
        {
            if (!context.Cars.Any())
            {
                var cars = await GetCarsFromKaggle();

                context.Cars.AddRange(cars);

                await context.SaveChangesAsync();
            }
        }

        private static string? GetValueFromRecord(dynamic record, string[] header, string columnName)
        {
            var matchingColumn = header
                .FirstOrDefault(h => h
                    .Contains(columnName, StringComparison.OrdinalIgnoreCase));
            if (matchingColumn != null)
            {
                var dictRecord = (IDictionary<string, object>)record;
                string? value = dictRecord[matchingColumn]?.ToString();

                if (!string.IsNullOrEmpty(value) && value.Length <= 100)
                {
                    return value;
                }

                return value?.Substring(0, 150) ?? null;
            }

            return null;
        }

        private async Task<List<Car>> GetCarsFromKaggle()
        {
            var cars = new List<Car>();

            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datasets");


            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            else
            {
                Directory.Delete(folderPath, true);
                Directory.CreateDirectory(folderPath);
            }

            string url =
                 "https://www.kaggle.com/api/v1/datasets/download/joebeachcapital/us-car-models";


            using HttpClient httpClient = new();


            try
            {
                string path = Path.Combine(folderPath, $"dataset.zip");

                byte[] fileBytes = await httpClient.GetByteArrayAsync(url);

                await File.WriteAllBytesAsync(path, fileBytes);

                string extractFolder = path.Replace(".zip", "");

                ZipFile.ExtractToDirectory(path, extractFolder);

                extractFolder = Path.Combine(extractFolder, "us-car-models-data-master");

                List<string> files = Directory
                    .GetFiles(extractFolder, "*.csv")
                    .ToList();


                files.ForEach(f =>
                {
                    cars = cars.Concat(ParseCSV(f))
                   .ToList();
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading file: {ex.Message}");
            }


            return cars;
        }

        private List<Car> ParseCSV(string filePath)
        {
            var cars = new List<Car>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                var header = csv.HeaderRecord ?? [];

                var records = csv.GetRecords<dynamic>().ToList();

                foreach (var record in records)
                {
                    List<Car> newCars = [];
                    var dictionary = (IDictionary<string, object>)record;

                    if (dictionary.TryGetValue("body_styles", out var bodyStylesObj)
                        && bodyStylesObj is string bodyStyles)
                    {
                        List<string> castStyles = JsonSerializer
                            .Deserialize<List<string>>(bodyStyles) ?? [];

                        // Add all variants
                        if (castStyles.Count != 0)
                        {
                            cars.AddRange(
                                castStyles.Select(s =>
                                {
                                    var newCar = new Car()
                                    {
                                        Manufacturer = GetValueFromRecord(record, header, "make"),
                                        Model = GetValueFromRecord(record, header, "model"),
                                        Type = s.ToString() ?? string.Empty
                                    };

                                    if (int.TryParse(GetValueFromRecord(record, header, "year"), out int year))
                                    {
                                        newCar.Year = new DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                                    }

                                    return newCar;
                                }));
                        }
                        else// Add one without variant
                        {
                            var newCar = new Car()
                            {
                                Manufacturer = GetValueFromRecord(record, header, "make"),
                                Model = GetValueFromRecord(record, header, "model"),
                            };

                            if (int.TryParse(GetValueFromRecord(record, header, "year"), out int year))
                            {
                                newCar.Year = new DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                            }

                            cars.Add(newCar);
                        }
                    }
                }
            }

            return cars;
        }
    }
}
