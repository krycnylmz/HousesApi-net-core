using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousesApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    houseid = table.Column<long>(name: "house_id", type: "bigint", nullable: false),
                    userid = table.Column<long>(name: "user_id", type: "bigint", nullable: false),
                    numberofpeople = table.Column<int>(name: "number_of_people", type: "int", nullable: false),
                    checkin = table.Column<DateTime>(name: "check_in", type: "datetime2", nullable: false),
                    checkout = table.Column<DateTime>(name: "check_out", type: "datetime2", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<long>(name: "user_id", type: "bigint", nullable: false),
                    housename = table.Column<string>(name: "house_name", type: "nvarchar(max)", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    gardenview = table.Column<bool>(name: "garden_view", type: "bit", nullable: false),
                    wifi = table.Column<bool>(type: "bit", nullable: false),
                    lakeview = table.Column<bool>(name: "lake_view", type: "bit", nullable: false),
                    pool = table.Column<bool>(type: "bit", nullable: false),
                    lakeaccess = table.Column<bool>(name: "lake_access", type: "bit", nullable: false),
                    hottub = table.Column<bool>(name: "hot_tub", type: "bit", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Houses");
        }
    }
}
