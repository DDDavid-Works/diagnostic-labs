using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DiagnosticLabsDAL.DatabaseContext
{
    public class DatabaseContext : DbContext
    {
        static readonly string _connectionString = ConfigurationManager.ConnectionStrings["DiagnosticLabsDBConnectionString"].ConnectionString;

        public DatabaseContext() : base() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleType> ModuleTypes { get; set; }
        public DbSet<SingleLineEntry> SingleLineEntries { get; set; }
        public DbSet<MultiLineEntry> MultiLineEntries{ get; set; }
        public DbSet<DefaultValue> DefaultValues { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientRegistration> PatientRegistrations { get; set; }
        public DbSet<PatientRegistrationService> PatientRegistrationServices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemLocation> ItemLocations { get; set; }
        public DbSet<ItemQuantity> ItemQuantities { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageService> PackageServices { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanySetup> CompanySetups { get; set; }
        public DbSet<StoolFecalysis> StoolFecalyses { get; set; }
        public DbSet<APE> APEs { get; set; }

        //VIEWS
        public DbSet<PatientCompany> PatientCompanies { get; set; }
        public DbSet<PatientRegistrationDetail> PatientRegistrationDetails { get; set; }
        public DbSet<PatientRegistrationBatch> PatientRegistrationBatches { get; set; }
        public DbSet<PatientRegistrationPayment> PatientRegistrationPayments { get; set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<LatestCodeNumber> LatestCodeNumbers { get; set; }
        public DbSet<LabResult> LabResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
