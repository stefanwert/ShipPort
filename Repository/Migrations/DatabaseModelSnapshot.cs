﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(Database))]
    partial class DatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Model.Cargo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Flammable")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("TransportId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TransportId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Cargos");
                });

            modelBuilder.Entity("Core.Model.Ship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<Guid>("ShipPortId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ShipPortId");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("Core.Model.ShipPort", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeOfCreation")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ShipPorts");
                });

            modelBuilder.Entity("Core.Model.Transport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CurrentShipCaptainId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShipId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShipPortFromId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShipPortToId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TimeFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeTo")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransportState")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentShipCaptainId");

                    b.HasIndex("ShipId");

                    b.HasIndex("ShipPortFromId");

                    b.HasIndex("ShipPortToId");

                    b.ToTable("Transports");
                });

            modelBuilder.Entity("Core.Model.Warehouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CargoCapacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ShipPortId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("StoreFlammableCargo")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ShipPortId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("Core.Model.Workers.Worker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Salary")
                        .HasColumnType("bigint");

                    b.Property<Guid>("ShipPortId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearsOfWorking")
                        .HasColumnType("int");

                    b.Property<string>("worker_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ShipPortId");

                    b.ToTable("Worker");

                    b.HasDiscriminator<string>("worker_type").HasValue("Worker");
                });

            modelBuilder.Entity("CrewTransport", b =>
                {
                    b.Property<Guid>("CrewId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TransportsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CrewId", "TransportsId");

                    b.HasIndex("TransportsId");

                    b.ToTable("CrewTransport");
                });

            modelBuilder.Entity("ShipCaptainTransport", b =>
                {
                    b.Property<Guid>("ShipCaptainsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TransportsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ShipCaptainsId", "TransportsId");

                    b.HasIndex("TransportsId");

                    b.ToTable("ShipCaptainTransport");
                });

            modelBuilder.Entity("Core.Model.Workers.Crew", b =>
                {
                    b.HasBaseType("Core.Model.Workers.Worker");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SailingHoursTotal")
                        .HasColumnType("int")
                        .HasColumnName("Crew_SailingHoursTotal");

                    b.HasDiscriminator().HasValue("Crew");
                });

            modelBuilder.Entity("Core.Model.Workers.ShipCaptain", b =>
                {
                    b.HasBaseType("Core.Model.Workers.Worker");

                    b.Property<double>("SailingHoursAsCaptain")
                        .HasColumnType("float");

                    b.Property<double>("SailingHoursTotal")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue("ShipCaptain");
                });

            modelBuilder.Entity("Core.Model.Workers.WarehouseClerk", b =>
                {
                    b.HasBaseType("Core.Model.Workers.Worker");

                    b.Property<string>("ClerkRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasDiscriminator().HasValue("WarehouseClerk");
                });

            modelBuilder.Entity("Core.Model.Cargo", b =>
                {
                    b.HasOne("Core.Model.Transport", null)
                        .WithMany("Cargos")
                        .HasForeignKey("TransportId");

                    b.HasOne("Core.Model.Warehouse", null)
                        .WithMany("Cargos")
                        .HasForeignKey("WarehouseId");
                });

            modelBuilder.Entity("Core.Model.Ship", b =>
                {
                    b.HasOne("Core.Model.ShipPort", null)
                        .WithMany("Ships")
                        .HasForeignKey("ShipPortId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Model.Transport", b =>
                {
                    b.HasOne("Core.Model.Workers.ShipCaptain", "CurrentShipCaptain")
                        .WithMany()
                        .HasForeignKey("CurrentShipCaptainId");

                    b.HasOne("Core.Model.Ship", "Ship")
                        .WithMany()
                        .HasForeignKey("ShipId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Core.Model.ShipPort", "ShipPortFrom")
                        .WithMany()
                        .HasForeignKey("ShipPortFromId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Core.Model.ShipPort", "ShipPortTo")
                        .WithMany()
                        .HasForeignKey("ShipPortToId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CurrentShipCaptain");

                    b.Navigation("Ship");

                    b.Navigation("ShipPortFrom");

                    b.Navigation("ShipPortTo");
                });

            modelBuilder.Entity("Core.Model.Warehouse", b =>
                {
                    b.HasOne("Core.Model.ShipPort", null)
                        .WithMany("Warehouses")
                        .HasForeignKey("ShipPortId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Model.Workers.Worker", b =>
                {
                    b.HasOne("Core.Model.ShipPort", null)
                        .WithMany("Workers")
                        .HasForeignKey("ShipPortId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CrewTransport", b =>
                {
                    b.HasOne("Core.Model.Workers.Crew", null)
                        .WithMany()
                        .HasForeignKey("CrewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Model.Transport", null)
                        .WithMany()
                        .HasForeignKey("TransportsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShipCaptainTransport", b =>
                {
                    b.HasOne("Core.Model.Workers.ShipCaptain", null)
                        .WithMany()
                        .HasForeignKey("ShipCaptainsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Model.Transport", null)
                        .WithMany()
                        .HasForeignKey("TransportsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Model.ShipPort", b =>
                {
                    b.Navigation("Ships");

                    b.Navigation("Warehouses");

                    b.Navigation("Workers");
                });

            modelBuilder.Entity("Core.Model.Transport", b =>
                {
                    b.Navigation("Cargos");
                });

            modelBuilder.Entity("Core.Model.Warehouse", b =>
                {
                    b.Navigation("Cargos");
                });
#pragma warning restore 612, 618
        }
    }
}
