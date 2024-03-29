﻿using Core.Model;
using Core.Model.test;
using Core.Model.TransportStates;
using Core.Model.Users;
using Core.Model.Workers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace DataLayer
{
    public class Database : DbContext
    {
        private IConfiguration _configuration;
        private string connectionString = "";

        public Database(IConfiguration configuration) : base()
        {
            _configuration = configuration;
            connectionString = _configuration.GetSection("ConnectionStrings").GetSection("ShipPort").Value;
        }

        public DbSet<WarehouseClerk> WarehouseClerks { get; set; }

        public DbSet<ShipCaptain> ShipCaptains { get; set; }

        public DbSet<Crew> Crew { get; set; }

        public DbSet<ShipPort> ShipPorts { get; set; }

        public DbSet<Ship> Ships { get; set; }

        public DbSet<Transport> Transports { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<Cargo> Cargos { get; set; }

        public DbSet<User> Users { get; set; }

        //public DbSet<Child> Children { get; set; }

        //public DbSet<House> Houses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
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
                x => Transport.ConvertStringToTransportState(x)
                );

            modelBuilder.Entity<Transport>()
                .Property(c => c.TransportState)
                .HasConversion<string>(converter);

            modelBuilder.Entity<Worker>()
                .HasDiscriminator<string>("worker_type")
                .HasValue<Crew>("Crew")
                .HasValue<ShipCaptain>("ShipCaptain")
                .HasValue<WarehouseClerk>("WarehouseClerk");

            modelBuilder.Entity<Transport>()
                .HasOne(x => x.ShipPortFrom)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transport>()
                .HasOne(x => x.ShipPortTo)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transport>()
                .HasMany(x => x.ShipCaptains)
                .WithMany(x => x.Transports);

            modelBuilder.Entity<Transport>()
                .HasOne(x => x.CurrentShipCaptain)
                .WithMany();

            modelBuilder.Entity<Transport>()
                .HasOne(x=>x.Ship)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Transport>()
            //    .HasMany(x=>x.Cargos)
            //    .WithOne()
            //    .HasForeignKey(x=>x.TransportId);

            //modelBuilder.Entity<House>()
            //    .HasMany(x => x.Children)
            //    .WithMany(x => x.Houses);

            base.OnModelCreating(modelBuilder);
        }

        private string ConvertStateToString(TransportState transportState)
        {
            // ako ne vraca dobro overajduj to string za svaki
            return transportState.ToString();
        }

        //private TransportState ConvertStringToTransportState(string transportState)
        //{
        //    var canceledTransport = CanceledTransport.Create();
        //    var transporting = Transporting.Create();
        //    var createingTransport = CreatingTransport.Create();
        //    if (canceledTransport.IsFailure || transporting.IsFailure || createingTransport.IsFailure)
        //        return null;

        //    return canceledTransport.Value.ToString().Equals(transportState) ? canceledTransport.Value :
        //        transporting.Value.ToString().Equals(transportState) ? transporting.Value :
        //        createingTransport.Value.ToString().Equals(transportState) ? createingTransport.Value: 
        //        null;
        //}
    }
}
