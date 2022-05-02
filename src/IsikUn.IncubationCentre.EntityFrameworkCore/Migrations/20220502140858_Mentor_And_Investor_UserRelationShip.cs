using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsikUn.IncubationCentre.Migrations
{
    public partial class Mentor_And_Investor_UserRelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInvestors_User_Id",
                table: "AppInvestors");

            migrationBuilder.DropForeignKey(
                name: "FK_AppMentors_User_Id",
                table: "AppMentors");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppMentors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "AppMentors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppMentors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppMentors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppMentors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppMentors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "AppMentors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AppMentors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppInvestors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "AppInvestors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppInvestors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppInvestors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppInvestors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppInvestors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "AppInvestors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AppInvestors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppMentors_UserId",
                table: "AppMentors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInvestors_UserId",
                table: "AppInvestors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInvestors_User_UserId",
                table: "AppInvestors",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppMentors_User_UserId",
                table: "AppMentors",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInvestors_User_UserId",
                table: "AppInvestors");

            migrationBuilder.DropForeignKey(
                name: "FK_AppMentors_User_UserId",
                table: "AppMentors");

            migrationBuilder.DropIndex(
                name: "IX_AppMentors_UserId",
                table: "AppMentors");

            migrationBuilder.DropIndex(
                name: "IX_AppInvestors_UserId",
                table: "AppInvestors");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppMentors");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AppMentors");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppMentors");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppMentors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppMentors");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppMentors");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "AppMentors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AppMentors");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppInvestors");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AppInvestors");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppInvestors");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppInvestors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppInvestors");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppInvestors");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "AppInvestors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AppInvestors");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInvestors_User_Id",
                table: "AppInvestors",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppMentors_User_Id",
                table: "AppMentors",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
