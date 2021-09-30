using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorDemoApp.Migrations
{
    public partial class InitialWeatherDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "weatherData",
                columns: table => new
                {
                    Weather_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Condition = table.Column<string>(nullable: true),
                    MaxTemp = table.Column<int>(nullable: false),
                    MinTemp = table.Column<int>(nullable: false),
                    AvgWind = table.Column<int>(nullable: false),
                    AvgHumidity = table.Column<int>(nullable: false),
                    AvgPressure = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weatherData", x => x.Weather_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "weatherData");
        }
    }
}
