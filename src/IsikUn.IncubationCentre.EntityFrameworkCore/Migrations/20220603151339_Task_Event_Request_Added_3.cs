using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsikUn.IncubationCentre.Migrations
{
    public partial class Task_Event_Request_Added_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppApplication_AppSystemManagers_ReceiverId",
                table: "AppApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_AppDocuments_AppProject_ProjectId",
                table: "AppDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_AppMilestone_AppProject_ProjectId",
                table: "AppMilestone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppMilestone",
                table: "AppMilestone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppApplication",
                table: "AppApplication");

            migrationBuilder.RenameTable(
                name: "AppMilestone",
                newName: "AppMilestones");

            migrationBuilder.RenameTable(
                name: "AppApplication",
                newName: "AppApplications");

            migrationBuilder.RenameIndex(
                name: "IX_AppMilestone_ProjectId",
                table: "AppMilestones",
                newName: "IX_AppMilestones_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_AppApplication_ReceiverId",
                table: "AppApplications",
                newName: "IX_AppApplications_ReceiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppMilestones",
                table: "AppMilestones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppApplications",
                table: "AppApplications",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AppEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_AppEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppEvents_AppPeople_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AppPeople",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppEvents_AppProject_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "AppProject",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_AppRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRequests_AppPeople_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AppPeople",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppRequests_AppPeople_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AppPeople",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedToId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    isDone = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_AppTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTasks_AppPeople_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "AppPeople",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppEvents_CreatorId",
                table: "AppEvents",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppEvents_ProjectId",
                table: "AppEvents",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRequests_ReceiverId",
                table: "AppRequests",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRequests_SenderId",
                table: "AppRequests",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTasks_AssignedToId",
                table: "AppTasks",
                column: "AssignedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppApplications_AppSystemManagers_ReceiverId",
                table: "AppApplications",
                column: "ReceiverId",
                principalTable: "AppSystemManagers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDocuments_AppProject_ProjectId",
                table: "AppDocuments",
                column: "ProjectId",
                principalTable: "AppProject",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppMilestones_AppProject_ProjectId",
                table: "AppMilestones",
                column: "ProjectId",
                principalTable: "AppProject",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppApplications_AppSystemManagers_ReceiverId",
                table: "AppApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_AppDocuments_AppProject_ProjectId",
                table: "AppDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_AppMilestones_AppProject_ProjectId",
                table: "AppMilestones");

            migrationBuilder.DropTable(
                name: "AppEvents");

            migrationBuilder.DropTable(
                name: "AppRequests");

            migrationBuilder.DropTable(
                name: "AppTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppMilestones",
                table: "AppMilestones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppApplications",
                table: "AppApplications");

            migrationBuilder.RenameTable(
                name: "AppMilestones",
                newName: "AppMilestone");

            migrationBuilder.RenameTable(
                name: "AppApplications",
                newName: "AppApplication");

            migrationBuilder.RenameIndex(
                name: "IX_AppMilestones_ProjectId",
                table: "AppMilestone",
                newName: "IX_AppMilestone_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_AppApplications_ReceiverId",
                table: "AppApplication",
                newName: "IX_AppApplication_ReceiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppMilestone",
                table: "AppMilestone",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppApplication",
                table: "AppApplication",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppApplication_AppSystemManagers_ReceiverId",
                table: "AppApplication",
                column: "ReceiverId",
                principalTable: "AppSystemManagers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDocuments_AppProject_ProjectId",
                table: "AppDocuments",
                column: "ProjectId",
                principalTable: "AppProject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppMilestone_AppProject_ProjectId",
                table: "AppMilestone",
                column: "ProjectId",
                principalTable: "AppProject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
