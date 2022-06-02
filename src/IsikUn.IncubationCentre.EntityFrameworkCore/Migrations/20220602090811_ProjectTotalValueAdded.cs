using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsikUn.IncubationCentre.Migrations
{
    public partial class ProjectTotalValueAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SharePerInvest",
                table: "AppProject",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TotalValuation",
                table: "AppProject",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalValuation",
                table: "AppProject");

            migrationBuilder.AlterColumn<int>(
                name: "SharePerInvest",
                table: "AppProject",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
