using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsikUn.IncubationCentre.Migrations
{
    public partial class ProjectEntrepreneurRelationShip2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppProjectEntrepreneur",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntrepreneurId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectEntrepreneur", x => new { x.EntrepreneurId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_AppProjectEntrepreneur_AppEntrepreneurs_EntrepreneurId",
                        column: x => x.EntrepreneurId,
                        principalTable: "AppEntrepreneurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppProjectEntrepreneur_AppProject_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "AppProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectEntrepreneur_ProjectId",
                table: "AppProjectEntrepreneur",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppProjectEntrepreneur");
        }
    }
}
