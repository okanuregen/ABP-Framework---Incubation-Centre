using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsikUn.IncubationCentre.Migrations
{
    public partial class ProjectInvestorAddShareColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Share",
                table: "AppProjectInvestor",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Share",
                table: "AppProjectInvestor");
        }
    }
}
