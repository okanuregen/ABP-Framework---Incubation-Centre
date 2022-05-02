using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsikUn.IncubationCentre.Migrations
{
    public partial class User_Types_Created : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrepreneur_AbpUsers_IdentityUserId",
                table: "Entrepreneur");

            migrationBuilder.DropForeignKey(
                name: "FK_EntrepreneurSkill_Entrepreneur_EntrepreneursId",
                table: "EntrepreneurSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entrepreneur",
                table: "Entrepreneur");

            migrationBuilder.RenameTable(
                name: "Entrepreneur",
                newName: "AppEntrepreneurs");

            migrationBuilder.RenameIndex(
                name: "IX_Entrepreneur_IdentityUserId",
                table: "AppEntrepreneurs",
                newName: "IX_AppEntrepreneurs_IdentityUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "CollaboratorId",
                table: "AppSkills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppEntrepreneurs",
                table: "AppEntrepreneurs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AppCollaborators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentityUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCollaborators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCollaborators_AbpUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppSystemManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentityUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSystemManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSystemManagers_AbpUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSkills_CollaboratorId",
                table: "AppSkills",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCollaborators_IdentityUserId",
                table: "AppCollaborators",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSystemManagers_IdentityUserId",
                table: "AppSystemManagers",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppEntrepreneurs_AbpUsers_IdentityUserId",
                table: "AppEntrepreneurs",
                column: "IdentityUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSkills_AppCollaborators_CollaboratorId",
                table: "AppSkills",
                column: "CollaboratorId",
                principalTable: "AppCollaborators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EntrepreneurSkill_AppEntrepreneurs_EntrepreneursId",
                table: "EntrepreneurSkill",
                column: "EntrepreneursId",
                principalTable: "AppEntrepreneurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppEntrepreneurs_AbpUsers_IdentityUserId",
                table: "AppEntrepreneurs");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSkills_AppCollaborators_CollaboratorId",
                table: "AppSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_EntrepreneurSkill_AppEntrepreneurs_EntrepreneursId",
                table: "EntrepreneurSkill");

            migrationBuilder.DropTable(
                name: "AppCollaborators");

            migrationBuilder.DropTable(
                name: "AppSystemManagers");

            migrationBuilder.DropIndex(
                name: "IX_AppSkills_CollaboratorId",
                table: "AppSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppEntrepreneurs",
                table: "AppEntrepreneurs");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "AppSkills");

            migrationBuilder.RenameTable(
                name: "AppEntrepreneurs",
                newName: "Entrepreneur");

            migrationBuilder.RenameIndex(
                name: "IX_AppEntrepreneurs_IdentityUserId",
                table: "Entrepreneur",
                newName: "IX_Entrepreneur_IdentityUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entrepreneur",
                table: "Entrepreneur",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entrepreneur_AbpUsers_IdentityUserId",
                table: "Entrepreneur",
                column: "IdentityUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EntrepreneurSkill_Entrepreneur_EntrepreneursId",
                table: "EntrepreneurSkill",
                column: "EntrepreneursId",
                principalTable: "Entrepreneur",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
