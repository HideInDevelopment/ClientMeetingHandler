using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientMeetingHandler.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ClientMeetingHandler");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "ClientMeetingHandler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                schema: "ClientMeetingHandler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                schema: "ClientMeetingHandler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<double>(type: "float", maxLength: 10, nullable: false),
                    Sessions = table.Column<int>(type: "int", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "ClientMeetingHandler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "ClientMeetingHandler",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                schema: "ClientMeetingHandler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meetings_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "ClientMeetingHandler",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meetings_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "ClientMeetingHandler",
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "ClientMeetingHandler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "ClientMeetingHandler",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Services_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalSchema: "ClientMeetingHandler",
                        principalTable: "ServiceTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                schema: "ClientMeetingHandler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NoteType = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ClientMeetingHandler",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_Id",
                schema: "ClientMeetingHandler",
                table: "Clients",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Client_Id",
                schema: "ClientMeetingHandler",
                table: "Clients",
                columns: new[] { "Id", "ContactId" });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Email",
                schema: "ClientMeetingHandler",
                table: "Contacts",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Id",
                schema: "ClientMeetingHandler",
                table: "Contacts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ClientId",
                schema: "ClientMeetingHandler",
                table: "Contacts",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_Id",
                schema: "ClientMeetingHandler",
                table: "Locations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Client_Localization_Meeting_Id",
                schema: "ClientMeetingHandler",
                table: "Meetings",
                columns: new[] { "Id", "ClientId", "LocationId" });

            migrationBuilder.CreateIndex(
                name: "IX_Client_Meeting_Id",
                schema: "ClientMeetingHandler",
                table: "Meetings",
                columns: new[] { "Id", "ClientId" });

            migrationBuilder.CreateIndex(
                name: "IX_Localization_Meeting_Id",
                schema: "ClientMeetingHandler",
                table: "Meetings",
                columns: new[] { "Id", "LocationId" });

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_Id",
                schema: "ClientMeetingHandler",
                table: "Meetings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_ClientId",
                schema: "ClientMeetingHandler",
                table: "Meetings",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_LocationId",
                schema: "ClientMeetingHandler",
                table: "Meetings",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_Id",
                schema: "ClientMeetingHandler",
                table: "Notes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ServiceId",
                schema: "ClientMeetingHandler",
                table: "Notes",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Id",
                schema: "ClientMeetingHandler",
                table: "Services",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ClientId",
                schema: "ClientMeetingHandler",
                table: "Services",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceTypeId",
                schema: "ClientMeetingHandler",
                table: "Services",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceType_Service_Id",
                schema: "ClientMeetingHandler",
                table: "Services",
                columns: new[] { "Id", "ServiceTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceType_Id",
                schema: "ClientMeetingHandler",
                table: "ServiceTypes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "ClientMeetingHandler");

            migrationBuilder.DropTable(
                name: "Meetings",
                schema: "ClientMeetingHandler");

            migrationBuilder.DropTable(
                name: "Notes",
                schema: "ClientMeetingHandler");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "ClientMeetingHandler");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "ClientMeetingHandler");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "ClientMeetingHandler");

            migrationBuilder.DropTable(
                name: "ServiceTypes",
                schema: "ClientMeetingHandler");
        }
    }
}
