using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsikUn.IncubationCentre.Migrations
{
    public partial class Collaborator_Skill_RelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSkills_AppCollaborators_CollaboratorId",
                table: "AppSkills");

            migrationBuilder.DropIndex(
                name: "IX_AppSkills_CollaboratorId",
                table: "AppSkills");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "AppSkills");

            migrationBuilder.CreateTable(
                name: "CollaboratorSkill",
                columns: table => new
                {
                    CollaboratorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorSkill", x => new { x.CollaboratorsId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_CollaboratorSkill_AppCollaborators_CollaboratorsId",
                        column: x => x.CollaboratorsId,
                        principalTable: "AppCollaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorSkill_AppSkills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "AppSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorSkill_SkillsId",
                table: "CollaboratorSkill",
                column: "SkillsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorSkill");

            migrationBuilder.AddColumn<Guid>(
                name: "CollaboratorId",
                table: "AppSkills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSkills_CollaboratorId",
                table: "AppSkills",
                column: "CollaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSkills_AppCollaborators_CollaboratorId",
                table: "AppSkills",
                column: "CollaboratorId",
                principalTable: "AppCollaborators",
                principalColumn: "Id");
        }
    }
}
