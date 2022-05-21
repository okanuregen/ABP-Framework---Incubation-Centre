using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsikUn.IncubationCentre.Migrations
{
    public partial class peopleSkillTableNameFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_People_AbpUsers_IdentityUserId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonSkill_AppSkills_SkillId",
                table: "PersonSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonSkill_People_PersonId",
                table: "PersonSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonSkill",
                table: "PersonSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "People");

            migrationBuilder.RenameTable(
                name: "PersonSkill",
                newName: "AppPersonSkill");

            migrationBuilder.RenameTable(
                name: "People",
                newName: "AppPeople");

            migrationBuilder.RenameIndex(
                name: "IX_PersonSkill_PersonId",
                table: "AppPersonSkill",
                newName: "IX_AppPersonSkill_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_People_IdentityUserId",
                table: "AppPeople",
                newName: "IX_AppPeople_IdentityUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppPersonSkill",
                table: "AppPersonSkill",
                columns: new[] { "SkillId", "PersonId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppPeople",
                table: "AppPeople",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCollaborators_AppPeople_Id",
                table: "AppCollaborators",
                column: "Id",
                principalTable: "AppPeople",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppEntrepreneurs_AppPeople_Id",
                table: "AppEntrepreneurs",
                column: "Id",
                principalTable: "AppPeople",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInvestors_AppPeople_Id",
                table: "AppInvestors",
                column: "Id",
                principalTable: "AppPeople",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppMentors_AppPeople_Id",
                table: "AppMentors",
                column: "Id",
                principalTable: "AppPeople",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPeople_AbpUsers_IdentityUserId",
                table: "AppPeople",
                column: "IdentityUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPersonSkill_AppPeople_PersonId",
                table: "AppPersonSkill",
                column: "PersonId",
                principalTable: "AppPeople",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppPersonSkill_AppSkills_SkillId",
                table: "AppPersonSkill",
                column: "SkillId",
                principalTable: "AppSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSystemManagers_AppPeople_Id",
                table: "AppSystemManagers",
                column: "Id",
                principalTable: "AppPeople",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCollaborators_AppPeople_Id",
                table: "AppCollaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_AppEntrepreneurs_AppPeople_Id",
                table: "AppEntrepreneurs");

            migrationBuilder.DropForeignKey(
                name: "FK_AppInvestors_AppPeople_Id",
                table: "AppInvestors");

            migrationBuilder.DropForeignKey(
                name: "FK_AppMentors_AppPeople_Id",
                table: "AppMentors");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPeople_AbpUsers_IdentityUserId",
                table: "AppPeople");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPersonSkill_AppPeople_PersonId",
                table: "AppPersonSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPersonSkill_AppSkills_SkillId",
                table: "AppPersonSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSystemManagers_AppPeople_Id",
                table: "AppSystemManagers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppPersonSkill",
                table: "AppPersonSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppPeople",
                table: "AppPeople");

            migrationBuilder.RenameTable(
                name: "AppPersonSkill",
                newName: "PersonSkill");

            migrationBuilder.RenameTable(
                name: "AppPeople",
                newName: "People");

            migrationBuilder.RenameIndex(
                name: "IX_AppPersonSkill_PersonId",
                table: "PersonSkill",
                newName: "IX_PersonSkill_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_AppPeople_IdentityUserId",
                table: "People",
                newName: "IX_People_IdentityUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonSkill",
                table: "PersonSkill",
                columns: new[] { "SkillId", "PersonId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "People",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_People_AbpUsers_IdentityUserId",
                table: "People",
                column: "IdentityUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

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
    }
}
