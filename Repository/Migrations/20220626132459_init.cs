using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShipPorts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipPorts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    ShipPortId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ships_ShipPorts_ShipPortId",
                        column: x => x.ShipPortId,
                        principalTable: "ShipPorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreFlammableCargo = table.Column<bool>(type: "bit", nullable: false),
                    CargoCapacity = table.Column<int>(type: "int", nullable: false),
                    ShipPortId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_ShipPorts_ShipPortId",
                        column: x => x.ShipPortId,
                        principalTable: "ShipPorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Worker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    YearsOfWorking = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<long>(type: "bigint", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    ShipPortId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    worker_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Crew_SailingHoursTotal = table.Column<int>(type: "int", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SailingHoursTotal = table.Column<double>(type: "float", nullable: true),
                    SailingHoursAsCaptain = table.Column<double>(type: "float", nullable: true),
                    ClerkRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Worker_ShipPorts_ShipPortId",
                        column: x => x.ShipPortId,
                        principalTable: "ShipPorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentShipCaptainId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShipPortFromId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShipPortToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransportState = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transports_ShipPorts_ShipPortFromId",
                        column: x => x.ShipPortFromId,
                        principalTable: "ShipPorts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transports_ShipPorts_ShipPortToId",
                        column: x => x.ShipPortToId,
                        principalTable: "ShipPorts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transports_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transports_Worker_CurrentShipCaptainId",
                        column: x => x.CurrentShipCaptainId,
                        principalTable: "Worker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Flammable = table.Column<bool>(type: "bit", nullable: false),
                    TransportId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargos_Transports_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cargos_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CrewTransport",
                columns: table => new
                {
                    CrewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransportsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewTransport", x => new { x.CrewId, x.TransportsId });
                    table.ForeignKey(
                        name: "FK_CrewTransport_Transports_TransportsId",
                        column: x => x.TransportsId,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrewTransport_Worker_CrewId",
                        column: x => x.CrewId,
                        principalTable: "Worker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipCaptainTransport",
                columns: table => new
                {
                    ShipCaptainsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransportsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipCaptainTransport", x => new { x.ShipCaptainsId, x.TransportsId });
                    table.ForeignKey(
                        name: "FK_ShipCaptainTransport_Transports_TransportsId",
                        column: x => x.TransportsId,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipCaptainTransport_Worker_ShipCaptainsId",
                        column: x => x.ShipCaptainsId,
                        principalTable: "Worker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_TransportId",
                table: "Cargos",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_WarehouseId",
                table: "Cargos",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_CrewTransport_TransportsId",
                table: "CrewTransport",
                column: "TransportsId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipCaptainTransport_TransportsId",
                table: "ShipCaptainTransport",
                column: "TransportsId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_ShipPortId",
                table: "Ships",
                column: "ShipPortId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_CurrentShipCaptainId",
                table: "Transports",
                column: "CurrentShipCaptainId",
                unique: true,
                filter: "[CurrentShipCaptainId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_ShipId",
                table: "Transports",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_ShipPortFromId",
                table: "Transports",
                column: "ShipPortFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_ShipPortToId",
                table: "Transports",
                column: "ShipPortToId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_ShipPortId",
                table: "Warehouses",
                column: "ShipPortId");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_ShipPortId",
                table: "Worker",
                column: "ShipPortId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "CrewTransport");

            migrationBuilder.DropTable(
                name: "ShipCaptainTransport");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "Worker");

            migrationBuilder.DropTable(
                name: "ShipPorts");
        }
    }
}
