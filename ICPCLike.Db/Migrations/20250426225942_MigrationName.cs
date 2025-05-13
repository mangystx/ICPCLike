using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ICPCLike.Db.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    country = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    icpc_id = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    sex = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "seasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seasons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    organization_id = table.Column<int>(type: "integer", nullable: true),
                    organization_id1 = table.Column<int>(type: "integer", nullable: true),
                    hidden = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.id);
                    table.ForeignKey(
                        name: "FK_teams_organizations_organization_id1",
                        column: x => x.organization_id1,
                        principalTable: "organizations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "stages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    season_id = table.Column<int>(type: "integer", nullable: false),
                    season_id1 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stages", x => x.id);
                    table.ForeignKey(
                        name: "FK_stages_seasons_season_id1",
                        column: x => x.season_id1,
                        principalTable: "seasons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "substitutions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    team_id = table.Column<int>(type: "integer", nullable: false),
                    team_id1 = table.Column<int>(type: "integer", nullable: false),
                    old_contestant_id = table.Column<int>(type: "integer", nullable: false),
                    old_contestant_id1 = table.Column<int>(type: "integer", nullable: false),
                    new_contestant_id = table.Column<int>(type: "integer", nullable: false),
                    new_contestant_id1 = table.Column<int>(type: "integer", nullable: false),
                    substitution_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_substitutions", x => x.id);
                    table.ForeignKey(
                        name: "FK_substitutions_persons_new_contestant_id1",
                        column: x => x.new_contestant_id1,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_substitutions_persons_old_contestant_id1",
                        column: x => x.old_contestant_id1,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_substitutions_teams_team_id1",
                        column: x => x.team_id1,
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_members",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    team_id = table.Column<int>(type: "integer", nullable: false),
                    team_id1 = table.Column<int>(type: "integer", nullable: false),
                    contestant_id = table.Column<int>(type: "integer", nullable: false),
                    contestant_id1 = table.Column<int>(type: "integer", nullable: false),
                    join_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    leave_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_members", x => x.id);
                    table.ForeignKey(
                        name: "FK_team_members_persons_contestant_id1",
                        column: x => x.contestant_id1,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_team_members_teams_team_id1",
                        column: x => x.team_id1,
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "results",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    stage_id = table.Column<int>(type: "integer", nullable: false),
                    stage_id1 = table.Column<int>(type: "integer", nullable: false),
                    team_id = table.Column<int>(type: "integer", nullable: false),
                    team_id1 = table.Column<int>(type: "integer", nullable: false),
                    rank = table.Column<int>(type: "integer", nullable: false),
                    solved_tasks = table.Column<int>(type: "integer", nullable: false),
                    penalty_time = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_results", x => x.id);
                    table.ForeignKey(
                        name: "FK_results_stages_stage_id1",
                        column: x => x.stage_id1,
                        principalTable: "stages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_results_teams_team_id1",
                        column: x => x.team_id1,
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_results_stage_id1",
                table: "results",
                column: "stage_id1");

            migrationBuilder.CreateIndex(
                name: "IX_results_team_id1",
                table: "results",
                column: "team_id1");

            migrationBuilder.CreateIndex(
                name: "IX_stages_season_id1",
                table: "stages",
                column: "season_id1");

            migrationBuilder.CreateIndex(
                name: "IX_substitutions_new_contestant_id1",
                table: "substitutions",
                column: "new_contestant_id1");

            migrationBuilder.CreateIndex(
                name: "IX_substitutions_old_contestant_id1",
                table: "substitutions",
                column: "old_contestant_id1");

            migrationBuilder.CreateIndex(
                name: "IX_substitutions_team_id1",
                table: "substitutions",
                column: "team_id1");

            migrationBuilder.CreateIndex(
                name: "IX_team_members_contestant_id1",
                table: "team_members",
                column: "contestant_id1");

            migrationBuilder.CreateIndex(
                name: "IX_team_members_team_id1",
                table: "team_members",
                column: "team_id1");

            migrationBuilder.CreateIndex(
                name: "IX_teams_organization_id1",
                table: "teams",
                column: "organization_id1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "results");

            migrationBuilder.DropTable(
                name: "substitutions");

            migrationBuilder.DropTable(
                name: "team_members");

            migrationBuilder.DropTable(
                name: "stages");

            migrationBuilder.DropTable(
                name: "persons");

            migrationBuilder.DropTable(
                name: "teams");

            migrationBuilder.DropTable(
                name: "seasons");

            migrationBuilder.DropTable(
                name: "organizations");
        }
    }
}
