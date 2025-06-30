using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoundSesh.Venues.Entities.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LookUp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DisplayDesc = table.Column<string>(nullable: true),
                    IsCustom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Studio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 12, nullable: false),
                    Address1 = table.Column<string>(maxLength: 100, nullable: false),
                    Address2 = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    State = table.Column<string>(maxLength: 100, nullable: false),
                    ZipCode = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: false),
                    Amenities = table.Column<string>(maxLength: 1000, nullable: true),
                    StudioScheduleId = table.Column<int>(nullable: false),
                    TimeZone = table.Column<string>(nullable: true),
                    RoomDetails = table.Column<string>(nullable: true),
                    GenreDetails = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudioAccountStaging",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 12, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    Address1 = table.Column<string>(maxLength: 100, nullable: true),
                    Address2 = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    State = table.Column<string>(maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    Amenities = table.Column<string>(maxLength: 1000, nullable: true),
                    Step = table.Column<int>(nullable: false),
                    StudioScheduleId = table.Column<int>(nullable: false),
                    TimeZone = table.Column<string>(nullable: true),
                    RoomDetails = table.Column<string>(nullable: true),
                    GenreDetails = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudioAccountStaging", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudioSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MondayIsOpen = table.Column<bool>(nullable: false),
                    MondayOpenTime = table.Column<DateTime>(nullable: false),
                    MondayCloseTime = table.Column<DateTime>(nullable: false),
                    TuesdayIsOpen = table.Column<bool>(nullable: false),
                    TuesdayOpenTime = table.Column<DateTime>(nullable: false),
                    TuesdayCloseTime = table.Column<DateTime>(nullable: false),
                    WednesdayIsOpen = table.Column<bool>(nullable: false),
                    WednesdayOpenTime = table.Column<DateTime>(nullable: false),
                    WednesdayCloseTime = table.Column<DateTime>(nullable: false),
                    ThursdayIsOpen = table.Column<bool>(nullable: false),
                    ThursdayOpenTime = table.Column<DateTime>(nullable: false),
                    ThursdayCloseTime = table.Column<DateTime>(nullable: false),
                    FridayIsOpen = table.Column<bool>(nullable: false),
                    FridayOpenTime = table.Column<DateTime>(nullable: false),
                    FridayCloseTime = table.Column<DateTime>(nullable: false),
                    SaturdayIsOpen = table.Column<bool>(nullable: false),
                    SaturdayOpenTime = table.Column<DateTime>(nullable: false),
                    SaturdayCloseTime = table.Column<DateTime>(nullable: false),
                    SundayIsOpen = table.Column<bool>(nullable: false),
                    SundayOpenTime = table.Column<DateTime>(nullable: false),
                    SundayCloseTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudioSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(maxLength: 100, nullable: false),
                    Biography = table.Column<string>(nullable: true),
                    GenreDetails = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "LookUp",
                columns: new[] { "Id", "Category", "Description", "DisplayDesc", "IsCustom" },
                values: new object[,]
                {
                    { 1, "Genres", "Rock", "Rock", null },
                    { 2, "Genres", "Country", "Country", null },
                    { 3, "Genres", "R&B", "R&B", null },
                    { 4, "Genres", "Electronic", "Electronic", null },
                    { 5, "Genres", "Jazz", "Jazz", null },
                    { 6, "Genres", "Reggae", "Reggae", null },
                    { 7, "Genres", "Rap", "Rap", null },
                    { 8, "Genres", "Dubstep", "Dubstep", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Studio_Email",
                table: "Studio",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudioAccountStaging_Email",
                table: "StudioAccountStaging",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_Email",
                table: "UserAccount",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LookUp");

            migrationBuilder.DropTable(
                name: "Studio");

            migrationBuilder.DropTable(
                name: "StudioAccountStaging");

            migrationBuilder.DropTable(
                name: "StudioSchedule");

            migrationBuilder.DropTable(
                name: "UserAccount");
        }
    }
}
