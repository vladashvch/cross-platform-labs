using Lab6.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Lab6.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<RefMedicationType> MedicationTypes { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<PatientMedication> PatientMedications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DataRelationship(modelBuilder);
            DataSet(modelBuilder);

            modelBuilder.Entity<Medication>()
                .Property(m => m.MedicationUnitCost)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PatientMedication>()
                .Property(pm => pm.Dosage)
                .HasColumnType("decimal(18, 2)");
        }

        protected void DataRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medication>()
                .HasOne(m => m.MedicationTypeCode)
                .WithMany(t => t.Medications)
                .HasForeignKey(m => m.MedicationTypeCodeId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Staff)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.StaffId);

            modelBuilder.Entity<PatientMedication>()
                .HasOne(pm => pm.Patient)
                .WithMany(p => p.PatientMedications)
                .HasForeignKey(pm => pm.PatientId);

            modelBuilder.Entity<PatientMedication>()
                .HasOne(pm => pm.Medication)
                .WithMany(m => m.PatientMedications)
                .HasForeignKey(pm => pm.MedicationId);
        }

        protected void DataSet(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefMedicationType>().HasData(
                new RefMedicationType
                {
                    MedicationTypeCode = "OTC",
                    MedicationTypeName = "Безрецептурні",
                    MedicationTypeDescription = "Медикаменти, які можна купити без рецепта",
                    Example = "Парацетамол"
                },
                new RefMedicationType
                {
                    MedicationTypeCode = "RX",
                    MedicationTypeName = "Рецептурні",
                    MedicationTypeDescription = "Медикаменти, які потребують рецепту",
                    Example = "Амоксицилін"
                }
            );

            modelBuilder.Entity<Medication>().HasData(
                new Medication
                {
                    MedicationId = 1,
                    MedicationTypeCodeId = "OTC",
                    MedicationUnitCost = 4.99m,
                    MedicationName = "Парацетамол",
                    MedicationDescription = "Знеболювальний засіб",
                    OtherDetails = "Приймати з їжею"
                },
                new Medication
                {
                    MedicationId = 2,
                    MedicationTypeCodeId = "RX",
                    MedicationUnitCost = 15.99m,
                    MedicationName = "Амоксицилін",
                    MedicationDescription = "Антибіотик",
                    OtherDetails = "Приймати за рецептом лікаря"
                }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    PatientId = 1,
                    FirstName = "Іван",
                    MiddleName = "Петрович",
                    LastName = "Шевченко",
                    DateOfBirth = new DateTime(1985, 5, 10),
                    Gender = "M",
                    Address = "вул. Садова, 12",
                    OtherDetails = null
                },
                new Patient
                {
                    PatientId = 2,
                    FirstName = "Олена",
                    MiddleName = "Вікторівна",
                    LastName = "Коваленко",
                    DateOfBirth = new DateTime(1990, 8, 20),
                    Gender = "F",
                    Address = "вул. Лісова, 5",
                    OtherDetails = null
                }
            );

            modelBuilder.Entity<Staff>().HasData(
                new Staff
                {
                    StaffId = 1,
                    FirstName = "Андрій",
                    MiddleName = "Сергійович",
                    LastName = "Ткаченко",
                    DateOfBirth = new DateTime(1978, 3, 15),
                    Gender = "M",
                    Qualifications = "Лікар терапевт",
                    OtherDetails = null
                },
                new Staff
                {
                    StaffId = 2,
                    FirstName = "Марія",
                    MiddleName = "Олександрівна",
                    LastName = "Гончар",
                    DateOfBirth = new DateTime(1982, 12, 30),
                    Gender = "F",
                    Qualifications = "Лікар хірург",
                    OtherDetails = null
                }
            );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    AppointmentId = 1,
                    PatientId = 1,
                    StaffId = 1,
                    DateTimeOfAppointment = new DateTime(2024, 11, 1, 9, 0, 0),
                    OtherDetails = "Призначити аналізи"
                },
                new Appointment
                {
                    AppointmentId = 2,
                    PatientId = 2,
                    StaffId = 2,
                    DateTimeOfAppointment = new DateTime(2024, 11, 2, 10, 30, 0),
                    OtherDetails = "Огляд після операції"
                }
            );

            modelBuilder.Entity<PatientMedication>().HasData(
                new PatientMedication
                {
                    PatientMedicationId = 1,
                    MedicationId = 1,
                    PatientId = 1,
                    DateTimeAdministered = DateTime.Now,
                    Dosage = 500,
                    Comments = "Приймати 1 раз на день",
                    OtherDetails = null
                },
                new PatientMedication
                {
                    PatientMedicationId = 2,
                    MedicationId = 2,
                    PatientId = 2,
                    DateTimeAdministered = DateTime.Now,
                    Dosage = 250,
                    Comments = "Приймати 2 рази на день",
                    OtherDetails = null
                }
            );
        }
    }
}
