using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SocialtapAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatsSet",
                columns: table => new
                {
                    TopBarName = table.Column<string>(nullable: false),
                    BarCount = table.Column<int>(nullable: false),
                    ReviewCount = table.Column<int>(nullable: false),
                    TopBarAvgBeverageVolume = table.Column<double>(nullable: false),
                    TopBarRate = table.Column<double>(nullable: false),
                    TotalAvgBeverageVolume = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsSet", x => x.TopBarName);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatsSet");
        }
    }
}
