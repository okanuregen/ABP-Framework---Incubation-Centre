using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsikUn.IncubationCentre.Migrations
{
    public partial class RemoveReceiverFromApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppApplications_AppSystemManagers_ReceiverId",
                table: "AppApplications");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "AppApplications",
                newName: "SystemManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_AppApplications_ReceiverId",
                table: "AppApplications",
                newName: "IX_AppApplications_SystemManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppApplications_AppSystemManagers_SystemManagerId",
                table: "AppApplications",
                column: "SystemManagerId",
                principalTable: "AppSystemManagers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppApplications_AppSystemManagers_SystemManagerId",
                table: "AppApplications");

            migrationBuilder.RenameColumn(
                name: "SystemManagerId",
                table: "AppApplications",
                newName: "ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_AppApplications_SystemManagerId",
                table: "AppApplications",
                newName: "IX_AppApplications_ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppApplications_AppSystemManagers_ReceiverId",
                table: "AppApplications",
                column: "ReceiverId",
                principalTable: "AppSystemManagers",
                principalColumn: "Id");
        }
    }
}
