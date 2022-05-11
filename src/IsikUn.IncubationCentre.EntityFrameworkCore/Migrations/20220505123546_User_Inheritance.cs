using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsikUn.IncubationCentre.Migrations
{
    public partial class User_Inheritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCollaborators_AbpUsers_IdentityUserId",
                table: "AppCollaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_AppEntrepreneurs_AbpUsers_IdentityUserId",
                table: "AppEntrepreneurs");

            migrationBuilder.DropForeignKey(
                name: "FK_AppInvestors_AbpUsers_IdentityUserId",
                table: "AppInvestors");

            migrationBuilder.DropForeignKey(
                name: "FK_AppMentors_AbpUsers_IdentityUserId",
                table: "AppMentors");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSystemManagers_AbpUsers_IdentityUserId",
                table: "AppSystemManagers");

            migrationBuilder.DropTable(
                name: "CollaboratorSkill");

            migrationBuilder.DropTable(
                name: "EntrepreneurSkill");

            migrationBuilder.DropTable(
                name: "InvestorSkill");

            migrationBuilder.DropTable(
                name: "MentorSkill");

            migrationBuilder.DropIndex(
                name: "IX_AppSystemManagers_IdentityUserId",
                table: "AppSystemManagers");

            migrationBuilder.DropIndex(
                name: "IX_AppMentors_IdentityUserId",
                table: "AppMentors");

            migrationBuilder.DropIndex(
                name: "IX_AppInvestors_IdentityUserId",
                table: "AppInvestors");

            migrationBuilder.DropIndex(
                name: "IX_AppEntrepreneurs_IdentityUserId",
                table: "AppEntrepreneurs");

            migrationBuilder.DropIndex(
                name: "IX_AppCollaborators_IdentityUserId",
                table: "AppCollaborators");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppSystemManagers");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AppSystemManagers");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppSystemManagers");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppSystemManagers");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "AppSystemManagers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppSystemManagers");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppSystemManagers");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "AppSystemManagers");

            migrationBuilder.DropColumn(
                name: "About",
                table: "AppMentors");

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
                name: "Experience",
                table: "AppMentors");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
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
                name: "About",
                table: "AppInvestors");

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
                name: "Experience",
                table: "AppInvestors");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
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
                name: "About",
                table: "AppEntrepreneurs");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppEntrepreneurs");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AppEntrepreneurs");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppEntrepreneurs");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppEntrepreneurs");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "AppEntrepreneurs");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "AppEntrepreneurs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppEntrepreneurs");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppEntrepreneurs");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "AppEntrepreneurs");

            migrationBuilder.DropColumn(
                name: "About",
                table: "AppCollaborators");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppCollaborators");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AppCollaborators");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppCollaborators");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppCollaborators");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "AppCollaborators");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "AppCollaborators");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppCollaborators");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppCollaborators");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "AppCollaborators");

            migrationBuilder.CreateTable(
                name: "People",
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
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_AbpUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PersonSkill",
                columns: table => new
                {
                    PeopleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonSkill", x => new { x.PeopleId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_PersonSkill_AppSkills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "AppSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonSkill_People_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_IdentityUserId",
                table: "People",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonSkill_SkillsId",
                table: "PersonSkill",
                column: "SkillsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCollaborators_People_Id",
                table: "AppCollaborators",
                column: "Id",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppEntrepreneurs_People_Id",
                table: "AppEntrepreneurs",
                column: "Id",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInvestors_People_Id",
                table: "AppInvestors",
                column: "Id",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppMentors_People_Id",
                table: "AppMentors",
                column: "Id",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSystemManagers_People_Id",
                table: "AppSystemManagers",
                column: "Id",
                principalTable: "People",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCollaborators_People_Id",
                table: "AppCollaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_AppEntrepreneurs_People_Id",
                table: "AppEntrepreneurs");

            migrationBuilder.DropForeignKey(
                name: "FK_AppInvestors_People_Id",
                table: "AppInvestors");

            migrationBuilder.DropForeignKey(
                name: "FK_AppMentors_People_Id",
                table: "AppMentors");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSystemManagers_People_Id",
                table: "AppSystemManagers");

            migrationBuilder.DropTable(
                name: "PersonSkill");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppSystemManagers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "AppSystemManagers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppSystemManagers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppSystemManagers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdentityUserId",
                table: "AppSystemManagers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppSystemManagers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppSystemManagers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "AppSystemManagers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "AppMentors",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "AppInvestors",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "AppEntrepreneurs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppEntrepreneurs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "AppEntrepreneurs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppEntrepreneurs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppEntrepreneurs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "AppEntrepreneurs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdentityUserId",
                table: "AppEntrepreneurs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppEntrepreneurs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppEntrepreneurs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "AppEntrepreneurs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "AppCollaborators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppCollaborators",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "AppCollaborators",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppCollaborators",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppCollaborators",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "AppCollaborators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdentityUserId",
                table: "AppCollaborators",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppCollaborators",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppCollaborators",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "AppCollaborators",
                type: "uniqueidentifier",
                nullable: true);

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
                        name: "FK_EntrepreneurSkill_AppEntrepreneurs_EntrepreneursId",
                        column: x => x.EntrepreneursId,
                        principalTable: "AppEntrepreneurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntrepreneurSkill_AppSkills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "AppSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_AppSystemManagers_IdentityUserId",
                table: "AppSystemManagers",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMentors_IdentityUserId",
                table: "AppMentors",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInvestors_IdentityUserId",
                table: "AppInvestors",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppEntrepreneurs_IdentityUserId",
                table: "AppEntrepreneurs",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCollaborators_IdentityUserId",
                table: "AppCollaborators",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorSkill_SkillsId",
                table: "CollaboratorSkill",
                column: "SkillsId");

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
                name: "FK_AppCollaborators_AbpUsers_IdentityUserId",
                table: "AppCollaborators",
                column: "IdentityUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppEntrepreneurs_AbpUsers_IdentityUserId",
                table: "AppEntrepreneurs",
                column: "IdentityUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AppSystemManagers_AbpUsers_IdentityUserId",
                table: "AppSystemManagers",
                column: "IdentityUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }
    }
}
