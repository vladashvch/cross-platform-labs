using Microsoft.EntityFrameworkCore;
using Entities;
using DotNetEnv;
using Bogus;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Lab6.Models;

namespace DataSeederLab6
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<PatientMedication> PatientMedications { get; set; }
        public DbSet<RefMedicationType> MedicationTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Env.Load();

            var dbHost = "localhost";
            var dbPort = "1430";
            var dbUser = "sa";
            var dbPassword = "Password_123!";
            var dbName = "Lab8Db";

            var connectionString = $"Server={dbHost},{dbPort};Database={dbName};User Id={dbUser};Password={dbPassword};TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);

            optionsBuilder
               .UseSqlServer(connectionString)
               .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning))
               .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

            optionsBuilder.LogTo(Console.WriteLine);
        }
    }

    public class DatabaseSeeder
    {
        private readonly ApplicationDbContext _context;

        public DatabaseSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAllData(int patientsCount = 1000, int medicationsCount = 100, int patientMedicationsCount = 15000)
        {
            try
            {
                Console.WriteLine("Testing database connection...");
                await _context.Database.CanConnectAsync();
                Console.WriteLine("Database connection successful!");

                //await SeedPatients(patientsCount);
                //await SeedMedications(medicationsCount);
                //await SeedPatientMedications(patientMedicationsCount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
        }

        private async Task SeedPatients(int count)
        {
            var faker = new Faker<Patient>()
                .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                .RuleFor(p => p.LastName, f => f.Name.LastName())
                .RuleFor(p => p.MiddleName, f => f.Random.Bool(0.7f) ? f.Name.FirstName() : null)
                .RuleFor(p => p.DateOfBirth, f => f.Date.Between(DateTime.Now.AddYears(-90), DateTime.Now.AddYears(-18)))
                .RuleFor(p => p.Gender, f => f.Random.Bool() ? "M" : "F")
                .RuleFor(p => p.Address, f => f.Address.FullAddress())
                .RuleFor(p => p.OtherDetails, f => f.Random.Bool(0.3f) ? f.Lorem.Sentence() : null);

            var patients = faker.Generate(count);
            var batches = count / 500 + (count % 500 > 0 ? 1 : 0);
            for (int i = 0; i < batches; i++)
            {
                var batchPatients = patients.Skip(i * 500).Take(500).ToList();
                await _context.Patients.AddRangeAsync(batchPatients);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Seeded patients batch {i + 1}/{batches}");
            }

            Console.WriteLine($"Seeded {count} patients");
        }

        private async Task SeedPatientMedications(int count)
        {
            var batchSize = 500;
            var patients = await _context.Patients.Select(p => p.PatientId).ToListAsync();
            var medications = await _context.Medications.Select(m => m.MedicationId).ToListAsync();

            var batches = count / batchSize + (count % batchSize > 0 ? 1 : 0);
            for (int i = 0; i < batches; i++)
            {
                var currentBatchSize = Math.Min(batchSize, count - (i * batchSize));
                var faker = new Faker<PatientMedication>()
                    .RuleFor(pm => pm.PatientId, f => f.PickRandom(patients))
                    .RuleFor(pm => pm.MedicationId, f => f.PickRandom(medications))
                    .RuleFor(pm => pm.DateTimeAdministered, f => f.Date.Recent(90))
                    .RuleFor(pm => pm.Dosage, f => f.Random.Decimal(0.5m, 10m))
                    .RuleFor(pm => pm.Comments, f => f.Random.Bool(0.7f) ? f.Lorem.Sentence() : null)
                    .RuleFor(pm => pm.OtherDetails, f => f.Random.Bool(0.3f) ? f.Lorem.Sentence() : null);

                var patientMedications = faker.Generate(currentBatchSize);
                await _context.PatientMedications.AddRangeAsync(patientMedications);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Seeded patient medications batch {i + 1}/{batches}");
            }

            Console.WriteLine($"Seeded {count} patient medications");
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting the seeding process...");

                using var context = new ApplicationDbContext();

                if (await context.Database.CanConnectAsync())
                {
                    Console.WriteLine("Successfully connected to the database!");

                    await context.Database.MigrateAsync();
                    var seeder = new DatabaseSeeder(context);

                    Console.WriteLine("Starting database seeding...");
                    await seeder.SeedAllData(
                        patientsCount: 1000,
                        medicationsCount: 100,
                        patientMedicationsCount: 15000
                    );
                    Console.WriteLine("Database seeding completed successfully!");
                }
                else
                {
                    Console.WriteLine("Could not connect to the database. Please check your connection string.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
        }
    }
}