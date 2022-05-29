using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsikUn.IncubationCentre.Migrations
{
    public partial class ProjectAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "AppDocuments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AppProject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvesmentReady = table.Column<bool>(type: "bit", nullable: false),
                    OpenForInvesment = table.Column<bool>(type: "bit", nullable: false),
                    SharePerInvest = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AppProject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppMilestone",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuccessCriteria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isCompleted = table.Column<bool>(type: "bit", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_AppMilestone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMilestone_AppProject_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "AppProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppProjectCollaborator",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectCollaborator", x => new { x.CollaboratorId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_AppProjectCollaborator_AppCollaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "AppCollaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppProjectCollaborator_AppProject_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "AppProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppProjectFounder",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectFounder", x => new { x.PersonId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_AppProjectFounder_AppPeople_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AppPeople",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppProjectFounder_AppProject_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "AppProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppProjectInvestor",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvestorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectInvestor", x => new { x.InvestorId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_AppProjectInvestor_AppInvestors_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "AppInvestors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppProjectInvestor_AppProject_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "AppProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppProjectMentor",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MentorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectMentor", x => new { x.MentorId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_AppProjectMentor_AppMentors_MentorId",
                        column: x => x.MentorId,
                        principalTable: "AppMentors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppProjectMentor_AppProject_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "AppProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDocuments_ProjectId",
                table: "AppDocuments",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMilestone_ProjectId",
                table: "AppMilestone",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectCollaborator_ProjectId",
                table: "AppProjectCollaborator",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectFounder_ProjectId",
                table: "AppProjectFounder",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectInvestor_ProjectId",
                table: "AppProjectInvestor",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectMentor_ProjectId",
                table: "AppProjectMentor",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDocuments_AppProject_ProjectId",
                table: "AppDocuments",
                column: "ProjectId",
                principalTable: "AppProject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDocuments_AppProject_ProjectId",
                table: "AppDocuments");

            migrationBuilder.DropTable(
                name: "AppMilestone");

            migrationBuilder.DropTable(
                name: "AppProjectCollaborator");

            migrationBuilder.DropTable(
                name: "AppProjectFounder");

            migrationBuilder.DropTable(
                name: "AppProjectInvestor");

            migrationBuilder.DropTable(
                name: "AppProjectMentor");

            migrationBuilder.DropTable(
                name: "AppProject");

            migrationBuilder.DropIndex(
                name: "IX_AppDocuments_ProjectId",
                table: "AppDocuments");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "AppDocuments");
        }
    }
}
