using Entities.Model;
using Entities.Model.TransportStates;
using Entities.Model.Workers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer
{
    public class Database : DbContext
    {
        public Database() : base() { }

        public DbSet<WarehouseClerk> WarehouseClerks { get; set; }

        public DbSet<ShipCaptain> ShipCaptains { get; set; }

        public DbSet<Crew> Crew { get; set; }

        public DbSet<ShipPort> ShipPorts { get; set; }

        public DbSet<Ship> Ships { get; set; }

        public DbSet<Transport> Transports { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ShipPort;Integrated Security=True");
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WarehouseClerk>()
                .Property(c => c.ClerkRole)
                .HasConversion<string>();

            modelBuilder.Entity<Crew>()
                .Property(c => c.Role)
                .HasConversion<string>();

            var converter = new ValueConverter<TransportState, string>(
                x => ConvertStateToString(x),
                x => ConvertStringToTransportState(x)
                );

            modelBuilder.Entity<Transport>()
                .Property(c => c.TransportState)
                .HasConversion<string>(converter);

            modelBuilder.Entity<Worker>()
                .HasDiscriminator<string>("worker_type")
                .HasValue<Crew>("Crew")
                .HasValue<ShipCaptain>("ShipCaptain")
                .HasValue<WarehouseClerk>("WarehouseClerk");

            base.OnModelCreating(modelBuilder);

        }

        private string ConvertStateToString(TransportState transportState)
        {
            return transportState is CanceledTransport? "CanceledTransport" 
                : transportState is CreateingTransport ? "CreateingTransport" 
                : transportState is CreateingTransport? "CreateingTransport"
                : string.Empty;
        }

        private TransportState ConvertStringToTransportState(string transportState)
        {
            //to do
             var x = CanceledTransport.Create(null);
            if (x.IsSuccess)
            {
                return x.Value;
            }
            return null;
;        }
    }
}
