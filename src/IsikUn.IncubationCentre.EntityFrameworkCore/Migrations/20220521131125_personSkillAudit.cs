using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsikUn.IncubationCentre.Migrations
{
    public partial class personSkillAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppPersonSkill");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppPersonSkill");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppPersonSkill");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppPersonSkill",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppPersonSkill",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppPersonSkill",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
