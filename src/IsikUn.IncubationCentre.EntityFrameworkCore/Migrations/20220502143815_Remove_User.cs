using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsikUn.IncubationCentre.Migrations
{
    public partial class Remove_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInvestors_User_UserId",
                table: "AppInvestors");

            migrationBuilder.DropForeignKey(
                name: "FK_AppMentors_User_UserId",
                table: "AppMentors");

            migrationBuilder.DropTable(
                name: "SkillUser");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_AppMentors_UserId",
                table: "AppMentors");

            migrationBuilder.DropIndex(
                name: "IX_AppInvestors_UserId",
                table: "AppInvestors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AppMentors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AppInvestors");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "AppMentors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "AppMentors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdentityUserId",
                table: "AppMentors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "AppInvestors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "AppInvestors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdentityUserId",
                table: "AppInvestors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Entrepreneur",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isActivated = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Entrepreneur", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entrepreneur_AbpUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvestorSkill",
                columns: table => new
                {
                    InvestorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestorSkill", x => new { x.InvestorsId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_InvestorSkill_AppInvestors_InvestorsId",
                        column: x => x.InvestorsId,
                        principalTable: "AppInvestors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestorSkill_AppSkills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "AppSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MentorSkill",
                columns: table => new
                {
                    MentorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MentorSkill", x => new { x.MentorsId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_MentorSkill_AppMentors_MentorsId",
                        column: x => x.MentorsId,
                        principalTable: "AppMentors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MentorSkill_AppSkills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "AppSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntrepreneurSkill",
                columns: table => new
                {
                    EntrepreneursId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntrepreneurSkill", x => new { x.EntrepreneursId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_EntrepreneurSkill_AppSkills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "AppSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntrepreneurSkill_Entrepreneur_EntrepreneursId",
                        column: x => x.EntrepreneursId,
                        principalTable: "Entrepreneur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppMentors_IdentityUserId",
                table: "AppMentors",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInvestors_IdentityUserId",
                table: "AppInvestors",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Entrepreneur_IdentityUserId",
                table: "Entrepreneur",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntrepreneurSkill_SkillsId",
                table: "EntrepreneurSkill",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestorSkill_SkillsId",
                table: "InvestorSkill",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_MentorSkill_SkillsId",
                table: "MentorSkill",
                column: "SkillsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInvestors_AbpUsers_IdentityUserId",
                table: "AppInvestors",
                column: "IdentityUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppMentors_AbpUsers_IdentityUserId",
                table: "AppMentors",
                column: "IdentityUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInvestors_AbpUsers_IdentityUserId",
                table: "AppInvestors");

            migrationBuilder.DropForeignKey(
                name: "FK_AppMentors_AbpUsers_IdentityUserId",
                table: "AppMentors");

            migrationBuilder.DropTable(
                name: "EntrepreneurSkill");

            migrationBuilder.DropTable(
                name: "InvestorSkill");

            migrationBuilder.DropTable(
                name: "MentorSkill");

            migrationBuilder.DropTable(
                name: "Entrepreneur");

            migrationBuilder.DropIndex(
                name: "IX_AppMentors_IdentityUserId",
                table: "AppMentors");

            migrationBuilder.DropIndex(
                name: "IX_AppInvestors_IdentityUserId",
                table: "AppInvestors");

            migrationBuilder.DropColumn(
                name: "About",
                table: "AppMentors");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "AppMentors");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "AppMentors");

            migrationBuilder.DropColumn(
                name: "About",
                table: "AppInvestors");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "AppInvestors");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "AppInvestors");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AppMentors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AppInvestors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentityUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_AbpUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SkillUser",
                columns: table => new
                {
                    SkillsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillUser", x => new { x.SkillsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_SkillUser_AppSkills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "AppSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillUser_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppMentors_UserId",
                table: "AppMentors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInvestors_UserId",
                table: "AppInvestors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillUser_UsersId",
                table: "SkillUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdentityUserId",
                table: "User",
                column: "IdentityUserId");

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
    }
}
