using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsikUn.IncubationCentre.Migrations
{
    public partial class PeopleSkillERel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonSkill_AppSkills_SkillsId",
                table: "PersonSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonSkill_People_PeopleId",
                table: "PersonSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonSkill",
                table: "PersonSkill");

            migrationBuilder.DropIndex(
                name: "IX_PersonSkill_SkillsId",
                table: "PersonSkill");

            migrationBuilder.RenameColumn(
                name: "SkillsId",
                table: "PersonSkill",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PeopleId",
                table: "PersonSkill",
                newName: "PersonId");

            migrationBuilder.AddColumn<Guid>(
                name: "SkillId",
                table: "PersonSkill",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "PersonSkill",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "PersonSkill",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "PersonSkill",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "PersonSkill",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PersonSkill",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "PersonSkill",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "PersonSkill",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonSkill",
                table: "PersonSkill",
                columns: new[] { "SkillId", "PersonId" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonSkill_PersonId",
                table: "PersonSkill",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonSkill_AppSkills_SkillId",
                table: "PersonSkill",
                column: "SkillId",
                principalTable: "AppSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonSkill_People_PersonId",
                table: "PersonSkill",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonSkill_AppSkills_SkillId",
                table: "PersonSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonSkill_People_PersonId",
                table: "PersonSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonSkill",
                table: "PersonSkill");

            migrationBuilder.DropIndex(
                name: "IX_PersonSkill_PersonId",
                table: "PersonSkill");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "PersonSkill");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "PersonSkill");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "PersonSkill");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "PersonSkill");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "PersonSkill");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PersonSkill");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "PersonSkill");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "PersonSkill");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PersonSkill",
                newName: "SkillsId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "PersonSkill",
                newName: "PeopleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonSkill",
                table: "PersonSkill",
                columns: new[] { "PeopleId", "SkillsId" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonSkill_SkillsId",
                table: "PersonSkill",
                column: "SkillsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonSkill_AppSkills_SkillsId",
                table: "PersonSkill",
                column: "SkillsId",
                principalTable: "AppSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonSkill_People_PeopleId",
                table: "PersonSkill",
                column: "PeopleId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
