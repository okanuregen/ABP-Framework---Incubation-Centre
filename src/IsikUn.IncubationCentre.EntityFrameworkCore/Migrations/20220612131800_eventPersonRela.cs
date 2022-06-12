using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsikUn.IncubationCentre.Migrations
{
    public partial class eventPersonRela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppEvents_AppPeople_CreatorId",
                table: "AppEvents");

            migrationBuilder.DropIndex(
                name: "IX_AppEvents_CreatorId",
                table: "AppEvents");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "AppEvents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorPersonId",
                table: "AppEvents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppEvents_CreatorPersonId",
                table: "AppEvents",
                column: "CreatorPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppEvents_AppPeople_CreatorPersonId",
                table: "AppEvents",
                column: "CreatorPersonId",
                principalTable: "AppPeople",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppEvents_AppPeople_CreatorPersonId",
                table: "AppEvents");

            migrationBuilder.DropIndex(
                name: "IX_AppEvents_CreatorPersonId",
                table: "AppEvents");

            migrationBuilder.DropColumn(
                name: "CreatorPersonId",
                table: "AppEvents");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "AppEvents",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_AppEvents_CreatorId",
                table: "AppEvents",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppEvents_AppPeople_CreatorId",
                table: "AppEvents",
                column: "CreatorId",
                principalTable: "AppPeople",
                principalColumn: "Id");
        }
    }
}
