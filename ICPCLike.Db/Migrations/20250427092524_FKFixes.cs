using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICPCLike.Db.Migrations
{
    /// <inheritdoc />
    public partial class FKFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_results_stages_stage_id1",
                table: "results");

            migrationBuilder.DropForeignKey(
                name: "FK_results_teams_team_id1",
                table: "results");

            migrationBuilder.DropForeignKey(
                name: "FK_stages_seasons_season_id1",
                table: "stages");

            migrationBuilder.DropForeignKey(
                name: "FK_substitutions_persons_new_contestant_id1",
                table: "substitutions");

            migrationBuilder.DropForeignKey(
                name: "FK_substitutions_persons_old_contestant_id1",
                table: "substitutions");

            migrationBuilder.DropForeignKey(
                name: "FK_substitutions_teams_team_id1",
                table: "substitutions");

            migrationBuilder.DropForeignKey(
                name: "FK_team_members_persons_contestant_id1",
                table: "team_members");

            migrationBuilder.DropForeignKey(
                name: "FK_team_members_teams_team_id1",
                table: "team_members");

            migrationBuilder.DropForeignKey(
                name: "FK_teams_organizations_organization_id1",
                table: "teams");

            migrationBuilder.RenameColumn(
                name: "organization_id1",
                table: "teams",
                newName: "organizationId");

            migrationBuilder.RenameIndex(
                name: "IX_teams_organization_id1",
                table: "teams",
                newName: "IX_teams_organizationId");

            migrationBuilder.RenameColumn(
                name: "team_id1",
                table: "team_members",
                newName: "teamId");

            migrationBuilder.RenameColumn(
                name: "contestant_id1",
                table: "team_members",
                newName: "contestantId");

            migrationBuilder.RenameIndex(
                name: "IX_team_members_team_id1",
                table: "team_members",
                newName: "IX_team_members_teamId");

            migrationBuilder.RenameIndex(
                name: "IX_team_members_contestant_id1",
                table: "team_members",
                newName: "IX_team_members_contestantId");

            migrationBuilder.RenameColumn(
                name: "team_id1",
                table: "substitutions",
                newName: "teamId");

            migrationBuilder.RenameColumn(
                name: "old_contestant_id1",
                table: "substitutions",
                newName: "oldContestantId");

            migrationBuilder.RenameColumn(
                name: "new_contestant_id1",
                table: "substitutions",
                newName: "newContestantId");

            migrationBuilder.RenameIndex(
                name: "IX_substitutions_team_id1",
                table: "substitutions",
                newName: "IX_substitutions_teamId");

            migrationBuilder.RenameIndex(
                name: "IX_substitutions_old_contestant_id1",
                table: "substitutions",
                newName: "IX_substitutions_oldContestantId");

            migrationBuilder.RenameIndex(
                name: "IX_substitutions_new_contestant_id1",
                table: "substitutions",
                newName: "IX_substitutions_newContestantId");

            migrationBuilder.RenameColumn(
                name: "season_id1",
                table: "stages",
                newName: "seasonId");

            migrationBuilder.RenameIndex(
                name: "IX_stages_season_id1",
                table: "stages",
                newName: "IX_stages_seasonId");

            migrationBuilder.RenameColumn(
                name: "team_id1",
                table: "results",
                newName: "teamId");

            migrationBuilder.RenameColumn(
                name: "stage_id1",
                table: "results",
                newName: "stageId");

            migrationBuilder.RenameIndex(
                name: "IX_results_team_id1",
                table: "results",
                newName: "IX_results_teamId");

            migrationBuilder.RenameIndex(
                name: "IX_results_stage_id1",
                table: "results",
                newName: "IX_results_stageId");

            migrationBuilder.AddForeignKey(
                name: "FK_results_stages_stageId",
                table: "results",
                column: "stageId",
                principalTable: "stages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_results_teams_teamId",
                table: "results",
                column: "teamId",
                principalTable: "teams",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stages_seasons_seasonId",
                table: "stages",
                column: "seasonId",
                principalTable: "seasons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_substitutions_persons_newContestantId",
                table: "substitutions",
                column: "newContestantId",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_substitutions_persons_oldContestantId",
                table: "substitutions",
                column: "oldContestantId",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_substitutions_teams_teamId",
                table: "substitutions",
                column: "teamId",
                principalTable: "teams",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_team_members_persons_contestantId",
                table: "team_members",
                column: "contestantId",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_team_members_teams_teamId",
                table: "team_members",
                column: "teamId",
                principalTable: "teams",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_teams_organizations_organizationId",
                table: "teams",
                column: "organizationId",
                principalTable: "organizations",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_results_stages_stageId",
                table: "results");

            migrationBuilder.DropForeignKey(
                name: "FK_results_teams_teamId",
                table: "results");

            migrationBuilder.DropForeignKey(
                name: "FK_stages_seasons_seasonId",
                table: "stages");

            migrationBuilder.DropForeignKey(
                name: "FK_substitutions_persons_newContestantId",
                table: "substitutions");

            migrationBuilder.DropForeignKey(
                name: "FK_substitutions_persons_oldContestantId",
                table: "substitutions");

            migrationBuilder.DropForeignKey(
                name: "FK_substitutions_teams_teamId",
                table: "substitutions");

            migrationBuilder.DropForeignKey(
                name: "FK_team_members_persons_contestantId",
                table: "team_members");

            migrationBuilder.DropForeignKey(
                name: "FK_team_members_teams_teamId",
                table: "team_members");

            migrationBuilder.DropForeignKey(
                name: "FK_teams_organizations_organizationId",
                table: "teams");

            migrationBuilder.RenameColumn(
                name: "organizationId",
                table: "teams",
                newName: "organization_id1");

            migrationBuilder.RenameIndex(
                name: "IX_teams_organizationId",
                table: "teams",
                newName: "IX_teams_organization_id1");

            migrationBuilder.RenameColumn(
                name: "teamId",
                table: "team_members",
                newName: "team_id1");

            migrationBuilder.RenameColumn(
                name: "contestantId",
                table: "team_members",
                newName: "contestant_id1");

            migrationBuilder.RenameIndex(
                name: "IX_team_members_teamId",
                table: "team_members",
                newName: "IX_team_members_team_id1");

            migrationBuilder.RenameIndex(
                name: "IX_team_members_contestantId",
                table: "team_members",
                newName: "IX_team_members_contestant_id1");

            migrationBuilder.RenameColumn(
                name: "teamId",
                table: "substitutions",
                newName: "team_id1");

            migrationBuilder.RenameColumn(
                name: "oldContestantId",
                table: "substitutions",
                newName: "old_contestant_id1");

            migrationBuilder.RenameColumn(
                name: "newContestantId",
                table: "substitutions",
                newName: "new_contestant_id1");

            migrationBuilder.RenameIndex(
                name: "IX_substitutions_teamId",
                table: "substitutions",
                newName: "IX_substitutions_team_id1");

            migrationBuilder.RenameIndex(
                name: "IX_substitutions_oldContestantId",
                table: "substitutions",
                newName: "IX_substitutions_old_contestant_id1");

            migrationBuilder.RenameIndex(
                name: "IX_substitutions_newContestantId",
                table: "substitutions",
                newName: "IX_substitutions_new_contestant_id1");

            migrationBuilder.RenameColumn(
                name: "seasonId",
                table: "stages",
                newName: "season_id1");

            migrationBuilder.RenameIndex(
                name: "IX_stages_seasonId",
                table: "stages",
                newName: "IX_stages_season_id1");

            migrationBuilder.RenameColumn(
                name: "teamId",
                table: "results",
                newName: "team_id1");

            migrationBuilder.RenameColumn(
                name: "stageId",
                table: "results",
                newName: "stage_id1");

            migrationBuilder.RenameIndex(
                name: "IX_results_teamId",
                table: "results",
                newName: "IX_results_team_id1");

            migrationBuilder.RenameIndex(
                name: "IX_results_stageId",
                table: "results",
                newName: "IX_results_stage_id1");

            migrationBuilder.AddForeignKey(
                name: "FK_results_stages_stage_id1",
                table: "results",
                column: "stage_id1",
                principalTable: "stages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_results_teams_team_id1",
                table: "results",
                column: "team_id1",
                principalTable: "teams",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stages_seasons_season_id1",
                table: "stages",
                column: "season_id1",
                principalTable: "seasons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_substitutions_persons_new_contestant_id1",
                table: "substitutions",
                column: "new_contestant_id1",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_substitutions_persons_old_contestant_id1",
                table: "substitutions",
                column: "old_contestant_id1",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_substitutions_teams_team_id1",
                table: "substitutions",
                column: "team_id1",
                principalTable: "teams",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_team_members_persons_contestant_id1",
                table: "team_members",
                column: "contestant_id1",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_team_members_teams_team_id1",
                table: "team_members",
                column: "team_id1",
                principalTable: "teams",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_teams_organizations_organization_id1",
                table: "teams",
                column: "organization_id1",
                principalTable: "organizations",
                principalColumn: "id");
        }
    }
}
